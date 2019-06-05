using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace synthesizer
{
    public class SynthWaveProvider : ISampleProvider
    {
        private int _sampleRate;
        private float[] waveTable;
        private double phase;
        private double phaseStep;
        public WaveFormat WaveFormat { get; }
        public double Frequency { get; set; }

        public float Volume { get; set; }

        public SynthWaveProvider(int sampleRate = 44100)
        {
            _sampleRate = sampleRate;
            var channels = 1; // Mono
            Volume = 0.25f;
            Frequency = 0.0f;

            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_sampleRate, channels);
            waveTable = new float[_sampleRate];

            for (int i = 0; i < _sampleRate; i++)
            {
                // waveTable[i] = (float)Math.Sin(2 * Math.PI * i/_sampleRate);
                waveTable[i] = ((float)Math.Sin(2 * Math.PI * i / _sampleRate)) > 0
                    ? 1.0f : -1.0f;
                //waveTable[i] = (float)Math.Sin(2 * Math.PI * i / _sampleRate)
                //    + (float)Math.Sin(4 * Math.PI * i / _sampleRate)
                //    + (float)Math.Sin(6 * Math.PI * i / _sampleRate)
                //    + (float)Math.Sin(8 * Math.PI * i / _sampleRate)
                //    + (float)Math.Sin(10 * Math.PI * i / _sampleRate);
                // waveTable[i] = (float)i / _sampleRate;
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            phaseStep = waveTable.Length * (Frequency / WaveFormat.SampleRate);

            for (int i = 0; i < count; i++)
            {
                var waveTableIndex = (int)phase % waveTable.Length;

                buffer[i + offset] = waveTable[waveTableIndex] * Volume;
                phase += phaseStep;

                if (phase > waveTable.Length)
                {
                    phase -= waveTable.Length;
                }
            }

            return count;
        }
    }
}

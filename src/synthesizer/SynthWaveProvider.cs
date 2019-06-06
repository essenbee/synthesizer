using NAudio.Wave;
using System;

namespace synthesizer
{
    public class SynthWaveProvider : ISampleProvider
    {
        private int _sampleRate;
        private float[] waveTable;
        private readonly int _note;
        private double phase;
        private double phaseStep;
        private readonly double _twelfthRootOfTwo = Math.Pow(2, 1.0 / 12.0);
        public WaveFormat WaveFormat { get; }
        public double BaseFrequency { get; set; } = 110.0;
        public double Frequency => BaseFrequency * Math.Pow(_twelfthRootOfTwo, _note);
        public bool NoteOn { get; set; }

        public SynthWaveProvider(int sampleRate = 44100, int note = 0)
        {
            _note = note;
            _sampleRate = sampleRate;
            var channels = 1; // Mono
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
            if (NoteOn)
            {
                phaseStep = waveTable.Length * (Frequency / WaveFormat.SampleRate);

                for (int i = 0; i < count; i++)
                {
                    var waveTableIndex = (int)phase % waveTable.Length;

                    buffer[i + offset] = waveTable[waveTableIndex];
                    phase += phaseStep;

                    if (phase > waveTable.Length)
                    {
                        phase -= waveTable.Length;
                    }
                }

                return count;
            }

            return 0;
        }
    }
}

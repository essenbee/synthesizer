using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace synthesizer
{
    public class SynthWaveProvider : ISampleProvider
    {
        private int _sampleRate;
        private readonly int _note;
        private readonly double _twelfthRootOfTwo = Math.Pow(2, 1.0 / 12.0);
        private readonly SignalGenerator _source;
        private readonly EnvelopeGenerator _adsr;
        public WaveFormat WaveFormat { get; }

        private float attackSeconds;
        public float AttackSeconds
        {
            get => attackSeconds;
            set
            {
                attackSeconds = value;
                _adsr.AttackRate = attackSeconds * WaveFormat.SampleRate;
            }
        }

        private float decaySeconds;
        public float DecaySeconds
        {
            get => decaySeconds;
            set
            {
                decaySeconds = value;
                _adsr.DecayRate = decaySeconds * WaveFormat.SampleRate;
            }
        }

        private float sustainLevel;
        public float SustainLevel
        {
            get => sustainLevel;
            set
            {
                sustainLevel = value;
                _adsr.SustainLevel = sustainLevel;
            }
        }

        private float releaseSeconds;
        public float ReleaseSeconds
        {
            get => releaseSeconds;

            set
            {
                releaseSeconds = value;
                _adsr.ReleaseRate = releaseSeconds * WaveFormat.SampleRate;
            }
        }

        private double baseFrequency;
        public double BaseFrequency
        {
            get => baseFrequency;
            set
            {
                baseFrequency = value;
                if (_source != null)
                {
                    _source.Frequency = Frequency;
                }
            }
        }
        //`
        //` <formula f_n = f_0 \cdot (\sqrt[12]{2})^n >
        //`
        public double Frequency => BaseFrequency * Math.Pow(_twelfthRootOfTwo, _note);

        public SynthWaveProvider(SignalGeneratorType waveType = SignalGeneratorType.Sin,
            int sampleRate = 44100, int note = 0)
        {
            _note = note;
            _sampleRate = sampleRate;
            var channels = 1; // Mono
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_sampleRate, channels);
            _adsr = new EnvelopeGenerator();

            //Defaults
            AttackSeconds = 0.01f;
            DecaySeconds = 0.0f;
            SustainLevel = 1.0f;
            ReleaseSeconds = 0.3f;

            _source = new SignalGenerator(_sampleRate, channels)
            {
                Frequency = Frequency,
                Type = waveType,
                Gain = 1.0f,
            };

            _adsr.Gate(true);
        }

        public void Stop() => _adsr.Gate(false);

        public int Read(float[] buffer, int offset, int count)
        {
            if (_adsr.State == EnvelopeGenerator.EnvelopeState.Idle)
            {
                return 0; // we've finished
            }

            var samples = _source.Read(buffer, offset, count);

            for (var i = 0; i < samples; i++)
            {
                buffer[offset++] *= _adsr.Process();
            }

            return samples;
        }
    }
}

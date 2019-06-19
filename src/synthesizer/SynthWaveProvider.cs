using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace synthesizer
{
    public class SynthWaveProvider : ISampleProvider
    {
        private int _sampleRate;
        private readonly double _twelfthRootOfTwo = Math.Pow(2, 1.0 / 12.0);
        private readonly SynthSignalGenerator _source;
        private readonly EnvelopeGenerator _adsr;
        public WaveFormat WaveFormat { get; }
        private bool enableSubOsc;
        public bool EnableSubOsc
        {
            get
            {
                return enableSubOsc;
            }
            set
            {
                enableSubOsc = value;
                if (_source != null)
                {
                    _source.EnableSubOsc = value;
                }
            }
        }

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

        private double note;
        public double Note
        {
            get => note;
            set
            {
                note = value;
                if (_source != null)
                {
                    //`
                    //` <formula f_n = f_0 \cdot (\sqrt[12]{2})^n >
                    //`
                    _source.Frequency = 110.0 * Math.Pow(_twelfthRootOfTwo, value - 33);
                }
            }
        }
        
        private double lfoFrequency;
        public double LfoFrequency
        {
            get
            {
                return lfoFrequency;
            }
            set
            {
                lfoFrequency = value;
                _source.LfoFrequency = value;
            }
        }
        private double lfoGain;
        public double LfoGain
        {
            get
            {
                return lfoGain;
            }
            set
            {
                lfoGain = value;
                _source.LfoGain = value;
            }
        }

        public SynthWaveProvider(SignalGeneratorType waveType = SignalGeneratorType.Sin,
            int sampleRate = 44100, float level = 1.0f)
        {
            _sampleRate = sampleRate;
            var channels = 1; // Mono
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_sampleRate, channels);
            _adsr = new EnvelopeGenerator();

            //Defaults
            AttackSeconds = 0.01f;
            DecaySeconds = 0.0f;
            SustainLevel = 1.0f;
            ReleaseSeconds = 0.3f;

            _source = new SynthSignalGenerator(_sampleRate, channels)
            {
                Frequency = 110.0 * Math.Pow(_twelfthRootOfTwo, 0),
                Type = waveType,
                Gain = level,
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

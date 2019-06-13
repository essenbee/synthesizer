using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace synthesizer
{
    public class SynthSignalGenerator : ISampleProvider
    {
        private readonly WaveFormat waveFormat;
        private readonly Random random = new Random();
        private readonly double[] pinkNoiseBuffer = new double[7];
        private const double TwoPi = 2 * Math.PI;
        private int nSample;
        private double phi;

        public WaveFormat WaveFormat => waveFormat;
        public double Frequency { get; set; }
        public double FrequencyLog => Math.Log(Frequency);
        public double FrequencyEnd { get; set; }
        public double FrequencyEndLog => Math.Log(FrequencyEnd);
        public double Gain { get; set; }
        public bool[] PhaseReverse { get; }
        public SignalGeneratorType Type { get; set; }
        public double SweepLengthSecs { get; set; }
        public double LfoFrequency { get; set; }
        public double LfoGain { get; set; }
        public bool EnableSubOsc { get; set; }

        public double SubOscillaorFrequency => Frequency / 2.0;

        public SynthSignalGenerator() : this(44100, 2)
        {

        }

        public SynthSignalGenerator(int sampleRate, int channel,
            double lfoFrequency = 0.0, double lfoGain = 0.0)
        {
            phi = 0;
            waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channel);

            // Default
            Type = SignalGeneratorType.Sin;
            Frequency = 440.0;
            Gain = 1;
            PhaseReverse = new bool[channel];
            SweepLengthSecs = 2;
            LfoFrequency = lfoFrequency;
            LfoGain = lfoGain;
        }

        private double lfoSample(int n)
        {
            //`
            //` <formula S_n = A_l \cdot sin(\frac{2\pi \cdot f_l \cdot n}{sr})>
            //`
            if (LfoGain == 0.0)
            {
                return 0.0;
            }

            var multiple = TwoPi * LfoFrequency / waveFormat.SampleRate;
            return LfoGain * Math.Sin(n * multiple);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int outIndex = offset;

            // Generator current value
            double multiple;
            double sampleValue;
            double sampleSaw;

            // Complete Buffer
            for (int sampleCount = 0; sampleCount < count / waveFormat.Channels; sampleCount++)
            {
                switch (Type)
                {
                    case SignalGeneratorType.Sin:

                        // Sinus Generator

                        multiple = TwoPi * Frequency / waveFormat.SampleRate;

                        if (EnableSubOsc)
                        {
                            var subOsc = Math.Sin(nSample * (TwoPi * SubOscillaorFrequency / waveFormat.SampleRate)
                                + lfoSample(nSample));
                            sampleValue = Gain * Math.Sin(nSample * multiple + lfoSample(nSample)
                                + (0.5 * Gain * subOsc));
                        }
                        else
                        {
                            sampleValue = Gain * Math.Sin(nSample * multiple + lfoSample(nSample));
                        }

                        nSample++;
                        
                        break;


                    case SignalGeneratorType.Square:

                        // Square Generator

                        multiple = TwoPi * Frequency / waveFormat.SampleRate;
                        sampleSaw = Math.Sin(nSample * multiple + lfoSample(nSample));
                        sampleValue = sampleSaw > 0 ? Gain : -Gain;

                        nSample++;
                        break;

                    case SignalGeneratorType.Triangle:

                        // Triangle Generator

                        multiple = TwoPi * Frequency / waveFormat.SampleRate;
                        sampleValue = Math.Asin(Math.Sin(nSample * multiple + lfoSample(nSample))) * (2.0 / Math.PI);
                        sampleValue *= Gain;

                        nSample++;
                        break;

                    case SignalGeneratorType.SawTooth:

                        // SawTooth Generator

                        multiple = 2 * Frequency / waveFormat.SampleRate;
                        sampleSaw = ((nSample * multiple + lfoSample(nSample)) % 2) - 1;
                        sampleValue = Gain * sampleSaw;

                        nSample++;
                        break;

                    case SignalGeneratorType.White:

                        // White Noise Generator
                        sampleValue = (Gain * NextRandomTwo());
                        break;

                    case SignalGeneratorType.Pink:

                        // Pink Noise Generator

                        double white = NextRandomTwo();
                        pinkNoiseBuffer[0] = 0.99886 * pinkNoiseBuffer[0] + white * 0.0555179;
                        pinkNoiseBuffer[1] = 0.99332 * pinkNoiseBuffer[1] + white * 0.0750759;
                        pinkNoiseBuffer[2] = 0.96900 * pinkNoiseBuffer[2] + white * 0.1538520;
                        pinkNoiseBuffer[3] = 0.86650 * pinkNoiseBuffer[3] + white * 0.3104856;
                        pinkNoiseBuffer[4] = 0.55000 * pinkNoiseBuffer[4] + white * 0.5329522;
                        pinkNoiseBuffer[5] = -0.7616 * pinkNoiseBuffer[5] - white * 0.0168980;
                        double pink = pinkNoiseBuffer[0] + pinkNoiseBuffer[1] + pinkNoiseBuffer[2] + pinkNoiseBuffer[3] + pinkNoiseBuffer[4] + pinkNoiseBuffer[5] + pinkNoiseBuffer[6] + white * 0.5362;
                        pinkNoiseBuffer[6] = white * 0.115926;
                        sampleValue = (Gain * (pink / 5));
                        break;

                    case SignalGeneratorType.Sweep:

                        // Sweep Generator
                        double f = Math.Exp(FrequencyLog + (nSample * (FrequencyEndLog - FrequencyLog)) / (SweepLengthSecs * waveFormat.SampleRate));

                        multiple = TwoPi * f / waveFormat.SampleRate;
                        phi += multiple;
                        sampleValue = Gain * (Math.Sin(phi));
                        nSample++;
                        if (nSample > SweepLengthSecs * waveFormat.SampleRate)
                        {
                            nSample = 0;
                            phi = 0;
                        }
                        break;

                    default:
                        sampleValue = 0.0;
                        break;
                }

                // Phase Reverse Per Channel
                for (int i = 0; i < waveFormat.Channels; i++)
                {
                    if (PhaseReverse[i])
                        buffer[outIndex++] = (float)-sampleValue;
                    else
                        buffer[outIndex++] = (float)sampleValue;
                }
            }
            return count;
        }

        private double NextRandomTwo()
        {
            return 2 * random.NextDouble() - 1;
        }

    }
}

using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace synthesizer
{
    public class TremoloSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider _source;
        private SignalGenerator _lfo;

        public int LfoFrequency { get; set; }
        public float LfoGain { get; set; }

        public WaveFormat WaveFormat => _source.WaveFormat;

        public TremoloSampleProvider(ISampleProvider source, int lfoFrequency = 5, float lfoAmplitude = 0.2f)
        {
            _source = source;
            LfoFrequency = lfoFrequency;
            LfoGain = LfoGain;
            UpdateLowFrequencyOscillator();
        }

        public void UpdateLowFrequencyOscillator()
        {
            _lfo = new SignalGenerator(_source.WaveFormat.SampleRate, _source.WaveFormat.Channels)
            {
                Type = SignalGeneratorType.Sin,
                Frequency = LfoFrequency,
                Gain = LfoGain,
            };
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var samples = _source.Read(buffer, offset, count);
            var lfoBuffer = new float[count];
            _lfo.Read(lfoBuffer, offset, count);

            for (int i = 0; i < samples; i++)
            {
                if (_lfo.Gain > 0.0f)
                {
                    buffer[offset + i] += buffer[offset + i] * lfoBuffer[offset + i];
                }
            }

            return samples;
        }
    }
}

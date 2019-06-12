using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace synthesizer
{
    public class TremoloSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider _source;
        private readonly SignalGenerator _lfo;
        
        public WaveFormat WaveFormat => _source.WaveFormat;

        public TremoloSampleProvider(ISampleProvider source, int lfoFrequency = 5, float lfoAmplitude = 0.2f)
        {
            _source = source;
            _lfo = new SignalGenerator(source.WaveFormat.SampleRate, source.WaveFormat.Channels)
            {
                Type = SignalGeneratorType.Sin,
                Frequency = lfoFrequency,
                Gain = lfoAmplitude,
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

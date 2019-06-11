using NAudio.Dsp;
using NAudio.Wave;

namespace synthesizer
{
    public class LowPassFilterSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider _source;
        private readonly BiQuadFilter _filter;
        
        public WaveFormat WaveFormat => _source.WaveFormat;

        public LowPassFilterSampleProvider(ISampleProvider source, int cutOffFrequency = 4000, float q = 0.7f)
        {
            _source = source;
            _filter = BiQuadFilter.LowPassFilter(source.WaveFormat.SampleRate, cutOffFrequency, q);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var samples = _source.Read(buffer, offset, count);

            for (int i = 0; i < samples; i++)
            {
                buffer[offset + i] = _filter.Transform(buffer[offset + i]);
            }

            return samples;
        }
    }
}

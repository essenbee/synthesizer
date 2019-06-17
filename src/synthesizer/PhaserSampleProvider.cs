using NAudio.Wave;
using System;

namespace synthesizer
{
    public class PhaserSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider _source;
        private float _wp;
        private float _stage1Input;
        private float _stage1Output;
        private float _stage2Input;
        private float _stage2Output;
        private float _stage3Input;
        private float _stage3Output;
        private float _stage4Input;
        private float _stage4Output;
        private float _stage5Input;
        private float _stage5Output;
        private float _stage6Input;
        private float _stage6Output;
        private float _stage7Input;
        private float _stage7Output;
        private float _stage8Input;
        private float _stage8Output;
        private float _sweepFactor;
        private float _maxWp;
        private float _minWp;

        public WaveFormat WaveFormat => _source.WaveFormat;

        public float DryMix { get; set; }
        public float WetMix { get; set; }
        public float Feedback { get; set; }
        
        private float bottomFrequency;
        public float BottomFrequency
        {
            get
            {
                return bottomFrequency;
            }
            set
            {
                bottomFrequency = value;
                SetSweep();
            }
        }
        private float width;
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                SetSweep();
            }
        }
        private float sweepRate;
        public float SweepRate
        {
            get
            {
                return sweepRate;
            }
            set
            {
                sweepRate = value;
                SetSweep();
            }
        }

        public PhaserSampleProvider(ISampleProvider source)
        {
            _source = source;

            SetSweep();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var samples = _source.Read(buffer, offset, count);

            if (Width > 0.0f)
            {
                for (int i = 0; i < samples; i++)
                {
                    buffer[offset + i] = Process(buffer[offset + i]);
                }
            }

            return samples;
        }
        private float Process(float inputSample)
        {
            //` <formula A = \frac{1 - w_p}{1 + w_p}>
            //` <formula w_p = \frac {\pi f_b}{S_r}>
            //`
            //` <formula 2; y_n = A \cdot (x_n + y_{n-1}) - x_{n-1}>
            //`
            var A = (1.0f - _wp) / (1.0f + _wp);
            var initialInput = inputSample + Feedback * _stage8Output;

            _stage1Output = A * (initialInput + _stage1Output) - _stage1Input;
            _stage1Input = initialInput;

            _stage2Output = A * (_stage1Output + _stage2Output) - _stage2Input;
            _stage2Input = _stage1Output;

            _stage3Output = A * (_stage2Output + _stage3Output) - _stage3Input;
            _stage3Input = _stage2Output;

            _stage4Output = A * (_stage3Output + _stage4Output) - _stage4Input;
            _stage4Input = _stage3Output;

            _stage5Output = A * (_stage4Output + _stage5Output) - _stage5Input;
            _stage5Input = _stage4Output;

            _stage6Output = A * (_stage5Output + _stage6Output) - _stage6Input;
            _stage6Input = _stage5Output;

            _stage7Output = A * (_stage6Output + _stage7Output) - _stage7Input;
            _stage7Input = _stage6Output;

            _stage8Output = A * (_stage7Output + _stage8Output) - _stage8Input;
            _stage8Input = _stage7Output;

            var output = (float)Math.Tanh(_stage8Output * WetMix + inputSample * DryMix);

            _wp *= _sweepFactor;

            if (_wp > _maxWp || _wp < _minWp)
            {
                _sweepFactor = 1.0f / _sweepFactor;
            }

            return output;
        }

        private void SetSweep()
        {
            _wp = _minWp = (float) (Math.PI * BottomFrequency / _source.WaveFormat.SampleRate);
            var widthOfSweep = Width + 6.0f;

            _maxWp = (float)(Math.PI * BottomFrequency * widthOfSweep / _source.WaveFormat.SampleRate);

            var actualSweepRate = (float)Math.Pow(10.0, SweepRate / _source.WaveFormat.SampleRate);
            actualSweepRate -= 1.0f;
            actualSweepRate *= 1.1f;
            actualSweepRate += 0.1f;

            _sweepFactor = (float)(Math.Pow(widthOfSweep, 2.0f * actualSweepRate));
        }
    }
}

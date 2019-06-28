using NAudio.Wave;
using System;

namespace synthesizer
{
    public class DelaySampleProvider : ISampleProvider
    {
        private readonly ISampleProvider _source;
        private float _delayPosition = 0.0f;
        private float _delayLength;
        private float _delay;
        private float _resamplePosition;
        private int _resamplePositionIntPart;
        private float _delayResamplePosition;
        private int _pos;
        private float[] _delayBuffer = new float[500000];

        public WaveFormat WaveFormat => _source.WaveFormat;

        // Resample if the delay length changes?
        private bool resample;
        public bool Resample
        {
            get => resample;
            set
            {
                resample = value;
                SetSlide();
            }
        }

        // Delay in milliseconds, 0 - 4000ms
        private double delayMs;
        public double DelayMs
        {
            get => delayMs;
            set
            {
                delayMs = value;
                _delayPosition = 0;
                SetSlide();
            }
        }

        // Amount of feedback, 0.0 - 0.95
        private float feedback;
        public float Feedback
        {
            get => feedback;
            set
            {
                feedback = value;
                SetSlide();
            }
        }

        // Amount of dry signal to feed into the delay effect, 0.0 - 1.0
        private float mix;
        public float Mix
        {
            get => mix;
            set
            {
                mix = value;
                SetSlide();
            }
        }

        // Output wet mix gain, 0.0 - 1.0
        private float outputWet;
        public float OutputWet
        {
            get => outputWet;
            set
            {
                outputWet = value;
                SetSlide();
            }
        }

        // Output dry mix gain, 0.0 - 1.0
        private float outputDry;
        public float OutputDry
        {
            get => outputDry;
            set
            {
                outputDry = value;
                SetSlide();
            }
        }

        public DelaySampleProvider(ISampleProvider source)
        {
            _source = source;
            _delayPosition = 0;
            Resample = true;

            SetSlide();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var read = _source.Read(buffer, offset, count);
            for (var i = 0; i < read; i++)
            {
                buffer[offset + i] = Process(buffer[offset + i]);
            }
            return read;
        }

        private float Process(float inputSample)
        {
            var delayPosition = (int)_delayPosition;
            var outputSample = _delayBuffer[delayPosition];

            _delayBuffer[delayPosition + 0] = Math.Min(Math.Max((inputSample * Mix) + (outputSample * Feedback), -4), 4);

            if ((_delayPosition += 1) >= _delayLength)
            {
                _delayPosition = 0;
            }

            return (inputSample * OutputDry) + (outputSample * OutputWet);
        }

        private void SetSlide()
        {
            _delay = _delayLength;
            _delayLength = (float)Math.Min((DelayMs * WaveFormat.SampleRate) / 1000, _delayBuffer.Length);

            if (_delay != _delayLength)
            {
                if (Resample && (_delay > _delayLength))
                {
                    _resamplePosition = 0;
                    _resamplePositionIntPart = 0;
                    _delayResamplePosition = _delay / _delayLength;

                    for (var i = 0; i < _delayLength; i++)
                    {
                        _pos = ((int)_resamplePosition) * 2;
                        _delayBuffer[_resamplePositionIntPart] = _delayBuffer[_pos];
                        _delayBuffer[_resamplePositionIntPart + 1] = _delayBuffer[_pos + 1];
                        _resamplePositionIntPart += 2;
                        _resamplePosition += _delayResamplePosition;
                    }

                    _delayPosition = _delayResamplePosition != 0.0f
                        ? _delayPosition / _delayResamplePosition
                        : _delayPosition;
                    _delayPosition = (_delayPosition < 0) ? 0 : (int)_delayPosition;
                }
                else
                {
                    if (Resample && (_delay < _delayLength))
                    {
                        _delayResamplePosition = _delay / _delayLength;
                        _resamplePosition = _delay;
                        _resamplePositionIntPart = ((int)_delayLength) * 2;

                        for (var i = 0; i < (int)_delayLength; i++)
                        {
                            _resamplePosition -= _delayResamplePosition;
                            _resamplePositionIntPart -= 2;

                            _pos = Math.Abs(((int)_resamplePosition) * 2);
                            _delayBuffer[_resamplePositionIntPart] = _delayBuffer[_pos];
                            _delayBuffer[_resamplePositionIntPart + 1] = _delayBuffer[_pos + 1];
                        }

                        _delayPosition = _delayResamplePosition != 0.0f
                            ? _delayPosition / _delayResamplePosition
                            : _delayPosition;
                        _delayPosition = (_delayPosition < 0) ? 0 : (int)_delayPosition;
                    }
                    else
                    {
                        if (Resample && (_delayPosition >= _delayLength))
                        {
                            _delayPosition = 0;
                        }
                    }
                }
            }
        }
    }
}

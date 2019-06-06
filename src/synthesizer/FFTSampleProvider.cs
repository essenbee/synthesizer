
using NAudio.Dsp;
using NAudio.Wave;
using System;

namespace synthesizer
{
  public class FFTSampleProvider : ISampleProvider
  {
    readonly int _fftPower;
    readonly int _fftWidth;
    readonly Action<float[], Complex[]> _sendFft;
    readonly ISampleProvider _next;

    public FFTSampleProvider(int fftPower, Action<float[], Complex[]> sendFft, ISampleProvider next)
    {
      _fftPower = Math.Max(fftPower, 4);
      _fftWidth = 1 << _fftPower;
      _sendFft = sendFft;
      _next = next;
    }
    public WaveFormat WaveFormat => _next.WaveFormat;

    public int Read(float[] buffer, int offset, int count)
    {
      var result = _next.Read (buffer, offset, count);

      var ss = new float[_fftWidth];
      var cs = new Complex[_fftWidth];
      var csLength = Math.Min(cs.Length, result);
      var step = ((float)csLength)/result;

      // Downsamples the waveform. Ugly.
      var acc = 0.0F;
      var pos = 0.0F;
      var cnt = 0;
      for (var iter = 0; iter < result; ++iter)
      {
        acc += buffer[iter];
        ++cnt;
        var newPos = pos + step;
        var ipos = (int)pos;
        var inewPos = (int)newPos;
        if (inewPos > ipos)
        {
          var s = acc/cnt;
          ss[ipos] = s;
          cs[ipos] = new Complex { X = s, Y = 0 };
          acc = 0.0F;
          cnt = 0;
        }
        pos = newPos;
      }

      if (cnt > 0)
      {
        var ipos = (int)pos;
        cs[ipos] = new Complex { X = acc/cnt, Y = 0 };
      }

      FastFourierTransform.FFT(true, _fftPower, cs);

      _sendFft(ss, cs);

      return result;
    }
  }
}

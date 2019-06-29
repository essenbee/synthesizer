using System;
using System.Windows.Media;

namespace synthesizer
{
  static partial class ExtensionMethods
  {
    public static double Lerp(this double t, double a, double b)
    {
      return a + t*(b-a);
    }

    public static Color Lerp(this double t, Color c0, Color c1)
    {
      var tt = t.Clamp(0, 1);

      var a = tt.Lerp(c0.A, c1.A).ToByte();
      var r = tt.Lerp(c0.R, c1.R).ToByte();
      var g = tt.Lerp(c0.G, c1.G).ToByte();
      var b = tt.Lerp(c0.B, c1.B).ToByte();

      return Color.FromArgb(a, r, g, b);
    }

    public static byte ToByte(this double v)
    {
      return (byte)Math.Round(v).Clamp(0, 255);
    }

    public static double Clamp(this double v, double a, double b)
    {
      var from  = Math.Min(a, b);
      var to    = Math.Max(a, b);

      return Math.Max(from, Math.Min(to, v));
    }

    public static double Min(this double v, double a)
    {
      return Math.Min(v, a);
    }

    public static double Max(this double v, double a)
    {
      return Math.Max(v, a);
    }

    public static bool IsNaN(this double v)
    {
      return double.IsNaN(v);
    }
  }
}

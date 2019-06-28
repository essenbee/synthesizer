using System;

namespace synthesizer
{
  static partial class ExtensionMethods
  {
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

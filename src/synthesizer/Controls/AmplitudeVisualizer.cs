using System;
using System.Windows;
using System.Windows.Media;

namespace synthesizer.Controls
{
  partial class AmplitudeVisualizer : FrameworkElement
  {
    static readonly Brush s_ampBrush = new LinearGradientBrush(Colors.Red, Colors.Green, new Point (0, 0), new Point (0, 1));

    protected override void OnRender(DrawingContext drawingContext)
    {
      base.OnRender(drawingContext);

      var width   = ActualWidth;
      var height  = ActualHeight;

      drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, width, height));

      var amps = Amplitudes;
      if (amps != null && amps.Length > 0)
      {
        var length  = amps.Length;

        var cellWidth = width / length;

        for (var iter = 0; iter < length; ++iter)
        {
          var h = height*Math.Min(Math.Max(0.0, amps[iter]), 1.0);
          drawingContext.DrawRectangle (s_ampBrush, null, new Rect(iter*cellWidth + 1, height - h, cellWidth - 2, h));
        }

      }
    }
  }
}

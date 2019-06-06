using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace synthesizer
{
  class AmplitudeVisualizer : FrameworkElement
  {
    public static readonly DependencyProperty AmplitudesProperty = DependencyProperty.Register (
        "Amplitudes",
        typeof (float[]),
        typeof (AmplitudeVisualizer),
        new FrameworkPropertyMetadata (
            null,
            FrameworkPropertyMetadataOptions.None,
            Changed_Amplitudes,
            Coerce_Amplitudes
        ));

    static object Coerce_Amplitudes(DependencyObject d, object v)
    {
      return v;
    }

    static void Changed_Amplitudes(DependencyObject d, DependencyPropertyChangedEventArgs a)
    {
      var fv = d as AmplitudeVisualizer;
      if (fv != null)
      {
        fv.InvalidateVisual();
      }
    }

    public float[] Amplitudes
    {
        get
        {
            return (float[])GetValue (AmplitudesProperty);
        }
        set
        {
            if (Amplitudes != value)
            {
                SetValue (AmplitudesProperty, value);
            }
        }
    }

    readonly Brush ampBrush = new LinearGradientBrush(Colors.Red, Colors.Green, new Point (0, 0), new Point (0, 1));

    public AmplitudeVisualizer()
    {
    }

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
          drawingContext.DrawRectangle (ampBrush, null, new Rect(iter*cellWidth + 1, height - h, cellWidth - 2, h));
        }

      }
    }
  }
}

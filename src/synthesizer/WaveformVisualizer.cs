﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace synthesizer
{
  class WaveformVisualizer : FrameworkElement
  {
    public static readonly DependencyProperty WaveformProperty = DependencyProperty.Register (
        "Waveform",
        typeof (float[]),
        typeof (WaveformVisualizer),
        new FrameworkPropertyMetadata (
            null,
            FrameworkPropertyMetadataOptions.None,
            Changed_Waveform,
            Coerce_Waveform
        ));

    static object Coerce_Waveform(DependencyObject d, object v)
    {
      return v;
    }

    static void Changed_Waveform(DependencyObject d, DependencyPropertyChangedEventArgs a)
    {
      var fv = d as WaveformVisualizer;
      if (fv != null)
      {
        fv.InvalidateVisual();
      }
    }

    public float[] Waveform
    {
        get
        {
            return (float[])GetValue (WaveformProperty);
        }
        set
        {
            if (Waveform != value)
            {
                SetValue (WaveformProperty, value);
            }
        }
    }

    public WaveformVisualizer()
    {
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
      base.OnRender(drawingContext);

      var width   = ActualWidth;
      var height  = ActualHeight;
      var h2      = height*0.5;

      drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, width, height));

      var samples = Waveform;
      if (samples != null && samples.Length > 0)
      {
        var length  = samples.Length;

        var cellWidth = width / length;

        for (var iter = 0; iter < length; ++iter)
        {
          var h = h2*Math.Min(Math.Max(-1.0, samples[iter]), 1.0) + h2;
          drawingContext.DrawRectangle (Brushes.White, null, new Rect(iter*cellWidth + 1, h-2, cellWidth - 2, 5));
        }

      }
    }
  }
}

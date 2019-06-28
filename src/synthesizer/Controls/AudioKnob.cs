using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace synthesizer.Controls
{
  partial class AudioKnob : FrameworkElement
  {
    readonly static Brush s_backgroundBrush   = new SolidColorBrush(Color.FromRgb(0x22, 0x22, 0x22));
    readonly static Brush s_ringBrush         = new SolidColorBrush(Color.FromRgb(0xEE, 0xEE, 0xEE));
    readonly static Brush s_beginRingBrush    = new SolidColorBrush(Color.FromRgb(0xEE, 0x00, 0x00));
    readonly static Brush s_middleRingBrush   = new SolidColorBrush(Color.FromRgb(0xEE, 0xEE, 0x00));
    readonly static Brush s_endRingBrush      = new SolidColorBrush(Color.FromRgb(0x00, 0xEE, 0x00));
    readonly static Pen   s_beginStroke       = new Pen(s_beginRingBrush  , 4.0);
    readonly static Pen   s_middleStroke      = new Pen(s_middleRingBrush , 4.0);
    readonly static Pen   s_endStroke         = new Pen(s_endRingBrush    , 4.0);

    double ComputeRatio()
    {
      var value = Value;
      var a     = From;
      var b     = To;
      var from  = a.Min(b);
      var to    = a.Max(b);
      var range = to - from;
      var ratio = ((value - from) / range).Clamp(0, 1);

      return ratio.IsNaN() ? 0 : ratio;
    }

    double ComputeValueFromPoint(Point pos)
    {
      var rs = RenderSize;

      var x = pos.X - rs.Width / 2.0;
      var y = pos.Y - rs.Height / 2.0;

      var angle = Math.Atan2(x, -y) * 180 / Math.PI + 180;

      var ratio = ((angle - 45) / 270).Clamp(0, 1);

      var a     = From;
      var b     = To;
      var from  = a.Min(b);
      var to    = a.Max(b);

      var range = to - from;

      var value = from + range * ratio;

      return value.IsNaN() ? a : value;
    }

    partial void Coerce_Value(ref double coercedValue)
    {
      coercedValue = coercedValue.Clamp(From, To);
    }

    partial void Changed_From(double oldValue, double newValue)
    {
      CoerceValue(ValueProperty);
    }

    partial void Changed_To(double oldValue, double newValue)
    {
      CoerceValue(ValueProperty);
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
      base.OnMouseDown(e);

      var pos   = e.GetPosition(this);

      Value     = ComputeValueFromPoint(pos);
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
      base.OnMouseDown(e);

      var pos   = e.GetPosition(this);

      Value     = ComputeValueFromPoint(pos);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      if (e.LeftButton == MouseButtonState.Pressed)
      {
        var pos   = e.GetPosition(this);

        Value     = ComputeValueFromPoint(pos);
      }
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
      base.OnRender(drawingContext);

      var rs = RenderSize;
      var width = rs.Width;
      var height = rs.Height;

      var zoom = width.Min(height)/100.0;

      if (!zoom.IsNaN())
      {
        if (IsFocused)
        {
          drawingContext.DrawRectangle(null, new Pen(s_backgroundBrush, 1.0), new Rect(0, 0, width, height));
        }

        var ratio = ComputeRatio();
        var angle = ratio*270 + 45;

        drawingContext.PushTransform(new TranslateTransform(width/2, height/2));
        drawingContext.PushTransform(new ScaleTransform(zoom, zoom));

        drawingContext.DrawEllipse(s_backgroundBrush, null, new Point(0, 0), 50, 50);

        if (ratio < 0.5)
        {
          drawingContext.DrawEllipse(null, s_beginStroke, new Point(0, 0), 43, 43);
          drawingContext.PushOpacity(ratio/0.5);
          drawingContext.DrawEllipse(null, s_middleStroke, new Point(0, 0), 43, 43);
          drawingContext.Pop();
        }
        else
        {
          drawingContext.DrawEllipse(null, s_middleStroke, new Point(0, 0), 43, 43);
          drawingContext.PushOpacity((ratio - 0.5)/0.5);
          drawingContext.DrawEllipse(null, s_endStroke, new Point(0, 0), 43, 43);
          drawingContext.Pop();
        }

        drawingContext.PushTransform(new RotateTransform(angle));
        drawingContext.DrawEllipse(s_ringBrush, null, new Point(0, 30), 10, 10);

        drawingContext.Pop();
        drawingContext.Pop();
      }

    }

  }
}

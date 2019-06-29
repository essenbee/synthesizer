using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace synthesizer.Controls
{
  partial class AudioKnob : FrameworkElement
  {
    readonly static Color s_backgroundColor   = Color.FromRgb(0x22, 0x22, 0x22);
    readonly static Color s_foregroundColor   = Color.FromRgb(0xEE, 0xEE, 0xEE);
    readonly static Color s_beginRingColor    = Color.FromRgb(0xEE, 0x00, 0x88);
    readonly static Color s_middleRingColor   = Color.FromRgb(0xEE, 0xEE, 0x88);
    readonly static Color s_endRingColor      = Color.FromRgb(0x00, 0xEE, 0x88);

    readonly static Brush s_foregroundBrush   = new SolidColorBrush(s_foregroundColor);

    readonly TranslateTransform _translate  = new TranslateTransform();
    readonly ScaleTransform     _scale      = new ScaleTransform()    ;
    readonly RotateTransform    _rotate     = new RotateTransform()   ;

    readonly RadialGradientBrush _knobBrush = new RadialGradientBrush(
      new GradientStopCollection
        {
          new GradientStop(s_backgroundColor, 0.0),
          new GradientStop(s_backgroundColor, 0.8),
          new GradientStop(s_foregroundColor, 0.8),
          new GradientStop(s_foregroundColor, 0.9),
          new GradientStop(s_backgroundColor, 0.9),
          new GradientStop(s_backgroundColor, 1.0),
        });

    MouseEventHandler MouseMovedHandler ;
    FrameworkElement  currentRoot       ;

    partial void Constructed__AudioKnob()
    {
      MouseMovedHandler = MouseMoved;
    }

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

    static FrameworkElement FindRoot(FrameworkElement e)
    {
      if (e != null)
      {
        var p = e.Parent as FrameworkElement;
        if (p != null)
        {
          return FindRoot(p);
        }
        else
        {
          return e;
        }
      }
      else
      {
        return null;
      }
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
      base.OnMouseDown(e);

      var pos     = e.GetPosition(this);

      Value       = ComputeValueFromPoint(pos);

      currentRoot = FindRoot(this);

      if(currentRoot != null)
      {
        currentRoot.AddHandler(MouseMoveEvent, MouseMovedHandler, true);
      }
    }

    void MouseMoved(object s, MouseEventArgs e)
    {
      var pos   = e.GetPosition(this);
      if (e.LeftButton == MouseButtonState.Pressed)
      {
        Value     = ComputeValueFromPoint(pos);
      }
      else if (currentRoot != null)
      {
        currentRoot.RemoveHandler(MouseMoveEvent, MouseMovedHandler);
        currentRoot = null;
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
        var ratio = ComputeRatio();
        var angle = ratio*270 + 45;

        var origo = new Point();

        _translate.X = width/2;
        _translate.Y = height/2;

        _scale.ScaleX = zoom;
        _scale.ScaleY = zoom;

        _rotate.Angle = angle;

        var stops     = _knobBrush.GradientStops;

        var ringColor = ratio < 0.5
          ? (2.0*ratio).Lerp(s_beginRingColor, s_middleRingColor)
          : (2.0*(ratio - 0.5)).Lerp(s_middleRingColor, s_endRingColor)
          ;

        stops[2].Color= ringColor;
        stops[3].Color= ringColor;

        drawingContext.PushTransform(_translate);
        drawingContext.PushTransform(_scale);

        drawingContext.DrawEllipse(_knobBrush, null, origo, 50, 50);

        drawingContext.PushTransform(_rotate);
        drawingContext.DrawEllipse(s_foregroundBrush, null, new Point(0, 28), 10, 10);

        drawingContext.Pop();
        drawingContext.Pop();
      }

    }

  }
}

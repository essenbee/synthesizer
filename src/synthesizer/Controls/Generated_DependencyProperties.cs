using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// ############################################################################
// #                                                                          #
// #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
// #                                                                          #
// # This means that any edits to the .cs file will be lost when its          #
// # regenerated. Changes should instead be applied to the corresponding      #
// # template file (.tt)                                                      #
// ############################################################################






// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable InconsistentNaming
// ReSharper disable InvocationIsSkipped
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PossibleUnintendedReferenceComparison
// ReSharper disable RedundantAssignment
// ReSharper disable RedundantCast
// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Local

namespace synthesizer.Controls
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    using System.Windows;
    using System.Windows.Media;

    // ------------------------------------------------------------------------
    // AudioKnob
    // ------------------------------------------------------------------------
    partial class AudioKnob
    {
        #region Uninteresting generated code
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register (
            "Value",
            typeof (double),
            typeof (AudioKnob),
            new FrameworkPropertyMetadata (
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                Changed_Value,
                Coerce_Value
            ));

        static void Changed_Value (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as AudioKnob;
            if (instance != null)
            {
                var oldValue = (double)eventArgs.OldValue;
                var newValue = (double)eventArgs.NewValue;

                instance.Changed_Value (oldValue, newValue);
            }
        }


        static object Coerce_Value (DependencyObject dependencyObject, object basevalue)
        {
            var instance = dependencyObject as AudioKnob;
            if (instance == null)
            {
                return basevalue;
            }
            var value = (double)basevalue;

            instance.Coerce_Value (ref value);


            return value;
        }

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register (
            "From",
            typeof (double),
            typeof (AudioKnob),
            new FrameworkPropertyMetadata (
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender,
                Changed_From,
                Coerce_From
            ));

        static void Changed_From (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as AudioKnob;
            if (instance != null)
            {
                var oldValue = (double)eventArgs.OldValue;
                var newValue = (double)eventArgs.NewValue;

                instance.Changed_From (oldValue, newValue);
            }
        }


        static object Coerce_From (DependencyObject dependencyObject, object basevalue)
        {
            var instance = dependencyObject as AudioKnob;
            if (instance == null)
            {
                return basevalue;
            }
            var value = (double)basevalue;

            instance.Coerce_From (ref value);


            return value;
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register (
            "To",
            typeof (double),
            typeof (AudioKnob),
            new FrameworkPropertyMetadata (
                100.0,
                FrameworkPropertyMetadataOptions.AffectsRender,
                Changed_To,
                Coerce_To
            ));

        static void Changed_To (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as AudioKnob;
            if (instance != null)
            {
                var oldValue = (double)eventArgs.OldValue;
                var newValue = (double)eventArgs.NewValue;

                instance.Changed_To (oldValue, newValue);
            }
        }


        static object Coerce_To (DependencyObject dependencyObject, object basevalue)
        {
            var instance = dependencyObject as AudioKnob;
            if (instance == null)
            {
                return basevalue;
            }
            var value = (double)basevalue;

            instance.Coerce_To (ref value);


            return value;
        }

        #endregion

        // --------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------
        public AudioKnob ()
        {
            CoerceAllProperties ();
            Constructed__AudioKnob ();
        }
        // --------------------------------------------------------------------
        partial void Constructed__AudioKnob ();
        // --------------------------------------------------------------------
        void CoerceAllProperties ()
        {
            CoerceValue (ValueProperty);
            CoerceValue (FromProperty);
            CoerceValue (ToProperty);
        }


        // --------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------


        // --------------------------------------------------------------------
        public double Value
        {
            get
            {
                return (double)GetValue (ValueProperty);
            }
            set
            {
                if (Value != value)
                {
                    SetValue (ValueProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Value (double oldValue, double newValue);
        partial void Coerce_Value (ref double coercedValue);
        // --------------------------------------------------------------------



        // --------------------------------------------------------------------
        public double From
        {
            get
            {
                return (double)GetValue (FromProperty);
            }
            set
            {
                if (From != value)
                {
                    SetValue (FromProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_From (double oldValue, double newValue);
        partial void Coerce_From (ref double coercedValue);
        // --------------------------------------------------------------------



        // --------------------------------------------------------------------
        public double To
        {
            get
            {
                return (double)GetValue (ToProperty);
            }
            set
            {
                if (To != value)
                {
                    SetValue (ToProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_To (double oldValue, double newValue);
        partial void Coerce_To (ref double coercedValue);
        // --------------------------------------------------------------------


    }
    // ------------------------------------------------------------------------

    // ------------------------------------------------------------------------
    // AmplitudeVisualizer
    // ------------------------------------------------------------------------
    partial class AmplitudeVisualizer
    {
        #region Uninteresting generated code
        public static readonly DependencyProperty AmplitudesProperty = DependencyProperty.Register (
            "Amplitudes",
            typeof (float[]),
            typeof (AmplitudeVisualizer),
            new FrameworkPropertyMetadata (
                default (float[]),
                FrameworkPropertyMetadataOptions.AffectsRender,
                Changed_Amplitudes,
                Coerce_Amplitudes
            ));

        static void Changed_Amplitudes (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as AmplitudeVisualizer;
            if (instance != null)
            {
                var oldValue = (float[])eventArgs.OldValue;
                var newValue = (float[])eventArgs.NewValue;

                instance.Changed_Amplitudes (oldValue, newValue);
            }
        }


        static object Coerce_Amplitudes (DependencyObject dependencyObject, object basevalue)
        {
            var instance = dependencyObject as AmplitudeVisualizer;
            if (instance == null)
            {
                return basevalue;
            }
            var value = (float[])basevalue;

            instance.Coerce_Amplitudes (ref value);


            return value;
        }

        #endregion

        // --------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------
        public AmplitudeVisualizer ()
        {
            CoerceAllProperties ();
            Constructed__AmplitudeVisualizer ();
        }
        // --------------------------------------------------------------------
        partial void Constructed__AmplitudeVisualizer ();
        // --------------------------------------------------------------------
        void CoerceAllProperties ()
        {
            CoerceValue (AmplitudesProperty);
        }


        // --------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------


        // --------------------------------------------------------------------
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
        // --------------------------------------------------------------------
        partial void Changed_Amplitudes (float[] oldValue, float[] newValue);
        partial void Coerce_Amplitudes (ref float[] coercedValue);
        // --------------------------------------------------------------------


    }
    // ------------------------------------------------------------------------

    // ------------------------------------------------------------------------
    // WaveformVisualizer
    // ------------------------------------------------------------------------
    partial class WaveformVisualizer
    {
        #region Uninteresting generated code
        public static readonly DependencyProperty WaveformProperty = DependencyProperty.Register (
            "Waveform",
            typeof (float[]),
            typeof (WaveformVisualizer),
            new FrameworkPropertyMetadata (
                default (float[]),
                FrameworkPropertyMetadataOptions.AffectsRender,
                Changed_Waveform,
                Coerce_Waveform
            ));

        static void Changed_Waveform (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as WaveformVisualizer;
            if (instance != null)
            {
                var oldValue = (float[])eventArgs.OldValue;
                var newValue = (float[])eventArgs.NewValue;

                instance.Changed_Waveform (oldValue, newValue);
            }
        }


        static object Coerce_Waveform (DependencyObject dependencyObject, object basevalue)
        {
            var instance = dependencyObject as WaveformVisualizer;
            if (instance == null)
            {
                return basevalue;
            }
            var value = (float[])basevalue;

            instance.Coerce_Waveform (ref value);


            return value;
        }

        #endregion

        // --------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------
        public WaveformVisualizer ()
        {
            CoerceAllProperties ();
            Constructed__WaveformVisualizer ();
        }
        // --------------------------------------------------------------------
        partial void Constructed__WaveformVisualizer ();
        // --------------------------------------------------------------------
        void CoerceAllProperties ()
        {
            CoerceValue (WaveformProperty);
        }


        // --------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------


        // --------------------------------------------------------------------
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
        // --------------------------------------------------------------------
        partial void Changed_Waveform (float[] oldValue, float[] newValue);
        partial void Coerce_Waveform (ref float[] coercedValue);
        // --------------------------------------------------------------------


    }
    // ------------------------------------------------------------------------

}

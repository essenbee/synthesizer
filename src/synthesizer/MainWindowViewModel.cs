using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace synthesizer
{
    public partial class MainWindowViewModel
    {
        private readonly List<Key> keyboard = new List<Key>
        {
            Key.Z, Key.S, Key.X, Key.C, Key.F, Key.V, Key.G, Key.B,
            Key.N, Key.J, Key.M, Key.K, Key.OemComma, Key.L,
            Key.OemPeriod, Key.OemQuestion,
        };

        private readonly SynthWaveProvider oscillator = new SynthWaveProvider();
        private readonly double _twefthRootOfTwo = Math.Pow(2, 1.0/12.0);
        private IWavePlayer player;

        public double BaseFrequency => 110.0f; // A2

        public void KeyDown(KeyEventArgs e)
        {
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1)
            {
                Frequency = BaseFrequency * Math.Pow(_twefthRootOfTwo, keyVal);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1)
            {
                Frequency = 0.0f;
            }
        }

        // Constraction event

        partial void Constructed()
        {
            Volume = 0.25;
        }

        // Property events

        partial void Changed_Volume(double prev, double current)
        {
          oscillator.Volume = (float)current;
          VolumeLabel = $"{(int)(Volume * 100.0)}%";
        }

        partial void Changed_Frequency(double prev, double current)
        {
          oscillator.Frequency = (float)current;
        }

        // Command events

        partial void CanExecute_OnCommand(ref bool result)
        {
          result = player == null;
        }

        partial void Execute_OnCommand()
        {
            if (player == null)
            {
                var waveOutEvent = new WaveOutEvent
                {
                    NumberOfBuffers = 2,
                    DesiredLatency = 100,
                };

                player = waveOutEvent;
                player.Init(new SampleToWaveProvider(oscillator));

                player.Play();

                ResetCanExecute ();
            }
        }

        partial void CanExecute_OffCommand(ref bool result)
        {
          result = player != null;
        }

        partial void Execute_OffCommand()
        {
            if (player != null)
            {
                player.Dispose();
                player = null;

                ResetCanExecute ();
            }
        }
    }
}

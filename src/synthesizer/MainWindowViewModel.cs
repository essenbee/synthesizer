using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace synthesizer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly List<Key> keyboard = new List<Key>
        {
            Key.Z, Key.S, Key.X, Key.C, Key.F, Key.V, Key.G, Key.B,
            Key.N, Key.J, Key.M, Key.K, Key.OemComma, Key.L,
            Key.OemPeriod, Key.OemQuestion,
        };

        
        private readonly SynthWaveProvider oscillator = new SynthWaveProvider();
        private IWavePlayer player;
        private float _twefthRootOfTwo = 18904.0f / 17843.0f;


        public float BaseFrequency => 110.0f; // A2
        

        public float Volume
        {
            get { return oscillator.Volume; }
            set
            {
                if (oscillator.Volume == value)
                {
                    return;
                }

                oscillator.Volume = value;
                OnPropertyChanged();
                OnPropertyChanged("VolumeLabel");
            }
        }

        public double Frequency
        {
            get { return oscillator.Frequency; }
            set
            {
                if (oscillator.Frequency == value)
                    return;
                oscillator.Frequency = value;
            }
        }
        
        public string VolumeLabel => $"{(int)(Volume * 100.0)}%";
        public ICommand OnCommand { get; private set; }
        public ICommand OffCommand { get; private set; }

        public MainWindowViewModel()
        {
            OnCommand = new UserCommand(On);
            OffCommand = new UserCommand(Off);
            Volume = 0.25f;
            oscillator = new SynthWaveProvider();
        }

        //`
        //` <formula f_n = f_b \cdot (\sqrt[12]{2})^n >
        //`

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

        private void On()
        {
            if (player is null)
            {
                var waveOutEvent = new WaveOutEvent
                {
                    NumberOfBuffers = 2,
                    DesiredLatency = 100,
                };

                player = waveOutEvent;
                player.Init(new SampleToWaveProvider(oscillator));
            }

            player.Play();
        }

        private void Off()
        {
            if (player != null)
            {
                player.Dispose();
                player = null;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyChanged = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }
    }
}

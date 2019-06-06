using NAudio.Wave;
using NAudio.Wave.SampleProviders;
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

        private SynthWaveProvider[] oscillators = new SynthWaveProvider[16];
        private VolumeSampleProvider _volControl;
        private MixingSampleProvider _mixer;
        
        private IWavePlayer player;

        public double BaseFrequency { get; set; } = 110.0;

        public void KeyDown(KeyEventArgs e)
        {
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1 && oscillators[keyVal] is null)
            {
                oscillators[keyVal] = new SynthWaveProvider(44100, keyVal)
                {
                    BaseFrequency = BaseFrequency,
                };

                oscillators[keyVal].NoteOn = true;
                _mixer.AddMixerInput(oscillators[keyVal]);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1)
            {
                oscillators[keyVal].NoteOn = false;
                oscillators[keyVal] = null;
            }
        }

        // Construction event

        partial void Constructed()
        {
            Volume = 0.25;
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);
            _mixer = new MixingSampleProvider(waveFormat) { ReadFully = true }; // Always produce samples
            _volControl = new VolumeSampleProvider(_mixer)
            {
                Volume = 0.25f,
            };
        }

        // Property events

        partial void Changed_Volume(double prev, double current)
        {
            if (_volControl != null)
            {
                _volControl.Volume = (float)current;
            }

            VolumeLabel = $"{(int)(Volume * 100.0)}%";
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
                player.Init(new SampleToWaveProvider(_volControl));

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

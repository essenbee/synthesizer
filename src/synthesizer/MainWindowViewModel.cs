using NAudio.Dsp;
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

        private SynthWaveProvider[] _oscillators = new SynthWaveProvider[16];
        private VolumeSampleProvider _volControl;
        private MixingSampleProvider _mixer;
        private FFTSampleProvider _fftProvider;
        private IWavePlayer _player;

        public double BaseFrequency { get; set; } = 110.0;

        public void KeyDown(KeyEventArgs e)
        {
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1 &&  _oscillators[keyVal] is null)
            {
                _oscillators[keyVal] = new SynthWaveProvider(44100, keyVal)
                {
                    BaseFrequency = BaseFrequency,
                };

                _oscillators[keyVal].NoteOn = true;
                _mixer.AddMixerInput(_oscillators[keyVal]);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1)
            {
                _oscillators[keyVal].NoteOn = false;
                _oscillators[keyVal] = null;
            }
        }

        void UpdateRealTimeData(float[] waveform, Complex[] frequencies)
        {
          var famps = new float[frequencies.Length/2];
          for (var iter = 0; iter < famps.Length; ++iter)
          {
            var amp = 4.0*Math.Sqrt(Math.Abs(frequencies[iter].X));
            famps[iter] = (float)amp;
          }

          Waveform = waveform;
          FrequencyAmplitudes = famps;
        }

        // Construction event
        partial void Constructed()
        {
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);
            _mixer = new MixingSampleProvider(waveFormat) { ReadFully = true }; // Always produce samples
            _volControl = new VolumeSampleProvider(_mixer)
            {
                Volume = 0.25f,
            };
            _fftProvider = new FFTSampleProvider(8, (ss, cs) => Dispatch(() => UpdateRealTimeData(ss, cs)), _volControl);
            Volume = 0.25;
        }

        // Property events
        partial void Changed_Volume(double prev, double current)
        {
          _volControl.Volume = (float)current;
          VolumeLabel = $"{(int)(Volume * 100.0)}%";
        }

        // Command events
        partial void CanExecute_OnCommand(ref bool result)
        {
          result = _player == null;
        }

        partial void Execute_OnCommand()
        {
            if (_player == null)
            {
                var waveOutEvent = new WaveOutEvent
                {
                    NumberOfBuffers = 2,
                    DesiredLatency = 100,
                };

                _player = waveOutEvent;
                _player.Init(new SampleToWaveProvider(_fftProvider));

                _player.Play();

                ResetCanExecute();
            }
        }

        partial void CanExecute_OffCommand(ref bool result)
        {
          result = _player != null;
        }

        partial void Execute_OffCommand()
        {
            if (_player != null)
            {
                _player.Dispose();
                _player = null;

                ResetCanExecute();
            }
        }
    }
}

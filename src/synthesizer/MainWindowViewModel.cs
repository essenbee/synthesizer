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
        private TremoloSampleProvider _tremolo;
        private IWavePlayer _player;

        public double BaseFrequency { get; set; } = 110.0;
        public SignalGeneratorType WaveType { get; set; } = SignalGeneratorType.Sin;
        public bool EnableLpf { get; set; }
        public bool EnableSubOsc { get; set; }
        public bool EnableVibrato { get; set; }

        public void KeyDown(KeyEventArgs e)
        {
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1 && _oscillators[keyVal] is null)
            {
                _oscillators[keyVal] = new SynthWaveProvider(WaveType, 44100, keyVal)
                {
                    BaseFrequency = BaseFrequency,
                    AttackSeconds = Attack,
                    DecaySeconds = Decay,
                    SustainLevel = Sustain,
                    ReleaseSeconds = Release,
                    LfoFrequency = 5.0,
                    LfoGain = EnableVibrato ? 0.2 : 0.0,
                    EnableSubOsc = EnableSubOsc,
                };

                _mixer.AddMixerInput(EnableLpf 
                    ? (ISampleProvider)new LowPassFilterSampleProvider(_oscillators[keyVal], CutOff, Q) 
                    : _oscillators[keyVal]);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1)
            {
                _oscillators[keyVal].Stop();
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

            _tremolo = new TremoloSampleProvider(_volControl, TremoloFreq, TremoloGain);
            _fftProvider = new FFTSampleProvider(8, (ss, cs) => Dispatch(() => UpdateRealTimeData(ss, cs)), _tremolo);

            WaveType = SignalGeneratorType.Sin;
            Volume = 0.25;
            Attack = 0.01f;
            Decay = 0.01f;
            Sustain = 1.0f;
            Release = 0.3f;
            CutOff = 4000;
            Q = 0.7f;
            TremoloFreq = 5;
        }

        // Property events
        partial void Changed_Volume(double prev, double current)
        {
          _volControl.Volume = (float)current;
          VolumeLabel = $"{(int)(Volume * 100.0)}%";
        }

        partial void Changed_Attack(float prev, float current)
        {
            AttackLabel = $"{(int)(Attack * 1000.0)} ms";
        }

        partial void Changed_Decay(float prev, float current)
        {
            DecayLabel = $"{(int)(Decay * 1000.0)} ms";
        }

        partial void Changed_Sustain(float prev, float current)
        {
            SustainLabel = $"{(int)(Sustain * 100.0)}%";
        }

        partial void Changed_Release(float prev, float current)
        {
            ReleaseLabel = $"{(int)(Release * 1000.0)} ms";
        }

        partial void Changed_CutOff(int prev, int current)
        {
            CutOffLabel = $"{CutOff} Hz";
        }

        partial void Changed_Q(float prev, float current)
        {
            QLabel = $"{((int)(Q * 100.0f))/ 100.0f}";
        }

        partial void Changed_TremoloFreq(int prev, int current)
        {
            TremoloFreqLabel = $"{TremoloFreq} Hz";

            if (_tremolo != null)
            {
                _tremolo.LfoFrequency = TremoloFreq; ;
                _tremolo.UpdateLowFrequencyOscillator();
            }
        }

        partial void Changed_TremoloGain(float prev, float current)
        {
            TremoloGainLabel = (TremoloGain > 0.0f)
                ? $"{TremoloGain * 100.0f}%"
                : "0%";

            if (_tremolo != null)
            {
                _tremolo.LfoGain = TremoloGain;
                _tremolo.UpdateLowFrequencyOscillator();
            }
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

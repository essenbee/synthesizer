﻿using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using synthesizer.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace synthesizer
{
    public enum BaseFrequency
    {
      A2  = 0 ,
      A3  = 1 ,
      A4  = 2 ,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Octave
    {
        [Description("-2")]
        Down_Two = 0,
        [Description("-1")]
        Down_One = 1,
        [Description("0")]
        Same = 2 ,
        [Description("+1")]
        Up_One = 3,
        [Description("+2")]
        Up_Two = 4,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Semitone
    {
        [Description("-11")]
        _1 = -0,
        [Description("-10")]
        _2 = 1,
        [Description("-9")]
        _3 = 2,
        [Description("-8")]
        _4 = 3,
        [Description("7")]
        _5 = 4,
        [Description("-6")]
        _6 = 5,
        [Description("-5")]
        _7 = 6,
        [Description("-4")]
        _8 = 7,
        [Description("-3")]
        _9 = 8,
        [Description("-2")]
        _10 = 9,
        [Description("-1")]
        _11 = 10,
        [Description("0")]
        Same  = 11 ,
        [Description("+1")]
        _12  = 12 ,
        [Description("+2")]
        _13  = 13 ,
        [Description("+3")]
        _14  = 14 ,
        [Description("+4")]
        _15 = 15 ,
        [Description("+5")]
        _16  = 16 ,
        [Description("+6")]
        _17  = 17 ,
        [Description("+7")]
        _18  = 18 ,
        [Description("+8")]
        _19  = 19 ,
        [Description("+9")]
        _20  = 20 ,
        [Description("+10")]
        _21 = 21,
        [Description("+11")]
        _22 = 22,
    }

    public partial class MainWindowViewModel
    {
        private readonly List<Key> keyboard = new List<Key>
        {
            Key.Z, Key.S, Key.X, Key.C, Key.F, Key.V, Key.G, Key.B,
            Key.N, Key.J, Key.M, Key.K, Key.OemComma, Key.L,
            Key.OemPeriod, Key.OemQuestion,
        };

        private SynthWaveProvider[,] _oscillators = new SynthWaveProvider[3,16];
        private VolumeSampleProvider _volControl;
        private MixingSampleProvider _mixer;
        private FFTSampleProvider _fftProvider;
        private TremoloSampleProvider _tremolo;
        private ChorusSampleProvider _chorus;
        private PhaserSampleProvider _phaser;
        private LowPassFilterSampleProvider _lpf;
        private IWavePlayer _player;

        public int Voice2Freq => 12*((int)Octave2 - 2) + ((int)Semitone2 - 11);
        public int Voice3Freq => 12*((int)Octave3 - 2) + ((int)Semitone3 - 11);

        public static T[] EnumValues<T>()
        {
          return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }

        public SignalGeneratorType[] SelectableWaveforms => 
          new [] 
          {
            SignalGeneratorType.SawTooth  ,
            SignalGeneratorType.Sin       ,
            SignalGeneratorType.Square    ,
            SignalGeneratorType.Triangle  ,
            SignalGeneratorType.White     ,
            SignalGeneratorType.Pink      ,
            SignalGeneratorType.Sweep     ,
          };

        public BaseFrequency[] SelectableBaseFrequencies => EnumValues<BaseFrequency> ();

        public Octave[] SelectableOctaves => EnumValues<Octave>();

        public Semitone[] SelectableSemiTones => EnumValues<Semitone>();

        public int KeyValueBase
        {
          get
          {
            switch(BaseFrequency)
            {
            case BaseFrequency.A2: return 33;
            case BaseFrequency.A3: return 45;
            case BaseFrequency.A4: return 57;
            default: return 33;
            }
          }
        }

        public bool MidiEnabled { get; set; }

        public void KeyDown(KeyEventArgs e)
        {
            if (MidiEnabled) return;
            
            var keyVal = keyboard.IndexOf(e.Key);
            var midiKeyVal = keyVal + KeyValueBase;
            if (keyVal > -1 && _oscillators[0,keyVal] is null)
            {
                _oscillators[0,keyVal] = new SynthWaveProvider(WaveType1, 44100, Level1)
                {
                    Note = midiKeyVal,
                    AttackSeconds = Attack,
                    DecaySeconds = Decay,
                    SustainLevel = Sustain,
                    ReleaseSeconds = Release,
                    LfoFrequency = 5.0,
                    LfoGain = EnableVibrato ? 0.2 : 0.0,
                    EnableSubOsc = EnableSubOsc,
                };

                _oscillators[1, keyVal] = new SynthWaveProvider(WaveType2, 44100, Level2)
                {
                    Note = midiKeyVal + Voice2Freq,
                    AttackSeconds = Attack2,
                    DecaySeconds = Decay2,
                    SustainLevel = Sustain2,
                    ReleaseSeconds = Release2,
                    LfoFrequency = 5.0,
                    LfoGain = EnableVibrato ? 0.2 : 0.0,
                    EnableSubOsc = false,
                };

                _oscillators[2, keyVal] = new SynthWaveProvider(WaveType3, 44100, Level3)
                {
                    Note = midiKeyVal + Voice3Freq,
                    AttackSeconds = Attack3,
                    DecaySeconds = Decay3,
                    SustainLevel = Sustain3,
                    ReleaseSeconds = Release3,
                    LfoFrequency = 5.0,
                    LfoGain = EnableVibrato ? 0.2 : 0.0,
                    EnableSubOsc = false,
                };

                _mixer.AddMixerInput(EnableLpf 
                    ? (ISampleProvider)new LowPassFilterSampleProvider(_oscillators[0, keyVal], CutOff, Q) 
                    : _oscillators[0,keyVal]);
                _mixer.AddMixerInput(EnableLpf
                    ? (ISampleProvider)new LowPassFilterSampleProvider(_oscillators[1, keyVal], CutOff, Q)
                    : _oscillators[1, keyVal]);
                _mixer.AddMixerInput(EnableLpf
                    ? (ISampleProvider)new LowPassFilterSampleProvider(_oscillators[2, keyVal], CutOff, Q)
                    : _oscillators[2, keyVal]);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            if (MidiEnabled) return;
            
            var keyVal = keyboard.IndexOf(e.Key);
            if (keyVal > -1)
            {
                _oscillators[0,keyVal].Stop();
                _oscillators[1, keyVal].Stop();
                _oscillators[2, keyVal].Stop();
                _oscillators[0,keyVal] = null;
                _oscillators[1, keyVal] = null;
                _oscillators[2, keyVal] = null;
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
            _chorus = new ChorusSampleProvider(_tremolo);
            _phaser = new PhaserSampleProvider(_chorus);
            _lpf = new LowPassFilterSampleProvider(_phaser, 20000);
            _fftProvider = new FFTSampleProvider(8, (ss, cs) => Dispatch(() => UpdateRealTimeData(ss, cs)), _lpf);

            WaveType1 = SignalGeneratorType.Sin;
            Volume = 0.25;
            Attack = Attack2 = Attack3 = 0.01f;
            Decay = Decay2 = Decay3 = 0.01f;
            Sustain = Sustain2 = Sustain3 = 1.0f;
            Release = Release2 = Release3 = 0.3f;
            CutOff = 4000;
            Q = 0.7f;
            TremoloFreq = 5;
            TremoloFreqMult = 1;
            ChorusDelay = 0.0f;
            ChorusSweep = 0.0f;
            ChorusWidth = 0.0f;
            PhaserDry = 0.0f;
            PhaserWet = 0.0f;
            PhaserFeedback = 0.0f;
            PhaserFreq = 0.0f;
            PhaserWidth = 0.0f;
            PhaserSweep = 0.0f;

            // Voice Levels
            Level1 = 1.0f;
            Level2 = 0.0f;
            Level3 = 0.0f;
        }

        // Property events
        partial void Changed_Volume(double prev, double current)
        {
          _volControl.Volume = (float)current;
        }

        partial void Changed_TremoloFreq(int prev, int current)
        {
            if (_tremolo != null)
            {
                _tremolo.LfoFrequency = TremoloFreqMult * TremoloFreq; ;
                _tremolo.UpdateLowFrequencyOscillator();
            }

            Raise_TremoloFreqMult();
        }

        partial void Changed_TremoloFreqMult(int prev, int current)
        {
            if (_tremolo != null)
            {
                _tremolo.LfoFrequency = TremoloFreqMult * TremoloFreq; ;
                _tremolo.UpdateLowFrequencyOscillator();
            }

            Raise_TremoloFreq();
        }

        partial void Changed_TremoloGain(float prev, float current)
        {
            if (_tremolo != null)
            {
                _tremolo.LfoGain = TremoloGain;
                _tremolo.UpdateLowFrequencyOscillator();
            }
        }

        partial void Changed_ChorusWidth(float prev, float current)
        {
            if (_chorus != null)
            {
                _chorus.Width = ChorusWidth;
            }
        }

        partial void Changed_ChorusSweep(float prev, float current)
        {
            if (_chorus != null)
            {
                _chorus.SweepRate = ChorusSweep;
            }
        }

        partial void Changed_ChorusDelay(float prev, float current)
        {
            if (_chorus != null)
            {
                _chorus.Delay = ChorusDelay;
            }
        }

        partial void Changed_PhaserDry(float prev, float current)
        {
            if (_phaser != null)
            {
                _phaser.DryMix = PhaserDry;
            }
        }
        partial void Changed_PhaserWet(float prev, float current)
        {
            if (_phaser != null)
            {
                _phaser.WetMix = PhaserWet;
            }
        }
        partial void Changed_PhaserFeedback(float prev, float current)
        {
            if (_phaser != null)
            {
                _phaser.Feedback = PhaserFeedback;
            }
        }
        partial void Changed_PhaserFreq(float prev, float current)
        {
            if (_phaser != null)
            {
                _phaser.BottomFrequency = PhaserFreq;
            }
        }
        partial void Changed_PhaserWidth(float prev, float current)
        {
            if (_phaser != null)
            {
                _phaser.Width = PhaserWidth;
            }
        }
        partial void Changed_PhaserSweep(float prev, float current)
        {
            if (_phaser != null)
            {
                _phaser.SweepRate = PhaserSweep;
            }
        }

        partial void Changed_EnableTremolo(bool prev, bool current)
        {
          TremoloGain = current ? 0.2F : 0.0F;
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

        partial void Execute_MidiOnCommand()
        {
            MidiEnabled = true;
            ResetCanExecute();
        }
        
        partial void Execute_MidiOffCommand()
        {
            MidiEnabled = false;
            ResetCanExecute();
        }

        partial void CanExecute_MidiOnCommand(ref bool result)
        {
            result = !MidiEnabled;
        }

        partial void CanExecute_MidiOffCommand(ref bool result)
        {
            result = MidiEnabled;
        }
    }
}

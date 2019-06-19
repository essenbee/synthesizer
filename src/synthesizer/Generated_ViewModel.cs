using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;




namespace synthesizer
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        readonly Dispatcher _dispatcher;

        public event PropertyChangedEventHandler PropertyChanged;

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Volume (double)
        // --------------------------------------------------------------------
        double _Volume = default;

        void Raise_Volume ()
        {
          OnPropertyChanged ("Volume");
          OnPropertyChanged ("VolumeLabel");
        }

        public string VolumeLabel => $"{(int)(Volume * 100.0)}%";

        public double Volume
        {
            get { return _Volume; }
            set
            {
                if (_Volume == value)
                {
                    return;
                }

                var prev = _Volume;

                _Volume = value;

                Changed_Volume (prev, _Volume);

                Raise_Volume ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Volume (double prev, double current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Volume (double)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: FrequencyAmplitudes (float[])
        // --------------------------------------------------------------------
        float[] _FrequencyAmplitudes = default;

        void Raise_FrequencyAmplitudes ()
        {
          OnPropertyChanged ("FrequencyAmplitudes");
        }

        public float[] FrequencyAmplitudes
        {
            get { return _FrequencyAmplitudes; }
            set
            {
                if (_FrequencyAmplitudes == value)
                {
                    return;
                }

                var prev = _FrequencyAmplitudes;

                _FrequencyAmplitudes = value;

                Changed_FrequencyAmplitudes (prev, _FrequencyAmplitudes);

                Raise_FrequencyAmplitudes ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_FrequencyAmplitudes (float[] prev, float[] current);
        // --------------------------------------------------------------------
        // END_PROPERTY: FrequencyAmplitudes (float[])
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Waveform (float[])
        // --------------------------------------------------------------------
        float[] _Waveform = default;

        void Raise_Waveform ()
        {
          OnPropertyChanged ("Waveform");
        }

        public float[] Waveform
        {
            get { return _Waveform; }
            set
            {
                if (_Waveform == value)
                {
                    return;
                }

                var prev = _Waveform;

                _Waveform = value;

                Changed_Waveform (prev, _Waveform);

                Raise_Waveform ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Waveform (float[] prev, float[] current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Waveform (float[])
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Attack (float)
        // --------------------------------------------------------------------
        float _Attack = default;

        void Raise_Attack ()
        {
          OnPropertyChanged ("Attack");
          OnPropertyChanged ("AttackLabel");
        }

        public string AttackLabel => $"{(int)(Attack * 1000.0)} ms";

        public float Attack
        {
            get { return _Attack; }
            set
            {
                if (_Attack == value)
                {
                    return;
                }

                var prev = _Attack;

                _Attack = value;

                Changed_Attack (prev, _Attack);

                Raise_Attack ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Attack (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Attack (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Decay (float)
        // --------------------------------------------------------------------
        float _Decay = default;

        void Raise_Decay ()
        {
          OnPropertyChanged ("Decay");
          OnPropertyChanged ("DecayLabel");
        }

        public string DecayLabel => $"{(int)(Decay * 1000.0)} ms";

        public float Decay
        {
            get { return _Decay; }
            set
            {
                if (_Decay == value)
                {
                    return;
                }

                var prev = _Decay;

                _Decay = value;

                Changed_Decay (prev, _Decay);

                Raise_Decay ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Decay (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Decay (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Sustain (float)
        // --------------------------------------------------------------------
        float _Sustain = default;

        void Raise_Sustain ()
        {
          OnPropertyChanged ("Sustain");
          OnPropertyChanged ("SustainLabel");
        }

        public string SustainLabel => $"{(int)(Sustain * 100.0)}%";

        public float Sustain
        {
            get { return _Sustain; }
            set
            {
                if (_Sustain == value)
                {
                    return;
                }

                var prev = _Sustain;

                _Sustain = value;

                Changed_Sustain (prev, _Sustain);

                Raise_Sustain ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Sustain (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Sustain (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Release (float)
        // --------------------------------------------------------------------
        float _Release = default;

        void Raise_Release ()
        {
          OnPropertyChanged ("Release");
          OnPropertyChanged ("ReleaseLabel");
        }

        public string ReleaseLabel => $"{(int)(Release * 1000.0)} ms";

        public float Release
        {
            get { return _Release; }
            set
            {
                if (_Release == value)
                {
                    return;
                }

                var prev = _Release;

                _Release = value;

                Changed_Release (prev, _Release);

                Raise_Release ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Release (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Release (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: CutOff (int)
        // --------------------------------------------------------------------
        int _CutOff = default;

        void Raise_CutOff ()
        {
          OnPropertyChanged ("CutOff");
          OnPropertyChanged ("CutOffLabel");
        }

        public string CutOffLabel => $"{CutOff} Hz";

        public int CutOff
        {
            get { return _CutOff; }
            set
            {
                if (_CutOff == value)
                {
                    return;
                }

                var prev = _CutOff;

                _CutOff = value;

                Changed_CutOff (prev, _CutOff);

                Raise_CutOff ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_CutOff (int prev, int current);
        // --------------------------------------------------------------------
        // END_PROPERTY: CutOff (int)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Q (float)
        // --------------------------------------------------------------------
        float _Q = default;

        void Raise_Q ()
        {
          OnPropertyChanged ("Q");
          OnPropertyChanged ("QLabel");
        }

        public string QLabel => $"{((int)(Q * 100.0f))/ 100.0f}";

        public float Q
        {
            get { return _Q; }
            set
            {
                if (_Q == value)
                {
                    return;
                }

                var prev = _Q;

                _Q = value;

                Changed_Q (prev, _Q);

                Raise_Q ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Q (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Q (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: TremoloFreq (int)
        // --------------------------------------------------------------------
        int _TremoloFreq = default;

        void Raise_TremoloFreq ()
        {
          OnPropertyChanged ("TremoloFreq");
          OnPropertyChanged ("TremoloFreqLabel");
        }

        public string TremoloFreqLabel => $"{TremoloFreqMult * TremoloFreq} Hz";

        public int TremoloFreq
        {
            get { return _TremoloFreq; }
            set
            {
                if (_TremoloFreq == value)
                {
                    return;
                }

                var prev = _TremoloFreq;

                _TremoloFreq = value;

                Changed_TremoloFreq (prev, _TremoloFreq);

                Raise_TremoloFreq ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_TremoloFreq (int prev, int current);
        // --------------------------------------------------------------------
        // END_PROPERTY: TremoloFreq (int)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: TremoloGain (float)
        // --------------------------------------------------------------------
        float _TremoloGain = default;

        void Raise_TremoloGain ()
        {
          OnPropertyChanged ("TremoloGain");
          OnPropertyChanged ("TremoloGainLabel");
        }

        public string TremoloGainLabel => $"{Math.Max(0, TremoloGain) * 100.0f}%";

        public float TremoloGain
        {
            get { return _TremoloGain; }
            set
            {
                if (_TremoloGain == value)
                {
                    return;
                }

                var prev = _TremoloGain;

                _TremoloGain = value;

                Changed_TremoloGain (prev, _TremoloGain);

                Raise_TremoloGain ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_TremoloGain (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: TremoloGain (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: TremoloFreqMult (int)
        // --------------------------------------------------------------------
        int _TremoloFreqMult = default;

        void Raise_TremoloFreqMult ()
        {
          OnPropertyChanged ("TremoloFreqMult");
          OnPropertyChanged ("TremoloFreqMultLabel");
        }

        public string TremoloFreqMultLabel => $"x{TremoloFreqMult}";

        public int TremoloFreqMult
        {
            get { return _TremoloFreqMult; }
            set
            {
                if (_TremoloFreqMult == value)
                {
                    return;
                }

                var prev = _TremoloFreqMult;

                _TremoloFreqMult = value;

                Changed_TremoloFreqMult (prev, _TremoloFreqMult);

                Raise_TremoloFreqMult ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_TremoloFreqMult (int prev, int current);
        // --------------------------------------------------------------------
        // END_PROPERTY: TremoloFreqMult (int)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: ChorusWidth (float)
        // --------------------------------------------------------------------
        float _ChorusWidth = default;

        void Raise_ChorusWidth ()
        {
          OnPropertyChanged ("ChorusWidth");
          OnPropertyChanged ("ChorusWidthLabel");
        }

        public string ChorusWidthLabel => $"{ChorusWidth}";

        public float ChorusWidth
        {
            get { return _ChorusWidth; }
            set
            {
                if (_ChorusWidth == value)
                {
                    return;
                }

                var prev = _ChorusWidth;

                _ChorusWidth = value;

                Changed_ChorusWidth (prev, _ChorusWidth);

                Raise_ChorusWidth ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_ChorusWidth (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: ChorusWidth (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: ChorusSweep (float)
        // --------------------------------------------------------------------
        float _ChorusSweep = default;

        void Raise_ChorusSweep ()
        {
          OnPropertyChanged ("ChorusSweep");
          OnPropertyChanged ("ChorusSweepLabel");
        }

        public string ChorusSweepLabel => $"{ChorusSweep}";

        public float ChorusSweep
        {
            get { return _ChorusSweep; }
            set
            {
                if (_ChorusSweep == value)
                {
                    return;
                }

                var prev = _ChorusSweep;

                _ChorusSweep = value;

                Changed_ChorusSweep (prev, _ChorusSweep);

                Raise_ChorusSweep ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_ChorusSweep (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: ChorusSweep (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: ChorusDelay (float)
        // --------------------------------------------------------------------
        float _ChorusDelay = default;

        void Raise_ChorusDelay ()
        {
          OnPropertyChanged ("ChorusDelay");
          OnPropertyChanged ("ChorusDelayLabel");
        }

        public string ChorusDelayLabel => $"{ChorusDelay}";

        public float ChorusDelay
        {
            get { return _ChorusDelay; }
            set
            {
                if (_ChorusDelay == value)
                {
                    return;
                }

                var prev = _ChorusDelay;

                _ChorusDelay = value;

                Changed_ChorusDelay (prev, _ChorusDelay);

                Raise_ChorusDelay ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_ChorusDelay (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: ChorusDelay (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: PhaserDry (float)
        // --------------------------------------------------------------------
        float _PhaserDry = default;

        void Raise_PhaserDry ()
        {
          OnPropertyChanged ("PhaserDry");
          OnPropertyChanged ("PhaserDryLabel");
        }

        public string PhaserDryLabel => $"{PhaserDry}";

        public float PhaserDry
        {
            get { return _PhaserDry; }
            set
            {
                if (_PhaserDry == value)
                {
                    return;
                }

                var prev = _PhaserDry;

                _PhaserDry = value;

                Changed_PhaserDry (prev, _PhaserDry);

                Raise_PhaserDry ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_PhaserDry (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: PhaserDry (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: PhaserWet (float)
        // --------------------------------------------------------------------
        float _PhaserWet = default;

        void Raise_PhaserWet ()
        {
          OnPropertyChanged ("PhaserWet");
          OnPropertyChanged ("PhaserWetLabel");
        }

        public string PhaserWetLabel => $"{PhaserWet}";

        public float PhaserWet
        {
            get { return _PhaserWet; }
            set
            {
                if (_PhaserWet == value)
                {
                    return;
                }

                var prev = _PhaserWet;

                _PhaserWet = value;

                Changed_PhaserWet (prev, _PhaserWet);

                Raise_PhaserWet ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_PhaserWet (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: PhaserWet (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: PhaserFeedback (float)
        // --------------------------------------------------------------------
        float _PhaserFeedback = default;

        void Raise_PhaserFeedback ()
        {
          OnPropertyChanged ("PhaserFeedback");
          OnPropertyChanged ("PhaserFeedbackLabel");
        }

        public string PhaserFeedbackLabel => $"{PhaserFeedback}";

        public float PhaserFeedback
        {
            get { return _PhaserFeedback; }
            set
            {
                if (_PhaserFeedback == value)
                {
                    return;
                }

                var prev = _PhaserFeedback;

                _PhaserFeedback = value;

                Changed_PhaserFeedback (prev, _PhaserFeedback);

                Raise_PhaserFeedback ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_PhaserFeedback (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: PhaserFeedback (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: PhaserFreq (float)
        // --------------------------------------------------------------------
        float _PhaserFreq = default;

        void Raise_PhaserFreq ()
        {
          OnPropertyChanged ("PhaserFreq");
          OnPropertyChanged ("PhaserFreqLabel");
        }

        public string PhaserFreqLabel => $"{PhaserFreq}";

        public float PhaserFreq
        {
            get { return _PhaserFreq; }
            set
            {
                if (_PhaserFreq == value)
                {
                    return;
                }

                var prev = _PhaserFreq;

                _PhaserFreq = value;

                Changed_PhaserFreq (prev, _PhaserFreq);

                Raise_PhaserFreq ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_PhaserFreq (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: PhaserFreq (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: PhaserWidth (float)
        // --------------------------------------------------------------------
        float _PhaserWidth = default;

        void Raise_PhaserWidth ()
        {
          OnPropertyChanged ("PhaserWidth");
          OnPropertyChanged ("PhaserWidthLabel");
        }

        public string PhaserWidthLabel => $"{PhaserWidth}";

        public float PhaserWidth
        {
            get { return _PhaserWidth; }
            set
            {
                if (_PhaserWidth == value)
                {
                    return;
                }

                var prev = _PhaserWidth;

                _PhaserWidth = value;

                Changed_PhaserWidth (prev, _PhaserWidth);

                Raise_PhaserWidth ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_PhaserWidth (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: PhaserWidth (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: PhaserSweep (float)
        // --------------------------------------------------------------------
        float _PhaserSweep = default;

        void Raise_PhaserSweep ()
        {
          OnPropertyChanged ("PhaserSweep");
          OnPropertyChanged ("PhaserSweepLabel");
        }

        public string PhaserSweepLabel => $"{PhaserSweep}";

        public float PhaserSweep
        {
            get { return _PhaserSweep; }
            set
            {
                if (_PhaserSweep == value)
                {
                    return;
                }

                var prev = _PhaserSweep;

                _PhaserSweep = value;

                Changed_PhaserSweep (prev, _PhaserSweep);

                Raise_PhaserSweep ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_PhaserSweep (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: PhaserSweep (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Level1 (float)
        // --------------------------------------------------------------------
        float _Level1 = default;

        void Raise_Level1 ()
        {
          OnPropertyChanged ("Level1");
          OnPropertyChanged ("Level1Label");
        }

        public string Level1Label => $"{(int)(Level1 * 100.0)}%";

        public float Level1
        {
            get { return _Level1; }
            set
            {
                if (_Level1 == value)
                {
                    return;
                }

                var prev = _Level1;

                _Level1 = value;

                Changed_Level1 (prev, _Level1);

                Raise_Level1 ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Level1 (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Level1 (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Level2 (float)
        // --------------------------------------------------------------------
        float _Level2 = default;

        void Raise_Level2 ()
        {
          OnPropertyChanged ("Level2");
          OnPropertyChanged ("Level2Label");
        }

        public string Level2Label => $"{(int)(Level2 * 100.0)}%";

        public float Level2
        {
            get { return _Level2; }
            set
            {
                if (_Level2 == value)
                {
                    return;
                }

                var prev = _Level2;

                _Level2 = value;

                Changed_Level2 (prev, _Level2);

                Raise_Level2 ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Level2 (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Level2 (float)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Level3 (float)
        // --------------------------------------------------------------------
        float _Level3 = default;

        void Raise_Level3 ()
        {
          OnPropertyChanged ("Level3");
          OnPropertyChanged ("Level3Label");
        }

        public string Level3Label => $"{(int)(Level3 * 100.0)}%";

        public float Level3
        {
            get { return _Level3; }
            set
            {
                if (_Level3 == value)
                {
                    return;
                }

                var prev = _Level3;

                _Level3 = value;

                Changed_Level3 (prev, _Level3);

                Raise_Level3 ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Level3 (float prev, float current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Level3 (float)
        // --------------------------------------------------------------------


        // --------------------------------------------------------------------
        // BEGIN_COMMAND: OnCommand
        // --------------------------------------------------------------------
        readonly UserCommand _OnCommand;

        bool CanExecuteOnCommand ()
        {
          bool result = false;
          CanExecute_OnCommand (ref result);

          return result;
        }

        void ExecuteOnCommand ()
        {
          Execute_OnCommand ();
        }

        public ICommand OnCommand { get { return _OnCommand;} }
        // --------------------------------------------------------------------
        partial void CanExecute_OnCommand (ref bool result);
        partial void Execute_OnCommand ();
        // --------------------------------------------------------------------
        // END_COMMAND: OnCommand
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_COMMAND: OffCommand
        // --------------------------------------------------------------------
        readonly UserCommand _OffCommand;

        bool CanExecuteOffCommand ()
        {
          bool result = false;
          CanExecute_OffCommand (ref result);

          return result;
        }

        void ExecuteOffCommand ()
        {
          Execute_OffCommand ();
        }

        public ICommand OffCommand { get { return _OffCommand;} }
        // --------------------------------------------------------------------
        partial void CanExecute_OffCommand (ref bool result);
        partial void Execute_OffCommand ();
        // --------------------------------------------------------------------
        // END_COMMAND: OffCommand
        // --------------------------------------------------------------------


        partial void Constructed ();

        public MainWindowViewModel (Dispatcher dispatcher)
        {
          _dispatcher = dispatcher;
          _OnCommand = new UserCommand (CanExecuteOnCommand, ExecuteOnCommand);
          _OffCommand = new UserCommand (CanExecuteOffCommand, ExecuteOffCommand);

          Constructed ();
        }

        void ResetCanExecute ()
        {
          _OnCommand.RefreshCanExecute ();
          _OffCommand.RefreshCanExecute ();
        }

        void Dispatch(Action action)
        {
          _dispatcher.BeginInvoke(action);
        }

        protected virtual void OnPropertyChanged (string propertyChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }
    }
}


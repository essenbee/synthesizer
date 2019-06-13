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
        }

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
        // BEGIN_PROPERTY: VolumeLabel (string)
        // --------------------------------------------------------------------
        string _VolumeLabel = default;

        void Raise_VolumeLabel ()
        {
          OnPropertyChanged ("VolumeLabel");
        }

        public string VolumeLabel
        {
            get { return _VolumeLabel; }
            set
            {
                if (_VolumeLabel == value)
                {
                    return;
                }

                var prev = _VolumeLabel;

                _VolumeLabel = value;

                Changed_VolumeLabel (prev, _VolumeLabel);

                Raise_VolumeLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_VolumeLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: VolumeLabel (string)
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
        }

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
        // BEGIN_PROPERTY: AttackLabel (string)
        // --------------------------------------------------------------------
        string _AttackLabel = default;

        void Raise_AttackLabel ()
        {
          OnPropertyChanged ("AttackLabel");
        }

        public string AttackLabel
        {
            get { return _AttackLabel; }
            set
            {
                if (_AttackLabel == value)
                {
                    return;
                }

                var prev = _AttackLabel;

                _AttackLabel = value;

                Changed_AttackLabel (prev, _AttackLabel);

                Raise_AttackLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_AttackLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: AttackLabel (string)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Decay (float)
        // --------------------------------------------------------------------
        float _Decay = default;

        void Raise_Decay ()
        {
          OnPropertyChanged ("Decay");
        }

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
        // BEGIN_PROPERTY: DecayLabel (string)
        // --------------------------------------------------------------------
        string _DecayLabel = default;

        void Raise_DecayLabel ()
        {
          OnPropertyChanged ("DecayLabel");
        }

        public string DecayLabel
        {
            get { return _DecayLabel; }
            set
            {
                if (_DecayLabel == value)
                {
                    return;
                }

                var prev = _DecayLabel;

                _DecayLabel = value;

                Changed_DecayLabel (prev, _DecayLabel);

                Raise_DecayLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_DecayLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: DecayLabel (string)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Sustain (float)
        // --------------------------------------------------------------------
        float _Sustain = default;

        void Raise_Sustain ()
        {
          OnPropertyChanged ("Sustain");
        }

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
        // BEGIN_PROPERTY: SustainLabel (string)
        // --------------------------------------------------------------------
        string _SustainLabel = default;

        void Raise_SustainLabel ()
        {
          OnPropertyChanged ("SustainLabel");
        }

        public string SustainLabel
        {
            get { return _SustainLabel; }
            set
            {
                if (_SustainLabel == value)
                {
                    return;
                }

                var prev = _SustainLabel;

                _SustainLabel = value;

                Changed_SustainLabel (prev, _SustainLabel);

                Raise_SustainLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_SustainLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: SustainLabel (string)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Release (float)
        // --------------------------------------------------------------------
        float _Release = default;

        void Raise_Release ()
        {
          OnPropertyChanged ("Release");
        }

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
        // BEGIN_PROPERTY: ReleaseLabel (string)
        // --------------------------------------------------------------------
        string _ReleaseLabel = default;

        void Raise_ReleaseLabel ()
        {
          OnPropertyChanged ("ReleaseLabel");
        }

        public string ReleaseLabel
        {
            get { return _ReleaseLabel; }
            set
            {
                if (_ReleaseLabel == value)
                {
                    return;
                }

                var prev = _ReleaseLabel;

                _ReleaseLabel = value;

                Changed_ReleaseLabel (prev, _ReleaseLabel);

                Raise_ReleaseLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_ReleaseLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: ReleaseLabel (string)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: CutOff (int)
        // --------------------------------------------------------------------
        int _CutOff = default;

        void Raise_CutOff ()
        {
          OnPropertyChanged ("CutOff");
        }

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
        // BEGIN_PROPERTY: CutOffLabel (string)
        // --------------------------------------------------------------------
        string _CutOffLabel = default;

        void Raise_CutOffLabel ()
        {
          OnPropertyChanged ("CutOffLabel");
        }

        public string CutOffLabel
        {
            get { return _CutOffLabel; }
            set
            {
                if (_CutOffLabel == value)
                {
                    return;
                }

                var prev = _CutOffLabel;

                _CutOffLabel = value;

                Changed_CutOffLabel (prev, _CutOffLabel);

                Raise_CutOffLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_CutOffLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: CutOffLabel (string)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: Q (float)
        // --------------------------------------------------------------------
        float _Q = default;

        void Raise_Q ()
        {
          OnPropertyChanged ("Q");
        }

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
        // BEGIN_PROPERTY: QLabel (string)
        // --------------------------------------------------------------------
        string _QLabel = default;

        void Raise_QLabel ()
        {
          OnPropertyChanged ("QLabel");
        }

        public string QLabel
        {
            get { return _QLabel; }
            set
            {
                if (_QLabel == value)
                {
                    return;
                }

                var prev = _QLabel;

                _QLabel = value;

                Changed_QLabel (prev, _QLabel);

                Raise_QLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_QLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: QLabel (string)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: TremoloFreq (int)
        // --------------------------------------------------------------------
        int _TremoloFreq = default;

        void Raise_TremoloFreq ()
        {
          OnPropertyChanged ("TremoloFreq");
        }

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
        // BEGIN_PROPERTY: TremoloFreqLabel (string)
        // --------------------------------------------------------------------
        string _TremoloFreqLabel = default;

        void Raise_TremoloFreqLabel ()
        {
          OnPropertyChanged ("TremoloFreqLabel");
        }

        public string TremoloFreqLabel
        {
            get { return _TremoloFreqLabel; }
            set
            {
                if (_TremoloFreqLabel == value)
                {
                    return;
                }

                var prev = _TremoloFreqLabel;

                _TremoloFreqLabel = value;

                Changed_TremoloFreqLabel (prev, _TremoloFreqLabel);

                Raise_TremoloFreqLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_TremoloFreqLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: TremoloFreqLabel (string)
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // BEGIN_PROPERTY: TremoloGain (float)
        // --------------------------------------------------------------------
        float _TremoloGain = default;

        void Raise_TremoloGain ()
        {
          OnPropertyChanged ("TremoloGain");
        }

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
        // BEGIN_PROPERTY: TremoloGainLabel (string)
        // --------------------------------------------------------------------
        string _TremoloGainLabel = default;

        void Raise_TremoloGainLabel ()
        {
          OnPropertyChanged ("TremoloGainLabel");
        }

        public string TremoloGainLabel
        {
            get { return _TremoloGainLabel; }
            set
            {
                if (_TremoloGainLabel == value)
                {
                    return;
                }

                var prev = _TremoloGainLabel;

                _TremoloGainLabel = value;

                Changed_TremoloGainLabel (prev, _TremoloGainLabel);

                Raise_TremoloGainLabel ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_TremoloGainLabel (string prev, string current);
        // --------------------------------------------------------------------
        // END_PROPERTY: TremoloGainLabel (string)
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


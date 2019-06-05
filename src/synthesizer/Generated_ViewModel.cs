using System.ComponentModel;
using System.Windows.Input;




namespace synthesizer
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
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
        // BEGIN_PROPERTY: Frequency (double)
        // --------------------------------------------------------------------
        double _Frequency = default;

        void Raise_Frequency ()
        {
          OnPropertyChanged ("Frequency");
        }

        public double Frequency
        {
            get { return _Frequency; }
            set
            {
                if (_Frequency == value)
                {
                    return;
                }

                var prev = _Frequency;

                _Frequency = value;

                Changed_Frequency (prev, _Frequency);

                Raise_Frequency ();
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Frequency (double prev, double current);
        // --------------------------------------------------------------------
        // END_PROPERTY: Frequency (double)
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

        public MainWindowViewModel ()
        {
          _OnCommand = new UserCommand (CanExecuteOnCommand, ExecuteOnCommand);
          _OffCommand = new UserCommand (CanExecuteOffCommand, ExecuteOffCommand);

          Constructed ();
        }

        void ResetCanExecute ()
        {
          _OnCommand.RefreshCanExecute ();
          _OffCommand.RefreshCanExecute ();
        }

        protected virtual void OnPropertyChanged (string propertyChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }
    }
}


using NAudio.Midi;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace synthesizer
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(Dispatcher);
            DataContext = _viewModel;
            Closing += ((obj, e) => _viewModel.OffCommand.Execute(null));

            for (var device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                midiInDevices.Items.Add(MidiIn.DeviceInfo(device).ProductName);
            }

            if (midiInDevices.Items.Count > 0)
            {
                midiInDevices.SelectedIndex = 0;
                _viewModel.SelectedMidiDevice = midiInDevices.SelectedIndex;
            }
            else
            {
                midiInDevices.Visibility = Visibility.Hidden;
                midiInLabel.Visibility = Visibility.Hidden;
                midiOn.Visibility = Visibility.Hidden;
                midiOff.Visibility = Visibility.Hidden;
                _viewModel.MidiEnabled = false;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            _viewModel.KeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            _viewModel.KeyUp(e);
        }

        private void MidiInDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = sender as ComboBox;
            if (_viewModel != null && _viewModel.MidiEnabled)
            {
                _viewModel.SelectedMidiDevice = selection.SelectedIndex;
            }
        }
    }
}

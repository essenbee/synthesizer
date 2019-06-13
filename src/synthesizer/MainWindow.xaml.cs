using NAudio.Wave.SampleProviders;
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
        }

        private void SetOctave(object sender, RoutedEventArgs e)
        {
            var octaveSel = sender as RadioButton;

            if (_viewModel != null)
            {
                switch (octaveSel.Name)
                {
                    case "A2":
                        _viewModel.BaseFrequency = 110.0;
                        break;
                    case "A3":
                        _viewModel.BaseFrequency = 220.0;
                        break;
                    case "A4":
                        _viewModel.BaseFrequency = 440.0;
                        break;
                }
            }
        }

        private void SetWaveform(object sender, RoutedEventArgs e)
        {
            var selector = sender as RadioButton;

            if (_viewModel != null)
            {
                switch (selector.Name)
                {
                    case "Sine":
                        _viewModel.WaveType = SignalGeneratorType.Sin;
                        break;
                    case "SawTooth":
                        _viewModel.WaveType = SignalGeneratorType.SawTooth;
                        break;
                    case "Square":
                        _viewModel.WaveType = SignalGeneratorType.Square;
                        break;
                    case "Triangle":
                        _viewModel.WaveType = SignalGeneratorType.Triangle;
                        break;
                    case "White":
                        _viewModel.WaveType = SignalGeneratorType.White;
                        break;
                }
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

        private void LowPassFilter_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableLpf = true;
            }
        }

        private void LowPassFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableLpf = false;
            }
        }

        private void SubOscillator_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableSubOsc = true;
            }
        }

        private void SubOscillator_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableSubOsc = false;
            }
        }

        private void Vibrato_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableVibrato = true;
            }
        }

        private void Vibrato_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableVibrato = false;
            }
        }
         private void Tremolo_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.TremoloGain = 0.2f;
            }
        }

        private void Tremolo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.TremoloGain = 0.0f;
            }
        }
    }
}

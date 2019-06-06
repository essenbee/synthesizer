using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace synthesizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
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
    }
}

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

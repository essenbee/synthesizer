using System;
using System.Windows.Input;

namespace synthesizer
{
    public class UserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action _execute;

        public UserCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}

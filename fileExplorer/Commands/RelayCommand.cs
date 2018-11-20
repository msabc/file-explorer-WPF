using System;
using System.Windows.Input;

namespace FileExplorer.Commands
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        private Action action;

        public RelayCommand(Action akc)
        {
            action = akc;
        }

        public bool CanExecute(object parameter = null) => true;
        
        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}

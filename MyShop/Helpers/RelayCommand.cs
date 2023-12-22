using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private Action execute;
    private Func<bool> canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object parameter)
    {
        return canExecute == null || canExecute();
    }

    public void Execute(object parameter)
    {
        execute();
    }
}

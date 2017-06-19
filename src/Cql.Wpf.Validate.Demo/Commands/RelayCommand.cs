using System;
using System.Windows.Input;

namespace Cql.Wpf.Validate.Demo.Commands
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Func<bool> _canExecute;
		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action<object> execute)
		{
			_execute = execute;
			_canExecute = () => true;
		}

		public RelayCommand(Action<object> execute, Func<bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute();
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		protected virtual void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Cql.Wpf.Validate.Demo.Commands;
using Cql.Wpf.Validate.Demo.Services;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.Demo.ViewModels
{
	public class LoginViewModel : INotifyPropertyChanged
	{
		private readonly MembershipService _membershipService;
		public event EventHandler LoginSuccess;
		public ICommand LoginCommand { get; set; }

		public LoginViewModel()
		{
			_membershipService = new MembershipService();
			LoginCommand = new RelayCommand(x => LoginAttempt());
		}

		private void LoginAttempt()
		{
			if (ValidationContext.Validate(ViewConfig, this).IsValid)
			{
				LoginSuccess?.Invoke(this, EventArgs.Empty);
			}
		}

		private void AddCustomValidation()
		{
			ViewConfig?.AddCustomValidator(
				fieldName: "Password",
				validate:  o => _membershipService.Login(Username, Password),
				messageKey: "InvalidUsernamePassword");
		}

		#region ViewConfig
		private ViewConfigViewModel _viewConfig;
		public ViewConfigViewModel ViewConfig
		{
			get { return _viewConfig; }
			set
			{
				if (_viewConfig != value)
				{
					_viewConfig = value;
					OnPropertyChanged();

					if (_viewConfig != null)
					{
						AddCustomValidation();
					}
				}
			}
		}

		#endregion

		#region Username
		private string _username;
		public string Username
		{
			get { return _username; }
			set
			{
				if (_username != value)
				{
					_username = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region Password
		private string _password;
		public string Password
		{
			get { return _password; }
			set
			{
				if (_password != value)
				{
					_password = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region INotifyPropertyChanged Implementation
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}

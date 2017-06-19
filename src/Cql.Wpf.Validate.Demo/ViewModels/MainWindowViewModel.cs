using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Cql.Wpf.Validate.Demo.Commands;
using Cql.Wpf.Validate.Demo.Services;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.Demo.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public ICommand AddEmployeeCommand { get; set; }
		public ICommand AddCompanyCommand { get; set; }

		public MainWindowViewModel()
		{
			AddEmployeeCommand = new RelayCommand(a =>
			{
				SetActiveItem(View.Employee);
				ShowList = (a as string ?? "") == "List";
				ShowFlyin = !ShowList;
			});
			AddCompanyCommand = new RelayCommand(a => SetActiveItem(View.Company));

			ValidationContext.Global.ValidationFailCommand = new RelayCommand(p => AddValidationMessage(p as ValidationMessage));
			ValidationContext.Global.ValidationStartingCommand = new RelayCommand(p => ValidationMessages.Clear());
			ShowList = true;
		}

		public void Init()
		{
			SetActiveItem(View.Login);
		}

		private async void ShowLogin()
		{
			var loginConfig = await ValidationContext.LoadViewConfigAsync("Login");

			LoginView = new LoginViewModel
			{
				ViewConfig = new ViewConfigViewModel(loginConfig)
			};

			LoginView.LoginSuccess += (s, e) =>
			{
				SetActiveItem(View.Empty);
			};
		}

		private void AddValidationMessage(ValidationMessage msg)
		{
			if (msg != null)
			{
				if (ShowList)
				{
					ValidationMessages.Add(new ViewModels.ValidationMessageViewModel { Message = msg.Message });
					return;
				}

				var existingValidation = ValidationMessages.FirstOrDefault();
				if (existingValidation == null)
				{
					ValidationMessages.Add(new ValidationMessageViewModel { Message = msg.Message, Remove = vm => ValidationMessages.Remove(vm) });
					return;
				}
				existingValidation.Message = existingValidation.Message + Environment.NewLine + msg.Message;
			}
		}

		private async void ShowAddEmployee()
		{
			var employeeConfig = await ValidationContext.LoadViewConfigAsync("Employee");
			var addressConfig = await ValidationContext.LoadViewConfigAsync("Address");

			EmployeeView = new EmployeeViewModel
			{
				Address = new AddressViewModel { ViewConfig = new ViewConfigViewModel(addressConfig) },
				ViewConfig = new ViewConfigViewModel(employeeConfig),
				Companies = new BindingList<string>(CompanyService.GetCompanies())
			};

			EmployeeView.EmployeeComplete += (s, e) =>
			{
				SetActiveItem(View.Empty);
			};
		}

		private async void ShowAddCompany()
		{
			var companyConfig = await ValidationContext.LoadViewConfigAsync("Company");

			CompanyView = new CompanyViewModel
			{
				ViewConfig = new ViewConfigViewModel(companyConfig)
			};

			CompanyView.ViewConfig.AddCustomValidator(
				fieldName: "CompanyName",
				validate: n => !CompanyService.CompanyExists((n as string ?? "").ToString()),
				messageKey: "CompanyExists");

			CompanyView.CompanyComplete += (s, e) =>
			{
				SetActiveItem(View.Empty);
			};
		}

		void SetActiveItem(View view)
		{
			LoginView = null;
			EmployeeView = null;
			CompanyView = null;

			switch (view)
			{
				case View.Login:
					ShowLogin();
					break;
				case View.Company:
					ShowAddCompany();
					break;
				case View.Employee:
					ShowAddEmployee();
					break;
			}

			// ReSharper disable once ExplicitCallerInfoArgument
			OnPropertyChanged("WelcomeMessageVisible");

			ValidationMessages.Clear();
		}

		#region ValidationMessages
		private BindingList<ValidationMessageViewModel> _validationMessages = new BindingList<ValidationMessageViewModel>();
		public BindingList<ValidationMessageViewModel> ValidationMessages
		{
			get { return _validationMessages; }
			set
			{
				if (_validationMessages != value)
				{
					_validationMessages = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region LoginView
		private LoginViewModel _loginView;
		public LoginViewModel LoginView
		{
			get { return _loginView; }
			set
			{
				if (_loginView != value)
				{
					_loginView = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region EmployeeView
		private EmployeeViewModel _employeeView;
		public EmployeeViewModel EmployeeView
		{
			get { return _employeeView; }
			set
			{
				if (_employeeView != value)
				{
					_employeeView = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region CompanyView
		private CompanyViewModel _companyView;
		public CompanyViewModel CompanyView
		{
			get { return _companyView; }
			set
			{
				if (_companyView != value)
				{
					_companyView = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region WelcomeMessageVisible
		public bool WelcomeMessageVisible => EmployeeView == null && LoginView == null && CompanyView == null;
		#endregion

		#region ShowFlyin
		private bool _showFlyin;
		public bool ShowFlyin
		{
			get { return _showFlyin; }
			set
			{
				if (_showFlyin != value)
				{
					_showFlyin = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region ShowList
		private bool _showList;
		public bool ShowList
		{
			get { return _showList; }
			set
			{
				if (_showList != value)
				{
					_showList = value;
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

		private enum View
		{
			Empty,
			Login,
			Company,
			Employee,
		}
	}
}

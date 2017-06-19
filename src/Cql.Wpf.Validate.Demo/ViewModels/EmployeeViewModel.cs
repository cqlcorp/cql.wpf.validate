using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Cql.Wpf.Validate.Demo.Commands;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.Demo.ViewModels
{
	public class EmployeeViewModel : INotifyPropertyChanged
	{
		public event EventHandler EmployeeComplete;

		public ICommand SaveCommand { get; set; }
		public ICommand CancelCommand { get; set; }

		public EmployeeViewModel()
		{
			SaveCommand = new RelayCommand(a => Save());
			CancelCommand = new RelayCommand(a => Cancel());
		}

		private void Cancel()
		{
			EmployeeComplete?.Invoke(this, EventArgs.Empty);
		}

		public void Save()
		{
			var result = ValidationContext.Validate(
				new ValidationSet { Model = this, ViewConfig = ViewConfig },
				new ValidationSet { Model = Address, ViewConfig = Address.ViewConfig });

			if (result.IsValid)
			{
				MessageBox.Show("Address saved!");
				EmployeeComplete?.Invoke(this, EventArgs.Empty);
			}
		}

		#region Companies
		private BindingList<string> _companies;
		public BindingList<string> Companies
		{
			get { return _companies; }
			set
			{
				if (_companies != value)
				{
					_companies = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region SelectedCompany
		private string _selectedCompany;
		public string SelectedCompany
		{
			get { return _selectedCompany; }
			set
			{
				if (_selectedCompany != value)
				{
					_selectedCompany = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region FirstName
		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				if (_firstName != value)
				{
					_firstName = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region LastName
		private string _lastName;
		public string LastName
		{
			get { return _lastName; }
			set
			{
				if (_lastName != value)
				{
					_lastName = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region Email
		private string _email;
		public string Email
		{
			get { return _email; }
			set
			{
				if (_email != value)
				{
					_email = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region StartDate
		private DateTime? _startDate;
		public DateTime? StartDate
		{
			get { return _startDate; }
			set
			{
				if (_startDate != value)
				{
					_startDate = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region VacationDays
		private int? _vacationDays;
		public int? VacationDays
		{
			get { return _vacationDays; }
			set
			{
				if (_vacationDays != value)
				{
					_vacationDays = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region Address
		private AddressViewModel _address;
		public AddressViewModel Address
		{
			get { return _address; }
			set
			{
				if (_address != value)
				{
					_address = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

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

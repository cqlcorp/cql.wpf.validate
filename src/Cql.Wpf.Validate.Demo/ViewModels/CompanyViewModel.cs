using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Cql.Wpf.Validate.Demo.Commands;
using Cql.Wpf.Validate.Demo.Services;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.Demo.ViewModels
{
	public class CompanyViewModel : INotifyPropertyChanged
	{
		public event EventHandler CompanyComplete;

		public ICommand SaveCommand { get; set; }
		public ICommand CancelCommand { get; set; }

		public CompanyViewModel()
		{
			SaveCommand = new RelayCommand(a => Save());
			CancelCommand = new RelayCommand(a => Cancel());
		}

		private void Cancel()
		{
			CompanyComplete?.Invoke(this, EventArgs.Empty);
		}

		public void Save()
		{
			var result = ValidationContext.Validate(ViewConfig, this);

			if (result.IsValid)
			{
				CompanyService.CreateCompany(CompanyName);
				MessageBox.Show("Company saved!");
				CompanyComplete?.Invoke(this, EventArgs.Empty);
			}
		}

		#region CompanyName
		private string _companyName;
		public string CompanyName
		{
			get { return _companyName; }
			set
			{
				if (_companyName != value)
				{
					_companyName = value;
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

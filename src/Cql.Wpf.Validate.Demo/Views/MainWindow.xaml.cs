using System.Windows;
using Cql.Wpf.Validate.Demo.ViewModels;

namespace Cql.Wpf.Validate.Demo.Views
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new MainWindowViewModel();
		}

		private MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			ViewModel.Init();
			ViewModel.PropertyChanged += ViewModel_PropertyChanged;
		}

		private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(MainWindowViewModel.EmployeeView))
			{
				if (ViewModel.EmployeeView == null)
				{
					EmployeeContainer.Children.Remove(EmployeeView);
				}
				else
				{
					EmployeeView = new EmployeeView { DataContext = ViewModel.EmployeeView };
					EmployeeContainer.Children.Add(EmployeeView);
				}
			}

			if (e.PropertyName == nameof(MainWindowViewModel.CompanyView))
			{
				if (ViewModel.CompanyView == null)
				{
					CompanyContainer.Children.Remove(CompanyView);
				}
				else
				{
					CompanyView = new CompanyView { DataContext = ViewModel.CompanyView };
					CompanyContainer.Children.Add(CompanyView);
				}
			}
		}
	}
}

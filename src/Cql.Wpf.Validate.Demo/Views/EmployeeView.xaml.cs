using System.Windows.Controls;
using Cql.Wpf.Validate.Demo.ViewModels;
using Cql.Wpf.Validate.Extensions;

namespace Cql.Wpf.Validate.Demo.Views
{
	public partial class EmployeeView : UserControl
	{
		public EmployeeView()
		{
			InitializeComponent();
			Loaded += EmployeeView_Loaded;
		}

		private void EmployeeView_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			FirstName.SetKeyboardFocus();
		}

		private void StartDate_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			var employeeViewModel = DataContext as EmployeeViewModel;
			if (employeeViewModel != null)
				employeeViewModel.StartDate = StartDate.SelectedDate;
		}
	}
}

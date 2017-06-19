using System.Windows;
using System.Windows.Controls;
using Cql.Wpf.Validate.Demo.ViewModels;
using Cql.Wpf.Validate.Extensions;

namespace Cql.Wpf.Validate.Demo.Views
{
	public partial class LoginView : UserControl
	{
		public LoginView()
		{
			InitializeComponent();

			Loaded += LoginView_Loaded;
		}

		private void LoginView_Loaded(object sender, RoutedEventArgs e)
		{
			Username.SetKeyboardFocus();
		}

		private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as LoginViewModel;
			if (vm != null)
				vm.Password = Password.Password;
		}
	}
}

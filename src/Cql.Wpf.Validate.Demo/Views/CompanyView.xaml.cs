using System.Windows;
using System.Windows.Controls;
using Cql.Wpf.Validate.Extensions;

namespace Cql.Wpf.Validate.Demo.Views
{
	public partial class CompanyView : UserControl
	{
		public CompanyView()
		{
			InitializeComponent();
			Loaded += CompanyView_Loaded;
		}

		private void CompanyView_Loaded(object sender, RoutedEventArgs e)
		{
			ANameThatDoesNotMatchAField.SetKeyboardFocus();
		}
	}
}

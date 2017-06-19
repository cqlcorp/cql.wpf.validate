using System;
using Cql.Wpf.Validate.Demo.ViewModels;

namespace Cql.Wpf.Validate.Demo.Views
{
    public partial class NotificationView
    {
        public NotificationView()
        {
            InitializeComponent();
        }

        private void ExitRight_Completed(object sender, EventArgs e)
        {
	        (DataContext as ValidationMessageViewModel)?.Remove?.Invoke((ValidationMessageViewModel)DataContext);
        }
    }
}

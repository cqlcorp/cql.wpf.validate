using System;

namespace Cql.Wpf.Validate.Demo.ViewModels
{
	public class ValidationMessageViewModel
	{
		public string Message { get; set; }
		public Action<ValidationMessageViewModel> Remove { get; set; }
	}
}

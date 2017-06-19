using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate
{
	/// <summary>
	/// Wraps a model with the config used to validate it.
	/// </summary>
	public class ValidationSet
	{
		public ViewConfigViewModel ViewConfig { get; set; }
		public object Model { get; set; }
	}
}

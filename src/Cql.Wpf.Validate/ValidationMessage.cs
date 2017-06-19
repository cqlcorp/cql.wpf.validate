using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate
{
	/// <summary>
	/// Contains message to display on the UI.
	/// </summary>
    public class ValidationMessage
    {
		/// <summary>
		/// User friendly message.
		/// </summary>
        public string Message { get; set; }

		/// <summary>
		/// Field that failed validation.
		/// </summary>
        public FieldConfigViewModel Field { get; set; }
    }
}

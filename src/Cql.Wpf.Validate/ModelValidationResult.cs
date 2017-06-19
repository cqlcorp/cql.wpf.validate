using System.Collections.Generic;

namespace Cql.Wpf.Validate
{
	/// <summary>
	/// Result of validation of one or more validation sets.
	/// </summary>
	public class ModelValidationResult
	{
		/// <summary>
		/// Gets whether validation was successful.
		/// </summary>
		public bool IsValid { get; internal set; }

		/// <summary>
		/// Messages to display to the user.
		/// </summary>
		public List<ValidationMessage> Messages { get; } = new List<ValidationMessage>();

		/// <summary>
		/// Default valid instance.
		/// </summary>
		public static ModelValidationResult Valid => new ModelValidationResult { IsValid = true };
	}
}

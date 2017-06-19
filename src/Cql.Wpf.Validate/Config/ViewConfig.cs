using System.Collections.Generic;

namespace Cql.Wpf.Validate.Config
{
	/// <summary>
	/// Contains configuration for a specific set of fields that need to be validated.
	/// </summary>
	public class ViewConfig
	{
		/// <summary>
		/// Name of the config. This should be unique.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// List of fields included in the validation.
		/// </summary>
		public List<FieldConfig> Fields { get; set; } = new List<FieldConfig>();
	}
}

using System.Collections.Generic;

namespace Cql.Wpf.Validate.Config
{
	/// <summary>
	/// Represents validation configuration.
	/// </summary>
    public class ValidationConfig
	{
		/// <summary>
		/// All the view configurations.
		/// </summary>
		public List<ViewConfig> ViewConfigs { get; set; } = new List<ViewConfig>();
	}
}
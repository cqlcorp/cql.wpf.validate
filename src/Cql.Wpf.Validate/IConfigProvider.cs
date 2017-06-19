using System.Threading.Tasks;
using Cql.Wpf.Validate.Config;

namespace Cql.Wpf.Validate
{
	/// <summary>
	/// Provides validation configuration.
	/// </summary>
	public interface IConfigProvider
	{
		/// <summary>
		/// Gets a specific <see cref="ViewConfig"/>.
		/// </summary>
		Task<ViewConfig> LoadViewConfigAsync(string name);

		/// <summary>
		/// Gets the entire <see cref="ValidationConfig"/>.
		/// </summary>
		Task<ValidationConfig> LoadConfigAsync();
	}
}

using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate
{
	/// <summary>
	/// Performs validation on models (objects).
	/// </summary>
	public interface IModelValidator
	{
		/// <summary>
		/// Validate a single model.
		/// </summary>
		ModelValidationResult Validate(ViewConfigViewModel viewConfig, object viewModel);

		/// <summary>
		/// Validate one or more models as a single request.
		/// </summary>
		ModelValidationResult Validate(params ValidationSet[] validationSets);
	}
}

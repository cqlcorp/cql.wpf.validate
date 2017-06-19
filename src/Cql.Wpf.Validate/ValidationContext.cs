using System.Threading.Tasks;
using System.Windows.Input;
using Cql.Wpf.Validate.Config;
using Cql.Wpf.Validate.Text;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate
{
	/// <summary>
	/// ValidationContext provides a context within which validation occurs.
	/// </summary>
    public class ValidationContext
    {
        private static ValidationContext _globalInstance;

		/// <summary>
		/// Command that is executed for each field that fails validation. Command parameter is a <see cref="ValidationMessage"/> object.
		/// This command is useful when you want to handle validation at a global level (for example you can slide in messages from the side).
		/// </summary>
		public ICommand ValidationFailCommand { get; set; }

		/// <summary>
		/// Command that is executed when before one or more <see cref="ValidationSet"/> starts validating.
		/// This command is useful when you want to clear out previous validation results from the UI.
		/// </summary>
        public ICommand ValidationStartingCommand { get; set; }

		/// <summary>
		/// Object that performs validation of a model.
		/// Global instance uses <see cref="ModelValidator"/>.
		/// </summary>
		public IModelValidator ModelValidator { get; set; }

		/// <summary>
		/// Object that provides the view configurations.
		/// Global instance uses <see cref="JsonFileConfigProvider"/> and looks for a "config.json" file in the app directory.
		/// </summary>
		public IConfigProvider ConfigProvider { get; set; }

		/// <summary>
		/// Object that provides validation messages and possibly label text.
		/// Global value uses <see cref="ApplicationResourcesMessageProvider"/>.
		/// </summary>
		public IMessageProvider MessageProvider { get; set; }

		/// <summary>
		/// Global context singleton. If you need more granular control, you can create your own instances.
		/// </summary>
		public static ValidationContext Global => _globalInstance ?? (_globalInstance = CreateDefaultInstance());

		/// <summary>
		/// Shortcut to ValidationContext.Global.ModelValidator.Validate(ViewConfigViewModel viewConfig, object viewModel)
		/// </summary>
		public static ModelValidationResult Validate(ViewConfigViewModel viewConfig, object viewModel)
	    {
		    return Global.ModelValidator.Validate(viewConfig, viewModel);
	    }

		/// <summary>
		/// Shortcut to ValidationContext.Global.ConfigProvider.LoadViewConfigAsync(name).
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Task<ViewConfig> LoadViewConfigAsync(string name)
	    {
		    return Global.ConfigProvider.LoadViewConfigAsync(name);
	    }

		/// <summary>
		/// Shortcut to Global.ModelValidator.Validate(params ValidationSet[] validationSets)
		/// </summary>
		public static ModelValidationResult Validate(params ValidationSet[] validationSets)
	    {
		    return Global.ModelValidator.Validate(validationSets);
	    }

		private static ValidationContext CreateDefaultInstance()
	    {
		    var ctx = new ValidationContext
		    {
			    ConfigProvider = new JsonFileConfigProvider("config.json"),
			    MessageProvider = new ApplicationResourcesMessageProvider(),
		    };
		    ctx.ModelValidator = new ModelValidator(ctx);
			return ctx;
	    }
	}
}

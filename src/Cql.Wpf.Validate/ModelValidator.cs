using System.Linq;
using Cql.Wpf.Validate.Validators;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate
{
	/// <summary>
	/// Default instance of the <see cref="IModelValidator"/> interface.
	/// </summary>
	public class ModelValidator : IModelValidator
	{
		private readonly ValidationContext _context;

		public ModelValidator(ValidationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Performs validation on a model.
		/// </summary>
		/// <returns></returns>
		public virtual ModelValidationResult Validate(ViewConfigViewModel viewConfig, object viewModel)
		{
			return Validate(new ValidationSet { Model = viewModel, ViewConfig = viewConfig });
		}

		/// <summary>
		/// Performs validation on multiple models. Use this when you have to validate multiple models as one validation request when
		/// your UI is composed of multiple models.
		/// </summary>
		/// <returns></returns>
		public virtual ModelValidationResult Validate(params ValidationSet[] validationSets)
		{
			if (validationSets == null || !validationSets.Any())
				return ModelValidationResult.Valid;

			OnValidationStarting(validationSets);

			var result = ModelValidationResult.Valid;
			foreach (var set in validationSets)
			{
				var setResult = ValidateInternal(set.ViewConfig, set.Model);
				result.IsValid = result.IsValid && setResult.IsValid;
				result.Messages.AddRange(setResult.Messages);
			}
			return result;
		}

		protected virtual void OnValidationStarting(ValidationSet[] validationSets)
		{
			if (_context.ValidationStartingCommand != null)
			{
				if (_context.ValidationStartingCommand.CanExecute(validationSets))
					_context.ValidationStartingCommand.Execute(validationSets);
			}
		}

		protected virtual void OnValidationFailed(ValidationMessage msg)
		{
			if (_context.ValidationFailCommand != null)
			{
				if (_context.ValidationFailCommand.CanExecute(msg))
					_context.ValidationFailCommand.Execute(msg);
			}
		}

		protected virtual ModelValidationResult ValidateInternal(ViewConfigViewModel viewConfig, object viewModel)
		{
			if (viewConfig == null || viewModel == null || viewConfig.Fields == null || !viewConfig.Fields.Any())
				return ModelValidationResult.Valid;

			var result = ModelValidationResult.Valid;

			foreach (var field in viewConfig.Fields)
			{
				var prop = viewModel.GetType().GetProperty(field.Name) ?? viewModel.GetType().GetProperty($"Selected{field.Name}");
				if (prop != null)
				{
					var value = prop.GetValue(viewModel);
					ValidateField(field, value, result);
				}
			}

			return result;
		}

		protected virtual void ValidateField(FieldConfigViewModel field, object value, ModelValidationResult validationResult)
		{
			if (field == null || !field.IsVisible)
				return;

			field.IsValid = true;

			var validators = field.FieldConfig.CreateValidators();

			foreach (var validator in validators)
			{
				var result = validator.Validate(value);
				validationResult.IsValid = validationResult.IsValid && result.IsValid;
				if (!result.IsValid)
				{
					field.IsValid = false;

					var message = GenerateMessage(field, result);
					var msg = new ValidationMessage() { Message = message, Field = field };
					validationResult.Messages.Add(msg);
					OnValidationFailed(msg);
					return;
				}
			}
		}

		protected virtual string GenerateMessage(FieldConfigViewModel field, ValidationResult result)
		{
			if (result.IsValid) return null;

			var formatString = _context.MessageProvider.GetText(field.ValidationMessageKey) ?? _context.MessageProvider.GetText(result.MessageKey);
			var label = _context.MessageProvider.GetText(field.GetLabelKey(), field.Name);
			var parameters = result.Parameters?.Select(p => string.Format(field.ValueFormat, p)) ?? new string[0];
			return !string.IsNullOrWhiteSpace(formatString) ? string.Format(formatString, new[] { label }.Concat(parameters).ToArray()) : null;
		}
	}
}

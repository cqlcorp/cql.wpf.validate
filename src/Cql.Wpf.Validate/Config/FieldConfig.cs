using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cql.Wpf.Validate.Validators;

namespace Cql.Wpf.Validate.Config
{
	/// <summary>
	/// Validation configuration for a specific field. 
	/// </summary>
    public class FieldConfig
    {
		/// <summary>
		/// Name of the field.
		/// </summary>
        public string Name { get; set; }

	    /// <summary>
	    /// If a LabelKey is provided, the text label will be overwritten using the <see cref="IMessageProvider"/> instance in context.
	    /// This is useful if multiple languages are supported, or you need to provide different labels in different contexts to the same field.
	    /// </summary>
		public string LabelKey { get; set; }

	    /// <summary>
	    /// If false, the control's parent Visibility will be set to Visibility.Collapsed.
	    /// </summary>
		public bool IsVisible { get; set; } = true;

		/// <summary>
		/// Sets whether the field is required.
		/// </summary>
		public bool IsRequired { get; set; }

	    /// <summary>
	    /// Regex used to validate the field.
	    /// </summary>
		public string Regex { get; set; }

	    /// <summary>
	    /// Lower bound number that is acceptable.
	    /// </summary>
		public double? NumberMin { get; set; }

	    /// <summary>
	    /// Upper bound number that is acceptable.
	    /// </summary>
		public double? NumberMax { get; set; }

	    /// <summary>
	    /// Lower bound date that is acceptable.
	    /// </summary>
		public DateTime? DateMin { get; set; }

	    /// <summary>
	    /// Upper bound date that is acceptable.
	    /// </summary>
		public DateTime? DateMax { get; set; }

		/// <summary>
		/// Uses a <see cref="DynamicDate"/> to determine the lower bound date that is acceptable.
		/// </summary>
		public DynamicDate? DynamicDateMin { get; set; }

	    /// <summary>
	    /// Uses a <see cref="DynamicDate"/> to determine the upper bound date that is acceptable.
	    /// </summary>
		public DynamicDate? DynamicDateMax { get; set; }

		/// <summary>
		/// Upper bound text length that is acceptable. Also sets MaxLength on <see cref="System.Windows.Controls.TextBox"/>.
		/// </summary>
		public int? MaxLength { get; set; }

	    /// <summary>
	    /// Lower bound text length that is acceptable.
	    /// </summary>
		public int? MinLength { get; set; }

	    /// <summary>
	    /// If a ValidationMessageKey is provided, the validation message will be determined using the <see cref="IMessageProvider"/> instance in context.
	    /// This is useful if multiple languages are supported, or you need to provide different or more helpful messages than the defaults.
	    /// </summary>
		public string ValidationMessageKey { get; set; }

	    /// <summary>
	    /// If ShareLabelWith is set, a single label will be used to provide feedback for multiple fields.
	    /// For example, if you have a Name label and a FirstName and LastName field, if either fail validation, the Name label will provide feedback.
	    /// </summary>
		public string ShareLabelWith { get; set; }

	    /// <summary>
	    /// Format string to format the value with when displaying validation messages.
	    /// For example, you may want to display a date with no time, or a number with a certain number of decimal places.
	    /// </summary>
		public string ValueFormat { get; set; } = "{0:0}";

	    internal List<CustomValidator> CustomValidations = new List<CustomValidator>();

		/// <summary>
		/// Add custom validation logic to this field.
		/// </summary>
		/// <param name="validate">Function that performs validation</param>
		/// <param name="messageKey">Key that is passed to the <see cref="IMessageProvider"/> to get the validation message.</param>
        public void AddCustomValidator(Func<object, bool> validate, string messageKey)
        {
            CustomValidations.Add(new CustomValidator(validate, messageKey));
        }

	    /// <summary>
	    /// Add custom async validation logic to this field.
	    /// </summary>
	    /// <param name="validate">Function that performs validation</param>
	    /// <param name="messageKey">Key that is passed to the <see cref="IMessageProvider"/> to get the validation message.</param>
		public void AddCustomAsyncValidator(Func<object, Task<bool>> validate, string messageKey)
        {
            Func<object, bool> validateWrapper = data =>
            {
                var task = Task.Run(async () => await validate(data));
                task.Wait();
                return task.Result;
            };

            CustomValidations.Add(new CustomValidator(validateWrapper, messageKey));
        }

	    internal IEnumerable<IValidator> CreateValidators()
	    {
		    if (IsRequired)
			    yield return new RequiredValidator();
		    if (MaxLength.HasValue)
			    yield return new LengthValidator(MaxLength.Value);
		    if (MinLength.HasValue)
			    yield return new LengthValidator(MinLength.Value, validateMax: false);
		    if (!string.IsNullOrWhiteSpace(Regex))
			    yield return new RegexValidator(Regex);
		    if (NumberMin != null || NumberMax != null)
			    yield return new RangeValidator<double>(NumberMin, NumberMax);
		    if (DateMin != null || DateMax != null)
			    yield return new RangeValidator<DateTime>(DateMin, DateMax);
		    if (DynamicDateMin != null || DynamicDateMax != null)
			    yield return new DynamicDateRangeValidator(DynamicDateMin, DynamicDateMax);
		    foreach (var customValidator in CustomValidations)
			    yield return customValidator;
	    }
	}
}
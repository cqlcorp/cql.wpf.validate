using System;
using Cql.Wpf.Validate.Config;

namespace Cql.Wpf.Validate.Validators
{
	/// <summary>
	/// Performs range validation on <see cref="DynamicDate"/> values.
	/// </summary>
	public class DynamicDateRangeValidator : IValidator
	{
		private readonly RangeValidator<DateTime> _innerValidator;

		public DynamicDateRangeValidator(DynamicDate? min, DynamicDate? max)
		{
			_innerValidator = new RangeValidator<DateTime>(min.ToDateTime(), max.ToDateTime());
		}

		public ValidationResult Validate(object data)
		{
			return _innerValidator.Validate(data);
		}
	}
}

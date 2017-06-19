using System;

namespace Cql.Wpf.Validate.Validators
{
	/// <summary>
	/// Performs range validation of any <see cref="IComparable"/> type.
	/// </summary>
	/// <typeparam name="T">struct, IComparable</typeparam>
	public class RangeValidator<T> : IValidator where T : struct, IComparable
    {
        private readonly T? _max;
        private readonly T? _min;

        public RangeValidator(T? min, T? max)
        {
            _min = min;
            _max = max;
        }

        public ValidationResult Validate(object data)
        {
            var result = new ValidationResult() { MessageKey = "Validation:Range" };
            if (data == null) return result.Valid(true);

            var comparable = Convert.ChangeType(data, typeof(T)) as IComparable;
            if (comparable == null) return result.Valid(false);

            result.MessageKey = "Validation:Range:Between";
            if (_min != null && _max != null)
                return result.Valid(comparable.CompareTo(_min) >= 0 && comparable.CompareTo(_max) <= 0, _min, _max);

            result.MessageKey = "Validation:Range:GreaterThan";
            if (_min != null)
                return result.Valid(comparable.CompareTo(_min) >= 0, _min);

            result.MessageKey = "Validation:Range:LessThan";
            if (_max != null)
                return result.Valid(comparable.CompareTo(_max) <= 0, _max);

            return result.Valid(true);
        }
    }
}
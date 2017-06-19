using System.Text.RegularExpressions;

namespace Cql.Wpf.Validate.Validators
{
	/// <summary>
	/// Performs regex validation.
	/// </summary>
    public class RegexValidator : IValidator
    {
        private readonly string _pattern;

        public RegexValidator(string pattern)
        {
            _pattern = pattern;
        }

        public ValidationResult Validate(object data)
        {
            var result = new ValidationResult() { MessageKey = "Validation:Regex" };
            if (data == null) return result.Valid(true);
            return result.Valid(Regex.IsMatch(data.ToString(), _pattern));
        }
    }
}
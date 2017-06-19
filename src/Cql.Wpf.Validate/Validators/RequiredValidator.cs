namespace Cql.Wpf.Validate.Validators
{
	/// <summary>
	/// Performs required field validation. 
	/// For strings, string.IsNullOrWhiteSpace is checked, for everything else, check for null.
	/// </summary>
    public class RequiredValidator : IValidator
    {
        public ValidationResult Validate(object data)
        {
            var result = new ValidationResult() { MessageKey = "Validation:Required" };
            if (data == null)
                return result.Valid(false);
            if (data is string)
                return result.Valid(!string.IsNullOrWhiteSpace(data.ToString()));
            return result.Valid(true);
        }
    }
}
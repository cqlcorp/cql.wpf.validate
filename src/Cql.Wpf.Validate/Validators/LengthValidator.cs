namespace Cql.Wpf.Validate.Validators
{
	/// <summary>
	/// Performs text-length validation.
	/// </summary>
    public class LengthValidator : IValidator
    {
        private readonly int _length;
	    private readonly bool _validateMax;

	    public LengthValidator(int length, bool validateMax = true)
	    {
		    _length = length;
		    _validateMax = validateMax;
	    }

        public ValidationResult Validate(object data)
        {
			if(_validateMax)
				return ValidateMaxLength(data);
	        return ValidateMinLength(data);
        }

	    private ValidationResult ValidateMinLength(object data)
	    {
			var result = new ValidationResult() { MessageKey = "Validation:Length:Min" };
		    if (data == null) return result.Valid(true);
		    return result.Valid(data.ToString().Length >= _length, _length);
		}

	    private ValidationResult ValidateMaxLength(object data)
	    {
		    var result = new ValidationResult() {MessageKey = "Validation:Length:Max"};
		    if (data == null) return result.Valid(true);
		    return result.Valid(data.ToString().Length <= _length, _length);
	    }
    }
}
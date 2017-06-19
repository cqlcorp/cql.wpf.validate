using System;

namespace Cql.Wpf.Validate.Validators
{
	/// <summary>
	/// Validator that allows custom logic to perform validation.
	/// </summary>
    public class CustomValidator : IValidator
    {
        private readonly string _messageKey;
        private readonly Func<object, bool> _validate;

        public CustomValidator(Func<object, bool> validate, string messageKey)
        {
            _validate = validate;
            _messageKey = messageKey;
        }

        public ValidationResult Validate(object data)
        {
            var result = new ValidationResult() { MessageKey = _messageKey };
            return result.Valid(_validate(data));
        }
    }
}
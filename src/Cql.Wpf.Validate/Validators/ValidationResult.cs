using System.Collections.Generic;
using System.Linq;

namespace Cql.Wpf.Validate.Validators
{
	/// <summary>
	/// Result of a single field validation.
	/// </summary>
    public class ValidationResult
    {
		/// <summary>
		/// Gets whether field is valid.
		/// </summary>
        public bool IsValid { get; set; }

		/// <summary>
		/// Key of the message text. Use a <see cref="IMessageProvider"/> to get message text.
		/// </summary>
        public string MessageKey { get; set; }

		/// <summary>
		/// Values to pass as part of a message format string.
		/// For example, the range validators can pass two values, upper and lower bounds.
		/// </summary>
        public IEnumerable<object> Parameters { get; set; } = Enumerable.Empty<string>();

		/// <summary>
		/// Default valid instance.
		/// </summary>
        public ValidationResult Valid(bool valid, params object[] parameters )
        {
            IsValid = valid;
            if (parameters != null)
                Parameters = parameters;
            return this;
        }
    }
}
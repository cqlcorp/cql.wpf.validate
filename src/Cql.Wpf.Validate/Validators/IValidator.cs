namespace Cql.Wpf.Validate.Validators
{
	/// <summary>
	/// Performs validation on a single value.
	/// </summary>
	public interface IValidator
	{
		ValidationResult Validate(object data);
	}
}

namespace Cql.Wpf.Validate
{
	/// <summary>
	/// Provides user-friendly messages and possibly label values.
	/// </summary>
    public interface IMessageProvider
    {
		/// <summary>
		/// Gets text for a key.
		/// </summary>
        string GetText(string key);

		/// <summary>
		/// Gets text for a key, with a default value to use if key is not present.
		/// </summary>
        string GetText(string key, string defaultValue);
    }
}

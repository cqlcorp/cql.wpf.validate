using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Cql.Wpf.Validate.Text
{
	/// <summary>
	/// Provides validation messages using <see cref="ResourceDictionary"/>'s. The default dictionary is contained in this assembly in /Resources/default.xaml.
	/// </summary>
	public class ApplicationResourcesMessageProvider : IMessageProvider
	{
		public static readonly Dictionary<string, ResourceDictionary> ResourceDictionaries = new Dictionary<string, ResourceDictionary>();

		static ApplicationResourcesMessageProvider()
		{
			ResourceDictionaries["__default__"] = new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/Cql.Wpf.Validate;component/Resources/default.xaml", UriKind.Absolute) };
			UseResourceDictionary("__default__");
		}

		/// <summary>
		/// Sets a <see cref="ResourceDictionary"/> to use. The default is contained in this assembly in /Resources/default.xaml. If you choose to use a different dictionary, you should make sure to 
		/// include all the keys from the default dictionary, or override all validation messages via the config (not suggested).
		/// </summary>
		public static void UseResourceDictionary(string key)
		{
			if (!ResourceDictionaries.ContainsKey(key))
			{
				throw new KeyNotFoundException($"{key} does not exist in ResourceDictionaries");
			}

			var dictionaries = Application.Current.Resources.MergedDictionaries.Where(d => d.Source != null && ResourceDictionaries.Any(k => k.Value.Source == d.Source)).ToList();
			foreach (var td in dictionaries)
				Application.Current.Resources.MergedDictionaries.Remove(td);

			Application.Current.Resources.MergedDictionaries.Add(ResourceDictionaries[key]);
		}

		/// <summary>
		/// Gets text from the current <see cref="ResourceDictionary"/> for the provided key, returning null if not found.
		/// </summary>
		public string GetText(string key)
		{
			if (key == null) return null;
			return Application.Current.TryFindResource(key) as string;
		}

		/// <summary>
		/// Gets text from the current <see cref="ResourceDictionary"/> for the provided key, returning the default value if not found.
		/// </summary>
		public string GetText(string key, string defaultValue)
		{
			var val = GetText(key);
			return string.IsNullOrWhiteSpace(val) ? defaultValue : val;
		}
	}
}

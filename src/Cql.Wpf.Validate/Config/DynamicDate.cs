using System;

namespace Cql.Wpf.Validate.Config
{
	/// <summary>
	/// Provides a method to configure dates for validation that are not known at compile time.
	/// </summary>
	public enum DynamicDate
	{
		Now,
		UtcNow,
		Today,
		UtcToday,
	}

	/// <summary>
	/// Extensions on <see cref="DynamicDate"/>.
	/// </summary>
	public static class DateTypeExtensions
	{
		/// <summary>
		/// Get <see cref="DateTime"/> value for a <see cref="DynamicDate"/>.
		/// </summary>
		public static DateTime? ToDateTime(this DynamicDate? dynamicDate)
		{
			if (dynamicDate == null) return null;
			switch (dynamicDate)
			{
				case DynamicDate.Now:
					return DateTime.Now;
				case DynamicDate.Today:
					return DateTime.Today;
				case DynamicDate.UtcNow:
					return DateTime.UtcNow;
				case DynamicDate.UtcToday:
					return DateTime.UtcNow.Date;
			}

			return null;
		}
	}
}
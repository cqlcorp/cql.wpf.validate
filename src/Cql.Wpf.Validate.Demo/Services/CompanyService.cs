using System;
using System.Collections.Generic;
using System.Linq;

namespace Cql.Wpf.Validate.Demo.Services
{
	public class CompanyService
	{
		static readonly List<string> Companies = new List<string>();

		static CompanyService()
		{
			Companies.Add("CQL");
		}

		public static void CreateCompany(string name)
		{
			if (!Companies.Contains(name))
				Companies.Add(name);
		}

		public static bool CompanyExists(string name)
		{
			return Companies.Any(c => c.Equals(name, StringComparison.CurrentCultureIgnoreCase));
		}

		public static List<string> GetCompanies()
		{
			return Companies.ToList();
		}
	}
}

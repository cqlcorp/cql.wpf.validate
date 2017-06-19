namespace Cql.Wpf.Validate.Demo.Services
{
	public class MembershipService
	{
		public bool Login(string username, string password)
		{
			return username == "admin" && password == "admin";
		}
	}
}

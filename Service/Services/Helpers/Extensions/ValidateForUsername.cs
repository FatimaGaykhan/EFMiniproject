using System;
using System.Text.RegularExpressions;

namespace Service.Services.Helpers.Extensions
{
	public static class ValidateForUsername
	{
		public static bool ValidateUsername(this string username)
		{
            string pattern = @"^[a-zA-Z0-9_]{3,20}$";
            return Regex.IsMatch(username, pattern);
        }
	}
}


using System;
using System.Text.RegularExpressions;

namespace Service.Services.Helpers.Extensions
{
	public static class ValidateForPassword
	{
		public static bool ValidatePassword(this string password)
		{
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
            return Regex.IsMatch(password, pattern);
        }
		
	}
}


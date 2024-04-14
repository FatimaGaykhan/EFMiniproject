using System;
using System.Text.RegularExpressions;

namespace Service.Services.Helpers.Extensions
{
	public static class ValidateForEmail
	{
		public static bool ValidateEmail(this string email)
		{
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(email, pattern);
        }
	}
}


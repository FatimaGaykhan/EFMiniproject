using System;
using System.Text.RegularExpressions;

namespace Service.Services.Helpers.Extensions
{
	public static class ValidateForFullName
	{
		public static bool ValidateFullName(this string fullName)
		{
            string pattern = @"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$";
            return Regex.IsMatch(fullName, pattern);
        }
	}
}


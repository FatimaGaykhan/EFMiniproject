using System;
namespace Service.Services.Helpers.Extensions
{
	public static class SymbolExtension
	{
        public static bool ContainsSymbol(this string str, string symbols)
        {
            foreach (char c in symbols)
            {
                if (str.Contains(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}


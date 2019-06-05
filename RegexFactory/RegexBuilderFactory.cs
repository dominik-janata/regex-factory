using System;
using System.Text.RegularExpressions;

namespace RegexFactory
{
    public static class RegexBuilderFactory
    {
        public static RegexBuilder CreateFromPattern(string pattern)
        {
            try
            {
                new Regex(pattern);
            }
            catch
            {
                throw new InvalidOperationException("Invalid pattern provided.");
            }

            return new RegexBuilder { Pattern = pattern };
        }

        public static RegexBuilder CreateFromRegex(Regex regex)
        {
            return CreateFromPattern(GetRegexPattern(regex));
        }

        private static string GetRegexPattern(Regex regex)
        {
            return regex.ToString();
        }
    }
}

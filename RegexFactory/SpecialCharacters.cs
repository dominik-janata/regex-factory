using System.Collections.Generic;

namespace RegexFactory
{
    public static class SpecialCharacters
    {
        public static readonly HashSet<char> Set = new HashSet<char> { '{', '}', '[', ']', '.', '\\', '?', '+', '*' };
    }
}

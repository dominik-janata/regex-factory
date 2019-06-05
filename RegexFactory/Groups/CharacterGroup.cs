using System;

namespace RegexFactory.Groups
{
    public class CharacterGroup : IGroup
    {
        public static readonly CharacterGroup Any = new CharacterGroup(".");
        private string _pattern;

        public string Pattern => this._pattern;

        private CharacterGroup(string pattern)
        {
            this._pattern = pattern;
        }

        public CharacterGroup(char character)
        {
            this._pattern = Escape(character);
        }

        private static string Escape(char character)
        {
            return SpecialCharacters.Set.Contains(character)
                ? "\\" + character
                : character.ToString();
        }

        public CharacterGroup RepeatOnePlusTimes()
        {
            this._pattern += '+';
            return this;
        }

        public CharacterGroup RepeatZeroPlusTimes()
        {
            this._pattern += '*';
            return this;
        }

        public static CharacterGroup CharRange(char from, char to)
        {
            return new CharacterGroup($"[{from}-{to}]");
        }

        public CharacterGroup RepeatMToNTimes(int m, int n)
        {
            if (m < 0)
            {
                throw new ArgumentException("M must be a non-negative integer.");
            }

            if (n < 0)
            {
                throw new ArgumentException("N must be a non-negative integer.");
            }

            if (n < m)
            {
                throw new ArgumentException("N must be greater than or equal to M.");
            }

            this._pattern += $"{{{m}, {n}}}";
            return this;
        }

        public CharacterGroup MakeOptional()
        {
            this._pattern += '?';
            return this;
        }
    }
}
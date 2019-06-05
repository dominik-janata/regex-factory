using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RegexFactory.Groups;

namespace RegexFactory
{
    public class RegexBuilder
    {
        private readonly Stack<string> _unterminatedGroups = new Stack<string>();
        private string _pattern = null;
        private readonly List<IGroup> _groups = new List<IGroup>();
        private readonly RegexOptions? _options;

        public bool IsBuilt => _pattern != null;
        public string Pattern
        {
            get
            {
                if (!this.IsBuilt)
                {
                    this.BuildPattern();
                }

                return this._pattern;
            }
        }

        public Regex Regex => this._options == null ? new Regex(this.Pattern) : new Regex(this.Pattern, this._options.Value);

        public RegexBuilder(RegexOptions? options = null)
        {
            this._options = options;
        }

        public RegexBuilder BeginNamedGroup(string name)
        {
            this._unterminatedGroups.Push(name);
            this._groups.Add(new NamedGroupBeginning(name));
            return this;
        }

        public RegexBuilder AddGroup(CharacterGroup characterGroup)
        {
            this._groups.Add(characterGroup);
            return this;
        }

        public RegexBuilder EndNamedGroup(string groupName = null)
        {
            if (!this._unterminatedGroups.Any())
            {
                throw new InvalidOperationException("No named group to terminate.");
            }

            var current = this._unterminatedGroups.Pop();
            if (groupName != null && current != groupName)
            {
                throw new InvalidOperationException($"The group '{current}' is currently open but '{groupName}' is being closed.");
            }

            this._groups.Add(new NamedGroupEnding());
            return this;
        }

        public RegexBuilder Concat(RegexBuilder other)
        {
            this._groups.AddRange(other._groups);
            return this;
        }

        public RegexBuilder Clone()
        {
            var clone = new RegexBuilder(this._options);
            clone._groups.AddRange(this._groups);
            foreach (var group in this._unterminatedGroups.Reverse())
            {
                clone._unterminatedGroups.Push(group);
            }

            return clone;
        }

        private void BuildPattern()
        {
            if (this._unterminatedGroups.Any())
            {
                throw new InvalidOperationException("All named groups must be terminated before building regex.");
            }

            this._pattern = string.Join("", this._groups.Select(group => group.Pattern));
        }
    }
}

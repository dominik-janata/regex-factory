namespace RegexFactory.Groups
{
    public class NamedGroupBeginning : IGroup
    {
        private readonly string _groupName;

        public NamedGroupBeginning(string groupName)
        {
            this._groupName = groupName;
        }

        public string Pattern => $"(?<{this._groupName}>";
    }
}

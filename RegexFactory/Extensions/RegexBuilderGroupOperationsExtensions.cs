namespace RegexFactory.Extensions
{
    public static class RegexBuilderGroupOperationsExtensions
    {
        public static RegexBuilder WrapInNamedGroup(this RegexBuilder builder, string groupName)
        {
            return new RegexBuilder()
                .BeginNamedGroup(groupName)
                .Concat(builder)
                .EndNamedGroup();
        }
    }
}

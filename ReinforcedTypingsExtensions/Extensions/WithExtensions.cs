using Reinforced.Typings.Fluent;

namespace ReinforcedTypingsExtensions
{
    public static class WithExtensions
    {
        public static ClassOrInterfaceExportBuilder WithAllMembers(this ClassOrInterfaceExportBuilder classOrInterfaceExportBuilder)
        {
            return classOrInterfaceExportBuilder.WithAllProperties().WithAllMethods().WithAllFields();
        }
    }
}

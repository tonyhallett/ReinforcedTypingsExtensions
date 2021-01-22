using Reinforced.Typings.Ast;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    internal static class RtMemberExtensions
    {
        public static void CopyActualProperties(this RtMember wrapper, RtMember actual)
        {
            wrapper.Documentation = actual.Documentation;
            wrapper.AccessModifier = actual.AccessModifier;
            wrapper.IsStatic = actual.IsStatic;
            wrapper.LineAfter = actual.LineAfter;
        }
    }

}

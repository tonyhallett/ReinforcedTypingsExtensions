using Reinforced.Typings.Ast;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    internal class ReturningGeneratedMemberHandler : IGeneratedMemberHandler
    {
        public RtNode GeneratedConstructor(RtNode node, ConstructorInfo constructorInfo)
        {
            return node;
        }

        public RtNode GeneratedField(RtNode node, FieldInfo fieldInfo)
        {
            return node;
        }

        public RtNode GeneratedMethod(RtNode node, MethodInfo methodInfo)
        {
            return node;
        }

        public RtNode GeneratedProperty(RtNode node, PropertyInfo propertyInfo)
        {
            return node;
        }
    }
}

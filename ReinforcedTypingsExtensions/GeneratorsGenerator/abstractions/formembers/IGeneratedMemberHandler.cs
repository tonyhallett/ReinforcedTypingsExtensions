using Reinforced.Typings.Ast;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    public interface IGeneratedMemberHandler
    {
        RtNode GeneratedField(RtNode node, FieldInfo field);

        RtNode GeneratedMethod(RtNode node, MethodInfo method);
        RtNode GeneratedProperty(RtNode node, PropertyInfo propertyInfo);

        RtNode GeneratedConstructor(RtNode node, ConstructorInfo constructorInfo);
    }
}

using Reinforced.Typings;
using Reinforced.Typings.Ast.TypeNames;
using System.Reflection;

namespace ReinforcedTypingsExtensions.CustomResolvingWithInference
{
    public interface ITypeInferer
    {
        RtTypeName PropertyInfer(MemberInfo member, TypeResolver typeResolver);
        RtTypeName FieldInfer(MemberInfo member, TypeResolver typeResolver);
        RtTypeName MethodInfer(MethodInfo method, TypeResolver typeResolver);
        RtTypeName ParameterInfer(ParameterInfo parameter, TypeResolver typeResolver);
    }
}

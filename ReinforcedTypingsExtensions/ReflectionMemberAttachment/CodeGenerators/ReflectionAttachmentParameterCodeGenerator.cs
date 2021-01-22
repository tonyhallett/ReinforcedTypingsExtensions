using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Generators;
using System.Reflection;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public class ReflectionAttachmentParameterCodeGenerator: ParameterCodeGenerator
    {
        public override RtArgument GenerateNode(ParameterInfo element, RtArgument result, TypeResolver resolver)
        {
            return new ReflectionAttachedRtArgument( base.GenerateNode(element, result, resolver),element);
        }
    }
}

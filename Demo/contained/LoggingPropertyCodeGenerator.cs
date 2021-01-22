using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Generators;
using System.Reflection;

namespace ReinforcedTypingsExtensions
{
    internal class LoggingPropertyCodeGenerator : PropertyCodeGenerator
    {
        public override RtField GenerateNode(MemberInfo element, RtField result, TypeResolver resolver)
        {
            Context.Log("PropertyCodeGenerator provided by Contained InterfaceCodeGenerator ");
            return base.GenerateNode(element, result, resolver);
        }
    }
}

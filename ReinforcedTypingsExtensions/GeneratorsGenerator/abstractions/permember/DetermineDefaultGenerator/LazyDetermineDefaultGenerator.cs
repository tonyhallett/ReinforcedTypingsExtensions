using Reinforced.Typings;
using Reinforced.Typings.Attributes;
using System;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    internal class LazyDetermineDefaultGenerator : IDetermineDefaultGenerator
    {
        private readonly ExportContext exportContext;

        public LazyDetermineDefaultGenerator(ExportContext exportContext)
        {
            this.exportContext = exportContext;
        }
        public bool MemberHasDefaultGenerator<TMember>(TMember member) where TMember : MemberInfo
        {
            return GetGeneratorTypeFromAttributeOrFluent(member) == null;
        }

        private Type GetGeneratorTypeFromAttributeOrFluent<T>(T member) where T : MemberInfo
        {
            Type GetFromAttribute(TsAttributeBase a)
            {
                if (a != null)
                {
                    return a.CodeGeneratorType;
                }
                return null;
            }

            var attr = exportContext.CurrentBlueprint.ForMember<TsTypedMemberAttributeBase>(member);
            var fromAttr = GetFromAttribute(attr);
            if (fromAttr != null) return fromAttr;

            // internal knowledge
            if (member is MethodInfo)
            {
                var classAttr = exportContext.CurrentBlueprint.Attr<TsClassAttribute>();
                if (classAttr != null && classAttr.DefaultMethodCodeGenerator != null)
                {
                    return classAttr.DefaultMethodCodeGenerator;
                }
            }
            return null;

        }

        public bool ParameterHasDefaultGenerator(ParameterInfo parameter)
        {
            return exportContext.CurrentBlueprint.ForMember(parameter).CodeGeneratorType == null;
        }
    }
}

using Reinforced.Typings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator.Attributed
{
    internal class AttributedTypeWideCodeGenerators
    {
        private Dictionary<For, Type> lookup;
        public AttributedTypeWideCodeGenerators(List<TypeWideCodeGeneratorAttribute> typeWideCodeGeneratorAttribute)
        {
            lookup = typeWideCodeGeneratorAttribute.ToDictionary(a => a.For, a => a.GeneratorType);
        }
        public Type GetCodeGeneratorType(For @for)
        {
            if (lookup.ContainsKey(@for))
            {
                return lookup[@for];
            }
            return null;
        }
    }



    internal class AttributedGeneratorProvidingTypeGenerators : GeneratorProvidingTypeGeneratorsWhenDefault
    {
        private Dictionary<Type, AttributedTypeWideCodeGenerators> typeAttributeLookup = new Dictionary<Type, AttributedTypeWideCodeGenerators>();
        public AttributedGeneratorProvidingTypeGenerators(ExportContext exportContext, bool provideGeneratorsForParameters = true, bool lazy = true) : base(exportContext, provideGeneratorsForParameters, lazy)
        {
        }

        protected override Type ProvideConstructorGeneratorTypeWhenDefault(ConstructorInfo constructor)
        {
            return GetFromType(constructor.DeclaringType, For.Constructor);
        }

        protected override Type ProvideFieldGeneratorTypeWhenDefault(FieldInfo field)
        {
            return GetFromType(field.DeclaringType, For.Field);
        }

        protected override Type ProvideMethodGeneratorTypeWhenDefault(MethodInfo method)
        {
            return GetFromType(method.DeclaringType, For.Method);
        }

        protected override Type ProvideParameterGeneratorTypeWhenDefault(ParameterInfo parameter)
        {
            return GetFromType(parameter.Member.DeclaringType, For.Parameter);
        }

        protected override Type ProvidePropertyGeneratorTypeWhenDefault(PropertyInfo property)
        {
            return GetFromType(property.DeclaringType, For.Property);
        }

        private Type GetFromType(Type declaringType, For @for)
        {
            if (!typeAttributeLookup.TryGetValue(declaringType, out var attributedTypeCodeGenerators))
            {
                var attributes = declaringType.GetCustomAttributes<TypeWideCodeGeneratorAttribute>().ToList();
                attributedTypeCodeGenerators = new AttributedTypeWideCodeGenerators(attributes);
                typeAttributeLookup.Add(declaringType, attributedTypeCodeGenerators);
            }
            return attributedTypeCodeGenerators.GetCodeGeneratorType(@for);
        }
    }
}

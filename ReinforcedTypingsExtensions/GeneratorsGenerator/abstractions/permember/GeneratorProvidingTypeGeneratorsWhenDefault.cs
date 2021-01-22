using Reinforced.Typings;
using System;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    public abstract class GeneratorProvidingTypeGeneratorsWhenDefault : GeneratorProvidingTypeGeneratorsBase
    {
        protected GeneratorProvidingTypeGeneratorsWhenDefault(ExportContext exportContext, bool provideGeneratorsForParameters = true, bool lazy = true) : base(exportContext, provideGeneratorsForParameters, lazy)
        {
        }

        protected sealed override Type ProvideConstructorGeneratorType(ConstructorInfo constructor, bool isDefault)
        {
            if (isDefault)
            {
                return ProvideConstructorGeneratorTypeWhenDefault(constructor);
            }
            return null;
        }
        protected virtual Type ProvideConstructorGeneratorTypeWhenDefault(ConstructorInfo constructor)
        {
            return null;
        }


        protected sealed override Type ProvideFieldGeneratorType(FieldInfo field, bool isDefault)
        {
            if (isDefault)
            {
                return ProvideFieldGeneratorTypeWhenDefault(field);
            }
            return null;
        }

        protected virtual Type ProvideFieldGeneratorTypeWhenDefault(FieldInfo field)
        {
            return null;
        }

        protected sealed override Type ProvideMethodGeneratorType(MethodInfo method, bool isDefault)
        {
            if (isDefault)
            {
                return ProvideMethodGeneratorTypeWhenDefault(method);
            }
            return null;
        }

        protected virtual Type ProvideMethodGeneratorTypeWhenDefault(MethodInfo method)
        {
            return null;
        }

        protected sealed override Type ProvideParameterGeneratorType(ParameterInfo parameter, bool isDefault)
        {
            if (isDefault)
            {
                return ProvideParameterGeneratorTypeWhenDefault(parameter);
            }
            return null;
        }

        protected virtual Type ProvideParameterGeneratorTypeWhenDefault(ParameterInfo parameter)
        {
            return null;
        }

        protected sealed override Type ProvidePropertyGeneratorType(PropertyInfo property, bool isDefault)
        {
            if (isDefault)
            {
                return ProvidePropertyGeneratorTypeWhenDefault(property);
            }
            return null;
        }

        protected virtual Type ProvidePropertyGeneratorTypeWhenDefault(PropertyInfo property)
        {
            return null;
        }
    }
}

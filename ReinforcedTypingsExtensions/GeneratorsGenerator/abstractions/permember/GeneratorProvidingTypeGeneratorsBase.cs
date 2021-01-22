using Reinforced.Typings;
using System;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    public abstract class GeneratorProvidingTypeGeneratorsBase
    {
        private bool provideGeneratorsForParameters;
        private IDetermineDefaultGenerator determineDefaultGenerator;

        protected ExportContext exportContext;

        public GeneratorProvidingTypeGeneratorsBase(ExportContext exportContext, bool provideGeneratorsForParameters = true, bool lazy = true)
        {
            this.exportContext = exportContext;
            this.provideGeneratorsForParameters = provideGeneratorsForParameters;

            if (lazy)
            {
                determineDefaultGenerator = new LazyDetermineDefaultGenerator(exportContext);
            }
            else
            {
                determineDefaultGenerator = new DetermineDefaultGeneratorFromManagerByInstantiation(this.exportContext.Generators);
            }

        }

        #region provide generator types
        protected virtual Type ProvideFieldGeneratorType(FieldInfo field, bool isDefault)
        {
            return null;
        }

        protected virtual Type ProvidePropertyGeneratorType(PropertyInfo property, bool isDefault)
        {
            return null;
        }

        protected virtual Type ProvideMethodGeneratorType(MethodInfo method, bool isDefault)
        {
            return null;
        }

        protected virtual Type ProvideConstructorGeneratorType(ConstructorInfo constructor, bool isDefault)
        {
            return null;
        }

        protected virtual Type ProvideParameterGeneratorType(ParameterInfo parameter, bool isDefault)
        {
            return null;
        }
        #endregion

        public void AddGeneratorType<T>(T member) where T : MemberInfo
        {
            Type generatorType = null;
            var isDefault = determineDefaultGenerator.MemberHasDefaultGenerator(member);
            switch (member.MemberType)
            {
                case MemberTypes.Method:
                    var methodInfo = member as MethodInfo;
                    generatorType = ProvideMethodGeneratorType(methodInfo, isDefault);
                    SetParameterGenerators(methodInfo);
                    break;
                case MemberTypes.Constructor:
                    var constructorInfo = member as ConstructorInfo;
                    generatorType = ProvideConstructorGeneratorType(constructorInfo, isDefault);
                    SetParameterGenerators(constructorInfo);
                    break;
                case MemberTypes.Property:
                    generatorType = ProvidePropertyGeneratorType(member as PropertyInfo, isDefault);
                    break;
                case MemberTypes.Field:
                    generatorType = ProvideFieldGeneratorType(member as FieldInfo, isDefault);
                    break;
            }

            SetGeneratorTypeForMember(member, generatorType);
        }

        private void SetGeneratorTypeForMember<T>(T member, Type generatorType) where T : MemberInfo
        {
            if (generatorType != null)
            {
                var attr = exportContext.CurrentBlueprint.ForMember(member);
                attr.CodeGeneratorType = generatorType;
            }
        }

        private void SetParameterGenerators(MethodBase methodBase)
        {
            if (provideGeneratorsForParameters)
            {
                var parameters = methodBase.GetParameters();
                foreach (var param in parameters)
                {
                    if (exportContext.CurrentBlueprint.IsIgnored(param)) continue;
                    var isDefault = determineDefaultGenerator.ParameterHasDefaultGenerator(param);
                    var parameterGeneratorType = ProvideParameterGeneratorType(param, isDefault);
                    if (parameterGeneratorType != null)
                    {
                        var attr = exportContext.CurrentBlueprint.ForMember(param);
                        attr.CodeGeneratorType = parameterGeneratorType;
                    }
                }
            }

        }

    }
}

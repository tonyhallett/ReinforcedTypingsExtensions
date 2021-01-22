using Reinforced.Typings;
using Reinforced.Typings.Generators;
using System;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    internal class DetermineDefaultGeneratorFromManagerByInstantiation : IDetermineDefaultGenerator
    {
        private bool DummyProperty { get; set; }

        #region not lazy generators

        #region dummy reflection
        private MethodInfo dummyMethodInfo;
        private PropertyInfo dummyPropertyInfo;
        private FieldInfo dummyFieldInfo;
        private ConstructorInfo dummyConstructorInfo;
        private ParameterInfo dummyParameterInfo;
        #endregion

        #region default generators
        private ITsCodeGenerator<MethodInfo> defaultMethodGenerator;
        private ITsCodeGenerator<FieldInfo> defaultFieldGenerator;
        private ITsCodeGenerator<PropertyInfo> defaultPropertyGenerator;
        private ITsCodeGenerator<ConstructorInfo> defaultConstructorGenerator;
        private ITsCodeGenerator<ParameterInfo> defaultParameterGenerator;
        private readonly GeneratorManager generators;

        private ITsCodeGenerator<MethodInfo> DefaultMethodGenerator
        {
            get
            {
                if (defaultMethodGenerator == null)
                {
                    SetDefaultGenerators();
                }
                return defaultMethodGenerator;
            }
        }
        private ITsCodeGenerator<FieldInfo> DefaultFieldGenerator
        {
            get
            {
                if (defaultFieldGenerator == null)
                {
                    SetDefaultGenerators();
                }
                return defaultFieldGenerator;
            }
        }
        private ITsCodeGenerator<PropertyInfo> DefaultPropertyGenerator
        {
            get
            {
                if (defaultPropertyGenerator == null)
                {
                    SetDefaultGenerators();
                }
                return defaultPropertyGenerator;
            }
        }
        private ITsCodeGenerator<ConstructorInfo> DefaultConstructorGenerator
        {
            get
            {
                if (defaultConstructorGenerator == null)
                {
                    SetDefaultGenerators();
                }
                return defaultConstructorGenerator;
            }
        }

        private ITsCodeGenerator<ParameterInfo> DefaultParameterGenerator
        {
            get
            {
                if (defaultParameterGenerator == null)
                {
                    SetDefaultGenerators();
                }
                return defaultParameterGenerator;
            }
        }

        #endregion
        #endregion

        public DetermineDefaultGeneratorFromManagerByInstantiation(GeneratorManager generators)
        {
            var thisType = this.GetType();
            this.generators = generators;
            dummyMethodInfo = thisType.GetMethod(nameof(MemberHasDefaultGenerator));
            dummyParameterInfo = dummyMethodInfo.GetParameters()[0];
            dummyPropertyInfo = thisType.GetProperty(nameof(DummyProperty), BindingFlags.NonPublic | BindingFlags.Instance);
            dummyFieldInfo = thisType.GetField(nameof(dummyFieldInfo), BindingFlags.NonPublic | BindingFlags.Instance);
            dummyConstructorInfo = thisType.GetConstructors()[0];
        }

        private void SetDefaultGenerators()
        {
            defaultFieldGenerator = generators.GeneratorFor(dummyFieldInfo);
            defaultPropertyGenerator = generators.GeneratorFor(dummyPropertyInfo);
            defaultMethodGenerator = generators.GeneratorFor(dummyMethodInfo);
            defaultParameterGenerator = generators.GeneratorFor(dummyParameterInfo);
        }

        public bool MemberHasDefaultGenerator<TMember>(TMember member) where TMember : MemberInfo
        {
            var generator = generators.GeneratorFor(member);
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return generator == DefaultFieldGenerator;
                case MemberTypes.Property:
                    return generator == DefaultPropertyGenerator;
                case MemberTypes.Method:
                    return generator == DefaultMethodGenerator;
                case MemberTypes.Constructor:
                    return generator == DefaultConstructorGenerator;
                default:
                    throw new Exception("Unsupported member type");
            }
        }

        public bool ParameterHasDefaultGenerator(ParameterInfo parameter)
        {
            var generator = generators.GeneratorFor(parameter);
            return generator == DefaultParameterGenerator;
        }
    }

}

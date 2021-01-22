using ReinforcedTypingsExtensions.GeneratorsGenerator.Contained;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReinforcedTypingsExtensions
{
    public class ContainedInterfaceGeneratorProvidingLoggingPropertyCodeGenerator: ContainedInterfaceGeneratorProvidingGenerators
    {
        public ContainedInterfaceGeneratorProvidingLoggingPropertyCodeGenerator() : base(new Dictionary<Type, Type>
        {
            { typeof(PropertyInfo), typeof(LoggingPropertyCodeGenerator)}
        }) {}
    }
}

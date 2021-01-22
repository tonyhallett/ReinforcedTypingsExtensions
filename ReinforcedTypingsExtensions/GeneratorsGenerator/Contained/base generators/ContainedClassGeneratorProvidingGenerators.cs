using System;
using System.Collections.Generic;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator.Contained
{
    public abstract class ContainedClassGeneratorProvidingGenerators: ClassGeneratorProvidingGeneratorsBase
    {
        protected Dictionary<Type, Type> codeGenerators;
        private readonly IGeneratedMemberHandler generatedMemberHandler;
        private readonly bool provideGeneratorsForParameters;
        private readonly bool lazy;

        public ContainedClassGeneratorProvidingGenerators(Dictionary<Type, Type> codeGenerators,IGeneratedMemberHandler generatedMemberHandler = null,bool provideGeneratorsForParameters = true,bool lazy = true)
        {
            this.codeGenerators = codeGenerators;
            this.generatedMemberHandler = generatedMemberHandler;
            this.provideGeneratorsForParameters = provideGeneratorsForParameters;
            this.lazy = lazy;
        }
        protected override IGeneratorProvidingTypeGeneratorForMembers GeneratorProvidingTypeGenerator =>
            new ContainedGeneratorProvidingTypeGeneratorForMembers(codeGenerators,generatedMemberHandler, provideGeneratorsForParameters, lazy);
    }
}

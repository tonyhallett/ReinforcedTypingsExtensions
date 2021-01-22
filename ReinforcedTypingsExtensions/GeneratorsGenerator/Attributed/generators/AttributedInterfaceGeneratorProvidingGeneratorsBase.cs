namespace ReinforcedTypingsExtensions.GeneratorsGenerator.Attributed
{
    public abstract class AttributedInterfaceGeneratorProvidingGeneratorsBase : InterfaceGeneratorProvidingGeneratorsBase
    {
        private IGeneratedMemberHandler generatedMemberHandler;
        private bool lazy;
        public AttributedInterfaceGeneratorProvidingGeneratorsBase(IGeneratedMemberHandler generatedMemberHandler = null, bool lazy = true)
        {
            this.generatedMemberHandler = generatedMemberHandler;
            this.lazy = lazy;

        }
        protected override IGeneratorProvidingTypeGeneratorForMembers GeneratorProvidingTypeGenerator => new AttributedGeneratorProvidingTypeGeneratorsForMembers(generatedMemberHandler, lazy);

    }

}

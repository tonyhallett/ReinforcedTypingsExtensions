using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Generators;
using System;
using System.Collections.Generic;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    public abstract class InterfaceGeneratorProvidingGeneratorsBase : InterfaceCodeGenerator
    {
        protected abstract IGeneratorProvidingTypeGeneratorForMembers GeneratorProvidingTypeGenerator { get; }
        protected override void GenerateMembers<T>(Type element, TypeResolver resolver, ITypeMember typeMember, IEnumerable<T> members)
        {
            GeneratorProvidingTypeGenerator.GenerateMembers(element, resolver, typeMember, members, Context);
        }
        
    }

}

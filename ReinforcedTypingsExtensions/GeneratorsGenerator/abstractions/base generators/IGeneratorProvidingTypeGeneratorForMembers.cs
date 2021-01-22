using Reinforced.Typings;
using Reinforced.Typings.Ast;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    public interface IGeneratorProvidingTypeGeneratorForMembers
    {
        void GenerateMembers<T>(Type element, TypeResolver resolver, ITypeMember typeMember, IEnumerable<T> members, ExportContext exportContext) where T : MemberInfo;
    }

}

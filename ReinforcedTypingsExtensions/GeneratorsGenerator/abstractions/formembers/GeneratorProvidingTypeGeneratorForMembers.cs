using Reinforced.Typings;
using Reinforced.Typings.Ast;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    public abstract class GeneratorProvidingTypeGeneratorForMembers : IGeneratorProvidingTypeGeneratorForMembers
    {
        private GeneratorProvidingTypeGeneratorsBase actual;
        private ExportContext exportContext;
        private ITypeMember typeMember;
        private IGeneratedMemberHandler generatedMemberHandler;
        public GeneratorProvidingTypeGeneratorForMembers(IGeneratedMemberHandler generatedMemberHandler = null)
        {
            this.generatedMemberHandler = generatedMemberHandler == null ? new ReturningGeneratedMemberHandler() : generatedMemberHandler;
        }
        protected abstract GeneratorProvidingTypeGeneratorsBase GetActual(ExportContext context);
        private GeneratorProvidingTypeGeneratorsBase Actual
        {
            get
            {
                if (actual == null)
                {
                    actual = this.GetActual(exportContext);
                }
                return actual;
            }
        }
        public void GenerateMembers<T>(Type element, TypeResolver resolver, ITypeMember typeMember, IEnumerable<T> members, ExportContext exportContext) where T : MemberInfo
        {
            this.typeMember = typeMember;
            this.exportContext = exportContext;
            foreach (var m in members)
            {
                Actual.AddGeneratorType(m);
                var generator = exportContext.Generators.GeneratorFor(m);

                var member = generator.Generate(m, resolver);
                switch (m.MemberType)
                {
                    case MemberTypes.Field:
                        GeneratedField(member, m as FieldInfo);
                        break;
                    case MemberTypes.Property:
                        GeneratedProperty(member, m as PropertyInfo);
                        break;
                    case MemberTypes.Method:
                        GeneratedMethod(member, m as MethodInfo);
                        break;
                    case MemberTypes.Constructor:
                        GeneratedConstructor(member, m as ConstructorInfo);
                        break;
                }
            }
        }
        #region add member
        protected void AddMember(RtNode member)
        {
            if (member != null) typeMember.Members.Add(member);
        }

        private void GeneratedField(RtNode node, FieldInfo field)
        {
            AddMember(generatedMemberHandler.GeneratedField(node,field));

        }
        private void GeneratedMethod(RtNode node, MethodInfo method)
        {
            AddMember(generatedMemberHandler.GeneratedMethod(node,method));
        }
        private void GeneratedProperty(RtNode node, PropertyInfo propertyInfo)
        {
            AddMember(generatedMemberHandler.GeneratedProperty(node,propertyInfo));
        }
        private void GeneratedConstructor(RtNode node, ConstructorInfo constructorInfo)
        {
            AddMember(generatedMemberHandler.GeneratedConstructor(node,constructorInfo));
        }
        #endregion
    }

}

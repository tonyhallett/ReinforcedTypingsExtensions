using Reinforced.Typings;
using Reinforced.Typings.Ast;
using System.Collections.Generic;
using System.Reflection;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    internal class ReflectionMemberAttachmentGenerator
    {
        public void GenerateMembers<T>(TypeResolver resolver, ITypeMember typeMember, IEnumerable<T> members, GeneratorManager Generators) where T : MemberInfo
        {
            foreach (var m in members)
            {
                var generator = Generators.GeneratorFor(m);
                var member = generator.Generate(m, resolver);
                if (member != null)
                {
                    RtNode wrapperNode = null;
                    switch (m.MemberType)
                    {
                        case MemberTypes.Field:
                            wrapperNode = new ReflectionAttachedRtField(member as RtField, m as FieldInfo);
                            break;
                        case MemberTypes.Property:
                            wrapperNode = new ReflectionAttachedRtField(member as RtField, m as PropertyInfo);
                            break;
                        case MemberTypes.Method:
                            wrapperNode = new ReflectionAttachedRtFunction(member as RtFunction, m as MethodInfo);
                            break;
                        case MemberTypes.Constructor:
                            wrapperNode = new ReflectionAttachedRtConstructor(member as RtConstructor, m as ConstructorInfo);
                            break;

                    }
                    typeMember.Members.Add(wrapperNode);

                }
            }
        }
    }

}

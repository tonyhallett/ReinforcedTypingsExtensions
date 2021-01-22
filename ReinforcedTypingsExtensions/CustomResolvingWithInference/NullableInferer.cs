using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using System;
using System.Reflection;

namespace ReinforcedTypingsExtensions.CustomResolvingWithInference
{
    public class NullableInferer : ITypeInferer
    {
        private readonly ExportContext context;

        public NullableInferer(ExportContext context)
        {
            this.context = context;
        }
        public RtTypeName FieldInfer(MemberInfo member, TypeResolver typeResolver)
        {
            return InferNullable((member as FieldInfo).FieldType, typeResolver);
        }

        public RtTypeName MethodInfer(MethodInfo method, TypeResolver typeResolver)
        {
            return InferNullable(method.ReturnType, typeResolver);
        }

        public RtTypeName ParameterInfer(ParameterInfo parameter, TypeResolver typeResolver)
        {
            return InferNullable(parameter.ParameterType, typeResolver);
        }

        public RtTypeName PropertyInfer(MemberInfo member, TypeResolver typeResolver)
        {
            return InferNullable((member as PropertyInfo).PropertyType, typeResolver);
        }

        private string GetNullabled(string nullableType)
        {
            return $"Nullable<{nullableType}>";
        }
        private RtSimpleTypeName GetSimpleNullabled(string nullableType)
        {
            return new RtSimpleTypeName(GetNullabled(nullableType));
        }
        private RtTypeName InferNullable(Type type, TypeResolver typeResolver) {
            if (type.IsNullable())
            {
                AddNullable();
                return GetSimpleNullabled(typeResolver.ResolveTypeName(type).ToString());
            }
            else
            {
                if (type.IsGenericType && !type.IsGenericTypeDefinition)
                {
                    // of course this does not do List<List<decimal?>> !
                    if(type.Name == "List`1")
                    {
                        var typeArgument = type.GetGenericArguments()[0];
                        if (typeArgument.IsNullable())
                        {
                            return new RtSimpleTypeName($"{GetNullabled(typeResolver.ResolveTypeName(typeArgument).ToString())}[]");
                        }
                    }
                    
                }
            }
            return null;
        }
        private bool addedNullable;
        private void AddNullable()
        {
            if (!addedNullable)
            {
                if (context.Location.CurrentNamespace != null)
                {
                    context.Location.CurrentNamespace.CompilationUnits.Add(new RtRaw("type Nullable<T> = T | null"));
                }
                addedNullable = true;
            }

        }
    }
}

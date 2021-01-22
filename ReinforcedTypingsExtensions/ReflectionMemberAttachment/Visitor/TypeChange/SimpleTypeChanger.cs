using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using System;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public abstract class SimpleTypeChanger : IReflectionTypeChanger
    {
        public ExportContext ExportContext { protected get; set; }
        public CompilationUnitsManager CompilationUnitsManager { protected get; set; }

        public RtTypeName ChangeConstructorParameterType(Type parameterType, RtTypeName rtTypeName, RtArgument rtArgument, ReflectionAttachedRtConstructor rtConstructor)
        {
            return ChangeType(parameterType, rtTypeName);
        }

        public RtTypeName ChangeFieldType(Type fieldType, RtTypeName rtTypeName, ReflectionAttachedRtField rtField)
        {
            return ChangeType(fieldType, rtTypeName);
        }

        public RtTypeName ChangeFunctionParameterType(Type parameterType, RtTypeName rtTypeName, RtArgument rtArgument, ReflectionAttachedRtFunction rtFunction)
        {
            return ChangeType(parameterType, rtTypeName);
        }

        public RtTypeName ChangeFunctionReturnType(Type returnType, RtTypeName rtTypeName, ReflectionAttachedRtFunction rtFunction)
        {
            return ChangeType(returnType, rtTypeName);
        }

        public RtTypeName ChangePropertyType(Type propertyType, RtTypeName rtTypeName, ReflectionAttachedRtField rtField)
        {
            return ChangeType(propertyType, rtTypeName);
        }

        protected abstract RtTypeName ChangeType(Type type, RtTypeName rtTypeName);
    }

}

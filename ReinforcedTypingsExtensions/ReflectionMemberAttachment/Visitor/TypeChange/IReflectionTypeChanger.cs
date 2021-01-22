using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using System;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public interface IReflectionTypeChanger
    {
        ExportContext ExportContext { set; }
        CompilationUnitsManager CompilationUnitsManager { set; }
        RtTypeName ChangeFieldType(Type fieldType, RtTypeName rtTypeName, ReflectionAttachedRtField rtField);
        RtTypeName ChangeFunctionReturnType(Type returnType, RtTypeName rtTypeName, ReflectionAttachedRtFunction rtFunction);
        RtTypeName ChangeFunctionParameterType(Type parameterType, RtTypeName rtTypeName, RtArgument rtArgument, ReflectionAttachedRtFunction rtFunction);
        RtTypeName ChangePropertyType(Type propertyType, RtTypeName rtTypeName, ReflectionAttachedRtField rtField);
        RtTypeName ChangeConstructorParameterType(Type parameterType, RtTypeName rtTypeName, RtArgument rtArgument, ReflectionAttachedRtConstructor rtConstructor);
    }

}

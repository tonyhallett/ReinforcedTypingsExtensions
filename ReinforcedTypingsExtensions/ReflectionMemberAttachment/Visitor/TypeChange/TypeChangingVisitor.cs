using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public abstract class TypeChangingVisitor : ReflectionGeneratedVisitorBase
    {
        protected CompilationUnitsManager compilationUnitsManager;
        private ParameterArgumentCorresponder parameterArgumentCorresponder;
        private readonly ExportContext exportContext;
        private readonly IReflectionTypeChanger typeChanger;

        public TypeChangingVisitor(ExportContext exportContext,IReflectionTypeChanger typeChanger)
        {
            this.exportContext = exportContext;
            this.typeChanger = typeChanger;
            this.typeChanger.ExportContext = exportContext;
            this.parameterArgumentCorresponder = new ParameterArgumentCorresponder(exportContext.Project);
        }

        public override void Visit(RtNamespace rtNamespace)
        {
            compilationUnitsManager = new CompilationUnitsManager(rtNamespace.CompilationUnits);
            typeChanger.CompilationUnitsManager = compilationUnitsManager;
            VisitNamespace(rtNamespace);
            compilationUnitsManager.Complete();
        }
        protected abstract void VisitNamespace(RtNamespace rtNamespace);

        protected override void ReflectionConstructor(ReflectionAttachedRtConstructor constructor)
        {
            ChangeParameters(constructor.ConstructorInfo, constructor.Arguments, (type, rtTypeName, rtArgument) => typeChanger.ChangeConstructorParameterType(type, rtTypeName, rtArgument, constructor));
        }

        protected override void ReflectionField(ReflectionAttachedRtField field)
        {
            var fieldType = field.FieldInfo.FieldType;
            var changedType = typeChanger.ChangeFieldType(fieldType, field.Type, field);
            if (changedType != null)
            {
                field.Type = changedType;
            }
        }
        private List<(Type parameterType,RtArgument rtArgument)> CorrespondParametersAndRtArguments(TypeBlueprint typeBluePrint,ParameterInfo[] parameters,List<RtArgument> rtArguments)
        {
            if (parameters.Length == rtArguments.Count)
            {
                return parameters.Select((p, i) => (p.ParameterType, rtArguments[i])).ToList();
            }

            var corresponding = new List<(Type parameterType, RtArgument rtArgument)>();
            var argumentPosition = 0;
            foreach(var parameter in parameters)
            {
                if (!typeBluePrint.IsIgnored(parameter))
                {
                    corresponding.Add((parameter.ParameterType, rtArguments[argumentPosition]));
                    argumentPosition++;
                }
            }
            return corresponding;
        }
        
        private void ChangeParameters(MethodBase methodBase, List<RtArgument> rtArguments,Func<Type,RtTypeName,RtArgument,RtTypeName> typeChanger)
        {
            var corresponding = parameterArgumentCorresponder.Correspond(methodBase, rtArguments);
            foreach (var correspondance in corresponding)
            {
                var argument = correspondance.rtArgument;
                var changedParameterType = typeChanger(correspondance.parameterType, argument.Type, argument);
                if (changedParameterType != null)
                {
                    argument.Type = changedParameterType;
                }
            }
        }
        
        protected override void ReflectionFunction(ReflectionAttachedRtFunction function)
        {
            var methodInfo = function.MethodInfo;
            var changedReturnType = typeChanger.ChangeFunctionReturnType(methodInfo.ReturnType, function.ReturnType, function);
            if (changedReturnType != null)
            {
                function.ReturnType = changedReturnType;
            }

            ChangeParameters(methodInfo, function.Arguments, (type, rtTypeName, rtArgument) => typeChanger.ChangeFunctionParameterType(type, rtTypeName, rtArgument, function));
        }

        protected override void ReflectionProperty(ReflectionAttachedRtField property)
        {
            var propertyType = property.PropertyInfo.PropertyType;
            var changedType = typeChanger.ChangePropertyType(propertyType, property.Type,property);
            if(changedType != null)
            {
                property.Type = changedType;
            }
        }

        
    }

}

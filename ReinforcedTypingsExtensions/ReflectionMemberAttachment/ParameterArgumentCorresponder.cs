using Reinforced.Typings;
using Reinforced.Typings.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public class ParameterArgumentCorresponder
    {
        private readonly ProjectBlueprint projectBlueprint;

        public ParameterArgumentCorresponder(ProjectBlueprint projectBlueprint)
        {
            this.projectBlueprint = projectBlueprint;
        }
        public List<(Type parameterType, RtArgument rtArgument)> Correspond(MethodBase methodBase, List<RtArgument> rtArguments)
        {
            var typeBlueprint = projectBlueprint.Blueprint(methodBase.DeclaringType);
            return CorrespondParametersAndRtArguments(typeBlueprint, methodBase.GetParameters(), rtArguments);
        }
        private List<(Type parameterType, RtArgument rtArgument)> CorrespondParametersAndRtArguments(TypeBlueprint typeBluePrint, ParameterInfo[] parameters, List<RtArgument> rtArguments)
        {
            if (parameters.Length == rtArguments.Count)
            {
                return parameters.Select((p, i) => (p.ParameterType, rtArguments[i])).ToList();
            }

            var corresponding = new List<(Type parameterType, RtArgument rtArgument)>();
            var argumentPosition = 0;
            foreach (var parameter in parameters)
            {
                if (!typeBluePrint.IsIgnored(parameter))
                {
                    corresponding.Add((parameter.ParameterType, rtArguments[argumentPosition]));
                    argumentPosition++;
                }
            }
            return corresponding;
        }

    }

}

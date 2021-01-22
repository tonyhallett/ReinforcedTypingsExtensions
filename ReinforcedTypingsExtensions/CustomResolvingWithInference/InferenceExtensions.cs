using Reinforced.Typings;
using Reinforced.Typings.Fluent;

namespace ReinforcedTypingsExtensions.CustomResolvingWithInference
{
    public static class InferenceExtensions
    {
        public static void WithAllWithInference(this ClassOrInterfaceExportBuilder exportBuilder, ITypeInferer typeInferer, ExportContext exportContext,WithAllBuilderCallbacks builderCallbacks = null)
        {
            if (builderCallbacks == null)
            {
                builderCallbacks = new WithAllBuilderCallbacks();
            }

            /*
                remember that using any of the overloads clears the current inferrer
                public static ISupportsInferring<T> InferType<T>(this ISupportsInferring<T> x, Func<T, TypeResolver, string> inferrer)
                {
                    x.TypeInferers.StringResolver = inferrer;
                    return x;
                }

                so if callback wants to add own it has to come after
            */

            

            exportBuilder.WithAllProperties(propertyExportBuilder =>
            {

                propertyExportBuilder.InferType((mi, resolver) =>
                {
                    return typeInferer.PropertyInfer(mi, resolver);
                });

                if (builderCallbacks.PropertyCallback != null)
                {
                    builderCallbacks.PropertyCallback(propertyExportBuilder);
                }
            });

            exportBuilder.WithAllFields(propertyExportBuilder => {
                
                propertyExportBuilder.InferType((mi, resolver) =>
                {
                    return typeInferer.FieldInfer(mi, resolver);
                });

                if (builderCallbacks.FieldCallback != null)
                {
                    builderCallbacks.FieldCallback(propertyExportBuilder);
                }
            });

            // for parameter inference
            var typeBluePrint = exportContext.Project.Blueprint(exportBuilder.Type);

            exportBuilder.WithAllMethods(methodExportBuilder =>
            {
                methodExportBuilder.InferType((mi, resolver) =>
                {
                    return typeInferer.MethodInfer(mi, resolver);
                });

                var parameters = methodExportBuilder.Member.GetParameters();
                foreach (var parameter in parameters)
                {
                    // Given that cannot do via the fluent api ( has to be strongly typed )
                    // can create the attribute !
                    var parameterAttribute = typeBluePrint.ForMember(parameter, true);
                    parameterAttribute.InferType((pi, resolver) =>
                    {
                        return typeInferer.ParameterInfer(pi, resolver);
                    });
                }

                if (builderCallbacks.MethodCallback != null)
                {
                    builderCallbacks.MethodCallback(methodExportBuilder);
                }
            });
            
        }

    }
}

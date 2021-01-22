using Reinforced.Typings.Fluent;
using Reinforced.Typings.Generators;
using System;

namespace ReinforcedTypingsExtensions
{
    public static class TypeExportBuilderExtensions
    {
        public static TTypeExportBuilder WithCodeGenerator<TTypeExportBuilder, TCodeGenerator>(this TTypeExportBuilder typeBuilder) where TTypeExportBuilder:TypeExportBuilder where TCodeGenerator:ITsCodeGenerator<Type>
        {
            switch (typeBuilder)
            {
                case ClassExportBuilder classExportBuilder:
                    return classExportBuilder.WithCodeGenerator<TCodeGenerator>() as TTypeExportBuilder;
                case InterfaceExportBuilder interfaceExportBuilder:
                    return interfaceExportBuilder.WithCodeGenerator<TCodeGenerator>() as TTypeExportBuilder;
                case EnumExportBuilder enumExportBuilder:
                    return enumExportBuilder.WithCodeGenerator<TCodeGenerator>() as TTypeExportBuilder;
            }
            throw new Exception("Unknown TypeExportBuilder");
        }
    }
}

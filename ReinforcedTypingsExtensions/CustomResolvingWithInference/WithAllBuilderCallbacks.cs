using Reinforced.Typings.Fluent;
using System;

namespace ReinforcedTypingsExtensions.CustomResolvingWithInference
{
    public class WithAllBuilderCallbacks
    {
        public Action<PropertyExportBuilder> PropertyCallback { get; set; }
        public Action<PropertyExportBuilder> FieldCallback { get; set; }
        public Action<MethodExportBuilder> MethodCallback { get; set; }
        
    }
}

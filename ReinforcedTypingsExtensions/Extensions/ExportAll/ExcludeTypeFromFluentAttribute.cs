using System;

namespace ReinforcedTypingsExtensions
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface|AttributeTargets.Struct, AllowMultiple = false)]
    public class ExcludeTypeFromFluentAttribute : Attribute { }
}

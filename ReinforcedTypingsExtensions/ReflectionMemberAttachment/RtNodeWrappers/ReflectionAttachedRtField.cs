using Reinforced.Typings.Ast;
using System.Reflection;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public class ReflectionAttachedRtField : RtField
    {
        public FieldInfo FieldInfo { get; }
        public PropertyInfo PropertyInfo { get; }

        internal bool Handled { get; set; }

        public ReflectionAttachedRtField(RtField actual, PropertyInfo propertyInfo):this(actual)
        {
            PropertyInfo = propertyInfo;
        }
        public ReflectionAttachedRtField(RtField actual, FieldInfo fieldInfo):this(actual)
        {
            FieldInfo = fieldInfo;
        }
        private ReflectionAttachedRtField(RtField actual)
        {
            this.Identifier = actual.Identifier;
            this.Type = actual.Type;
            this.InitializationExpression = actual.InitializationExpression;
            foreach (var decorator in actual.Decorators)
            {
                Decorators.Add(decorator);
            }

            this.CopyActualProperties(actual);

        }
    }

}

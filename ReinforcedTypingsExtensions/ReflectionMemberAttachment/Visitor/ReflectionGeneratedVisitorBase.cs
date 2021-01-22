using Reinforced.Typings.Ast;
using Reinforced.Typings.Visitors;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public abstract class ReflectionGeneratedVisitorBase : VisitorBase
    {
        public override void Visit(RtConstructor node)
        {
            var reflectionAttachedRtConstructor = node as ReflectionAttachedRtConstructor;
            if (reflectionAttachedRtConstructor != null && !reflectionAttachedRtConstructor.Handled)
            {
                reflectionAttachedRtConstructor.Handled = true;

                ReflectionConstructor(reflectionAttachedRtConstructor);

            }
        }

        protected abstract void ReflectionConstructor(ReflectionAttachedRtConstructor constructor);

        public override void Visit(RtFunction node)
        {
            var reflectionAttachedRtFunction = node as ReflectionAttachedRtFunction;
            if (reflectionAttachedRtFunction != null && !reflectionAttachedRtFunction.Handled)
            {
                reflectionAttachedRtFunction.Handled = true;

                ReflectionFunction(reflectionAttachedRtFunction);

            }
        }

        protected abstract void ReflectionFunction(ReflectionAttachedRtFunction function);

        public override void Visit(RtField node)
        {
            var reflectionAttachedRtField = node as ReflectionAttachedRtField;
            if (reflectionAttachedRtField != null && !reflectionAttachedRtField.Handled)
            {
                reflectionAttachedRtField.Handled = true;

                if (reflectionAttachedRtField.PropertyInfo != null)
                {
                    ReflectionProperty(reflectionAttachedRtField);
                }
                else
                {
                    ReflectionField(reflectionAttachedRtField);
                }

            }

        }

        protected abstract void ReflectionField(ReflectionAttachedRtField field);
        protected abstract void ReflectionProperty(ReflectionAttachedRtField property);

        public override void Visit(RtArgument node)
        {
            var reflectionAttachedRtArgument = node as ReflectionAttachedRtArgument;
            if (reflectionAttachedRtArgument != null && !reflectionAttachedRtArgument.Handled)
            {
                reflectionAttachedRtArgument.Handled = true;
                ReflectionParameter(reflectionAttachedRtArgument);
            }
        }
        protected abstract void ReflectionParameter(ReflectionAttachedRtArgument argument);

    }

}

using Reinforced.Typings.Ast;
using System.Reflection;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public class ReflectionAttachedRtConstructor : RtConstructor
    {
        public ConstructorInfo ConstructorInfo { get; }
        internal bool Handled { get; set; }
        public ReflectionAttachedRtConstructor(RtConstructor actual, ConstructorInfo constructorInfo)
        {
            this.ConstructorInfo = constructorInfo;

            this.Arguments = actual.Arguments;
            foreach(var superCallParameter in actual.SuperCallParameters)
            {
                SuperCallParameters.Add(superCallParameter);
            }
            this.NeedsSuperCall = actual.NeedsSuperCall;
            this.Body = actual.Body;

            this.CopyActualProperties(actual);
        }
    }

}

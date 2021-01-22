using Reinforced.Typings.Attributes;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public class ReflectionMemberAttachmentClassAttribute : TsClassAttribute
    {
        public ReflectionMemberAttachmentClassAttribute()
        {
            CodeGeneratorType = typeof(ReflectionMemberAttachmentInterfaceCodeGenerator);
        }
    }

}

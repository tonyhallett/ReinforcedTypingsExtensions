using Reinforced.Typings.Attributes;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public class ReflectionMemberAttachmentInterfaceAttribute : TsInterfaceAttribute
    {
        public ReflectionMemberAttachmentInterfaceAttribute()
        {
            CodeGeneratorType = typeof(ReflectionMemberAttachmentInterfaceCodeGenerator);
        }
    }

}

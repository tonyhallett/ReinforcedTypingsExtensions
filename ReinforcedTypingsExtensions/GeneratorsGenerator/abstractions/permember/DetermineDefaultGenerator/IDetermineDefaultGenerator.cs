using System.Reflection;

namespace ReinforcedTypingsExtensions.GeneratorsGenerator
{
    internal interface IDetermineDefaultGenerator
    {
        bool MemberHasDefaultGenerator<TMember>(TMember member) where TMember : MemberInfo;
        bool ParameterHasDefaultGenerator(ParameterInfo parameter);
    }

}

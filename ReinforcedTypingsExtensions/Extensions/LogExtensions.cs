using Reinforced.Typings;

namespace ReinforcedTypingsExtensions
{
    public static class LogExtensions
    {
        public static void Log(this ExportContext context, string message)
        {
            context.Warnings.Add(new Reinforced.Typings.Exceptions.RtWarning(0, "Log", message));
        }
    }
}

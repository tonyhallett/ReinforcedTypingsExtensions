using Reinforced.Typings.Ast;
using System.Collections.Generic;
using System.Linq;

namespace ReinforcedTypingsExtensions.ReflectionMemberAttachment
{
    public class CompilationUnitsManager
    {
        private readonly List<RtNode> compilationUnits;
        private readonly List<RtNode> insertions = new List<RtNode>();

        public CompilationUnitsManager(List<RtNode> compilationUnits)
        {
            this.compilationUnits = compilationUnits;
        }
        public void InsertCompilationUnitsAtStart(params RtNode[] additionalCompilationUnits)
        {
            insertions.InsertRange(0, additionalCompilationUnits);
        }
        public void InsertRawCompilationUnitsAtStart(params string[] rawCompilationUnits)
        {
            InsertCompilationUnitsAtStart(rawCompilationUnits.Select(r => new RtRaw(r)).ToArray());
        }

        internal void Complete()
        {
            compilationUnits.InsertRange(0, insertions);
        }
    }

}

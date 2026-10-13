using RimWorld;
using System.Collections.Generic;
using Verse;

namespace CoveredUglyNullifier
{
    public static class HumanHeadPartDefs
    {
        private static HashSet<BodyPartDef> _set;

        public static HashSet<BodyPartDef> Set
        {
            get
            {
                if (_set != null) return _set;
                _set = BuildFromHuman();
                return _set;
            }
        }

        private static HashSet<BodyPartDef> BuildFromHuman()
        {
            var result = new HashSet<BodyPartDef>();

            var body = BodyDefOf.Human;
            BodyPartRecord head = null;

            var parts = body.AllParts;
            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].def == BodyPartDefOf.Head)
                {
                    head = parts[i];
                    break;
                }
            }

            if (head == null) return result;

            AddRecursively(head, result);
            return result;
        }

        private static void AddRecursively(BodyPartRecord node, HashSet<BodyPartDef> set)
        {
            if (node?.def != null) set.Add(node.def);
            var kids = node?.parts;
            if (kids == null) return;

            for (int i = 0; i < kids.Count; i++)
                AddRecursively(kids[i], set);
        }
    }
}

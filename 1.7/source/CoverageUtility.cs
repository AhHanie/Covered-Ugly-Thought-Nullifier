using System.Collections.Generic;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    public static class CoverageUtility
    {
        public static HashSet<ThingDef> ActiveCoverageDefs
        {
            get
            {
                return ModSettings.UseFaceOnly
                    ? FullyCoveringApparelCache.FullHeadApparelDefs
                    : FullyCoveringApparelCache.FullyCoveringApparelDefs;
            }
        }

        // True when a single worn apparel item satisfies the active coverage mode.
        public static bool IsCovered(Pawn pawn)
        {
            List<Apparel> worn = pawn?.apparel?.WornApparel;
            if (worn == null || worn.Count == 0)
                return false;

            HashSet<ThingDef> coverageDefs = ActiveCoverageDefs;
            if (coverageDefs.Count == 0)
                return false;

            for (int i = 0; i < worn.Count; i++)
            {
                ThingDef def = worn[i]?.def;
                if (def != null && coverageDefs.Contains(def))
                    return true;
            }

            return false;
        }
    }
}

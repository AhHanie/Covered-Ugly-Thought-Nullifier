using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    [HarmonyPatch(typeof(ThoughtWorker_Pretty), "CurrentSocialStateInternal")]
    public static class Patch_ThoughtWorker_Pretty_CurrentSocialStateInternal
    {
        public static bool Prepare() => ModSettings.UsePrettyNullification;
        public static void Postfix(Pawn pawn, Pawn other, ref ThoughtState __result)
        {
            if (!__result.Active)
                return;

            if (other.apparel?.WornApparel == null || other.apparel.WornApparel.Count == 0)
                return;

            HashSet<ThingDef> coverageDefs = ModSettings.UseFaceOnly
                ? FullyCoveringApparelCache.FullHeadApparelDefs
                : FullyCoveringApparelCache.FullyCoveringApparelDefs;

            var worn = other.apparel.WornApparel;
            for (int i = 0; i < worn.Count; i++)
            {
                var app = worn[i];
                if (coverageDefs.Contains(app.def))
                {
                    __result = false; // nullify pretty thought
                    return;
                }
            }

            return;
        }
    }
}

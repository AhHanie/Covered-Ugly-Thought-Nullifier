using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    [HarmonyPatch(typeof(ThoughtWorker_Ugly), "CurrentSocialStateInternal")]
    public static class Patch_ThoughtWorker_Ugly_CurrentSocialStateInternal
    {
        public static bool Prefix(Pawn pawn, Pawn other, ref ThoughtState __result)
        {
            if (other?.apparel?.WornApparel == null || other.apparel.WornApparel.Count == 0)
                return true;

            HashSet<ThingDef> coverageDefs = ModSettings.UseFaceOnly
                ? FullyCoveringApparelCache.FullHeadApparelDefs
                : FullyCoveringApparelCache.FullyCoveringApparelDefs;
            if (coverageDefs.Count == 0)
                return true;

            var worn = other.apparel.WornApparel;
            for (int i = 0; i < worn.Count; i++)
            {
                var app = worn[i];
                if (app?.def != null && coverageDefs.Contains(app.def))
                {
                    __result = false; // nullify ugly thought
                    return false;     // skip vanilla
                }
            }

            return true; // run vanilla
        }
    }
}

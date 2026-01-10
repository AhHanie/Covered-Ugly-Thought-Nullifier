using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace CoveredUglyNullifier.Patches
{
    [HarmonyPatch(typeof(RelationsUtility), nameof(RelationsUtility.IsDisfigured))]
    public static class Patch_RelationsUtility_IsDisfigured
    {
        public static bool Prepare() => ModSettings.UseDisfiguredCalculation;
        public static void Postfix(Pawn pawn, Pawn forPawn, bool ignoreSightSources, ref bool __result)
        {
            // Only do work if vanilla found them disfigured
            if (!__result) return;

            if (!IsCovered(pawn)) return;

            __result = false;
            return;


            // All the beauty related body parts are on the head for humans
            // This means there's no need for the complex check below
            // I'll keep it just in case
            // Face-only mode:
            // If there exists any disfiguring hediff on a NON-head part => keep disfigured.
            // Otherwise => not disfigured while covered.
            // Full burka mode: covered => never disfigured
            if (!ModSettings.UseFaceOnly)
            {
                __result = false;
                return;
            }

            var headDefs = HumanHeadPartDefs.Set;

            bool ignoreScarification = forPawn == null || forPawn.Ideo == null || forPawn.Ideo.RequiredScars == 0;
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;

            for (int i = 0; i < hediffs.Count; i++)
            {
                var h = hediffs[i];
                if (!IsDisfiguringHediff(h, pawn, ignoreSightSources, ignoreScarification))
                    continue;

                var partDef = h.Part?.def;
                if (partDef != null && !headDefs.Contains(partDef))
                {
                    // Disfigurement is from outside head => still disfigured even if face is covered
                    return;
                }
            }

            __result = false;
        }

        private static bool IsCovered(Pawn pawn)
        {
            var worn = pawn.apparel?.WornApparel;
            if (worn == null || worn.Count == 0) return false;

            var defs = ModSettings.UseFaceOnly
                ? FullyCoveringApparelCache.FullHeadApparelDefs
                : FullyCoveringApparelCache.FullyCoveringApparelDefs;

            for (int i = 0; i < worn.Count; i++)
            {
                var app = worn[i];
                if (defs.Contains(app.def))
                    return true;
            }
            return false;
        }

        private static bool IsDisfiguringHediff(Hediff h, Pawn pawn, bool ignoreSightSources, bool ignoreScarification)
        {
            if (h.def == HediffDefOf.Scarification && !ignoreScarification)
                return false;

            if (h.Part == null || !h.Part.def.beautyRelated)
                return false;

            if (h is Hediff_MissingPart mp)
            {
                if (ignoreSightSources && mp.Part.def.tags.Contains(BodyPartTagDefOf.SightSource))
                    return false;

                if (pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(mp.Part))
                    return false;

                return true;
            }

            return h is Hediff_Injury;
        }
    }
}

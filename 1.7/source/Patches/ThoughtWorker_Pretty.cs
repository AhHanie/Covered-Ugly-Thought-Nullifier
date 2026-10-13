using HarmonyLib;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    // Infix on the single GetStatValue(PawnBeauty) call inside the vanilla worker (Harmony 3).
    // A covered target never reads as pretty; vanilla keeps its disfigurement and blindness checks.
    [HarmonyPatch(typeof(ThoughtWorker_Pretty), "CurrentSocialStateInternal")]
    public static class Patch_ThoughtWorker_Pretty_CurrentSocialStateInternal
    {
        public static bool Prepare() => ModSettings.UsePrettyNullification;

        [HarmonyInfix(typeof(StatExtension), nameof(StatExtension.GetStatValue), typeof(Thing), typeof(StatDef), typeof(bool), typeof(int))]
        public static float Postfix(float __result, StatDef stat, [HarmonyOuter] Pawn other)
        {
            if (stat != StatDefOf.PawnBeauty || __result <= 0f)
                return __result;

            return CoverageUtility.IsCovered(other) ? 0f : __result;
        }
    }
}

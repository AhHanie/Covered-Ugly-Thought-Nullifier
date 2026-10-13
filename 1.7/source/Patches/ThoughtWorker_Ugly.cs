using HarmonyLib;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    // Infix on the single GetStatValue(PawnBeauty) call inside the vanilla worker (Harmony 3).
    // Vanilla keeps its own gating (humanlike, known, blind, CaresAboutOthersAppearance) and
    // only sees a beauty value that is never ugly while the target is covered.
    [HarmonyPatch(typeof(ThoughtWorker_Ugly), "CurrentSocialStateInternal")]
    public static class Patch_ThoughtWorker_Ugly_CurrentSocialStateInternal
    {
        [HarmonyInfix(typeof(StatExtension), nameof(StatExtension.GetStatValue), typeof(Thing), typeof(StatDef), typeof(bool), typeof(int))]
        public static float Postfix(float __result, StatDef stat, [HarmonyOuter] Pawn other)
        {
            if (stat != StatDefOf.PawnBeauty || __result >= 0f)
                return __result;

            return CoverageUtility.IsCovered(other) ? 0f : __result;
        }
    }
}

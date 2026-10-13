using HarmonyLib;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    [HarmonyPatch("AlphaGenes.ThoughtWorker_UnsettlingAppearance", "CurrentSocialStateInternal")]
    public static class Patch_ThoughtWorker_UnsettlingAppearance_CurrentSocialStateInternal
    {
        public static bool Prepare()
        {
            return AlphaGenesCompat.Enabled
                && ModSettings.UseAlphaGenesUnsettlingAppearance
                && AlphaGenesCompat.HasType("AlphaGenes.ThoughtWorker_UnsettlingAppearance");
        }

        public static void Postfix(Pawn pawn, Pawn other, ref ThoughtState __result)
        {
            if (!__result.Active)
                return;

            if (CoverageUtility.IsCovered(other))
                __result = false;
        }
    }
}

using HarmonyLib;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    [HarmonyPatch("AlphaGenes.ThoughtWorker_EldritchVisage", "CurrentSocialStateInternal")]
    public static class Patch_ThoughtWorker_EldritchVisage_CurrentSocialStateInternal
    {
        public static bool Prepare()
        {
            return AlphaGenesCompat.Enabled
                && ModSettings.UseAlphaGenesEldritchVisage
                && AlphaGenesCompat.HasType("AlphaGenes.ThoughtWorker_EldritchVisage");
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

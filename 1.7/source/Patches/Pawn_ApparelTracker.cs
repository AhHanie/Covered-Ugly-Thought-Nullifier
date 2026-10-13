using HarmonyLib;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    public static class Pawn_ApparelTrackerPatches
    {
        [HarmonyPatch(typeof(Pawn_ApparelTracker), nameof(Pawn_ApparelTracker.Notify_ApparelAdded))]
        public static class Notify_ApparelAdded
        {
            public static bool Prepare()
            {
                return AlphaGenesCompat.Enabled
                    && (ModSettings.UseAlphaGenesRepulsive
                        || ModSettings.UseAlphaGenesAngelic);
            }

            public static void Postfix(Pawn_ApparelTracker __instance, Apparel apparel)
            {
                Pawn pawn = __instance.pawn;
                if (!IsValidPawn(pawn))
                    return;

                if (!CoverageUtility.IsCovered(pawn))
                    return;

                if (pawn.genes == null)
                    return;

                if (ModSettings.UseAlphaGenesRepulsive && HasGene(pawn, AlphaGenesCompat.AG_Beauty_VeryVeryUgly))
                    TryRemoveHediff(pawn, AlphaGenesCompat.AG_Disgust);

                if (ModSettings.UseAlphaGenesAngelic && HasGene(pawn, AlphaGenesCompat.AG_Beauty_Angelic))
                    TryRemoveHediff(pawn, AlphaGenesCompat.AG_Awe);
            }
        }

        [HarmonyPatch(typeof(Pawn_ApparelTracker), nameof(Pawn_ApparelTracker.Notify_ApparelRemoved))]
        public static class Notify_ApparelRemoved
        {
            public static bool Prepare()
            {
                return AlphaGenesCompat.Enabled
                    && (ModSettings.UseAlphaGenesRepulsive
                        || ModSettings.UseAlphaGenesAngelic);
            }

            public static void Postfix(Pawn_ApparelTracker __instance, Apparel apparel)
            {
                Pawn pawn = __instance.pawn;
                if (!IsValidPawn(pawn))
                    return;

                if (CoverageUtility.IsCovered(pawn))
                    return;

                if (pawn.genes == null)
                    return;

                if (ModSettings.UseAlphaGenesRepulsive && HasGene(pawn, AlphaGenesCompat.AG_Beauty_VeryVeryUgly))
                    TryAddHediff(pawn, AlphaGenesCompat.AG_Disgust);

                if (ModSettings.UseAlphaGenesAngelic && HasGene(pawn, AlphaGenesCompat.AG_Beauty_Angelic))
                    TryAddHediff(pawn, AlphaGenesCompat.AG_Awe);
            }
        }

        private static bool IsValidPawn(Pawn pawn)
        {
            return pawn != null && !pawn.Dead;
        }

        // Alpha Genes defs are resolved with GetNamedSilentFail, so any of them can be null.
        private static bool HasGene(Pawn pawn, GeneDef gene)
        {
            return gene != null && pawn.genes.HasActiveGene(gene);
        }

        private static void TryRemoveHediff(Pawn pawn, HediffDef hediffDef)
        {
            if (hediffDef == null || pawn.health?.hediffSet == null)
                return;

            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(hediffDef);
            if (hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
            }
        }

        private static void TryAddHediff(Pawn pawn, HediffDef hediffDef)
        {
            if (hediffDef == null || pawn.health == null)
                return;

            Hediff existing = pawn.health.hediffSet?.GetFirstHediffOfDef(hediffDef);
            if (existing != null)
                return;

            pawn.health.AddHediff(hediffDef);
        }
    }
}

using System.Collections.Generic;
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

                if (!IsFullyCovered(pawn))
                    return;

                if (pawn.genes == null)
                    return;

                if (ModSettings.UseAlphaGenesRepulsive && HasRepulsiveGene(pawn))
                    TryRemoveHediff(pawn, AlphaGenesCompat.AG_Disgust);

                if (ModSettings.UseAlphaGenesAngelic && HasAngelicGene(pawn))
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

                if (IsFullyCovered(pawn))
                    return;

                if (pawn.genes == null)
                    return;

                if (ModSettings.UseAlphaGenesRepulsive && HasRepulsiveGene(pawn))
                    TryAddHediff(pawn, AlphaGenesCompat.AG_Disgust);

                if (ModSettings.UseAlphaGenesAngelic && HasAngelicGene(pawn))
                    TryAddHediff(pawn, AlphaGenesCompat.AG_Awe);
            }
        }

        private static bool IsValidPawn(Pawn pawn)
        {
            return pawn != null && !pawn.Dead;
        }

        private static bool HasRepulsiveGene(Pawn pawn)
        {
            return pawn.genes.HasActiveGene(AlphaGenesCompat.AG_Beauty_VeryVeryUgly);
        }

        private static bool HasAngelicGene(Pawn pawn)
        {
            return pawn.genes.HasActiveGene(AlphaGenesCompat.AG_Beauty_Angelic);
        }

        private static bool IsFullyCovered(Pawn pawn)
        {
            if (pawn.apparel?.WornApparel == null || pawn.apparel.WornApparel.Count == 0)
                return false;

            HashSet<ThingDef> coverageDefs = ModSettings.UseFaceOnly
                ? FullyCoveringApparelCache.FullHeadApparelDefs
                : FullyCoveringApparelCache.FullyCoveringApparelDefs;

            var worn = pawn.apparel.WornApparel;
            for (int i = 0; i < worn.Count; i++)
            {
                ThingDef def = worn[i]?.def;
                if (coverageDefs.Contains(def))
                    return true;
            }

            return false;
        }

        private static void TryRemoveHediff(Pawn pawn, HediffDef hediffDef)
        {
            if (pawn.health?.hediffSet == null)
                return;

            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(hediffDef);
            if (hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
            }
        }

        private static void TryAddHediff(Pawn pawn, HediffDef hediffDef)
        {
            if (pawn.health == null)
                return;

            Hediff existing = pawn.health.hediffSet?.GetFirstHediffOfDef(hediffDef);
            if (existing != null)
                return;

            pawn.health.AddHediff(hediffDef);
        }
    }
}

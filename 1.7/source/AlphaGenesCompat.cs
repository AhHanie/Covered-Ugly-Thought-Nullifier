using HarmonyLib;
using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    public static class AlphaGenesCompat
    {
        public static bool Enabled => ModsConfig.IsActive("sarg.alphagenes");

        public static HediffDef AG_Disgust;
        public static HediffDef AG_Awe;
        public static GeneDef AG_Beauty_VeryVeryUgly;
        public static GeneDef AG_Beauty_Angelic;

        public static void Init()
        {
            if (!Enabled)
            {
                return;
            }

            AG_Disgust = DefDatabase<HediffDef>.GetNamedSilentFail("AG_Disgust");
            AG_Awe = DefDatabase<HediffDef>.GetNamedSilentFail("AG_Awe");
            AG_Beauty_VeryVeryUgly = DefDatabase<GeneDef>.GetNamedSilentFail("AG_Beauty_VeryVeryUgly");
            AG_Beauty_Angelic = DefDatabase<GeneDef>.GetNamedSilentFail("AG_Beauty_Angelic");

            WarnIfMissing(AG_Disgust, "HediffDef AG_Disgust");
            WarnIfMissing(AG_Awe, "HediffDef AG_Awe");
            WarnIfMissing(AG_Beauty_VeryVeryUgly, "GeneDef AG_Beauty_VeryVeryUgly");
            WarnIfMissing(AG_Beauty_Angelic, "GeneDef AG_Beauty_Angelic");
        }

        // Harmony 3 PatchAll has no per-class error handling, so a renamed third-party type must be
        // detected in Prepare() and skipped instead of aborting every remaining patch.
        public static bool HasType(string typeName)
        {
            if (AccessTools.TypeByName(typeName) != null)
            {
                return true;
            }

            Log.Warning("[CoveredUglyNullifier] Alpha Genes type " + typeName + " was not found; skipping that compatibility patch.");
            return false;
        }

        private static void WarnIfMissing(Def def, string description)
        {
            if (def == null)
            {
                Log.Warning("[CoveredUglyNullifier] Alpha Genes " + description + " was not found; the related compatibility option is disabled.");
            }
        }
    }
}

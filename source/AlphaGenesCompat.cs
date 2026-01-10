using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    public static class AlphaGenesCompat
    {
        public static bool Enabled => ModsConfig.IsActive("sarg.alphagenes");

        public static HediffDef AG_Disgust;
        public static GeneDef AG_Beauty_VeryVeryUgly;

        public static void Init()
        {
            if (!Enabled)
            {
                return;
            }

            AG_Disgust = DefDatabase<HediffDef>.GetNamedSilentFail("AG_Disgust");
            AG_Beauty_VeryVeryUgly = DefDatabase<GeneDef>.GetNamedSilentFail("AG_Beauty_VeryVeryUgly");
        }
    }
}

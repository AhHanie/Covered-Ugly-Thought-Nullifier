using RimWorld;
using Verse;

namespace CoveredUglyNullifier
{
    [DefOf]
    public static class AlphaGenesCompat
    {
        public static bool Enabled => ModsConfig.IsActive("sarg.alphagenes");

        public static HediffDef AG_Disgust;
        public static GeneDef AG_Beauty_VeryVeryUgly;

        static AlphaGenesCompat()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(AlphaGenesCompat));
        }
    }
}

using Verse;

namespace CoveredUglyNullifier
{
    public class ModSettings : Verse.ModSettings
    {
        public static bool UseFullCoverage = true;
        public static bool UseFaceOnly = false;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref UseFullCoverage, "UseFullCoverage", true);
            Scribe_Values.Look(ref UseFaceOnly, "UseFaceOnly", false);
        }
    }
}

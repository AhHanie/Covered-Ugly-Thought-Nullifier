using Verse;

namespace CoveredUglyNullifier
{
    public class ModSettings : Verse.ModSettings
    {
        public static bool UseFullCoverage = true;
        public static bool UseFaceOnly = false;
        public static bool UseDisfiguredCalculation = true;
        public static bool UsePrettyNullification = false;
        public static bool UseAlphaGenesRepulsive = true;
        public static bool UseAlphaGenesAngelic = false;
        public static bool UseAlphaGenesUnsettlingAppearance = true;
        public static bool UseAlphaGenesEldritchVisage = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref UseFullCoverage, "UseFullCoverage", true);
            Scribe_Values.Look(ref UseFaceOnly, "UseFaceOnly", false);
            Scribe_Values.Look(ref UseDisfiguredCalculation, "UseDisfiguredCalculation", true);
            Scribe_Values.Look(ref UsePrettyNullification, "UsePrettyNullification", false);
            Scribe_Values.Look(ref UseAlphaGenesRepulsive, "UseAlphaGenesRepulsive", true);
            Scribe_Values.Look(ref UseAlphaGenesAngelic, "UseAlphaGenesAngelic", false);
            Scribe_Values.Look(ref UseAlphaGenesUnsettlingAppearance, "UseAlphaGenesUnsettlingAppearance", true);
            Scribe_Values.Look(ref UseAlphaGenesEldritchVisage, "UseAlphaGenesEldritchVisage", true);
        }
    }
}

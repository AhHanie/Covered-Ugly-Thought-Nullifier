using UnityEngine;
using Verse;

namespace CoveredUglyNullifier
{
    public static class ModSettingsWindow
    {
        public static void Draw(Rect parent)
        {
            var listing = new Listing_Standard();
            listing.Begin(parent);

            string fullCoverageTooltip = "CoveredUglyNullifier.SettingFullCoverageTooltip".Translate();
            bool useFullCoverage = ModSettings.UseFullCoverage;
            listing.CheckboxLabeled("CoveredUglyNullifier.SettingFullCoverage".Translate(), ref useFullCoverage, fullCoverageTooltip);
            if (useFullCoverage != ModSettings.UseFullCoverage)
            {
                ModSettings.UseFullCoverage = useFullCoverage;
                ModSettings.UseFaceOnly = !useFullCoverage;
            }

            bool useFaceOnly = ModSettings.UseFaceOnly;
            listing.CheckboxLabeled("CoveredUglyNullifier.SettingFaceOnly".Translate(), ref useFaceOnly);
            if (useFaceOnly != ModSettings.UseFaceOnly)
            {
                ModSettings.UseFaceOnly = useFaceOnly;
                ModSettings.UseFullCoverage = !useFaceOnly;
            }

            listing.CheckboxLabeled(
                "CoveredUglyNullifier.SettingDisfiguredCalculation".Translate(),
                ref ModSettings.UseDisfiguredCalculation,
                "CoveredUglyNullifier.SettingDisfiguredCalculationTooltip".Translate());

            listing.CheckboxLabeled(
                "CoveredUglyNullifier.SettingPrettyNullification".Translate(),
                ref ModSettings.UsePrettyNullification,
                "CoveredUglyNullifier.SettingPrettyNullificationTooltip".Translate());

            if (AlphaGenesCompat.Enabled)
            {
                listing.GapLine();
                listing.Label("CoveredUglyNullifier.AlphaGenesSectionTitle".Translate());
                listing.CheckboxLabeled(
                    "CoveredUglyNullifier.SettingAlphaGenesRepulsive".Translate(),
                    ref ModSettings.UseAlphaGenesRepulsive);
                listing.CheckboxLabeled(
                    "CoveredUglyNullifier.SettingAlphaGenesAngelic".Translate(),
                    ref ModSettings.UseAlphaGenesAngelic);
                listing.CheckboxLabeled(
                    "CoveredUglyNullifier.SettingAlphaGenesUnsettlingAppearance".Translate(),
                    ref ModSettings.UseAlphaGenesUnsettlingAppearance);
                listing.CheckboxLabeled(
                    "CoveredUglyNullifier.SettingAlphaGenesEldritchVisage".Translate(),
                    ref ModSettings.UseAlphaGenesEldritchVisage);
            }

            listing.Label("CoveredUglyNullifier.SettingRestartNote".Translate());

            listing.End();
        }
    }
}

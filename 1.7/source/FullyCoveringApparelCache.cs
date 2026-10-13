using System.Collections.Generic;
using Verse;

namespace CoveredUglyNullifier
{
    public static class FullyCoveringApparelCache
    {
        private const string FullHeadGroupDefName = "FullHead";
        public static readonly HashSet<ThingDef> FullyCoveringApparelDefs = new HashSet<ThingDef>();
        public static readonly HashSet<ThingDef> FullHeadApparelDefs = new HashSet<ThingDef>();

        private static readonly HashSet<string> FullCoverageGroupDefNames = new HashSet<string>
        {
            FullHeadGroupDefName,
            "UpperHead",
            "Neck",
            "Shoulders",
            "Torso",
            "Legs",
            "Arms"
        };

        private static bool HasBodyPartGroup(IList<BodyPartGroupDef> groups, string defName)
        {
            for (int g = 0; g < groups.Count; g++)
            {
                var grp = groups[g];
                if (grp != null && grp.defName == defName)
                    return true;
            }

            return false;
        }

        public static void BuildCache()
        {
            FullyCoveringApparelDefs.Clear();
            FullHeadApparelDefs.Clear();

            var allDefs = DefDatabase<ThingDef>.AllDefsListForReading;

            for (int i = 0; i < allDefs.Count; i++)
            {
                ThingDef def = allDefs[i];
                if (def == null || !def.IsApparel)
                    continue;

                var apparel = def.apparel;
                if (apparel?.bodyPartGroups == null || apparel.bodyPartGroups.Count == 0)
                    continue;

                if (HasBodyPartGroup(apparel.bodyPartGroups, FullHeadGroupDefName))
                    FullHeadApparelDefs.Add(def);

                bool coversAll = true;
                foreach (string req in FullCoverageGroupDefNames)
                {
                    if (!HasBodyPartGroup(apparel.bodyPartGroups, req))
                    {
                        coversAll = false;
                        break;
                    }
                }

                if (coversAll)
                    FullyCoveringApparelDefs.Add(def);
            }
        }
    }
}

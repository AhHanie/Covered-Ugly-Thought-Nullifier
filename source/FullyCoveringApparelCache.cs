using System.Collections.Generic;
using Verse;

namespace CoveredUglyNullifier
{
    public static class FullyCoveringApparelCache
    {
        public static readonly HashSet<ThingDef> FullyCoveringApparelDefs = new HashSet<ThingDef>();

        private static readonly HashSet<string> RequiredGroupDefNames = new HashSet<string>
        {
            "FullHead",
            "UpperHead",
            "Neck",
            "Shoulders",
            "Torso",
            "Legs",
            "Arms"
        };

        public static void BuildCache()
        {
            FullyCoveringApparelDefs.Clear();

            var allDefs = DefDatabase<ThingDef>.AllDefsListForReading;

            for (int i = 0; i < allDefs.Count; i++)
            {
                ThingDef def = allDefs[i];
                if (def == null || !def.IsApparel)
                    continue;

                var apparel = def.apparel;
                if (apparel?.bodyPartGroups == null || apparel.bodyPartGroups.Count == 0)
                    continue;

                bool coversAll = true;

                foreach (string req in RequiredGroupDefNames)
                {
                    bool found = false;
                    var groups = apparel.bodyPartGroups;

                    for (int g = 0; g < groups.Count; g++)
                    {
                        var grp = groups[g];
                        if (grp != null && grp.defName == req)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
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

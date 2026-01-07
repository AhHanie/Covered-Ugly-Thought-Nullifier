using HarmonyLib;
using Verse;

namespace CoveredUglyNullifier
{
    public class CoveredUglyNullifierMod : Mod
    {
        public CoveredUglyNullifierMod(ModContentPack content) : base(content)
        {
            new Harmony("sk.covereduglynullifier").PatchAll();
            LongEventHandler.QueueLongEvent(Init, "Covered Ugly Init", doAsynchronously: true, null);
        }

        public void Init()
        {
            FullyCoveringApparelCache.BuildCache();
        }
    }
}

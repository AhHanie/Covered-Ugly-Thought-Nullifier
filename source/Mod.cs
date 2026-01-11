using HarmonyLib;
using Verse;

namespace CoveredUglyNullifier
{
    public class CoveredUglyNullifierMod : Mod
    {
        public CoveredUglyNullifierMod(ModContentPack content) : base(content)
        {
            LongEventHandler.QueueLongEvent(Init, "Covered Ugly Init", doAsynchronously: true, null);
        }

        public override string SettingsCategory()
        {
            return "CoveredUglyNullifier.SettingsTitle".Translate();
        }

        public override void DoSettingsWindowContents(UnityEngine.Rect inRect)
        {
            ModSettingsWindow.Draw(inRect);
            base.DoSettingsWindowContents(inRect);
        }

        public void Init()
        {
            GetSettings<ModSettings>();
            AlphaGenesCompat.Init();
            FullyCoveringApparelCache.BuildCache();
            new Harmony("sk.covereduglynullifier").PatchAll();
        }
    }
}

using HarmonyLib;
using System;
using System.Runtime;
using UnityEngine;
using Verse;

namespace TameableAnomalies
{
    public class TameableAnomaliesMod : Mod
    {
        public static TameableAnomaliesSettings settings;
        public TameableAnomaliesMod(ModContentPack content)
            : base(content)
        {
            settings = GetSettings<TameableAnomaliesSettings>();

            Harmony harmony = new Harmony("pyromann.tameableanomalies");
            harmony.PatchAll();

            Type t = GenTypes.GetTypeInAnyAssembly("TameableAnomalies.Jobs.JobDriver_ReturnToHoldingPlatform");

            Log.Message("GenTypes result: " + (t == null ? "NULL" : t.AssemblyQualifiedName));
            Log.Message("[Tameable Anomalies] Loaded!");
        }

        public override string SettingsCategory()
        {
            return "Tameable Anomalies";
        }

        public override void DoSettingsWindowContents(UnityEngine.Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();

            listing.Begin(inRect);

            listing.Label("Conditioning");

            listing.CheckboxLabeled(
                "Conditioning boosts containment strength",
                ref settings.conditioningBoostsContainment);

            listing.GapLine();
            listing.GapLine();
            listing.GapLine();

            listing.Label($"Conditioning decay per day: {settings.conditioningDecayPerDay:0.0}%");

            settings.conditioningDecayPerDay =
                listing.Slider(settings.conditioningDecayPerDay, 0f, 25f);

            settings.conditioningDecayPerDay = Mathf.Round(settings.conditioningDecayPerDay);

            listing.End();

            settings.Write();
        }
    }
}
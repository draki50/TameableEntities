using HarmonyLib;
using RimWorld;
using TameableAnomalies.Components;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(StudyManager), nameof(StudyManager.StudyAnomaly))]
    public static class Patch_StudyManager
    {
        static bool Prefix(Thing studiedThing)
        {
            Pawn studiedPawn = studiedThing as Pawn;

            if (studiedPawn == null)
                return true;

            CompConditionableEntity comp =
                studiedPawn.TryGetComp<CompConditionableEntity>();

            if (comp == null)
                return true;

            if (!comp.ConditionMode)
                return true;

            // Skip the vanilla StudyAnomaly method entirely.
            return false;
        }
    }
}
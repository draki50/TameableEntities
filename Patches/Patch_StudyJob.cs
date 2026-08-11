using HarmonyLib;
using RimWorld;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompStudiable), nameof(CompStudiable.Study))]
    public static class Patch_StudyJob
    {
        static void Postfix(CompStudiable __instance, Pawn studier)
        {
            Pawn studiedPawn = __instance.parent as Pawn;

            if (studiedPawn == null)
                return;

            CompConditionableEntity conditionComp =
                studiedPawn.TryGetComp<CompConditionableEntity>();

            if (conditionComp == null || !conditionComp.ConditionMode)
            {
                return;
            }

            float oldDisplayed = ConditioningUtility.GetDisplayedConditioning(studiedPawn);

            float gain = ConditioningUtility.ApplyConditioning(
                studier,
                studiedPawn);

            float newDisplayed = ConditioningUtility.GetDisplayedConditioning(studiedPawn);

            float actualGain = newDisplayed - oldDisplayed;

            float conditioning =
                ConditioningUtility.GetConditioning(studiedPawn);
            Thing thing = studiedPawn;

            if (thing.Map == null)
            {
                thing = thing.ParentHolder as Thing;
            }

            if (thing != null)
            {
                MoteMaker.ThrowText(
                    thing.DrawPos,
                    thing.Map,
                    $"+{actualGain:0.#}% Conditioning",
                    3f);
            }
        }
    }
}
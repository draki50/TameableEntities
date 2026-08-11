using HarmonyLib;
using RimWorld;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(Hediff), nameof(Hediff.TendableNow))]
    public static class Patch_Hediff_TendableNow
    {
        public static void Postfix(
            Hediff __instance,
            ref bool __result)
        {
            Pawn pawn = __instance.pawn;

            if (pawn == null)
                return;

            if (pawn.TryGetComp<CompConditionableEntity>() == null)
                return;

            CompConditionableEntity comp =
                pawn.TryGetComp<CompConditionableEntity>();


            float conditioning = ConditioningUtility.GetConditioning(pawn);

            if (conditioning < ConditioningUtility.FriendlyConditioning)
            {
                return;
            }

            if (__instance is not Hediff_Injury injury)
            {
                return;
            }

            if (injury.IsTended())
            {
                return;
            }


            __result = true;
        }
    }
}
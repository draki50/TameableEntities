using HarmonyLib;
using RimWorld;
using TameableAnomalies.Components;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(HealthAIUtility), nameof(HealthAIUtility.ShouldBeTendedNowByPlayer))]
    public static class Patch_HealthAIUtility_ShouldBeTended
    {
        public static void Postfix(Pawn pawn, ref bool __result)
        {
            if (pawn == null)
                return;

            if (pawn.TryGetComp<CompConditionableEntity>() == null)
                return;
        }
    }
}
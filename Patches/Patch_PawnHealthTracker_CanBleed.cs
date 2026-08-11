using HarmonyLib;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(Pawn_HealthTracker), nameof(Pawn_HealthTracker.CanBleed), MethodType.Getter)]
    public static class Patch_PawnHealthTracker_CanBleed
    {
        public static void Postfix(
            Pawn ___pawn,
            ref bool __result)
        {
            // Vanilla already allows bleeding.
            if (__result)
                return;

            if (___pawn == null)
                return;

            CompConditionableEntity comp =
                ___pawn.TryGetComp<CompConditionableEntity>();

            if (comp == null)
                return;

            // Only tamed entities.
            if (ConditioningUtility.GetConditioning(___pawn) < 40f)
            {
                __result = true;
            }
        }
    }
}
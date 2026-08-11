using HarmonyLib;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.IsAnimal), MethodType.Getter)]
    public static class Patch_Pawn_IsAnimal
    {
        public static void Postfix(Pawn __instance, ref bool __result)
        {
            if (__result)
                return;

            if (ConditioningUtility.CanBeConditioned(__instance) &&
                ConditioningUtility.IsFriendly(__instance))
            {
                __result = true;
            }
        }
    }
}
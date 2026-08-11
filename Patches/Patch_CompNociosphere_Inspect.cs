using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompNociosphere), nameof(CompNociosphere.CompInspectStringExtra))]
    public static class Patch_CompNociosphere_Inspect
    {
        public static bool Prefix(CompNociosphere __instance, ref string __result)
        {
            if (!ConditioningUtility.IsConditioned(__instance.Pawn))
                return true;

            if (ConditioningUtility.GetDisplayedConditioning(__instance.Pawn) < 20f)
                return true;

            __result = "Nociosphere Conditioned\nStability Secured";

            return false;
        }
    }
}
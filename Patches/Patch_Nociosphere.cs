using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompNociosphere), "get_CanSend")]
    public static class Patch_Nociosphere_CanSend
    {
        public static bool Prefix(CompNociosphere __instance, ref bool __result)
        {
            if (!ConditioningUtility.IsConditioned(__instance.Pawn))
                return true;

            __result = true;
            return false;
        }
    }
}
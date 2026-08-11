using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompCauseHediff_AoE), "IsPawnAffected")]
    public static class Patch_CompCauseHediff_AoE
    {
        public static bool Prefix(
            CompCauseHediff_AoE __instance,
            Pawn target,
            ref bool __result)
        {
            if (__instance.parent is not Pawn source)
                return true;

            // Only affect conditioned Nociospheres.
            if (source.def.defName != "Nociosphere")
                return true;

            if (!ConditioningUtility.IsConditioned(source))
                return true;

            // Ignore allies.
            if (target.Faction == source.Faction)
            {
                __result = false;
                return false;
            }

            return true;
        }
    }
}
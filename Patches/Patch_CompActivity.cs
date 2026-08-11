using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompActivity), nameof(CompActivity.AdjustActivity))]
    public static class Patch_CompActivity_AdjustActivity
    {
        public static void Prefix(CompActivity __instance, ref float delta)
        {
            if (__instance.parent is not Pawn pawn)
                return;

            if (pawn.def.defName != "Nociosphere")
                return;

            if (!ConditioningUtility.IsConditioned(pawn))
                return;

            float conditioning =
                ConditioningUtility.GetDisplayedConditioning(pawn) / 100f;

            if (delta > 0f)
            {
                delta *= (1f - conditioning);
            }
        }
    }
}
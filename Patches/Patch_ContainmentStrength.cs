using HarmonyLib;
using RimWorld;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(StatExtension), nameof(StatExtension.GetStatValue))]
    public static class Patch_ContainmentStrength
    {
        public static void Postfix(
            Thing thing,
            StatDef stat,
            ref float __result)
        {
            if (stat != StatDefOf.MinimumContainmentStrength)
                return;

            if (thing is not Pawn pawn)
                return;

            if (!ConditioningUtility.CanBeConditioned(pawn))
                return;

            CompConditionableEntity comp = pawn.TryGetComp<CompConditionableEntity>();

            if (comp == null)
                return;

            if (!TameableAnomaliesMod.settings.conditioningBoostsContainment)
                return;

            float conditioning = ConditioningUtility.GetDisplayedConditioning(pawn);

            __result *= 1f - (conditioning / 100f);
        }
    }
}
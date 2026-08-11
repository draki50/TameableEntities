using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompStudiable), nameof(CompStudiable.CompInspectStringExtra))]
    public static class Patch_CompStudiable
    {
        static void Postfix(CompStudiable __instance, ref string __result)
        {
            Pawn pawn = __instance.parent as Pawn;
            if (pawn == null)
                return;

            if (!ConditioningUtility.CanBeConditioned(pawn))
                return;

            float conditioning = ConditioningUtility.GetDisplayedConditioning(pawn);
            if (!string.IsNullOrEmpty(__result))
                __result += "\n";

            __result +=
                "Conditioning\n" +
                ConditioningUtility.GetConditioningBar(pawn) +
                "\n" +
                $"{conditioning}%" +
                $"\nDisposition: {ConditioningUtility.GetDisposition(pawn)}";
        }
    }
}
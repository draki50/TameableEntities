using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompNociosphere), nameof(CompNociosphere.CompTick))]
    public static class Patch_CompNociosphere_CompTick
    {
        public static void Prefix(CompNociosphere __instance)
        {
            Pawn pawn = __instance.Pawn;

            if (!ConditioningUtility.IsConditioned(pawn))
                return;

            if (ConditioningUtility.GetDisplayedConditioning(pawn) < 10f)
                return;

            int becoming =
                (int)AccessTools.Field(typeof(CompNociosphere), "becomingUnstableTick")
                    .GetValue(__instance);

            int unstable =
                (int)AccessTools.Field(typeof(CompNociosphere), "unstableTick")
                    .GetValue(__instance);

            if (becoming > 0 || unstable > 0)
            {
                AccessTools.Field(typeof(CompNociosphere), "becomingUnstableTick")
                    .SetValue(__instance, -1);

                AccessTools.Field(typeof(CompNociosphere), "unstableTick")
                    .SetValue(__instance, -1);
            }
        }
    }
}
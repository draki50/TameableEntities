using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using TameableAnomalies.Utilities;
using UnityEngine;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(Ability), nameof(Ability.GetGizmos))]
    public static class Patch_MetalhorrorLarvaIcon
    {
        public static void Postfix(
            Ability __instance,
            ref IEnumerable<Gizmo> __result)
        {
            if (__instance == null)
                return;

            // Only affect our Metalhorror Larva ability.
            if (__instance.def.defName != "TA_ImplantMetalhorrorLarva")
                return;

            Pawn pawn = __instance.pawn;

            if (pawn == null)
                return;

            // Only affect our conditioned entities.
            if (!ConditioningUtility.CanBeConditioned(pawn))
                return;

            Texture2D icon = ContentFinder<Texture2D>.Get(
                "Abilities/MetalhorrorLarva",
                reportFailure: false);

            if (icon == null)
            {
                return;
            }

            foreach (Gizmo gizmo in __result)
            {
                if (gizmo is Command command)
                {
                    command.icon = icon;
                }
            }
        }
    }
}
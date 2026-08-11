using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using UnityEngine;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompHoldingPlatformTarget), nameof(CompHoldingPlatformTarget.CompGetGizmosExtra))]
    public static class Patch_CompHoldingPlatformTarget
    {
        static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> __result, CompHoldingPlatformTarget __instance)
        {
            // First, return all of RimWorld's original gizmos.
            foreach (Gizmo gizmo in __result)
            {
                yield return gizmo;
            }

            // Only show our button if the entity is currently inside a holding platform.
            if (!__instance.CurrentlyHeldOnPlatform)
                yield break;

            if (Prefs.DevMode)
            {
                yield return new Command_Action
                {
                    defaultLabel = "+10 Conditioning",
                    defaultDesc = "Developer tool: Increase conditioning by 10.",
                    action = () =>
                    {
                        Pawn pawn = (Pawn)__instance.parent;
                        ConditioningUtility.AddConditioning(pawn, 10);
                    }
                };
            }

            Pawn pawn = (Pawn)__instance.parent;

            Texture2D entityIcon = EntityIconUtility.GetEntityIcon(pawn);

            ConditioningProfile profile = ConditioningUtility.GetProfile(pawn);

            if (ConditioningUtility.IsFullyConditioned(pawn))
            {
                yield return new Command_Action
                {
                    defaultLabel = "Release Conditioned Entity",
                    defaultDesc = "Release this conditioned entity into your colony.",
                    icon = entityIcon,
                    action = () =>
                    {
                        if (!ConditioningUtility.ReleaseAsAlly(pawn))
                        {
                            Messages.Message(
                                "This entity has not been fully conditioned.",
                                MessageTypeDefOf.RejectInput,
                                false);

                            return;
                        }

                        __instance.HeldPlatform.EjectContents();
                    }
                };
            }
        }
    }
}
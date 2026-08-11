using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using TameableAnomalies.Dialogs;
using UnityEngine;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(RenameUIUtility), nameof(RenameUIUtility.DrawRenameButton), new[] { typeof(Rect), typeof(Pawn) })]
    public static class Patch_RenameUIUtility
    {
        static bool Prefix(Rect rect, Pawn pawn)
        {
            // Not one of our entities?
            if (!ConditioningUtility.IsFriendly(pawn))
                return true;

            TooltipHandler.TipRegionByKey(rect, "RenameAnimal");

            if (Widgets.ButtonImage(rect, TexButton.Rename))
            {
                Find.WindowStack.Add(new Dialog_RenameEntity(pawn));
            }

            return false;
        }
    }
}
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(HealthCardUtility), nameof(HealthCardUtility.DrawPawnHealthCard))]
    public static class Patch_HealthCardUtility
    {
        static void Postfix(Rect outRect, Pawn pawn, bool allowOperations, bool showBloodLoss, Thing thingForMedBills)
        {
            if (!Utilities.ConditioningUtility.IsFriendly(pawn) || pawn.RaceProps.IsFlesh)
                return;

            float x = 45f;
            float y = 96f;

            Rect buttonRect = new Rect(
                outRect.x + x,
                outRect.y + y,
                140f,
                24f);

            Widgets.DrawButtonGraphic(buttonRect);
            MedicalCareUtility.MedicalCareSelectButton(buttonRect, pawn);
        }
    }
}
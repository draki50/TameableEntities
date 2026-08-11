using HarmonyLib;
using RimWorld;
using Verse;
using TameableAnomalies.Utilities;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(TrainableUtility), nameof(TrainableUtility.GetTrainability))]
    public static class Patch_TrainableUtility_GetTrainability
    {
        public static void Postfix(Pawn pawn, ref TrainabilityDef __result)
        {
            if (pawn == null)
                return;

            if (!pawn.RaceProps.IsAnomalyEntity)
                return;

            if (!ConditioningUtility.IsFriendly(pawn))
                return;

            ConditioningProfile profile = ConditioningUtility.GetProfile(pawn);

            if (profile?.Trainability != null)
                __result = profile.Trainability;
        }
    }
}
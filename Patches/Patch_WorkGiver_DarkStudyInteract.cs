using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(WorkGiver_DarkStudyInteract), nameof(WorkGiver_DarkStudyInteract.HasJobOnThing))]
    public static class Patch_WorkGiver_DarkStudyInteract
    {
        public static void Postfix(
            Pawn pawn,
            Thing t,
            bool forced,
            ref bool __result)
        {            
            if (!__result)
                return;

            Pawn entity = t as Pawn;

            if (t is Building_HoldingPlatform platform)
                entity = platform.HeldPawn;

            if (entity == null)
            {
                return;
            }

            ConditioningProfile profile = ConditioningUtility.GetProfile(entity);

            if (profile == null)
            {
                return;
            }

            if (!ConditioningUtility.MeetsSkillRequirements(pawn, entity))
            {
                __result = false;
            }
        }
    }
}
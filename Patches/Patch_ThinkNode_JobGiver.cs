using HarmonyLib;
using Verse;
using Verse.AI;
using TameableAnomalies.Utilities;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(ThinkNode_JobGiver), "TryIssueJobPackage")]
    public static class Patch_ThinkNode_JobGiver
    {
        public static void Postfix(
            ThinkNode_JobGiver __instance,
            Pawn pawn,
            ThinkResult __result)
        {
            if (!ConditioningUtility.IsFriendly(pawn))
                return;

        }
    }
}
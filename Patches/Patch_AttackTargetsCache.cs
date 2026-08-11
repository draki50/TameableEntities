using HarmonyLib;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(AttackTargetsCache), nameof(AttackTargetsCache.GetPotentialTargetsFor))]
    public static class Patch_AttackTargetsCache
    {
        public static void Postfix(IAttackTargetSearcher th, ref System.Collections.Generic.List<IAttackTarget> __result)
        {
            __result.RemoveAll(target =>
                target is Pawn pawn &&
                ConditioningUtility.IsFriendlyConditionedNociosphere(pawn));
        }
    }
}
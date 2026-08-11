using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(JobGiver_AIFightEnemy), "MeleeAttackJob")]
    public static class Patch_RevenantAttack
    {
        public static bool Prefix(Pawn pawn, Thing enemyTarget, ref Job __result)
        {

            // Only affect conditioned Revenants.
            CompRevenant comp = pawn.TryGetComp<CompRevenant>();
            if (comp == null)
                return true;

            if (!ConditioningUtility.IsConditioned(pawn))
                return true;

            // Don't replace the job if the Revenant is already performing one.
            if (pawn.CurJobDef == JobDefOf.RevenantAttack)
                return true;

            // Respect the vanilla hypnosis cooldown.
            if (Find.TickManager.TicksGame < comp.nextHypnosis)
                return true;

            // Hypnosis only works on hostile humanlike pawns.
            if (enemyTarget is not Pawn target)
                return true;

            if (!target.RaceProps.Humanlike)
                return true;

            if (!target.HostileTo(Faction.OfPlayer))
                return true;

            // Use the vanilla Revenant attack job.
            __result = JobMaker.MakeJob(TameableAnomaliesJobDefOf.TameableRevenantAttack, enemyTarget);
            return false;
        }
    }
}
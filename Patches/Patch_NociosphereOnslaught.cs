using HarmonyLib;
using RimWorld;
using System.Linq;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompNociosphere), "OnInteracted")]
    public static class Patch_Nociosphere_OnInteracted
    {
        public static void Prefix(CompNociosphere __instance)
        {
            Pawn pawn = __instance.Pawn;

            if (!ConditioningUtility.IsConditioned(pawn))
                return;

            // Make absolutely sure it begins the onslaught as a player pawn.
            pawn.SetFaction(Faction.OfPlayer);
            pawn.MapHeld?.attackTargetsCache.UpdateTarget(pawn);
        }
    }

    [HarmonyPatch(typeof(CompNociosphere), "OnPassive")]
    public static class Patch_Nociosphere_OnPassive
    {
        public static void Postfix(CompNociosphere __instance)
        {
            Pawn pawn = __instance.Pawn;

            if (!ConditioningUtility.IsConditioned(pawn))
                return;

            Map map = pawn.MapHeld;
            if (map == null)
                return;

            pawn.SetFaction(Faction.OfPlayer);
            map.attackTargetsCache.UpdateTarget(pawn);

            foreach (Pawn other in map.mapPawns.AllPawnsSpawned.ToList())
            {
                if (!ConditioningUtility.IsFriendly(other))
                    continue;

                // If this entity is currently attacking, make it rethink.
                if (other.CurJobDef == JobDefOf.AttackMelee)
                {
                    other.jobs.EndCurrentJob(JobCondition.InterruptForced);
                }

                other.mindState.enemyTarget = null;
                map.attackTargetsCache.UpdateTarget(other);
            }

            foreach (Pawn colonist in map.mapPawns.FreeColonistsSpawned.ToList())
            {
                if (colonist.CurJobDef == JobDefOf.AttackMelee ||
                    colonist.CurJobDef == JobDefOf.Wait_Combat)
                {
                    colonist.jobs.EndCurrentJob(JobCondition.InterruptForced);
                }

                colonist.mindState.enemyTarget = null;
                map.attackTargetsCache.UpdateTarget(colonist);
            }
        }
    }
}
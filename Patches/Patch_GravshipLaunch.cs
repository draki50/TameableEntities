using HarmonyLib;
using RimWorld;
using System.Linq;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(RitualBehaviorWorker_GravshipLaunch), nameof(RitualBehaviorWorker_GravshipLaunch.TryExecuteOn))]
    public static class Patch_GravshipLaunch
    {
        static void Postfix(TargetInfo target)
        {
            Building_GravEngine engine =
                target.Thing?.TryGetComp<CompPilotConsole>()?.engine;

            if (engine == null)
            {
                return;
            }

            foreach (Pawn pawn in target.Map.mapPawns.AllPawnsSpawned)
            {
                // Ignore pawns that aren't tameable entities
                if (!ConditioningUtility.CanBeConditioned(pawn))
                    continue;

                // Ignore entities that aren't conditioned yet
                if (!ConditioningUtility.IsConditioned(pawn))
                    continue;

                engine.pawnsToBoard.Add(pawn);
                IntVec3 spot;

                if (GravshipUtility.IsOnboardGravship_NewTemp(
                    pawn.Position,
                    engine,
                    pawn,
                    desperate: false,
                    respectAllowedAreas: false))
                {
                    spot = pawn.Position;
                }
                else if (!GravshipUtility.TryFindSpotOnGravship(pawn, engine, out spot))
                {
                    continue;
                }

                Job job = JobMaker.MakeJob(JobDefOf.GotoShip, spot);
                job.locomotionUrgency = LocomotionUrgency.Jog;

                pawn.jobs.StartJob(job, JobCondition.InterruptForced);
            }
        }
    }
}
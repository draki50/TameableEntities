using RimWorld;
using System.Collections.Generic;
using System.Linq;
using TameableAnomalies.Utilities;
using UnityEngine;
using Verse;
using Verse.AI;

namespace TameableAnomalies.FloatMenu
{
    public class FloatMenuOptionProvider_ReturnToHoldingPlatform : FloatMenuOptionProvider
    {
        protected override bool Drafted => true;

        protected override bool Undrafted => true;

        protected override bool Multiselect => false;

        protected override bool RequiresManipulation => true;

        protected override bool AppliesInt(FloatMenuContext context)
        {
            return ModsConfig.AnomalyActive;
        }

        public override IEnumerable<FloatMenuOption> GetOptionsFor(
            Thing clickedThing,
            FloatMenuContext context)
        {
            if (clickedThing is not Pawn pawn)
                yield break;

            if (!ConditioningUtility.IsFriendly(pawn))
                yield break;

            if (pawn.GetComp<CompHoldingPlatformTarget>() == null)
                yield break;

            Pawn selectedPawn = context.FirstSelectedPawn;

            if (!selectedPawn.CanReserveAndReach(
                    pawn,
                    PathEndMode.OnCell,
                    Danger.Deadly,
                    1,
                    -1,
                    null,
                    ignoreOtherReservations: true))
            {
                yield break;
            }

            IEnumerable<Building_HoldingPlatform> buildings =
                from x in selectedPawn.Map.listerBuildings.AllBuildingsColonistOfClass<Building_HoldingPlatform>()
                where !x.Occupied &&
                      selectedPawn.CanReserveAndReach(x, PathEndMode.Touch, Danger.Deadly)
                select x;

            Thing building = GenClosest.ClosestThing_Global_Reachable(
                selectedPawn.Position,
                selectedPawn.Map,
                buildings,
                PathEndMode.ClosestTouch,
                TraverseParms.For(selectedPawn, Danger.Some),
                9999f,
                null,
                delegate (Thing t)
                {
                    CompEntityHolder holder = t.TryGetComp<CompEntityHolder>();

                    return holder != null
                        ? holder.ContainmentStrength /
                          Mathf.Max(pawn.PositionHeld.DistanceTo(t.Position), 1f)
                        : 0f;
                });

            if (building == null)
                yield break;

            yield return FloatMenuUtility.DecoratePrioritizedTask(
                new FloatMenuOption(
                    "Return to Holding Platform",
                    delegate
                    {
                        Job job = JobMaker.MakeJob(
                            DefDatabase<JobDef>.GetNamed("TA_ReturnToHoldingPlatform"),
                            building,
                            pawn);

                        job.count = 1;

                        selectedPawn.jobs.TryTakeOrderedJob(job);
                    }),
                selectedPawn,
                pawn);
        }
    }
}
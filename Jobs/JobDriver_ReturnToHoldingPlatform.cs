using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Jobs
{
    public class JobDriver_ReturnToHoldingPlatform : JobDriver
    {
        private const TargetIndex DestHolderIndex = TargetIndex.A;
        private const TargetIndex TakeeIndex = TargetIndex.B;

        private Thing Takee => job.GetTarget(TakeeIndex).Thing;

        private CompEntityHolder DestHolder =>
            job.GetTarget(DestHolderIndex).Thing.TryGetComp<CompEntityHolder>();

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            if (pawn.Reserve(Takee, job, 1, -1, null, errorOnFailed))
            {
                return pawn.Reserve(DestHolder.parent, job, 1, -1, null, errorOnFailed);
            }

            return false;
        }
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(TakeeIndex);
            this.FailOnDespawnedNullOrForbidden(DestHolderIndex);
            this.FailOn(() => !DestHolder.Available);

            // If someone else took the platform while we were walking.
           // this.FailOn(() =>
             //   Takee.TryGetComp<CompHoldingPlatformTarget>().EntityHolder != DestHolder);

            if (pawn.carryTracker.CarriedThing != Takee)
            {
                yield return Toils_Goto.GotoThing(TakeeIndex, PathEndMode.OnCell);
            }

            yield return Toils_Haul.StartCarryThing(TakeeIndex);

            foreach (Toil toil in JobDriver_CarryToEntityHolder.ChainTakeeToPlatformToils(
                pawn,
                Takee,
                DestHolder,
                DestHolderIndex))
            {
                yield return toil;
            }
        }
    }
}
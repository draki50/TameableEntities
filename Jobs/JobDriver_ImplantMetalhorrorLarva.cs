using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Jobs
{
    public class JobDriver_ImplantMetalhorrorLarva : JobDriver
    {
        private const TargetIndex HostIndex = TargetIndex.A;

        private Pawn Host => (Pawn)job.GetTarget(HostIndex).Thing;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(Host, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(HostIndex);

            // Walk to the host
            yield return Toils_Goto.GotoThing(
                HostIndex,
                PathEndMode.Touch);

            // Wait a little while implanting
            Toil wait = Toils_General.Wait(180);

            wait.WithProgressBarToilDelay(
                HostIndex);

            yield return wait;

            // Actually implant the larva
            yield return Toils_General.Do(delegate
            {
                Host.health.AddHediff(
                    TameableAnomaliesHediffDefOf.MetalhorrorLarva);

                Host.needs?.mood?.thoughts?.memories?.TryGainMemory(
    TameableAnomaliesThoughtDefOf.TA_ImplantedMetalhorrorLarva,
    pawn);

                Find.TickManager.slower.SignalForceNormalSpeedShort();

                Ability ability =
                    pawn.abilities?.GetAbility(
                        TameableAnomaliesAbilityDefOf.TA_ImplantMetalhorrorLarva);

                ability?.StartCooldown(
                    ability.def.cooldownTicksRange.TrueMin);
            });
        }
    }
}
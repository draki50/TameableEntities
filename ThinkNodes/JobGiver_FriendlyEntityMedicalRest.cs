using RimWorld;
using Verse;
using Verse.AI;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;

namespace TameableAnomalies.ThinkNodes
{
    public class JobGiver_FriendlyEntityMedicalRest : ThinkNode_JobGiver
    {
        public Job GetJob(Pawn pawn)
        {
            return TryGiveJob(pawn);
        }

        protected override Job TryGiveJob(Pawn pawn)
        {

            CompConditionableEntity comp = pawn.TryGetComp<CompConditionableEntity>();

            if (comp == null)
            {
                return null;
            }


            float conditioning = ConditioningUtility.GetConditioning(pawn);

            if (conditioning < 40f)
            {
                return null;
            }

            bool seek = HealthAIUtility.ShouldSeekMedicalRest(pawn);

            if (!seek)
                return null;

            Building_Bed bed = RestUtility.FindBedFor(
                pawn,
                pawn,
                checkSocialProperness: false);

            if (bed == null)
                return null;

            Job job = JobMaker.MakeJob(JobDefOf.LayDown, bed);
            job.checkOverrideOnExpire = true;

            return job;
        }
    }
}
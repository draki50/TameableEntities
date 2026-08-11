using RimWorld;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Utilities
{
    public class EntityHaulJobGiver : JobGiver_Haul
    {
        public Job GetHaulJob(Pawn pawn)
        {
            Job job = TryGiveJob(pawn);

            return job;
        }
    }
}
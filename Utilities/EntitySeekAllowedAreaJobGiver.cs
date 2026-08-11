using RimWorld;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Utilities
{
    public class EntitySeekAllowedAreaJobGiver : JobGiver_SeekAllowedArea
    {
        public Job GetAllowedAreaJob(Pawn pawn)
        {
            Job job = TryGiveJob(pawn);

            return job;
        }
    }
}
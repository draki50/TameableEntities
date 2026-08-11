using RimWorld;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.ThinkNodes
{
    public class ThinkNode_ConditionalFriendlyShouldFollowMaster : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn)
        {
            if (!ConditioningUtility.IsFriendly(pawn))
                return false;

            return ThinkNode_ConditionalShouldFollowMaster.ShouldFollowMaster(pawn);
        }
    }
}
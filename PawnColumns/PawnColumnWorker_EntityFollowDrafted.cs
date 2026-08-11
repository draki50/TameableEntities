using RimWorld;
using Verse;
using TameableAnomalies.Utilities;

namespace TameableAnomalies.PawnColumns
{
    public class PawnColumnWorker_EntityFollowDrafted : PawnColumnWorker_Checkbox
    {
        private bool anyAnimalWithObedience;

        public override bool VisibleCurrently => anyAnimalWithObedience;

        public override void Recache()
        {
            anyAnimalWithObedience = false;

            foreach (Pawn pawn in Find.CurrentMap.mapPawns.AllPawnsSpawned)
            {
                if (!ConditioningUtility.IsFriendly(pawn))
                    continue;

                if (pawn.training != null &&
                    pawn.training.HasLearned(TrainableDefOf.Obedience))
                {
                    anyAnimalWithObedience = true;
                    break;
                }
            }
        }

        protected override bool HasCheckbox(Pawn pawn)
        {
            return ConditioningUtility.IsFriendly(pawn)
                && pawn.training != null
                && pawn.training.HasLearned(TrainableDefOf.Obedience);
        }

        protected override bool GetValue(Pawn pawn)
        {
            return pawn.playerSettings.followDrafted;
        }

        protected override void SetValue(Pawn pawn, bool value, PawnTable table)
        {
            pawn.playerSettings.followDrafted = value;
        }
    }
}
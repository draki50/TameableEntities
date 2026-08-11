using System.Collections.Generic;
using RimWorld;
using Verse;

namespace TameableAnomalies
{
    public class Recipe_RemoveMetalhorrorLarva : Recipe_Surgery
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (!(thing is Pawn pawn))
                return false;

            return pawn.health.hediffSet.HasHediff(
                TameableAnomaliesHediffDefOf.MetalhorrorLarva);
        }
        public override void ApplyOnPawn(
            Pawn pawn,
            BodyPartRecord part,
            Pawn billDoer,
            List<Thing> ingredients,
            Bill bill)
        {
            base.ApplyOnPawn(
                pawn,
                part,
                billDoer,
                ingredients,
                bill);

            Hediff larva =
                pawn.health.hediffSet.GetFirstHediffOfDef(
                    TameableAnomaliesHediffDefOf.MetalhorrorLarva);

            if (larva != null)
            {
                pawn.health.RemoveHediff(larva);

                Messages.Message(
                    pawn.LabelShortCap + "'s Metalhorror larva was successfully removed.",
                    pawn,
                    MessageTypeDefOf.PositiveEvent);
            }
        }
    }
}
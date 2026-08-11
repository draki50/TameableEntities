using Verse;

namespace TameableAnomalies
{
    public class HediffComp_MetalhorrorLarva : HediffComp
    { 
        private int ticksUntilBirth = 120000; // 60000 is one day. 120000 is two days.

        public float DaysRemaining
        {
            get
            {
                return ticksUntilBirth / 60000f;
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            ticksUntilBirth--;

            if (ticksUntilBirth == 0)
            {
                MetalhorrorUtility_Emerge.EmergeMetalhorror(
                    Pawn,
                    (Hediff_MetalhorrorLarva)parent);

                Pawn.health.RemoveHediff(parent);
            }
        }
        public override string CompLabelInBracketsExtra
        {
            get
            {
                float days = ticksUntilBirth / 60000f;

                return days.ToString("0.0") + " days";
            }
        }
    }
}
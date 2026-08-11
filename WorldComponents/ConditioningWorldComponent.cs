using RimWorld.Planet;
using System.Collections.Generic;
using Verse;

namespace TameableAnomalies.WorldComponents
{
    public class ConditioningWorldComponent : WorldComponent
    {
        private Dictionary<int, float> conditioning = new Dictionary<int, float>();
        public ConditioningWorldComponent(World world) : base(world)
        {
        }

        public float GetConditioning(Pawn pawn)
        {
            if (conditioning.TryGetValue(pawn.thingIDNumber, out float value))
               return value;

            return 0f;
        }

        public void SetConditioning(Pawn pawn, float value)
        {
            conditioning[pawn.thingIDNumber] = value;
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref conditioning,
                "ConditioningValues",
                LookMode.Value,
                LookMode.Value);
        }
    }
}

using RimWorld;
using TameableAnomalies.Utilities;
using Verse;
using System.Collections.Generic;

namespace TameableAnomalies.Components
{
    public class CompConditionableEntity : ThingComp
    {
        // public float Conditioning;
        public int LastReleasedTick = -1;
        public bool ConditionMode = false;
        public int ConditionDecayFrom = 0;

        //private const float DecayPercent = 0.01f; // 1% of max conditioning
        private const int DecayIntervalTicks = 60000; // The second number is how often in days

        public override void PostExposeData()
        {
            base.PostExposeData();

            // Scribe_Values.Look(ref Conditioning, "Conditioning", 0f);
            Scribe_Values.Look(ref LastReleasedTick, "LastReleasedTick", -1);
            Scribe_Values.Look(ref ConditionMode, "ConditionMode", false);
            Scribe_Values.Look(ref ConditionDecayFrom, "ConditionDecayFrom", 0);
        }
        public override void PostPostMake()
        {
            base.PostPostMake();

            ConditionDecayFrom = Find.TickManager.TicksGame;
        }
        public override void CompTickRare()
        {
            base.CompTickRare();


            if (parent is not Pawn pawn)
            {
                return;
            }

            float currentConditioning = ConditioningUtility.GetConditioning(pawn);

            if (currentConditioning <= 0f)
            {
                ConditionDecayFrom = Find.TickManager.TicksGame;
                return;
            }

            if (pawn.IsOnHoldingPlatform)
            {
                ConditionDecayFrom = Find.TickManager.TicksGame;
                return;
            }

            if (Find.TickManager.TicksGame < ConditionDecayFrom + DecayIntervalTicks)
            {
                return;
            }

            ConditionDecayFrom = Find.TickManager.TicksGame;

            float maxConditioning = ConditioningUtility.GetRequiredConditioning(parent.def);

            float decayAmount =
                maxConditioning *
                (TameableAnomaliesMod.settings.conditioningDecayPerDay / 100f);

            ConditioningUtility.AddConditioning(
                pawn,
                -decayAmount);

        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
                yield return gizmo;

            if (!Prefs.DevMode)
                yield break;

            if (parent is not Pawn pawn)
                yield break;

            if (!ConditioningUtility.CanBeConditioned(pawn))
                yield break;

            yield return new Command_Action
            {
                defaultLabel = "-10 Condition",
                defaultDesc = "Removes 10 conditioning for testing.",
                action = delegate
                {
                    ConditioningUtility.AddConditioning(pawn, -10f);
                }
            };
        }
    }
}
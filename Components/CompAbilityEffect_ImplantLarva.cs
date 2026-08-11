using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace TameableAnomalies
{
    public class CompAbilityEffect_ImplantLarva : CompAbilityEffect
    {
        public override bool GizmoDisabled(out string reason)
        {
            Pawn pawn = parent.pawn;

            TrainableDef trainable =
                DefDatabase<TrainableDef>.GetNamed("ImplantLarva");

            if (parent.pawn.ageTracker.AgeBiologicalTicks < 18 * GenDate.TicksPerDay)
            {
                reason = "Too young to implant larvae. Must be 18 days or older.";
                return true;
            }

            if (!pawn.training.HasLearned(trainable))
            {
                reason = "Requires Implant Larva training.";
                return true;
            }
            return base.GizmoDisabled(out reason);
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;

            if (pawn == null)
                return false;
            
            if (pawn.Dead)
                return false;

            if (!pawn.RaceProps.Humanlike)
                return false;

            if (pawn.health.hediffSet.HasHediff(TameableAnomaliesHediffDefOf.MetalhorrorLarva))
            {
                if (throwMessages)
                {
                    Messages.Message(
                        "Target already carries a Metalhorror larva.",
                        MessageTypeDefOf.RejectInput,
                        false);
                }

                return false;
            }
            return base.Valid(target, throwMessages);
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn caster = parent.pawn;
            Pawn victim = target.Pawn;

            Job job = JobMaker.MakeJob(
                TameableAnomaliesJobDefOf.TA_ImplantMetalhorrorLarva,
                victim);

            caster.jobs.TryTakeOrderedJob(job);
        }

        //        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        //        {
        //            Log.Error("========== NEW APPLY ==========");
        //            base.Apply(target, dest);

        //            Pawn pawn = parent.pawn;

        //            List<FloatMenuOption> options = new();

        //            foreach (Pawn victim in pawn.Map.mapPawns.AllPawnsSpawned)
        //            {
        //                if (victim == pawn)
        //                    continue;

        //                // Only humans
        //                if (!victim.RaceProps.Humanlike)
        //                    continue;

        //                // Allow player pawns OR prisoners
        //                bool validTarget =
        //                    victim.Faction == Faction.OfPlayer ||
        //                    victim.IsPrisoner;

        //                if (!validTarget)
        //                    continue;

        //                if (victim.Dead)
        //                    continue;

        //                // Already infected
        //                string status;

        //                Hediff_MetalhorrorLarva larva =
        //                    victim.health.hediffSet.GetFirstHediffOfDef(
        //                        TameableAnomaliesHediffDefOf.MetalhorrorLarva)
        //                    as Hediff_MetalhorrorLarva;

        //                bool alreadyImplanted = larva != null;

        //                string implantTime = "";

        //                if (alreadyImplanted)
        //                {
        //                    HediffComp_MetalhorrorLarva comp =
        //                        larva.TryGetComp<HediffComp_MetalhorrorLarva>();

        //                    if (comp != null)
        //                        implantTime = $" - {comp.DaysRemaining:0.0} days";
        //                }

        //                if (alreadyImplanted)
        //                    status = $"Already Implanted{implantTime}";
        //                else if (victim.IsPrisoner)
        //                    status = "Prisoner";
        //                else if (victim.IsSlave)
        //                    status = "Slave";
        //                else
        //                    status = "Colonist";

        //                if (alreadyImplanted)
        //                {
        //                    options.Add(new FloatMenuOption(
        //                        $"{victim.LabelShortCap} ({status})",
        //                        null,
        //                        victim,
        //                        Color.white));
        //                }
        //                else
        //                {
        //                    options.Add(new FloatMenuOption(
        //                        $"{victim.LabelShortCap} ({status})",
        //                        () =>
        //                        {
        //Log.Message("[TA] Selected victim = " + victim.LabelShort);

        //Job job = JobMaker.MakeJob(
        //    TameableAnomaliesJobDefOf.TA_ImplantMetalhorrorLarva,
        //    victim);

        //Log.Message("[TA] Job target = " + job.targetA);

        //pawn.jobs.TryTakeOrderedJob(job);
        //                        },
        //                        victim,
        //                        Color.white));
        //                }
        //            }

        //            Find.WindowStack.Add(new Verse.FloatMenu(options));

        //        }
    }
}
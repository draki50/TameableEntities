using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace TameableAnomalies
{
    public class CompLarvaImplanter : ThingComp
    {
        private int nextImplantTick = 0;

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(
                ref nextImplantTick,
                "nextImplantTick",
                0);
        }
        //public override IEnumerable<Gizmo> CompGetGizmosExtra()
        //{
        //    foreach (Gizmo gizmo in base.CompGetGizmosExtra())
        //        yield return gizmo;

        //    Pawn pawn = parent as Pawn;

        //    if (pawn == null)
        //        yield break;

        //    if (pawn.Faction != Faction.OfPlayer)
        //        yield break;

        //    yield return new Command_Action
        //    {
        //        defaultLabel = "Implant Metalhorror Larva",
        //        defaultDesc = "Implant a Metalhorror larva into a host.",
        //        icon = TexCommand.Attack,

        //        action = delegate
        //        {
        //            List<FloatMenuOption> options = new();

        //            //foreach (Pawn target in pawn.Map.mapPawns.AllPawnsSpawned)
        //            //{
        //            //    if (target == pawn)
        //            //        continue;

        //            //    // Only humans
        //            //    if (!target.RaceProps.Humanlike)
        //            //        continue;

        //            //    // Allow player pawns OR prisoners
        //            //    bool validTarget =
        //            //        target.Faction == Faction.OfPlayer ||
        //            //        target.IsPrisoner;

        //            //    if (!validTarget)
        //            //        continue;

        //            //    if (target.Dead)
        //            //        continue;

        //            //    // Already infected
        //            //    string status;

        //            //    Hediff_MetalhorrorLarva larva =
        //            //        target.health.hediffSet.GetFirstHediffOfDef(
        //            //            TameableAnomaliesHediffDefOf.MetalhorrorLarva)
        //            //        as Hediff_MetalhorrorLarva;

        //            //    bool alreadyImplanted = larva != null;

        //            //    string implantTime = "";

        //            //    if (alreadyImplanted)
        //            //    {
        //            //        HediffComp_MetalhorrorLarva comp =
        //            //            larva.TryGetComp<HediffComp_MetalhorrorLarva>();

        //            //        if (comp != null)
        //            //            implantTime = $" - {comp.DaysRemaining:0.0} days";
        //            //    }

        //            //    if (alreadyImplanted)
        //            //        status = $"Already Implanted{implantTime}";
        //            //    else if (target.IsPrisoner)
        //            //        status = "Prisoner";
        //            //    else if (target.IsSlave)
        //            //        status = "Slave";
        //            //    else
        //            //        status = "Colonist";

        //            //    if (alreadyImplanted)
        //            //    {
        //            //        options.Add(new FloatMenuOption(
        //            //            $"{target.LabelShortCap} ({status})",
        //            //            null,
        //            //            target,
        //            //            Color.white));
        //            //    }
        //            //    else
        //            //    {
        //            //        options.Add(new FloatMenuOption(
        //            //            $"{target.LabelShortCap} ({status})",
        //            //            () =>
        //            //            {
        //            //                Job job = JobMaker.MakeJob(
        //            //                    TameableAnomaliesJobDefOf.TA_ImplantMetalhorrorLarva,
        //            //                    target);

        //            //                pawn.jobs.TryTakeOrderedJob(job);

        //            //            },
        //            //            target,
        //            //            Color.white));
        //            //    }
        //            //}

        //            Find.WindowStack.Add(new Verse.FloatMenu(options));
        //        }
        //    };
        //}
    }
}
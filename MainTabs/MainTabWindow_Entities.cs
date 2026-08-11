using RimWorld;
using System.Collections.Generic;
using TameableAnomalies.Utilities;
using System.Linq;
using UnityEngine;
using Verse;

namespace TameableAnomalies.MainTabs
{
    public class MainTabWindow_Entities : MainTabWindow_PawnTable
    {
        protected override PawnTableDef PawnTableDef => PawnTableDefOf.Entities;
        protected override IEnumerable<Pawn> Pawns =>Find.CurrentMap.mapPawns.AllPawnsSpawned.Where(p =>ConditioningUtility.IsFriendly(p) && p.Faction == Faction.OfPlayer);
        public override void DoWindowContents(Rect rect)
        {
            base.DoWindowContents(rect);
        }
    }
}
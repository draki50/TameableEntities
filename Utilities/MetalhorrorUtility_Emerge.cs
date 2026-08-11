using RimWorld;
using TameableAnomalies.Utilities;
using TameableAnomalies.WorldComponents;
using Verse;

namespace TameableAnomalies
{
    public static class MetalhorrorUtility_Emerge
    {
        public static Pawn EmergeMetalhorror(
            Pawn infected,
            Hediff_MetalhorrorLarva larva)
        {
            Pawn metalhorror = PawnGenerator.GeneratePawn(
                new PawnGenerationRequest(
                    PawnKindDefOf.Metalhorror,
                    Faction.OfPlayer));

            ConditioningUtility.SetConditioning(metalhorror, 100f);

            //metalhorror.ageTracker.LockCurrentLifeStageIndex(0);

            metalhorror.ageTracker.AgeBiologicalTicks = 0;
            metalhorror.ageTracker.AgeChronologicalTicks = 0;

            if (!GenAdj.TryFindRandomAdjacentCell8WayWithRoom(
                infected.SpawnedParentOrMe,
                out IntVec3 spawnCell))
            {
                spawnCell = infected.PositionHeld;
            }

            HealthUtility.DamageUntilDowned(
            infected,
            allowBleedingWounds: true,
            DamageDefOf.Cut,
            ThingDefOf.Metalhorror,
            BodyPartGroupDefOf.LeftBlade);

            Pawn spawned = (Pawn)GenSpawn.Spawn(
                metalhorror,
                spawnCell,
                infected.MapHeld);

            spawned.stances.stunner.StunFor(
                2500,
                null,
                addBattleLog: false);

            return spawned;
        }
    }
}
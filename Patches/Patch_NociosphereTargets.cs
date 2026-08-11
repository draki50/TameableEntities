using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(NociosphereUtility), nameof(NociosphereUtility.FindTarget))]
    public static class Patch_Nociosphere_FindTarget
    {
        public static bool Prefix(Pawn pawn, ref Thing __result)
        {
            // Vanilla behaviour for hostile nociospheres.
            if (!ConditioningUtility.IsConditioned(pawn))
                return true;

            Pawn target = GenClosest.ClosestThingReachable(
                pawn.Position,
                pawn.Map,
                ThingRequest.ForGroup(ThingRequestGroup.Pawn),
                PathEndMode.Touch,
                TraverseParms.For(pawn),
                25f,
                t =>
                {
                    if (t is not Pawn p)
                        return false;

                    // Never target ourselves.
                    if (p == pawn)
                        return false;

                    // Ignore downed pawns.
                    if (p.Downed)
                        return false;

                    // Only attack pawns hostile to this nociosphere.
                    if (p.Faction == null)
                        return false;

                    return p.Faction != pawn.Faction;
                }) as Pawn;

            __result = target;
            return false;
        }
    }
}
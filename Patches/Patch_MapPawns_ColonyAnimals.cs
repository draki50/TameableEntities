using System.Collections.Generic;
using HarmonyLib;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(MapPawns), nameof(MapPawns.ColonyAnimals), MethodType.Getter)]
    public static class Patch_MapPawns_ColonyAnimals
    {
        public static void Postfix(ref List<Pawn> __result)
        {
            __result.RemoveAll(p => ConditioningUtility.IsFriendly(p));
        }
    }
}
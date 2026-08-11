using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(Pawn_Thinker), nameof(Pawn_Thinker.MainThinkTree), MethodType.Getter)]
    public static class Patch_PawnThinker_MainThinkTree
    {
        public static bool Prefix(Pawn_Thinker __instance, ref ThinkTreeDef __result)
        {
            Pawn pawn = __instance.pawn;

            if (!ConditioningUtility.IsFriendly(pawn))
                return true;

            // The Nociosphere keeps its vanilla AI.
            if (pawn.def.defName == "Nociosphere")
                return true;

            __result = DefDatabase<ThinkTreeDef>.GetNamed("FriendlyEntity");

            return false;
        }
    }
}
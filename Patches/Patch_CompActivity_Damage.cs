using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompActivity), nameof(CompActivity.PostPostApplyDamage))]
    public static class Patch_CompActivity_Damage
    {
        public static bool Prefix(CompActivity __instance)
        {
            if (__instance.parent is Pawn pawn &&
                pawn.def.defName == "Nociosphere")
            {
                if (ConditioningUtility.GetDisplayedConditioning(pawn) >= 10f)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
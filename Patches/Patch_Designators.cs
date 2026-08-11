using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    //========================================================
    // Slaughter
    //========================================================
    [HarmonyPatch(typeof(Designator_Slaughter), nameof(Designator_Slaughter.CanDesignateThing))]
    public static class Patch_Designator_Slaughter
    {
        public static void Postfix(Thing t, ref AcceptanceReport __result)
        {
            if (!__result.Accepted)
                return;

            if (t is not Pawn pawn)
                return;

            if (ConditioningUtility.IsConditioned(pawn))
                __result = false;
        }
    }

    //========================================================
    // Release to Wild
    //========================================================
    [HarmonyPatch(typeof(Designator_ReleaseAnimalToWild), nameof(Designator_ReleaseAnimalToWild.CanDesignateThing))]
    public static class Patch_Designator_ReleaseAnimalToWild
    {
        public static void Postfix(Thing t, ref AcceptanceReport __result)
        {
            if (!__result.Accepted)
                return;

            if (t is not Pawn pawn)
                return;

            if (ConditioningUtility.IsConditioned(pawn))
                __result = false;
        }
    }

    //========================================================
    // Hunt
    //========================================================
    [HarmonyPatch(typeof(Designator_Hunt), nameof(Designator_Hunt.CanDesignateThing))]
    public static class Patch_Designator_Hunt
    {
        public static void Postfix(Thing t, ref AcceptanceReport __result)
        {
            if (!__result.Accepted)
                return;

            if (t is not Pawn pawn)
                return;

            if (ConditioningUtility.CanBeConditioned(pawn))
                __result = false;
        }
    }

    //========================================================
    // Tame
    //========================================================
    [HarmonyPatch(typeof(TameUtility), nameof(TameUtility.CanTame))]
    public static class Patch_TameUtility
    {
        public static void Postfix(Pawn pawn, ref bool __result)
        {
            if (!__result)
                return;

            if (ConditioningUtility.CanBeConditioned(pawn))
                __result = false;
        }
    }
}
using HarmonyLib;
using RimWorld;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(TrainableUtility), nameof(TrainableUtility.CanBeMaster))]
    public static class Patch_TrainableUtility_CanBeMaster
    {
        public static bool Prefix(
            Pawn master,
            Pawn animal,
            bool checkSpawned,
            ref bool __result)
        {
            CompConditionableEntity comp =
                animal.TryGetComp<CompConditionableEntity>();

            if (comp == null)
            {
                // Not one of our entities, let vanilla handle it.
                return true;
            }

            ConditioningProfile profile =
                ConditioningUtility.GetProfile(animal);

            if (profile == null)
            {
                return true;
            }

            if ((checkSpawned && !master.Spawned) || master.IsPrisoner)
            {
                __result = false;
                return false;
            }

            int animals =
                master.skills.GetSkill(SkillDefOf.Animals).Level;

            int intellectual =
                master.skills.GetSkill(SkillDefOf.Intellectual).Level;

            __result =
                animals >= profile.MinAnimals &&
                intellectual >= profile.MinIntellectual;

            return false;
        }
    }
}
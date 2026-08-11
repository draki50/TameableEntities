using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using TameableAnomalies.Components;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(JobGiver_AIFightEnemy), "TryGiveJob")]
    public static class Patch_JobGiver_AIFightEnemy
    {
        public static bool Prefix(JobGiver_AIFightEnemy __instance, Pawn pawn, ref Job __result)
        {
            return true;
        }

        private static bool IsColonistForCombat(Pawn pawn)
        {
            if (pawn.TryGetComp<CompConditionableEntity>() != null)
                return false;

            return pawn.IsColonist;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);

            MethodInfo isColonistGetter = AccessTools.PropertyGetter(typeof(Pawn), nameof(Pawn.IsColonist));
            MethodInfo replacement = AccessTools.Method(typeof(Patch_JobGiver_AIFightEnemy), nameof(IsColonistForCombat));

            bool replaced = false;

            for (int i = 0; i < code.Count; i++)
            {
                if (!replaced && code[i].Calls(isColonistGetter))
                {
                    code[i] = new CodeInstruction(OpCodes.Call, replacement);
                    replaced = true;
                }
            }

            return code;
        }
    }
}
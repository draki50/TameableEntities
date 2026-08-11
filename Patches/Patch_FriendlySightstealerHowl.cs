using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using TameableAnomalies.Utilities;
using Verse;
using Verse.Sound;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(CompSightstealer), nameof(CompSightstealer.Notify_BecameVisible))]
    public static class Patch_FriendlySightstealerHowl
    {
        static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo originalMethod = AccessTools.Method(
                typeof(SoundStarter),
                "PlayOneShotOnCamera");

            MethodInfo replacementMethod = AccessTools.Method(
                typeof(Patch_FriendlySightstealerHowl),
                nameof(PlayHowlIfHostile));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call &&
                    instruction.operand is MethodInfo method &&
                    method == originalMethod)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    instruction.operand = replacementMethod;
                }

                yield return instruction;
            }
        }

        public static void PlayHowlIfHostile(
            SoundDef soundDef,
            Map map,
            CompSightstealer comp)
        {
            Pawn pawn = comp.parent as Pawn;

            if (pawn != null &&
                ConditioningUtility.IsFriendly(pawn))
            {
                return;
            }

            SoundStarter.PlayOneShotOnCamera(soundDef, map);
        }
    }
}
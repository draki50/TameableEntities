using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(
        typeof(LetterStack),
        nameof(LetterStack.ReceiveLetter),
        new[]
        {
        typeof(TaggedString),
        typeof(TaggedString),
        typeof(LetterDef),
        typeof(LookTargets),
        typeof(Faction),
        typeof(Quest),
        typeof(List<ThingDef>),
        typeof(string),
        typeof(int),
        typeof(bool)
        })]
    public static class Patch_FriendlyLetters
    {
        static bool Prefix(
            TaggedString label,
            TaggedString text,
            LetterDef textLetterDef,
            LookTargets lookTargets,
            Faction relatedFaction,
            Quest quest,
            List<ThingDef> hyperlinkThingDefs,
            string debugInfo,
            int delayTicks,
            bool playSound)
        {
            Pawn pawn = lookTargets.PrimaryTarget.Thing as Pawn;

            if (pawn == null)
                return true;

            if (!Utilities.ConditioningUtility.IsFriendly(pawn))
                return true;

            if (label == "LetterLabelRevenantRevealed".Translate() || label == "LetterLabelSightstealerRevealed".Translate())
            {
                return false;
            }

            return true;
        }

    }

    [HarmonyPatch(
        typeof(Messages),
        nameof(Messages.Message),
        new[]
        {
        typeof(string),
        typeof(LookTargets),
        typeof(MessageTypeDef),
        typeof(bool)
        })]
    public static class Patch_FriendlyMessages
    {
        static bool Prefix(
            string text,
            LookTargets lookTargets,
            MessageTypeDef def,
            bool historical)
        {
            Pawn pawn = lookTargets.PrimaryTarget.Thing as Pawn;

            if (pawn == null)
                return true;

            if (!Utilities.ConditioningUtility.IsFriendly(pawn))
                return true;

            if (text == "MessageSightstealerRevealed".Translate())
            {
                return false;
            }

            return true;
        }
    }
}
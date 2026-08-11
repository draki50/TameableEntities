using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(FloatMenuOptionProvider_WorkGivers), "GetOptionsFor", new[] { typeof(Thing), typeof(FloatMenuContext) })]
    public static class Patch_FloatMenuOptionProvider_WorkGivers
    {
        public static void Postfix(
            Thing clickedThing,
            FloatMenuContext context,
            ref IEnumerable<FloatMenuOption> __result)
        {
            List<FloatMenuOption> options = __result?.ToList() ?? new List<FloatMenuOption>();

            if (clickedThing is not Building_HoldingPlatform platform)
            {
                __result = options;
                return;
            }

            Pawn entity = platform.HeldPawn;

            if (entity == null)
            {
                __result = options;
                return;
            }

            CompStudiable studiable = entity.TryGetComp<CompStudiable>();
            CompConditionableEntity comp = entity.TryGetComp<CompConditionableEntity>();

            if (comp == null)
            {
                __result = options;
                return;
            }

            if (!comp.ConditionMode)
            {
                __result = options;
                return;
            }

            options.RemoveAll(o =>
                o.Label.ToLower().Contains("study"));

            Pawn pawn = context.FirstSelectedPawn;

            bool meetsRequirements =
                ConditioningUtility.MeetsSkillRequirements(
                    pawn,
                    entity);

            ConditioningProfile profile =
                ConditioningUtility.GetProfile(entity);

            bool onCooldown =
    studiable != null &&
    studiable.TicksTilNextStudy > 0;

            string label;

            if (!meetsRequirements)
            {
                label =
                    $"Condition Entity\nRequires Animals {profile.MinAnimals}, Intellectual {profile.MinIntellectual}";
            }
            else if (onCooldown)
            {
                label =
                    "ConditionEntity".Translate() + "\n" +
                    "CanBeConditionedInDuration".Translate(
                        studiable.TicksTilNextStudy.ToStringTicksToPeriod());
            }
            else
            {
                label = "Condition Entity";
            }

            FloatMenuOption option;

            if (meetsRequirements && !onCooldown)
            {
                option = new FloatMenuOption(
                    label,
                    () =>
                    {
                        Job job =
                            JobMaker.MakeJob(
                                JobDefOf.StudyInteract,
                                platform);

                        pawn.jobs.TryTakeOrderedJob(job);
                    });
            }
            else
            {
                option = new FloatMenuOption(label, null);
            }

            options.Add(option);

            __result = options;


        }
    }
}
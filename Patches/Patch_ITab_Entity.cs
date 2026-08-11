using HarmonyLib;
using RimWorld;
using System.Text;
using TameableAnomalies.Components;
using UnityEngine;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(ITab_Entity), "FillTab")]
    public static class Patch_ITab_Entity
    {
        private static readonly AccessTools.FieldRef<InspectTabBase, Vector2> SizeRef =
    AccessTools.FieldRefAccess<InspectTabBase, Vector2>("size");

        [HarmonyPrefix]
        public static bool Prefix(ITab_Entity __instance)
        {
            Vector2 size = SizeRef(__instance);

            Rect rect = new Rect(0f, 0f, size.x, size.y).ContractedBy(10f);
            Listing_Standard listing = new Listing_Standard();
            listing.maxOneColumn = true;
            listing.Begin(rect);

            Pawn selPawn = Traverse.Create(__instance).Property<Pawn>("SelPawn").Value;

            if (selPawn == null)
            {
                listing.End();
                return false;
            }
            if (selPawn.ParentHolder is Thing thing &&
    thing.TryGetComp(out CompEntityHolder comp))
            {
                StatDef containmentStrength = StatDefOf.ContainmentStrength;
                float containmentValue = comp.ContainmentStrength;

                string explanation =
                    containmentStrength.description +
                    "\n\n" +
                    containmentStrength.Worker.GetExplanationFull(
                        StatRequest.For(thing),
                        containmentStrength.toStringNumberSense,
                        containmentValue);

                Widgets.DrawHighlightIfMouseover(
                    listing.Label(
                        containmentStrength.LabelCap + ": " +
                        containmentStrength.ValueToString(containmentValue),
                        -1f,
                        new TipSignal(explanation, __instance.GetHashCode() + 1)));
            }

            StringBuilder escapeExplanation = new StringBuilder();

            float mtbDays =
                ContainmentUtility.InitiateEscapeMtbDays(
                    selPawn,
                    escapeExplanation);

            int numTicks = Mathf.FloorToInt(mtbDays * 60000f);

            TaggedString escapeLabel =
                "HoldingPlatformEscapeMTBDays".Translate() + ": ";

            if (mtbDays < 0f)
            {
                escapeLabel += "Never".Translate();
            }
            else
            {
                escapeLabel +=
                    numTicks.ToStringTicksToPeriod()
                    .Colorize(ColoredText.DateTimeColor);
            }

            string tooltip =
                "HoldingPlatformEscapeMTBDaysDesc".Translate();

            if (escapeExplanation.Length > 0)
            {
                tooltip += "\n\n" + escapeExplanation;
            }

            Widgets.DrawHighlightIfMouseover(
                listing.Label(
                    escapeLabel,
                    -1f,
                    new TipSignal(
                        tooltip,
                        __instance.GetHashCode() + 2)));

            CompStudiable studiable =
                selPawn.TryGetComp<CompStudiable>();

            if (studiable != null)
            {
                ITab_Entity.DoStudyPeriodListing(
                    listing,
                    studiable);

                ITab_Entity.DoKnowledgeGainListing(
                    listing,
                    studiable);
            }

            listing.Gap(1f);
            Rect medicineRect = listing.GetRect(24f).Rounded();

            TooltipHandler.TipRegionByKey(
                medicineRect,
                "MedicineQualityDescriptionEntity");

            Widgets.DrawHighlightIfMouseover(medicineRect);

            Rect leftRect = medicineRect;
            leftRect.xMax = medicineRect.center.x - 4f;

            Rect rightRect = medicineRect;
            rightRect.xMin = medicineRect.center.x + 4f;

            Text.Anchor = TextAnchor.MiddleLeft;

            Widgets.Label(
                leftRect,
                string.Format("{0}:", "AllowMedicine".Translate()));

            Text.Anchor = TextAnchor.UpperLeft;

            Widgets.DrawButtonGraphic(rightRect);

            MedicalCareUtility.MedicalCareSelectButton(
                rightRect,
                selPawn);

            listing.Gap(4f);
            CompHoldingPlatformTarget platformTarget =
    selPawn.TryGetComp<CompHoldingPlatformTarget>();

            if (platformTarget != null)
            {
                float height = 160f;

                Rect sectionRect = listing.GetRect(height).Rounded();

                Widgets.DrawMenuSection(sectionRect);

                Rect contents = sectionRect.ContractedBy(10f);

                Widgets.BeginGroup(contents);

                Rect buttonRect = new Rect(0f, 0f, contents.width, 28f);
                CompConditionableEntity conditionComp =
    selPawn.TryGetComp<CompConditionableEntity>();

                if (Widgets.RadioButtonLabeled(
                    buttonRect,
                    "EntityStudyMode_MaintainOnly".Translate(),
                    platformTarget.containmentMode == EntityContainmentMode.MaintainOnly &&
                    (conditionComp == null || !conditionComp.ConditionMode)))
                {
                    platformTarget.containmentMode = EntityContainmentMode.MaintainOnly;

                    if (conditionComp != null)
                    {
                        conditionComp.ConditionMode = false;
                    }
                }

                Widgets.DrawHighlightIfMouseover(buttonRect);

                TooltipHandler.TipRegion(
                    buttonRect,
                    "EntityStudyMode_MaintainOnlyDesc".Translate());

                buttonRect.y += 28f;

                if (Widgets.RadioButtonLabeled(
                    buttonRect,
                    "EntityStudyMode_Study".Translate(),
                    platformTarget.containmentMode == EntityContainmentMode.Study &&
                    (conditionComp == null || !conditionComp.ConditionMode)))
                {
                    platformTarget.containmentMode = EntityContainmentMode.Study;

                    if (conditionComp != null)
                    {
                        conditionComp.ConditionMode = false;
                    }
                }

                Widgets.DrawHighlightIfMouseover(buttonRect);

                TooltipHandler.TipRegion(
                    buttonRect,
                    "EntityStudyMode_StudyDesc".Translate());

                buttonRect.y += 28f;

                bool conditionSelected =
                    conditionComp != null &&
                    conditionComp.ConditionMode;

                if (Widgets.RadioButtonLabeled(
                    buttonRect,
                    "Condition",
                    conditionSelected))
                {
                    platformTarget.containmentMode = EntityContainmentMode.Study;
                    if (conditionComp != null)
                    {
                        conditionComp.ConditionMode = true;
                    }
                }

                Widgets.DrawHighlightIfMouseover(buttonRect);

                TooltipHandler.TipRegion(
                    buttonRect,
                    "Colonists will condition this entity instead of studying it.");

                buttonRect.y += 28f;

                if (Widgets.RadioButtonLabeled(
                    buttonRect,
                    "EntityStudyMode_Release".Translate(),
                    platformTarget.containmentMode == EntityContainmentMode.Release))
                {
                    platformTarget.containmentMode = EntityContainmentMode.Release;

                    if (conditionComp != null)
                    {
                        conditionComp.ConditionMode = false;
                    }
                }

                Widgets.DrawHighlightIfMouseover(buttonRect);

                TooltipHandler.TipRegion(
                    buttonRect,
                    "EntityStudyMode_ReleaseDesc".Translate());

                buttonRect.y += 28f;
                if (Widgets.RadioButtonLabeled(
                    buttonRect,
                    "EntityStudyMode_Execute".Translate(),
                    platformTarget.containmentMode == EntityContainmentMode.Execute,
                    !platformTarget.Props.canBeExecuted))
                {
                    if (!platformTarget.Props.canBeExecuted)
                    {
                        Messages.Message(
                            "CantBeExecuted".Translate(),
                            MessageTypeDefOf.RejectInput,
                            historical: false);
                    }
                    else
                    {
                        platformTarget.containmentMode = EntityContainmentMode.Execute;

                        if (conditionComp != null)
                        {
                            conditionComp.ConditionMode = false;
                        }
                    }
                }

                Widgets.DrawHighlightIfMouseover(buttonRect);

                TooltipHandler.TipRegion(
                    buttonRect,
                    "EntityStudyMode_ExecuteDesc".Translate() +
                    (platformTarget.Props.canBeExecuted
                        ? ""
                        : ("\n\n" + "CantBeExecuted".Translate().ToString())));

                buttonRect.y += 28f;
                Widgets.EndGroup();

                listing.Gap();
                height = 48f;
                Rect bioRect = listing.GetRect(height).Rounded();

                Widgets.DrawMenuSection(bioRect);

                Rect bioContents = bioRect.ContractedBy(10f);

                Widgets.BeginGroup(bioContents);

                string disabledReason = null;

                if (!ResearchProjectDefOf.BioferriteExtraction.IsFinished)
                {
                    disabledReason = "RequiresBioferriteExtraction".Translate();
                }
                else
                {
                    Building_HoldingPlatform heldPlatform =
                        platformTarget?.HeldPlatform;

                    if (heldPlatform != null &&
                        heldPlatform.HasAttachedBioferriteHarvester)
                    {
                        disabledReason = "BioferriteHarvesterAttached".Translate();
                    }
                }

                Rect extractRect = new Rect(0f, 0f, bioContents.width, 28f);

                Widgets.CheckboxLabeled(
                    extractRect,
                    "EntityStudyMode_Extract".Translate(),
                    ref platformTarget.extractBioferrite,
                    disabledReason != null);

                Widgets.DrawHighlightIfMouseover(extractRect);

                TaggedString extractTooltip =
                    "EntityStudyMode_ExtractDesc".Translate();

                if (disabledReason != null)
                {
                    extractTooltip +=
                        "\n\n" +
                        disabledReason.Colorize(ColoredText.WarningColor);
                }

                TooltipHandler.TipRegion(
                    extractRect,
                    extractTooltip);

                Widgets.EndGroup();

            }

            listing.End();

            SizeRef(__instance) =
                new Vector2(
                    280f,
                    listing.CurHeight + 10f + 24f);

            return false;
        }
    }
}
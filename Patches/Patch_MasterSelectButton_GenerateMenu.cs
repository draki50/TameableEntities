using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using TameableAnomalies.Components;
using TameableAnomalies.Utilities;
using Verse;

namespace TameableAnomalies.Patches
{
    [HarmonyPatch(typeof(TrainableUtility), "MasterSelectButton_GenerateMenu")]
    public static class Patch_MasterSelectButton_GenerateMenu
    {
        public static void Postfix(
            Pawn p,
            ref IEnumerable<Widgets.DropdownMenuElement<Pawn>> __result)
        {
            CompConditionableEntity comp =
                p.TryGetComp<CompConditionableEntity>();

            if (comp == null)
                return;

            ConditioningProfile profile =
                ConditioningUtility.GetProfile(p);

            if (profile == null)
                return;

            List<Widgets.DropdownMenuElement<Pawn>> list =
                new List<Widgets.DropdownMenuElement<Pawn>>();

            foreach (Widgets.DropdownMenuElement<Pawn> element in __result)
            {
                if (element.payload == null)
                {
                    list.Add(element);
                    continue;
                }

                Pawn candidate = element.payload;

                if (element.option.Disabled)
                {
                    string label =
                        $"{candidate.Label}\nRequires Animals {profile.MinAnimals}, Intellectual {profile.MinIntellectual}";

                    list.Add(
                        new Widgets.DropdownMenuElement<Pawn>
                        {
                            payload = candidate,
                            option = new FloatMenuOption(
                                label,
                                null)
                        });

                    continue;
                }

                list.Add(element);
            }

            __result = list;

            __result = list;
        }
    }
}
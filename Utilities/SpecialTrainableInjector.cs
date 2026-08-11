//using RimWorld;
//using System.Collections.Generic;
//using TameableAnomalies.Defs;
//using Verse;

//namespace TameableAnomalies.Utilities
//{
//    [StaticConstructorOnStartup]
//    public static class SpecialTrainableInjector
//    {
//        static SpecialTrainableInjector()
//        {
//            AddSpecialTrainable(
//                TameableAnomaliesThingDefOf.Sightstealer,
//                "Haul");

//            AddSpecialTrainable(
//                TameableAnomaliesThingDefOf.Revenant,
//                "Haul");

//            // We'll add the rest later.
//        }

//        private static void AddSpecialTrainable(ThingDef thingDef, string trainableName)
//        {
//            if (thingDef?.race == null)
//                return;

//            TrainableDef trainable =
//                DefDatabase<TrainableDef>.GetNamedSilentFail(trainableName);

//            if (trainable == null)
//                return;

//            if (thingDef.race.specialTrainables == null)
//                thingDef.race.specialTrainables = new List<TrainableDef>();

//            if (!thingDef.race.specialTrainables.Contains(trainable))
//                thingDef.race.specialTrainables.Add(trainable);

//            Log.Message($"Added {trainable.defName} to {thingDef.defName}. Count = {thingDef.race.specialTrainables.Count}");
//        }
//    }
//}
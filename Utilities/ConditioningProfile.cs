using System.Collections.Generic;
using Verse;

namespace TameableAnomalies.Utilities
{
    public class ConditioningProfile
    {
        public float RequiredConditioning;
        public float Difficulty = 1f;

        public int MinAnimals;

        public int MinIntellectual;

        public TrainabilityDef Trainability;

        public List<string> AutoTrainables;
    }
}
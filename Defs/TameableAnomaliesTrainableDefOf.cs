using RimWorld;
using Verse;

namespace TameableAnomalies.Defs
{
    [DefOf]
    public static class TameableAnomaliesTrainableDefOf
    {
        public static TrainableDef Haul;

        static TameableAnomaliesTrainableDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TameableAnomaliesTrainableDefOf));
        }
    }
}
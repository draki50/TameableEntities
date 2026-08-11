using RimWorld;
using Verse;

namespace TameableAnomalies
{
    [DefOf]
    public static class TameableAnomaliesThoughtDefOf
    {
        public static ThoughtDef TA_ImplantedMetalhorrorLarva;

        static TameableAnomaliesThoughtDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TameableAnomaliesThoughtDefOf));
        }
    }
}
using RimWorld;
using Verse;

namespace TameableAnomalies
{
    [DefOf]
    public static class TameableAnomaliesHediffDefOf
    {
        public static HediffDef MetalhorrorLarva;

        static TameableAnomaliesHediffDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TameableAnomaliesHediffDefOf));
        }
    }
}
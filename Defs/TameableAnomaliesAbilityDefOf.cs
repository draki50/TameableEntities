using RimWorld;
using Verse;

namespace TameableAnomalies
{
    [DefOf]
    public static class TameableAnomaliesAbilityDefOf
    {
        public static AbilityDef TA_ImplantMetalhorrorLarva;

        static TameableAnomaliesAbilityDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TameableAnomaliesAbilityDefOf));
        }
    }
}
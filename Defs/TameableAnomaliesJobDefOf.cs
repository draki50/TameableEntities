using RimWorld;
using Verse;

namespace TameableAnomalies
{
    [DefOf]
    public static class TameableAnomaliesJobDefOf
    {
        static TameableAnomaliesJobDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TameableAnomaliesJobDefOf));
        }

        public static JobDef TA_ImplantMetalhorrorLarva;
        public static JobDef TameableRevenantAttack;
    }
}
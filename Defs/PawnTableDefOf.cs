using RimWorld;
using Verse;

namespace TameableAnomalies
{
    [DefOf]
    public static class PawnTableDefOf
    {
        public static PawnTableDef Entities;

        static PawnTableDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnTableDefOf));
        }
    }
}
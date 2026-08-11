using RimWorld;
using Verse;

namespace TameableAnomalies.Defs
{
    [DefOf]
    public static class TameableAnomaliesThingDefOf
    {
        public static ThingDef Sightstealer;
        public static ThingDef Revenant;

        public static ThingDef Metalhorror;
        public static ThingDef Gorehulk;
        public static ThingDef Noctol;
        public static ThingDef Bulbfreak;

        public static ThingDef Fingerspike;
        public static ThingDef Toughspike;
        public static ThingDef Trispike;
        public static ThingDef Chimera;
        public static ThingDef Devourer;
        public static ThingDef Nociosphere;

        static TameableAnomaliesThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TameableAnomaliesThingDefOf));
        }
    }
}
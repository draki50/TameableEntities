using System.Linq;
using RimWorld;
using Verse;
using TameableAnomalies.Components;
using System.Collections.Generic;

namespace TameableAnomalies.Utilities
{
    [StaticConstructorOnStartup]
    public static class ConditionedEntityInjector
    {
        static ConditionedEntityInjector()
        {
            foreach (ThingDef def in DefDatabase<ThingDef>.AllDefs)
            {
                if (def.race == null)
                    continue;

                def.comps ??= new List<CompProperties>();

                if (!def.comps.Any(c => c is CompPropertiesConditionableEntity))
                {
                    def.comps.Add(new CompPropertiesConditionableEntity());
                }
            }
        }
    }
}
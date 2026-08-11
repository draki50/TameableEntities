using System.Linq;
using RimWorld;
using Verse;

namespace TameableAnomalies.Utilities
{
    [StaticConstructorOnStartup]
    public static class EntityPawnTableInjector
    {
        static EntityPawnTableInjector()
        {
            LongEventHandler.ExecuteWhenFinished(InjectTrainableColumns);
        }

        private static void InjectTrainableColumns()
        {
            PawnTableDef animals = RimWorld.PawnTableDefOf.Animals;
            PawnTableDef entities = PawnTableDefOf.Entities;

            int insertIndex = entities.columns.FindIndex(
                c => c.defName == "TameableAnomalies_EntityFollowDrafted");

            if (insertIndex < 0)
                insertIndex = entities.columns.Count;

            foreach (PawnColumnDef column in animals.columns)
            {
                if (column.defName == null)
                    continue;

                if (!column.defName.StartsWith("Trainable_"))
                    continue;

                if (entities.columns.Contains(column))
                    continue;

                entities.columns.Insert(insertIndex++, column);
            }
            
        }
    }
}
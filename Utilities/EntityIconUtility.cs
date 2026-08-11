using UnityEngine;
using Verse;

namespace TameableAnomalies.Utilities
{
    public static class EntityIconUtility
    {
        public static Texture2D GetEntityIcon(Pawn pawn)
        {
            if (pawn == null)
                return null;

            string defName = pawn.def.defName;

            return ContentFinder<Texture2D>.Get(
                "UI/Entities/" + defName,
                false);
        }
    }
}
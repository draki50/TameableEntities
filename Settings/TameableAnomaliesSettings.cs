using Verse;

namespace TameableAnomalies
{
    public class TameableAnomaliesSettings : ModSettings
    {

        public static TameableAnomaliesSettings Settings;
        public bool conditioningBoostsContainment = true;

        public float conditioningDecayPerDay = 1f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref conditioningBoostsContainment,
                "conditioningBoostsContainment", true);

            Scribe_Values.Look(ref conditioningDecayPerDay,
                "conditioningDecayPerDay", 1f);

            base.ExposeData();
        }
    }
}
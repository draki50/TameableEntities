using RimWorld;
using System.Collections.Generic;
using System.Text;
using TameableAnomalies.Defs;
using TameableAnomalies.WorldComponents;
using UnityEngine;
using Verse;
using Verse.AI;

namespace TameableAnomalies.Utilities
{
    public static class ConditioningUtility
    {
        public const float FriendlyConditioning = 40f;
        private static readonly ConditioningProfile DefaultProfile = new ConditioningProfile
        {
            RequiredConditioning = 100f,
            MinAnimals = 0,
            MinIntellectual = 0
        };

        public static void ReleaseConditioning(Pawn pawn)
        {
            pawn.SetFaction(Faction.OfEntities);

            pawn.jobs?.StopAll();

            pawn.mindState?.Reset(false, false, false);

            pawn.thinker = new Pawn_Thinker(pawn);

            pawn.Drawer?.renderer?.SetAllGraphicsDirty();
        }

        private static readonly List<string> IntermediateTrainables = new()
            {
                "Tameness",
                "Obedience"
            };

        private static readonly List<string> AdvancedTrainables = new()
            {
               "Tameness",
               "Obedience",
               "Haul",
               "Rescue",
               "Release",
            };

        private static readonly List<string> MetalhorrorTrainables = new()
            {
                "Tameness",
                "Obedience",
                "Haul",
                "Rescue",
                "Release",
                "ImplantLarva"
            };

        private static readonly Dictionary<ThingDef, ConditioningProfile> Profiles = new()
        {
            {
                TameableAnomaliesThingDefOf.Fingerspike,
                new ConditioningProfile
                {
                    RequiredConditioning = 25f,
                    MinAnimals = 2,
                    MinIntellectual = 3,

                    Trainability = TrainabilityDefOf.Intermediate,
                    AutoTrainables = IntermediateTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Toughspike,
                new ConditioningProfile
                {
                    RequiredConditioning = 40f,
                    MinAnimals = 3,
                    MinIntellectual = 4,

                    Trainability = TrainabilityDefOf.Intermediate,
                    AutoTrainables = IntermediateTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Trispike,
                new ConditioningProfile
                {
                    RequiredConditioning = 55f,
                    MinAnimals = 3,
                    MinIntellectual = 6,

                    Trainability = TrainabilityDefOf.Intermediate,
                    AutoTrainables = IntermediateTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Chimera,
                new ConditioningProfile
                {
                    RequiredConditioning = 60f,
                    MinAnimals = 7,
                    MinIntellectual = 7,

                    Trainability = TrainabilityDefOf.Intermediate,
                    AutoTrainables = IntermediateTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Devourer,
                new ConditioningProfile
                {
                    RequiredConditioning = 70f,
                    MinAnimals = 7,
                    MinIntellectual = 10,

                    Trainability = TrainabilityDefOf.Intermediate,
                    AutoTrainables = IntermediateTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Nociosphere,
                new ConditioningProfile
                {
                    RequiredConditioning = 340f,
                    MinAnimals = 14,
                    MinIntellectual = 20,

                    Trainability = TrainabilityDefOf.Intermediate,
                    AutoTrainables = IntermediateTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Sightstealer,
                new ConditioningProfile
                {
                    RequiredConditioning = 30f,
                    MinAnimals = 3,
                    MinIntellectual = 4,

                    Trainability = TrainabilityDefOf.Advanced,
                    AutoTrainables = AdvancedTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Metalhorror,
                new ConditioningProfile
                {
                    RequiredConditioning = 60f,
                    MinAnimals = 8,
                    MinIntellectual = 10,

                    Trainability = TrainabilityDefOf.Advanced,
                    AutoTrainables = MetalhorrorTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Gorehulk,
                new ConditioningProfile
                {
                    RequiredConditioning = 50f,
                    MinAnimals = 5,
                    MinIntellectual = 8,

                    Trainability = TrainabilityDefOf.Advanced,
                    AutoTrainables = AdvancedTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Noctol,
                new ConditioningProfile
                {
                    RequiredConditioning = 45f,
                    MinAnimals = 7,
                    MinIntellectual = 10,

                    Trainability = TrainabilityDefOf.Advanced,
                    AutoTrainables = AdvancedTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Revenant,
                new ConditioningProfile
                {
                    RequiredConditioning = 120f,
                    MinAnimals = 12,
                    MinIntellectual = 14,

                    Trainability = TrainabilityDefOf.Advanced,
                    AutoTrainables = AdvancedTrainables
                }
            },
            {
                TameableAnomaliesThingDefOf.Bulbfreak,
                new ConditioningProfile
                {
                    RequiredConditioning = 240f,
                    MinAnimals = 20,
                    MinIntellectual = 14,

                    Trainability = TrainabilityDefOf.Advanced,
                    AutoTrainables = AdvancedTrainables
                }
            },
        };

        public const float MaxConditioning = 1000f;
        private const int BarSegments = 20;

        public static ConditioningWorldComponent Component
        {
            get
            {
                return Find.World.GetComponent<ConditioningWorldComponent>();
            }
        }

        public static float GetConditioning(Pawn pawn)
        {
            return Component.GetConditioning(pawn);
        }

        public static float GetDisplayedConditioning(Pawn pawn)
        {
            ConditioningProfile profile = GetProfile(pawn);

            return Mathf.Clamp(
                GetConditioning(pawn) / profile.RequiredConditioning * 100f,
                0f,
                100f);
        }

        public static float GetConditioningProgress(Pawn pawn)
        {
            return (float)GetConditioning(pawn) / MaxConditioning;
        }

        public static void SetConditioning(Pawn pawn, float amount)
        {
            ConditioningProfile profile = GetProfile(pawn);

            amount = Mathf.Clamp(amount, 0f, profile.RequiredConditioning);

            float previousConditioning = GetDisplayedConditioning(pawn);

            bool wasAbove60 = previousConditioning >= 60f;
            bool wasAbove45 = previousConditioning >= 45f;

            bool wasConditioned = IsConditioned(pawn);

            Component.SetConditioning(pawn, amount);

            float currentConditioning = GetDisplayedConditioning(pawn);

            if (wasAbove60 && currentConditioning < 60f)
            {
                Messages.Message(
                    pawn.LabelShortCap + "'s conditioning has dropped below 60%.",
                    pawn,
                    MessageTypeDefOf.NeutralEvent);
            }

            if (wasAbove45 && currentConditioning < 45f)
            {
                Messages.Message(
                    pawn.LabelShortCap + "'s conditioning is dangerously low. Return it to a holding platform soon.",
                    pawn,
                    MessageTypeDefOf.NeutralEvent);
            }

            bool isConditioned = IsConditioned(pawn);

            if (wasConditioned && !isConditioned)
            {
                ReleaseConditioning(pawn);
            }

            if (IsConditioned(pawn) && pawn.training != null)
            {
                foreach (string trainableName in profile.AutoTrainables)
                {
                    TrainableDef trainable = DefDatabase<TrainableDef>.GetNamed(trainableName);

                    if (!pawn.training.HasLearned(trainable))
                    {
                        pawn.training.Train(trainable, null, true);
                    }
                }
            }
        }

        public static void AddConditioning(Pawn pawn, float amount)
        {
            SetConditioning(pawn, GetConditioning(pawn) + amount);
        }

        public static bool IsConditioned(Pawn pawn)
        {
            return GetDisplayedConditioning(pawn) >= FriendlyConditioning;
        }

        public static Disposition GetDisposition(Pawn pawn)
        {
            float conditioning = GetDisplayedConditioning(pawn);

            if (conditioning >= 100f)
                return Disposition.Tamed;

            if (conditioning >= 80f)
                return Disposition.Loyal;

            if (conditioning >= 60f)
                return Disposition.Cooperative;

            if (conditioning >= 40f)
                return Disposition.NonHostile;

            if (conditioning >= 20f)
                return Disposition.Suspicious;

            return Disposition.Hostile;
        }

        public static bool IsFriendly(Pawn pawn)
        {
            return GetDisplayedConditioning(pawn) >= 40f;
        }

        public static bool IsFullyConditioned(Pawn pawn)
        {
            return GetDisplayedConditioning(pawn) >= 100f;
        }

        public static string GetConditioningBar(Pawn pawn)
        {
            float conditioning = GetDisplayedConditioning(pawn);

            int filled = Mathf.RoundToInt(conditioning * BarSegments / 100f);

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < BarSegments; i++)
            {
                builder.Append(i < filled ? '█' : '░');
            }

            return builder.ToString();
        }

        public static bool CanBeConditioned(Pawn pawn)
        {
            if (pawn == null)
                return false;

            if (pawn.TryGetComp<CompProducesBioferrite>() == null)
                return false;

            // Fleshmass Nucleus can produce bioferrite but should never be tameable.
            if (pawn.def.defName == "FleshmassNucleus")
                return false;

            return true;
        }

        public static float GetRequiredConditioning(ThingDef def)
        {
            if (Profiles.TryGetValue(def, out ConditioningProfile profile))
                return profile.RequiredConditioning;

            return DefaultProfile.RequiredConditioning;
        }

        public static bool ReleaseAsAlly(Pawn pawn)
        {
            if (!IsConditioned(pawn))
                return false;

            pawn.SetFaction(Faction.OfPlayer);
            pawn.thinker = new Pawn_Thinker(pawn);

            if (pawn.Name == null)
            {
                pawn.Name = new NameSingle(pawn.LabelCap);
            }

            // Initialize vanilla animal systems
            if (pawn.playerSettings != null)
            {
                pawn.playerSettings.medCare = MedicalCareCategory.Best;
                pawn.playerSettings.followDrafted = true;
            }

            if (pawn.training != null)
            {
                pawn.training.Train(TrainableDefOf.Tameness, null, true);
                pawn.training.Train(TrainableDefOf.Obedience, null, true);
            }
            return true;
        }
        public static float CalculateConditioningGain(Pawn trainer, Pawn entity)
        {
            float gain = 1f;

            if (trainer != null)
            {
                gain += trainer.skills.GetSkill(SkillDefOf.Animals).Level * 0.05f;
                gain += trainer.skills.GetSkill(SkillDefOf.Intellectual).Level * 0.05f;
            }

            return gain;
        }
        public static float ApplyConditioning(Pawn trainer, Pawn entity)
        {
            ConditioningProfile profile = GetProfile(entity);

            float gain = CalculateConditioningGain(trainer, entity);

            AddConditioning(entity, gain);

            return gain;
        }

        public static bool MeetsSkillRequirements(Pawn pawn, Pawn entity)
        {
            if (pawn?.skills == null)
                return false;

            ConditioningProfile profile = GetProfile(entity);

            if (pawn.skills.GetSkill(SkillDefOf.Animals).Level < profile.MinAnimals)
                return false;

            if (pawn.skills.GetSkill(SkillDefOf.Intellectual).Level < profile.MinIntellectual)
                return false;

            return true;
        }
        public static ConditioningProfile GetProfile(Pawn entity)
        {
            if (Profiles.TryGetValue(entity.def, out ConditioningProfile profile))
            {
                return profile;
            }

            return DefaultProfile;
        }
        public static bool IsFriendlyConditionedNociosphere(Thing thing)
        {
            return thing is Pawn pawn
                && pawn.def.defName == "Nociosphere"
                && IsConditioned(pawn);
        }


    }
}
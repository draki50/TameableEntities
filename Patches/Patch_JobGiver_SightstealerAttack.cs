//using HarmonyLib;
//using RimWorld;
//using TameableAnomalies.ThinkNodes;
//using TameableAnomalies.Utilities;
//using Verse;
//using Verse.AI;

//namespace TameableAnomalies.Patches
//{
//    [HarmonyPatch(typeof(JobGiver_SightstealerAttack), "TryGiveJob")]
//    public static class Patch_JobGiver_SightstealerAttack
//    {

//        public static bool Prefix(Pawn pawn, ref Job __result)
//        {
//            if (!ConditioningUtility.IsFriendly(pawn))
//                return true;

//            bool friendly = ConditioningUtility.IsFriendly(pawn);

//            if (!friendly)
//                return true;

//            Pawn master = pawn.playerSettings?.Master;

//            if (master == null)
//            {
//                return true;
//            }

//            EntitySeekAllowedAreaJobGiver areaGiver = new EntitySeekAllowedAreaJobGiver();

//            Job areaJob = areaGiver.GetAllowedAreaJob(pawn);

//            if (areaJob != null)
//            {
//                __result = areaJob;
//                return false;
//            }

//            Job medicalJob = new JobGiver_FriendlyEntityMedicalRest().GetJob(pawn);

//            if (medicalJob != null)
//            {
//                __result = medicalJob;
//                return false;
//            }

//            bool shouldFollow =
//                (pawn.playerSettings.followDrafted && master.Drafted)
//                ||
//                (pawn.playerSettings.followFieldwork &&
//                 !master.Drafted &&
//                 master.CurJob != null &&
//                 master.CurJob.def != JobDefOf.Wait);

//            if (shouldFollow)
//            {
//                Job job = JobMaker.MakeJob(JobDefOf.FollowClose, master);
//                job.expiryInterval = 200;
//                job.checkOverrideOnExpire = true;
//                job.followRadius = 3f;

//                __result = job;
//                return false;
//            }

//            if (pawn.training != null &&
//                pawn.training.HasLearned(DefDatabase<TrainableDef>.GetNamed("Haul")))
//            {

//                EntityHaulJobGiver haulGiver = new EntityHaulJobGiver();

//                Job haulJob = haulGiver.GetHaulJob(pawn);

//                if (haulJob != null)
//                {
//                    __result = haulJob;
//                    return false;
//                }

//            }

//            return true;
//        }
//    }
//}
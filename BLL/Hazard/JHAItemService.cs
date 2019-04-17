using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class JHAItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="JHAItemId"></param>
        /// <returns></returns>
        public static Model.Hazard_JHAItem GetJHAItemById(string JHAItemId)
        {
            return Funs.DB.Hazard_JHAItem.FirstOrDefault(e => e.JHAItemId == JHAItemId);
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Hazard_JHAItem> GetJHAItemListByJobActivityId(string JobActivityId)
        {
            return (from x in Funs.DB.Hazard_JHAItem where x.JobActivityId == JobActivityId select x).ToList();
        }

        /// <summary>
        /// 添加JHAItem信息
        /// </summary>
        /// <param name="jhaItem"></param>
        public static void AddJHAItem(Model.Hazard_JHAItem jhaItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_JHAItem newJHAItem = new Model.Hazard_JHAItem
            {
                JHAItemId = jhaItem.JHAItemId,
                JobActivityId = jhaItem.JobActivityId,
                SortIndex = jhaItem.SortIndex,
                JobStep = jhaItem.JobStep,
                PossibleAccidents = jhaItem.PossibleAccidents,
                NowControlMeasures = jhaItem.NowControlMeasures,
                HazardJudge_L = jhaItem.HazardJudge_L,
                HazardJudge_L1 = jhaItem.HazardJudge_L1,
                HazardJudge_L2 = jhaItem.HazardJudge_L2,
                HazardJudge_L3 = jhaItem.HazardJudge_L3,
                HazardJudge_L4 = jhaItem.HazardJudge_L4,
                HazardJudge_S = jhaItem.HazardJudge_S,
                HazardJudge_S1 = jhaItem.HazardJudge_S1,
                HazardJudge_S2 = jhaItem.HazardJudge_S2,
                HazardJudge_S3 = jhaItem.HazardJudge_S3,
                HazardJudge_S4 = jhaItem.HazardJudge_S4,
                HazardJudge_S5 = jhaItem.HazardJudge_S5,
                HazardJudge_R = jhaItem.HazardJudge_R,
                RiskLevel = jhaItem.RiskLevel,
                ControlMeasures = jhaItem.ControlMeasures,
                ManagementMeasures = jhaItem.ManagementMeasures,
                ProtectiveMeasures = jhaItem.ProtectiveMeasures,
                OtherMeasures = jhaItem.OtherMeasures,
            };
            db.Hazard_JHAItem.InsertOnSubmit(newJHAItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改JHAItem信息
        /// </summary>
        /// <param name="jhaItem"></param>
        public static void UpdateJHAItem(Model.Hazard_JHAItem jhaItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_JHAItem newJHAItem = db.Hazard_JHAItem.FirstOrDefault(e => e.JHAItemId == jhaItem.JHAItemId);
            if (newJHAItem != null)
            {
                newJHAItem.SortIndex = jhaItem.SortIndex;
                newJHAItem.JobStep = jhaItem.JobStep;
                newJHAItem.PossibleAccidents = jhaItem.PossibleAccidents;
                newJHAItem.NowControlMeasures = jhaItem.NowControlMeasures;
                newJHAItem.HazardJudge_L = jhaItem.HazardJudge_L;
                newJHAItem.HazardJudge_L1 = jhaItem.HazardJudge_L1;
                newJHAItem.HazardJudge_L2 = jhaItem.HazardJudge_L2;
                newJHAItem.HazardJudge_L3 = jhaItem.HazardJudge_L3;
                newJHAItem.HazardJudge_L4 = jhaItem.HazardJudge_L4;
                newJHAItem.HazardJudge_S = jhaItem.HazardJudge_S;
                newJHAItem.HazardJudge_S1 = jhaItem.HazardJudge_S1;
                newJHAItem.HazardJudge_S2 = jhaItem.HazardJudge_S2;
                newJHAItem.HazardJudge_S3 = jhaItem.HazardJudge_S3;
                newJHAItem.HazardJudge_S4 = jhaItem.HazardJudge_S4;
                newJHAItem.HazardJudge_S5 = jhaItem.HazardJudge_S5;
                newJHAItem.HazardJudge_R = jhaItem.HazardJudge_R;
                newJHAItem.RiskLevel = jhaItem.RiskLevel;
                newJHAItem.ControlMeasures = jhaItem.ControlMeasures;
                newJHAItem.ManagementMeasures = jhaItem.ManagementMeasures;
                newJHAItem.ProtectiveMeasures = jhaItem.ProtectiveMeasures;
                newJHAItem.OtherMeasures = jhaItem.OtherMeasures;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="JHAItemId"></param>
        public static void DeleteJHAItemById(string JHAItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_JHAItem JHAItem = db.Hazard_JHAItem.FirstOrDefault(e => e.JHAItemId == JHAItemId);
            if (JHAItem != null)
            {
                BLL.JHAItemRecordService.DeleteJHAItemRecordByJHAItemId(JHAItemId);
                db.Hazard_JHAItem.DeleteOnSubmit(JHAItem);
                db.SubmitChanges();
            }
        }
    }
}
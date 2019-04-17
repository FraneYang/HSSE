using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class RescueInfoPlanService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Emergency_RescueInfoPlan GetRescueInfoPlanById(string rescueInfoPlanId)
        {
            return Funs.DB.Emergency_RescueInfoPlan.FirstOrDefault(e => e.RescueInfoPlanId == rescueInfoPlanId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="rescueInfoPlanId"></param>
        public static void DeleteRescueInfoPlanById(string rescueInfoPlanId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Emergency_RescueInfoPlan RescueInfoPlan = db.Emergency_RescueInfoPlan.FirstOrDefault(e => e.RescueInfoPlanId == rescueInfoPlanId);
            if (RescueInfoPlan != null)
            {
                CommonService.DeleteSysPushRecordByDataId(RescueInfoPlan.RescueInfoPlanId);
                db.Emergency_RescueInfoPlan.DeleteOnSubmit(RescueInfoPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="rescueInfoId"></param>
        public static void DeleteRescueInfoPlanByRescueInfoId(string rescueInfoId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var rescueInfoPlans = from x in db.Emergency_RescueInfoPlan where x.RescueInfoId == rescueInfoId select x;
            if (rescueInfoPlans.Count() > 0)
            {
                db.Emergency_RescueInfoPlan.DeleteAllOnSubmit(rescueInfoPlans);
                db.SubmitChanges();
            }
        }
    }
}
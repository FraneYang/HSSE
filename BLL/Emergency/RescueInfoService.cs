using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class RescueInfoService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Emergency_RescueInfo GetRescueInfoById(string rescueInfoId)
        {
            return Funs.DB.Emergency_RescueInfo.FirstOrDefault(e => e.RescueInfoId == rescueInfoId);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="plan"></param>
        public static void UpdateRescueInfo(Model.Emergency_RescueInfo rescueInfo)
        {
            Model.Emergency_RescueInfo newRescueInfo = db.Emergency_RescueInfo.FirstOrDefault(e => e.RescueInfoId == rescueInfo.RescueInfoId);
            if (newRescueInfo != null)
            {
                newRescueInfo.InstallationNames = rescueInfo.InstallationNames;
                //newPlan.States = plan.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="rescueInfoId"></param>
        public static void DeleteRescueInfoById(string rescueInfoId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Emergency_RescueInfo rescueInfo = db.Emergency_RescueInfo.FirstOrDefault(e => e.RescueInfoId == rescueInfoId);
            if (rescueInfo != null)
            {
                BLL.RescueInfoPlanService.DeleteRescueInfoPlanByRescueInfoId(rescueInfo.RescueInfoId);
                CommonService.DeleteSysPushRecordByDataId(rescueInfo.RescueInfoId);
                db.Emergency_RescueInfo.DeleteOnSubmit(rescueInfo);
                db.SubmitChanges();
            }
        }
    }
}
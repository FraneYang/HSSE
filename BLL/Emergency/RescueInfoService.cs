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
        /// 根据主键删除信息
        /// </summary>
        /// <param name="rescueInfoId"></param>
        public static void DeleteRescueInfoById(string rescueInfoId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Emergency_RescueInfo rescueInfo = db.Emergency_RescueInfo.FirstOrDefault(e => e.RescueInfoId == rescueInfoId);
            if (rescueInfo != null)
            {
                CommonService.DeleteSysPushRecordByDataId(rescueInfo.RescueInfoId);
                db.Emergency_RescueInfo.DeleteOnSubmit(rescueInfo);
                db.SubmitChanges();
            }
        }
    }
}
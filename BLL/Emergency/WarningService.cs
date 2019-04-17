using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class WarningService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Emergency_Warning GetWarningById(string WarningId)
        {
            return Funs.DB.Emergency_Warning.FirstOrDefault(e => e.WarningId == WarningId);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="plan"></param>
        public static void UpdateWarning(Model.Emergency_Warning warning)
        {
            Model.Emergency_Warning newWarning = db.Emergency_Warning.FirstOrDefault(e => e.WarningId == warning.WarningId);
            if (newWarning != null)
            {
                newWarning.InstallationNames = warning.InstallationNames;
                //newPlan.States = plan.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="WarningId"></param>
        public static void DeleteWarningById(string WarningId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Emergency_Warning Warning = db.Emergency_Warning.FirstOrDefault(e => e.WarningId == WarningId);
            if (Warning != null)
            {
                CommonService.DeleteSysPushRecordByDataId(Warning.WarningId);
                db.Emergency_Warning.DeleteOnSubmit(Warning);
                db.SubmitChanges();
            }
        }
    }
}
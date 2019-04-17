using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class BreakGroundService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_BreakGround GetBreakGroundById(string BreakGroundId)
        {
            return Funs.DB.License_BreakGround.FirstOrDefault(e => e.BreakGroundId == BreakGroundId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="BreakGroundId"></param>
        public static void DeleteBreakGroundById(string BreakGroundId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_BreakGround BreakGround = db.License_BreakGround.FirstOrDefault(e => e.BreakGroundId == BreakGroundId);
            if (BreakGround != null)
            {
                CommonService.DeleteLicenseItemByDataId(BreakGroundId);
                CommonService.DeleteSysPushRecordByDataId(BreakGroundId);

                db.License_BreakGround.DeleteOnSubmit(BreakGround);
                db.SubmitChanges();
            }
        }
    }
}
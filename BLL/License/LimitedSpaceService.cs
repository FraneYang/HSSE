using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class LimitedSpaceService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_LimitedSpace GetLimitedSpaceById(string LimitedSpaceId)
        {
            return Funs.DB.License_LimitedSpace.FirstOrDefault(e => e.LimitedSpaceId == LimitedSpaceId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="LimitedSpaceId"></param>
        public static void DeleteLimitedSpaceById(string LimitedSpaceId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_LimitedSpace LimitedSpace = db.License_LimitedSpace.FirstOrDefault(e => e.LimitedSpaceId == LimitedSpaceId);
            if (LimitedSpace != null)
            {
                CommonService.DeleteLicenseItemByDataId(LimitedSpaceId);
                CommonService.DeleteSysPushRecordByDataId(LimitedSpaceId);

                db.License_LimitedSpace.DeleteOnSubmit(LimitedSpace);
                db.SubmitChanges();
            }
        }
    }
}
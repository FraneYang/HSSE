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
       /// 
       /// </summary>
       /// <param name="LimitedSpaceId"></param>
       /// <returns></returns>
        public static Model.View_License_LimitedSpace GetViewLimitedSpaceById(string LimitedSpaceId)
        {
            return db.View_License_LimitedSpace.FirstOrDefault(e => e.LimitedSpaceId == LimitedSpaceId);
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
                var ans = from x in Funs.DB.License_LimitedSpaceAnalysis where x.LimitedSpaceId == LimitedSpaceId select x;
                if (ans.Count() > 0)
                {
                    db.License_LimitedSpaceAnalysis.DeleteAllOnSubmit(ans);
                }
                CommonService.DeleteLicenseItemByDataId(LimitedSpaceId);
                CommonService.DeleteSysPushRecordByDataId(LimitedSpaceId);

                db.License_LimitedSpace.DeleteOnSubmit(LimitedSpace);
                db.SubmitChanges();
            }
        }
    }
}
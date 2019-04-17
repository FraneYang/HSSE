using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class FireWorkService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_FireWork GetFireWorkById(string FireWorkId)
        {
            return Funs.DB.License_FireWork.FirstOrDefault(e => e.FireWorkId == FireWorkId);
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.View_License_FireWork GetView_License_FireWorkById(string FireWorkId)
        {
            return Funs.DB.View_License_FireWork.FirstOrDefault(e => e.FireWorkId == FireWorkId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="fireWorkId"></param>
        public static void DeleteFireWorkById(string fireWorkId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_FireWork FireWork = db.License_FireWork.FirstOrDefault(e => e.FireWorkId == fireWorkId);
            if (FireWork != null)
            {
                var fireWorkAnalysis = from x in db.License_FireWorkAnalysis where x.FireWorkId == fireWorkId select x;
                if (fireWorkAnalysis.Count() > 0)
                {
                    db.License_FireWorkAnalysis.DeleteAllOnSubmit(fireWorkAnalysis);
                }

                CommonService.DeleteLicenseItemByDataId(FireWork.FireWorkId);
                CommonService.DeleteSysPushRecordByDataId(fireWorkId);

                db.License_FireWork.DeleteOnSubmit(FireWork);
                db.SubmitChanges();
            }
        }
    }
}
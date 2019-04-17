using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class LiftingWorkService
    {
       public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_LiftingWork GetLiftingWorkById(string LiftingWorkId)
        {
            return Funs.DB.License_LiftingWork.FirstOrDefault(e => e.LiftingWorkId == LiftingWorkId);
        }

       /// <summary>
       /// 根据主键获取视图信息
       /// </summary>
       /// <param name="liftingWorkId"></param>
       /// <returns></returns>
        public static Model.View_License_LiftingWork GetViewLiftingWorkById(string liftingWorkId)
        {
            return db.View_License_LiftingWork.FirstOrDefault(e => e.LiftingWorkId == liftingWorkId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="LiftingWorkId"></param>
        public static void DeleteLiftingWorkById(string LiftingWorkId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_LiftingWork LiftingWork = db.License_LiftingWork.FirstOrDefault(e => e.LiftingWorkId == LiftingWorkId);
            if (LiftingWork != null)
            {
                CommonService.DeleteLicenseItemByDataId(LiftingWorkId);
                CommonService.DeleteSysPushRecordByDataId(LiftingWorkId);

                db.License_LiftingWork.DeleteOnSubmit(LiftingWork);
                db.SubmitChanges();
            }
        }
    }
}
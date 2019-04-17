using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class HeightWorkService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_HeightWork GetHeightWorkById(string HeightWorkId)
        {
            return Funs.DB.License_HeightWork.FirstOrDefault(e => e.HeightWorkId == HeightWorkId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="HeightWorkId"></param>
        public static void DeleteHeightWorkById(string HeightWorkId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_HeightWork HeightWork = db.License_HeightWork.FirstOrDefault(e => e.HeightWorkId == HeightWorkId);
            if (HeightWork != null)
            {
                CommonService.DeleteLicenseItemByDataId(HeightWorkId);
                CommonService.DeleteSysPushRecordByDataId(HeightWorkId);

                db.License_HeightWork.DeleteOnSubmit(HeightWork);
                db.SubmitChanges();
            }
        }
    }
}
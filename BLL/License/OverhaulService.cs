using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class OverhaulService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_Overhaul GetOverhaulById(string overhaulId)
        {
            return Funs.DB.License_Overhaul.FirstOrDefault(e => e.OverhaulId == overhaulId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="overhaulId"></param>
        public static void DeleteOverhaulById(string overhaulId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_Overhaul Overhaul = db.License_Overhaul.FirstOrDefault(e => e.OverhaulId == overhaulId);
            if (Overhaul != null)
            {
                CommonService.DeleteLicenseItemByDataId(overhaulId);
                CommonService.DeleteSysPushRecordByDataId(overhaulId);

                db.License_Overhaul.DeleteOnSubmit(Overhaul);
                db.SubmitChanges();
            }
        }
    }
}
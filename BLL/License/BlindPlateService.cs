using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class BlindPlateService
    {
       public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_BlindPlate GetBlindPlateById(string BlindPlateId)
        {
            return Funs.DB.License_BlindPlate.FirstOrDefault(e => e.BlindPlateId == BlindPlateId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="BlindPlateId"></param>
        public static void DeleteBlindPlateById(string BlindPlateId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_BlindPlate BlindPlate = db.License_BlindPlate.FirstOrDefault(e => e.BlindPlateId == BlindPlateId);
            if (BlindPlate != null)
            {
                CommonService.DeleteLicenseItemByDataId(BlindPlateId);
                CommonService.DeleteSysPushRecordByDataId(BlindPlateId);

                db.License_BlindPlate.DeleteOnSubmit(BlindPlate);
                db.SubmitChanges();
            }
        }

       /// <summary>
        ///根据主键获取盲板抽堵安全作业票视图
       /// </summary>
       /// <param name="blindPlateId"></param>
       /// <returns></returns>
        public static Model.View_License_BlindPlate GetViewBlindPlateById(string blindPlateId)
        {
            return db.View_License_BlindPlate.FirstOrDefault(e => e.BlindPlateId == blindPlateId);
        }
    }
}
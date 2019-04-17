using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class ElectricityService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_Electricity GetElectricityById(string ElectricityId)
        {
            return Funs.DB.License_Electricity.FirstOrDefault(e => e.ElectricityId == ElectricityId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ElectricityId"></param>
        public static void DeleteElectricityById(string ElectricityId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_Electricity Electricity = db.License_Electricity.FirstOrDefault(e => e.ElectricityId == ElectricityId);
            if (Electricity != null)
            {
                CommonService.DeleteLicenseItemByDataId(ElectricityId);
                CommonService.DeleteSysPushRecordByDataId(ElectricityId);

                db.License_Electricity.DeleteOnSubmit(Electricity);
                db.SubmitChanges();
            }
        }
    }
}
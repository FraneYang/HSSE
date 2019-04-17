using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class OpenCircuitService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_OpenCircuit GetOpenCircuitById(string OpenCircuitId)
        {
            return Funs.DB.License_OpenCircuit.FirstOrDefault(e => e.OpenCircuitId == OpenCircuitId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="OpenCircuitId"></param>
        public static void DeleteOpenCircuitById(string OpenCircuitId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_OpenCircuit OpenCircuit = db.License_OpenCircuit.FirstOrDefault(e => e.OpenCircuitId == OpenCircuitId);
            if (OpenCircuit != null)
            {
                CommonService.DeleteLicenseItemByDataId(OpenCircuitId);
                CommonService.DeleteSysPushRecordByDataId(OpenCircuitId);

                db.License_OpenCircuit.DeleteOnSubmit(OpenCircuit);
                db.SubmitChanges();
            }
        }
    }
}
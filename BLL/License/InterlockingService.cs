using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class InterlockingService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.License_Interlocking GetInterlockingById(string interlockingId)
        {
            return Funs.DB.License_Interlocking.FirstOrDefault(e => e.InterlockingId == interlockingId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="interlockingId"></param>
        public static void DeleteInterlockingById(string interlockingId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.License_Interlocking Interlocking = db.License_Interlocking.FirstOrDefault(e => e.InterlockingId == interlockingId);
            if (Interlocking != null)
            {              
                CommonService.DeleteSysPushRecordByDataId(interlockingId);
                db.License_Interlocking.DeleteOnSubmit(Interlocking);
                db.SubmitChanges();
            }
        }
    }
}
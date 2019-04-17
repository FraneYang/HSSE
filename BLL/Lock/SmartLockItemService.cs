using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class SmartLockItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Lock_SmartLockItem GetSmartLockItemById(string SmartLockItemId)
        {
            return Funs.DB.Lock_SmartLockItem.FirstOrDefault(e => e.SmartLockItemId == SmartLockItemId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="smartLockItemId"></param>
        public static void DeleteSmartLockItemById(string smartLockItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Lock_SmartLockItem SmartLockItem = db.Lock_SmartLockItem.FirstOrDefault(e => e.SmartLockItemId == smartLockItemId);
            if (SmartLockItem != null)
            {
                CommonService.DeleteSysPushRecordByDataId(smartLockItemId);
                db.Lock_SmartLockItem.DeleteOnSubmit(SmartLockItem);
                db.SubmitChanges();
            }
        }
    }
}
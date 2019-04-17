using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class SmartLockService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Lock_SmartLock GetSmartLockById(string SmartLockId)
        {
            return Funs.DB.Lock_SmartLock.FirstOrDefault(e => e.SmartLockId == SmartLockId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="smartLockId"></param>
        public static void DeleteSmartLockById(string smartLockId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Lock_SmartLock smartLock = db.Lock_SmartLock.FirstOrDefault(e => e.SmartLockId == smartLockId);
            if (smartLock != null)
            {
                var smartLockItem = from x in db.Lock_SmartLockItem where x.SmartLockId == smartLockId select x;
                if (smartLockItem.Count() > 0)
                {
                    foreach (var item in smartLockItem)
                    {
                        SmartLockItemService.DeleteSmartLockItemById(item.SmartLockItemId);
                    }
                }

                CommonService.DeleteSysPushRecordByDataId(smartLock.SmartLockId);
                db.Lock_SmartLock.DeleteOnSubmit(smartLock);
                db.SubmitChanges();
            }
        }
    }
}
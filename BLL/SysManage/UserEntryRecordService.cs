using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 进出场管理
    /// </summary>
    public static class UserEntryRecordService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键删除进出场管理
        /// </summary>
        /// <param name="entryRecordId"></param>
        public static void DeleteUserEntryRecordById(string entryRecordId)
        {
            Model.Sys_UserEntryRecord userEntryRecord = db.Sys_UserEntryRecord.FirstOrDefault(e => e.EntryRecordId == entryRecordId);
            if (userEntryRecord != null)
            {
                db.Sys_UserEntryRecord.DeleteOnSubmit(userEntryRecord);
                db.SubmitChanges();
            }
        }
    }
}

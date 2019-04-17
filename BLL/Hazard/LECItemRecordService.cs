using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class LECItemRecordService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="lecItemId"></param>
        /// <returns></returns>
        public static List< Model.Hazard_LECItemRecord> GetLECItemRecordListByLECItemId(string lecItemId)
        {
            return (from x in Funs.DB.Hazard_LECItemRecord where x.LECItemId == lecItemId select x).ToList();
        }

        /// <summary>
        /// 添加LECItemRecord信息
        /// </summary>
        /// <param name="lecItemRecord"></param>
        public static void AddLECItemRecord(Model.Hazard_LECItemRecord lecItemRecord)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LECItemRecord newLEC = new Model.Hazard_LECItemRecord
            {
                LECItemRecordId = lecItemRecord.LECItemRecordId,
                LECItemId = lecItemRecord.LECItemId,
                EvaluatorId = lecItemRecord.EvaluatorId,
                EvaluationTime = lecItemRecord.EvaluationTime,
                RiskLevel = lecItemRecord.RiskLevel,
            };
            db.Hazard_LECItemRecord.InsertOnSubmit(newLEC);
            db.SubmitChanges();
        }
        
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="lecId"></param>
        public static void DeleteLECItemRecordByLECItemId(string lecItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var lecItemRecord = from x in db.Hazard_LECItemRecord where x.LECItemId == lecItemId select x;
            if (lecItemRecord.Count() > 0)
            {                
                db.Hazard_LECItemRecord.DeleteAllOnSubmit(lecItemRecord);
                db.SubmitChanges();
            }
        }
    }
}
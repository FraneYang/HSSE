using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class JHAItemRecordService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="lecItemId"></param>
        /// <returns></returns>
        public static List< Model.Hazard_JHAItemRecord> GetJHAItemRecordListByJHAItemId(string lecItemId)
        {
            return (from x in Funs.DB.Hazard_JHAItemRecord where x.JHAItemId == lecItemId select x).ToList();
        }

        /// <summary>
        /// 添加JHAItemRecord信息
        /// </summary>
        /// <param name="lecItemRecord"></param>
        public static void AddJHAItemRecord(Model.Hazard_JHAItemRecord lecItemRecord)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_JHAItemRecord newJHA = new Model.Hazard_JHAItemRecord
            {
                JHAItemRecordId = lecItemRecord.JHAItemRecordId,
                JHAItemId = lecItemRecord.JHAItemId,
                EvaluatorId = lecItemRecord.EvaluatorId,
                EvaluationTime = lecItemRecord.EvaluationTime,
                RiskLevel = lecItemRecord.RiskLevel,
            };
            db.Hazard_JHAItemRecord.InsertOnSubmit(newJHA);
            db.SubmitChanges();
        }
        
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="lecId"></param>
        public static void DeleteJHAItemRecordByJHAItemId(string lecItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var lecItemRecord = from x in db.Hazard_JHAItemRecord where x.JHAItemId == lecItemId select x;
            if (lecItemRecord.Count() > 0)
            {                
                db.Hazard_JHAItemRecord.DeleteAllOnSubmit(lecItemRecord);
                db.SubmitChanges();
            }
        }
    }
}
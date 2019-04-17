using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class SCLItemRecordService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="lecItemId"></param>
        /// <returns></returns>
        public static List< Model.Hazard_SCLItemRecord> GetSCLItemRecordListBySCLItemId(string lecItemId)
        {
            return (from x in Funs.DB.Hazard_SCLItemRecord where x.SCLItemId == lecItemId select x).ToList();
        }

        /// <summary>
        /// 添加SCLItemRecord信息
        /// </summary>
        /// <param name="lecItemRecord"></param>
        public static void AddSCLItemRecord(Model.Hazard_SCLItemRecord lecItemRecord)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_SCLItemRecord newSCL = new Model.Hazard_SCLItemRecord
            {
                SCLItemRecordId = lecItemRecord.SCLItemRecordId,
                SCLItemId = lecItemRecord.SCLItemId,
                EvaluatorId = lecItemRecord.EvaluatorId,
                EvaluationTime = lecItemRecord.EvaluationTime,
                RiskLevel = lecItemRecord.RiskLevel,
            };
            db.Hazard_SCLItemRecord.InsertOnSubmit(newSCL);
            db.SubmitChanges();
        }
        
        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="lecId"></param>
        public static void DeleteSCLItemRecordBySCLItemId(string lecItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var lecItemRecord = from x in db.Hazard_SCLItemRecord where x.SCLItemId == lecItemId select x;
            if (lecItemRecord.Count() > 0)
            {                
                db.Hazard_SCLItemRecord.DeleteAllOnSubmit(lecItemRecord);
                db.SubmitChanges();
            }
        }
    }
}
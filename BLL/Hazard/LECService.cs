using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class LECService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Hazard_LEC GetLECById(string LECId)
        {
            return Funs.DB.Hazard_LEC.FirstOrDefault(e => e.LECId == LECId);
        }

        /// <summary>
        /// 添加LEC信息
        /// </summary>
        /// <param name="lec"></param>
        public static void AddLEC(Model.Hazard_LEC lec)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LEC newLEC = new Model.Hazard_LEC();
            newLEC.LECId=SQLHelper.GetNewID(typeof(Model.Hazard_LEC));
            newLEC.InstallationId = lec.InstallationId;
            newLEC.EvaluatorId = lec.EvaluatorId;
            newLEC.EvaluationTime = lec.EvaluationTime;
            newLEC.States = lec.States;
            db.Hazard_LEC.InsertOnSubmit(newLEC);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改LEC信息
        /// </summary>
        /// <param name="lec"></param>
        public static void UpdateLEC(Model.Hazard_LEC lec)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LEC newLEC = db.Hazard_LEC.FirstOrDefault(e => e.LECId == lec.LECId);
            if (newLEC != null)
            {
                newLEC.InstallationId = lec.InstallationId;
                newLEC.EvaluatorId = lec.EvaluatorId;
                newLEC.EvaluationTime = lec.EvaluationTime;
                newLEC.States = lec.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="lecId"></param>
        public static void DeleteLECById(string lecId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LEC lec = db.Hazard_LEC.FirstOrDefault(e => e.LECId == lecId);
            if (lec != null)
            {
                var lecItem = from x in db.Hazard_LECItem where x.LECId == lecId select x;
                if (lecItem.Count() > 0)
                {
                    db.Hazard_LECItem.DeleteAllOnSubmit(lecItem);
                }

                db.Hazard_LEC.DeleteOnSubmit(lec);
                db.SubmitChanges();
            }
        }
    }
}
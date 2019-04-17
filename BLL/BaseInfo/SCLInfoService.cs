using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SCLInfoService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_SCL GetSCLInfoById(string SCLId)
        {
            return Funs.DB.Base_SCL.FirstOrDefault(e => e.SCLId == SCLId);
        }

        /// <summary>
        /// 添加SCL信息
        /// </summary>
        /// <param name="SCL"></param>
        public static void AddSCLInfo(Model.Base_SCL SCL)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_SCL newSCL = new Model.Base_SCL();
            newSCL.SCLId=SQLHelper.GetNewID(typeof(Model.Base_SCL));
            newSCL.SortIndex = SCL.SortIndex;
            newSCL.CheckItem = SCL.CheckItem;
            newSCL.Standard = SCL.Standard;
            newSCL.Consequence = SCL.Consequence;
            newSCL.NowControlMeasures = SCL.NowControlMeasures;
            newSCL.HazardJudge_L = SCL.HazardJudge_L;
            newSCL.HazardJudge_S = SCL.HazardJudge_S;
            newSCL.HazardJudge_R = SCL.HazardJudge_R;
            newSCL.RiskLevel = SCL.RiskLevel;
            newSCL.ControlMeasures = SCL.ControlMeasures;
            db.Base_SCL.InsertOnSubmit(newSCL);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改SCL信息
        /// </summary>
        /// <param name="SCL"></param>
        public static void UpdateSCLInfo(Model.Base_SCL SCL)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_SCL newSCL = db.Base_SCL.FirstOrDefault(e => e.SCLId == SCL.SCLId);
            if (newSCL != null)
            {
                newSCL.SortIndex = SCL.SortIndex;
                newSCL.CheckItem = SCL.CheckItem;
                newSCL.Standard = SCL.Standard;
                newSCL.Consequence = SCL.Consequence;
                newSCL.NowControlMeasures = SCL.NowControlMeasures;
                newSCL.HazardJudge_L = SCL.HazardJudge_L;
                newSCL.HazardJudge_S = SCL.HazardJudge_S;
                newSCL.HazardJudge_R = SCL.HazardJudge_R;
                newSCL.RiskLevel = SCL.RiskLevel;
                newSCL.ControlMeasures = SCL.ControlMeasures;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="SCLId"></param>
        public static void DeleteSCLInfoById(string SCLId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_SCL SCL = db.Base_SCL.FirstOrDefault(e => e.SCLId == SCLId);
            if (SCL != null)
            {
                db.Base_SCL.DeleteOnSubmit(SCL);
                db.SubmitChanges();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    
    /// <summary>
    /// 作业风险对应值
    /// </summary>
   public static class OverhaulRiskGradeService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_OverhaulRiskGrade GetOverhaulRiskGradeById(string OverhaulRiskGradeId)
        {
            return Funs.DB.Base_OverhaulRiskGrade.FirstOrDefault(e => e.OverhaulRiskGradeId == OverhaulRiskGradeId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddOverhaulRiskGrade(Model.Base_OverhaulRiskGrade OverhaulRiskGrade)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_OverhaulRiskGrade newOverhaulRiskGrade = new Model.Base_OverhaulRiskGrade
            {
                OverhaulRiskGradeId = OverhaulRiskGrade.OverhaulRiskGradeId,
                RiskGrade = OverhaulRiskGrade.RiskGrade,
                MinValue = OverhaulRiskGrade.MinValue,
                MaxValue = OverhaulRiskGrade.MaxValue,
                Remark = OverhaulRiskGrade.Remark
            };
            db.Base_OverhaulRiskGrade.InsertOnSubmit(newOverhaulRiskGrade);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateOverhaulRiskGrade(Model.Base_OverhaulRiskGrade OverhaulRiskGrade)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_OverhaulRiskGrade newOverhaulRiskGrade = db.Base_OverhaulRiskGrade.FirstOrDefault(e => e.OverhaulRiskGradeId == OverhaulRiskGrade.OverhaulRiskGradeId);
            if (newOverhaulRiskGrade != null)
            {
                newOverhaulRiskGrade.RiskGrade = OverhaulRiskGrade.RiskGrade;
                newOverhaulRiskGrade.MinValue = OverhaulRiskGrade.MinValue;
                newOverhaulRiskGrade.MaxValue = OverhaulRiskGrade.MaxValue;
                newOverhaulRiskGrade.Remark = OverhaulRiskGrade.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="OverhaulRiskGradeId"></param>
        public static void DeleteOverhaulRiskGradeById(string OverhaulRiskGradeId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_OverhaulRiskGrade OverhaulRiskGrade = db.Base_OverhaulRiskGrade.FirstOrDefault(e => e.OverhaulRiskGradeId == OverhaulRiskGradeId);
            {
                db.Base_OverhaulRiskGrade.DeleteOnSubmit(OverhaulRiskGrade);
                db.SubmitChanges();
            }
        }
    }
}
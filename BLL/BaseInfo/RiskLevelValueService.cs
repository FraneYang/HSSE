using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    
    /// <summary>
    /// 风险等级对应值
    /// </summary>
   public static class RiskLevelValueService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_RiskLevelValue GetRiskLevelValueById(string RiskLevelValueId)
        {
            return Funs.DB.Base_RiskLevelValue.FirstOrDefault(e => e.RiskLevelValueId == RiskLevelValueId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddRiskLevelValue(Model.Base_RiskLevelValue RiskLevelValue)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_RiskLevelValue newRiskLevelValue = new Model.Base_RiskLevelValue();
            newRiskLevelValue.RiskLevelValueId = RiskLevelValue.RiskLevelValueId;
            newRiskLevelValue.RiskLevelId = RiskLevelValue.RiskLevelId;
            newRiskLevelValue.MinValue = RiskLevelValue.MinValue;
            newRiskLevelValue.MaxValue = RiskLevelValue.MaxValue;
            newRiskLevelValue.Remark = RiskLevelValue.Remark;
            db.Base_RiskLevelValue.InsertOnSubmit(newRiskLevelValue);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateRiskLevelValue(Model.Base_RiskLevelValue RiskLevelValue)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_RiskLevelValue newRiskLevelValue = db.Base_RiskLevelValue.FirstOrDefault(e => e.RiskLevelValueId == RiskLevelValue.RiskLevelValueId);
            if (newRiskLevelValue != null)
            {
                newRiskLevelValue.RiskLevelId = RiskLevelValue.RiskLevelId;
                newRiskLevelValue.MinValue = RiskLevelValue.MinValue;
                newRiskLevelValue.MaxValue = RiskLevelValue.MaxValue;
                newRiskLevelValue.Remark = RiskLevelValue.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="RiskLevelValueId"></param>
        public static void DeleteRiskLevelValueById(string RiskLevelValueId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_RiskLevelValue RiskLevelValue = db.Base_RiskLevelValue.FirstOrDefault(e => e.RiskLevelValueId == RiskLevelValueId);
            {
                db.Base_RiskLevelValue.DeleteOnSubmit(RiskLevelValue);
                db.SubmitChanges();
            }
        }
    }
}
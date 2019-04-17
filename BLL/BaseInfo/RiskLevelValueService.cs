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
            Model.Base_RiskLevelValue newRiskLevelValue = new Model.Base_RiskLevelValue
            {
                RiskLevelValueId = RiskLevelValue.RiskLevelValueId,
                RiskLevelId = RiskLevelValue.RiskLevelId,
                MinValue = RiskLevelValue.MinValue,
                MaxValue = RiskLevelValue.MaxValue,
                Remark = RiskLevelValue.Remark,
                ControlMeasures = RiskLevelValue.ControlMeasures,
                LimitTime = RiskLevelValue.LimitTime,
            };
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
                newRiskLevelValue.MinValue = RiskLevelValue.MinValue;
                newRiskLevelValue.MaxValue = RiskLevelValue.MaxValue;
                newRiskLevelValue.Remark = RiskLevelValue.Remark;
                newRiskLevelValue.ControlMeasures = RiskLevelValue.ControlMeasures;
                newRiskLevelValue.LimitTime = RiskLevelValue.LimitTime;
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

        /// <summary>
        /// 根据最新设置的风险等级值更新风险库的值
        /// </summary>
        public static void RefreshRiskLevelByValue()
        {
            var riskLevelValues = from x in Funs.DB.Base_RiskLevelValue select x;
            if (riskLevelValues.Count() > 0)
            {
                var lecItem = from x in Funs.DB.Hazard_LECItem select x;
                if (lecItem.Count() > 0)
                {
                    ////循环判断 lec评价方法里的风险等级
                    foreach (var item in lecItem) 
                    {
                        var riskLevelLec = Funs.DB.Base_RiskLevelValue.FirstOrDefault(x => x.Identification == "LEC" && x.MinValue <= item.HazardJudge_D && x.MaxValue >= item.HazardJudge_D);
                        if (riskLevelLec != null)
                        {
                            item.RiskLevel = riskLevelLec.RiskLevelId;
                        }
                        else
                        {
                            item.RiskLevel = "5";
                        }

                        BLL.LECItemService.UpdateLECItem(item);
                        ////lec评价方法对应风险库里的风险等级
                        var riskListLec = BLL.RiskListService.GetRiskListByLECItemId(item.LECItemId);
                        if (riskListLec != null)
                        {
                            ///等级不为空则更新风险库为空则从风险库
                            riskListLec.RiskLevel = item.RiskLevel;
                            BLL.RiskListService.UpdateRiskList(riskListLec);
                            ////更新基础表中的等级 关联厂区地图显示
                            BLL.RiskListService.SetEuipmentJobEnvironmentRiskLevel(item.DataType, item.DataId);
                        }                        
                    }
                }

                var sclItem = from x in Funs.DB.Hazard_SCLItem select x;
                if (sclItem.Count() > 0)
                {
                    ////循环判断 SCL评价方法里的风险等级
                    foreach (var item in sclItem)
                    {
                        var riskLevelScl = Funs.DB.Base_RiskLevelValue.FirstOrDefault(x => x.Identification == "SCL" && x.MinValue <= item.HazardJudge_R && x.MaxValue >= item.HazardJudge_R);
                        if (riskLevelScl != null)
                        {
                            item.RiskLevel = riskLevelScl.RiskLevelId;
                        }
                        else
                        {
                            item.RiskLevel = "5";
                        }

                        BLL.SCLItemService.UpdateSCLItem(item);
                        ////SCL评价方法对应风险库里的风险等级
                        var riskListScl = BLL.RiskListService.GetRiskListBySCLItemId(item.SCLItemId);
                        if (riskListScl != null)
                        {
                            ///等级不为空则更新风险库为空则从风险库
                            riskListScl.RiskLevel = item.RiskLevel;
                            BLL.RiskListService.UpdateRiskList(riskListScl);
                            ////更新基础表中的等级 关联厂区地图显示
                            BLL.RiskListService.SetEuipmentJobEnvironmentRiskLevel("0", item.EuipmentId);  
                        }
                    }
                }

                var jhaItem = from x in Funs.DB.Hazard_JHAItem select x;
                if (jhaItem.Count() > 0)
                {
                    ////循环判断 JHA评价方法里的风险等级
                    foreach (var item in jhaItem)
                    {
                        var riskLevelLec = Funs.DB.Base_RiskLevelValue.FirstOrDefault(x => x.Identification == "JHA" && x.MinValue <= item.HazardJudge_R && x.MaxValue >= item.HazardJudge_R);
                        if (riskLevelLec != null)
                        {
                            item.RiskLevel = riskLevelLec.RiskLevelId;
                        }
                        else
                        {
                            item.RiskLevel = "5";
                        }

                        BLL.JHAItemService.UpdateJHAItem(item);                        
                    }
                }
            }
        }
    }
}
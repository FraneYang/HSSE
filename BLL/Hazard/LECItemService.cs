using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class LECItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Hazard_LECItem GetLECItemById(string LECItemId)
        {
            return Funs.DB.Hazard_LECItem.FirstOrDefault(e => e.LECItemId == LECItemId);
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Hazard_LECItem> GetLECItemListByDataId(string dataId)
        {
            return (from x in Funs.DB.Hazard_LECItem where x.DataId == dataId select x).ToList();
        }

        /// <summary>
        /// 添加LECItem信息
        /// </summary>
        /// <param name="lecItem"></param>
        public static void AddLECItem(Model.Hazard_LECItem lecItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LECItem newLECItem = new Model.Hazard_LECItem
            {
                LECItemId = lecItem.LECItemId,
                DataId = lecItem.DataId,
                DataType = lecItem.DataType,
                SortIndex = lecItem.SortIndex,
                HazardDescription = lecItem.HazardDescription,
                PossibleAccidents = lecItem.PossibleAccidents,
                HazardJudge_L = lecItem.HazardJudge_L,
                HazardJudge_E = lecItem.HazardJudge_E,
                HazardJudge_C = lecItem.HazardJudge_C,
                HazardJudge_D = lecItem.HazardJudge_D,
                RiskLevel = lecItem.RiskLevel,
                ControlMeasures = lecItem.ControlMeasures,
                ManagementMeasures = lecItem.ManagementMeasures,
                ProtectiveMeasures = lecItem.ProtectiveMeasures,
                OtherMeasures = lecItem.OtherMeasures,
                Remark = lecItem.Remark
            };
            db.Hazard_LECItem.InsertOnSubmit(newLECItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改LECItem信息
        /// </summary>
        /// <param name="lecItem"></param>
        public static void UpdateLECItem(Model.Hazard_LECItem lecItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LECItem newLECItem = db.Hazard_LECItem.FirstOrDefault(e => e.LECItemId == lecItem.LECItemId);
            if (newLECItem != null)
            {
                newLECItem.SortIndex = lecItem.SortIndex;
                newLECItem.HazardDescription = lecItem.HazardDescription;
                newLECItem.PossibleAccidents = lecItem.PossibleAccidents;
                newLECItem.HazardJudge_L = lecItem.HazardJudge_L;
                newLECItem.HazardJudge_E = lecItem.HazardJudge_E;
                newLECItem.HazardJudge_C = lecItem.HazardJudge_C;
                newLECItem.HazardJudge_D = lecItem.HazardJudge_D;
                newLECItem.RiskLevel = lecItem.RiskLevel;
                newLECItem.ControlMeasures = lecItem.ControlMeasures;
                newLECItem.ManagementMeasures = lecItem.ManagementMeasures;
                newLECItem.ProtectiveMeasures = lecItem.ProtectiveMeasures;
                newLECItem.OtherMeasures = lecItem.OtherMeasures;
                newLECItem.Remark = lecItem.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="LECItemId"></param>
        public static void DeleteLECItemById(string LECItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LECItem LECItem = db.Hazard_LECItem.FirstOrDefault(e => e.LECItemId == LECItemId);
            if (LECItem != null)
            {
                BLL.LECItemRecordService.DeleteLECItemRecordByLECItemId(LECItemId);
                db.Hazard_LECItem.DeleteOnSubmit(LECItem);
                db.SubmitChanges();
            }
        }
        
        /// <summary>
        /// 根据评价明细插入风险库并更新设备
        /// </summary>
        /// <param name="lecItem"></param>
        public static void SetSubmitInfo(Model.Hazard_LECItem lecItem,string userId)
        {
            string taskActivity = string.Empty;
            string installationId = string.Empty;
            string euipmentId = string.Empty;
            ////更新基础表中的等级 关联厂区地图显示
            BLL.RiskListService.SetEuipmentJobEnvironmentRiskLevel(lecItem.DataType, lecItem.DataId);
            if (lecItem.DataType == "0")
            {
                var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(lecItem.DataId);
                if (euipment != null)
                {
                    taskActivity = euipment.EuipmentName;
                    installationId = euipment.InstallationId;
                    euipmentId = euipment.EuipmentId;
                }
            }
            else
            {
                var jobEnvironment = BLL.JobEnvironmentService.GetJobEnvironmentByJobEnvironmentId(lecItem.DataId);
                if (jobEnvironment != null)
                {
                    taskActivity = jobEnvironment.JobEnvironmentName;
                    installationId = jobEnvironment.InstallationId;
                }
            }
            if (!string.IsNullOrEmpty(lecItem.RiskLevel) && !string.IsNullOrEmpty(installationId))
            {
                ///写入风险库
                Model.Hazard_RiskList newRiskList = new Model.Hazard_RiskList
                {
                    InstallationId = installationId,
                    TaskActivity = taskActivity,
                    HazardDescription = lecItem.HazardDescription,
                    PossibleAccidents = lecItem.PossibleAccidents,
                    RiskLevel = lecItem.RiskLevel,
                    EvaluationMethod = "LEC",
                    ControlMeasures = lecItem.ControlMeasures,
                    ManagementMeasures = lecItem.ManagementMeasures,
                    ProtectiveMeasures = lecItem.ProtectiveMeasures,
                    OtherMeasures = lecItem.OtherMeasures,
                    Cancelled = false,                  
                    LECItemId = lecItem.LECItemId,
                    RiskManId = userId, ///评价人
                    States = "1",  ////评价后直接进入风险信息库
                    EvaluationTime = DateTime.Now, ///评价时间
                    StartDate= DateTime.Now, ///启用时间
                    EuipmentId = euipmentId,
                };

                var getRiskList = BLL.RiskListService.GetRiskListByLECItemId(lecItem.LECItemId);
                if (getRiskList != null)
                {
                    getRiskList.InstallationId = installationId;
                    getRiskList.TaskActivity = taskActivity;
                    getRiskList.HazardDescription = lecItem.HazardDescription;
                    getRiskList.PossibleAccidents = lecItem.PossibleAccidents;
                    getRiskList.RiskLevel = lecItem.RiskLevel;
                    getRiskList.EvaluationMethod = "LEC";
                    getRiskList.ControlMeasures = lecItem.ControlMeasures;
                    getRiskList.ManagementMeasures = lecItem.ManagementMeasures;
                    getRiskList.ProtectiveMeasures = lecItem.ProtectiveMeasures;
                    getRiskList.OtherMeasures = lecItem.OtherMeasures;
                    getRiskList.Cancelled = false;
                    getRiskList.LECItemId = lecItem.LECItemId;
                    getRiskList.RiskManId = userId; ///评价人
                    getRiskList.States = "1";  ////评价后直接进入风险信息库
                    getRiskList.EvaluationTime = DateTime.Now; ///评价时间
                    getRiskList.StartDate = DateTime.Now; ///启用时间
                    getRiskList.EuipmentId = euipmentId;
                    BLL.RiskListService.UpdateRiskList(getRiskList);
                }
                else
                {
                    newRiskList.RiskListId = SQLHelper.GetNewID(typeof(Model.Hazard_RiskList));
                    BLL.RiskListService.AddRiskList(newRiskList);
                }
            }
        }
    }
}
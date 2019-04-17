using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class SCLItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="SCLItemId"></param>
        /// <returns></returns>
        public static Model.Hazard_SCLItem GetSCLItemById(string SCLItemId)
        {
            return Funs.DB.Hazard_SCLItem.FirstOrDefault(e => e.SCLItemId == SCLItemId);
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Hazard_SCLItem> GetSCLItemListByEuipmentId(string EuipmentId)
        {
            return (from x in Funs.DB.Hazard_SCLItem where x.EuipmentId == EuipmentId select x).ToList();
        }

        /// <summary>
        /// 添加SCLItem信息
        /// </summary>
        /// <param name="SCLItem"></param>
        public static void AddSCLItem(Model.Hazard_SCLItem SCLItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_SCLItem newSCLItem = new Model.Hazard_SCLItem
            {
                SCLItemId = SCLItem.SCLItemId,
                EuipmentId = SCLItem.EuipmentId,
                SortIndex = SCLItem.SortIndex,
                CheckItem = SCLItem.CheckItem,
                Standard = SCLItem.Standard,
                Consequence = SCLItem.Consequence,
                NowControlMeasures = SCLItem.NowControlMeasures,
                HazardJudge_L = SCLItem.HazardJudge_L,
                HazardJudge_S = SCLItem.HazardJudge_S,
                HazardJudge_S1 = SCLItem.HazardJudge_S1,
                HazardJudge_S2 = SCLItem.HazardJudge_S2,
                HazardJudge_S3 = SCLItem.HazardJudge_S3,
                HazardJudge_S4 = SCLItem.HazardJudge_S4,
                HazardJudge_S5 = SCLItem.HazardJudge_S5,
                HazardJudge_R = SCLItem.HazardJudge_R,
                RiskLevel = SCLItem.RiskLevel,
                ControlMeasures = SCLItem.ControlMeasures,
                ManagementMeasures = SCLItem.ManagementMeasures,
                ProtectiveMeasures = SCLItem.ProtectiveMeasures,
                OtherMeasures = SCLItem.OtherMeasures,
            };
            db.Hazard_SCLItem.InsertOnSubmit(newSCLItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改SCLItem信息
        /// </summary>
        /// <param name="SCLItem"></param>
        public static void UpdateSCLItem(Model.Hazard_SCLItem SCLItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_SCLItem newSCLItem = db.Hazard_SCLItem.FirstOrDefault(e => e.SCLItemId == SCLItem.SCLItemId);
            if (newSCLItem != null)
            {
                newSCLItem.SortIndex = SCLItem.SortIndex;
                newSCLItem.CheckItem = SCLItem.CheckItem;
                newSCLItem.Standard = SCLItem.Standard;
                newSCLItem.Consequence = SCLItem.Consequence;
                newSCLItem.NowControlMeasures = SCLItem.NowControlMeasures;
                newSCLItem.HazardJudge_L = SCLItem.HazardJudge_L;
                newSCLItem.HazardJudge_S = SCLItem.HazardJudge_S;
                newSCLItem.HazardJudge_S1 = SCLItem.HazardJudge_S1;
                newSCLItem.HazardJudge_S2 = SCLItem.HazardJudge_S2;
                newSCLItem.HazardJudge_S3 = SCLItem.HazardJudge_S3;
                newSCLItem.HazardJudge_S4 = SCLItem.HazardJudge_S4;
                newSCLItem.HazardJudge_S5 = SCLItem.HazardJudge_S5;
                newSCLItem.HazardJudge_R = SCLItem.HazardJudge_R;
                newSCLItem.RiskLevel = SCLItem.RiskLevel;
                newSCLItem.ControlMeasures = SCLItem.ControlMeasures;
                newSCLItem.ManagementMeasures = SCLItem.ManagementMeasures;
                newSCLItem.ProtectiveMeasures = SCLItem.ProtectiveMeasures;
                newSCLItem.OtherMeasures = SCLItem.OtherMeasures;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="SCLItemId"></param>
        public static void DeleteSCLItemById(string SCLItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_SCLItem SCLItem = db.Hazard_SCLItem.FirstOrDefault(e => e.SCLItemId == SCLItemId);
            if (SCLItem != null)
            {
                BLL.SCLItemRecordService.DeleteSCLItemRecordBySCLItemId(SCLItemId);
                db.Hazard_SCLItem.DeleteOnSubmit(SCLItem);
                db.SubmitChanges();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static void SetSubmitInfo(Model.Hazard_SCLItem sclItem,string userId)
        {
            string taskActivity = string.Empty;
            string installationId = string.Empty;
            ////更新基础表中的等级 关联厂区地图显示
            BLL.RiskListService.SetEuipmentJobEnvironmentRiskLevel("0", sclItem.EuipmentId);
            var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(sclItem.EuipmentId);
            if (euipment != null)
            {
                taskActivity = euipment.EuipmentName;
                installationId = euipment.InstallationId;
            }
            if (!string.IsNullOrEmpty(sclItem.RiskLevel) && !string.IsNullOrEmpty(installationId))
            {
                ///写入风险库
                Model.Hazard_RiskList newRiskList = new Model.Hazard_RiskList
                {
                    InstallationId = installationId,
                    TaskActivity = taskActivity,
                    HazardDescription = sclItem.CheckItem,
                    PossibleAccidents = sclItem.Consequence,
                    RiskLevel = sclItem.RiskLevel,
                    EvaluationMethod = "SCL",
                    ControlMeasures = sclItem.ControlMeasures,
                    ManagementMeasures = sclItem.ManagementMeasures,
                    ProtectiveMeasures = sclItem.ProtectiveMeasures,
                    OtherMeasures = sclItem.OtherMeasures,
                    Cancelled = false,                   
                    SCLItemId = sclItem.SCLItemId,
                    RiskManId = userId,
                    States = "1",  ////评价后直接进入风险信息库
                    EvaluationTime = System.DateTime.Now,
                    StartDate = DateTime.Now, ///启用时间
                    EuipmentId = sclItem.EuipmentId,
                };

                var getRiskList = BLL.RiskListService.GetRiskListBySCLItemId(sclItem.SCLItemId);
                if (getRiskList != null)
                {
                    getRiskList.InstallationId = installationId;
                    getRiskList.TaskActivity = taskActivity;
                    getRiskList.HazardDescription = sclItem.CheckItem;
                    getRiskList.PossibleAccidents = sclItem.Consequence;
                    getRiskList.RiskLevel = sclItem.RiskLevel;
                    getRiskList.EvaluationMethod = "SCL";
                    getRiskList.ControlMeasures = sclItem.ControlMeasures;
                    getRiskList.ManagementMeasures = sclItem.ManagementMeasures;
                    getRiskList.ProtectiveMeasures = sclItem.ProtectiveMeasures;
                    getRiskList.OtherMeasures = sclItem.OtherMeasures;
                    getRiskList.Cancelled = false;
                    getRiskList.SCLItemId = sclItem.SCLItemId;
                    getRiskList.RiskManId = userId;
                    getRiskList.States = "1";  ////评价后直接进入风险信息库
                    getRiskList.EvaluationTime = System.DateTime.Now;
                    getRiskList.StartDate = DateTime.Now; ///启用时间
                    getRiskList.EuipmentId = sclItem.EuipmentId;
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
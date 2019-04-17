using System.Linq;

namespace BLL
{
    public static class RiskListService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="RiskListId"></param>
        /// <returns></returns>
        public static Model.Hazard_RiskList GetRiskListById(string RiskListId)
        {
            return Funs.DB.Hazard_RiskList.FirstOrDefault(e => e.RiskListId == RiskListId);
        }

        /// <summary>
        /// 根据主键获取风险视图信息
        /// </summary>
        /// <param name="RiskListId"></param>
        /// <returns></returns>
        public static Model.View_Hazard_RiskList GetViewRiskListById(string RiskListId)
        {
            return Funs.DB.View_Hazard_RiskList.FirstOrDefault(e => e.RiskListId == RiskListId);
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Hazard_RiskList GetRiskListByLECItemId(string lecItemId)
        {
            return Funs.DB.Hazard_RiskList.FirstOrDefault(e => e.LECItemId == lecItemId);
        }
        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Hazard_RiskList GetRiskListBySCLItemId(string sclItemId)
        {
            return Funs.DB.Hazard_RiskList.FirstOrDefault(e => e.SCLItemId == sclItemId);
        }

        /// <summary>
        /// 添加RiskList信息
        /// </summary>
        /// <param name="riskList"></param>
        public static void AddRiskList(Model.Hazard_RiskList riskList)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_RiskList newRiskList = new Model.Hazard_RiskList
            {
                RiskListId = riskList.RiskListId,
                HiddenHazardId = riskList.HiddenHazardId,
                InstallationId = riskList.InstallationId,
                RiskPlace = riskList.RiskPlace,
                TaskActivity = riskList.TaskActivity,
                HazardDescription = riskList.HazardDescription,
                PossibleAccidents = riskList.PossibleAccidents,
                RiskLevel = riskList.RiskLevel,
                RiskValue = riskList.RiskValue,
                ControlMeasures = riskList.ControlMeasures,
                ManagementMeasures = riskList.ManagementMeasures,
                ProtectiveMeasures = riskList.ProtectiveMeasures,
                OtherMeasures = riskList.OtherMeasures,
                ControlUnit = riskList.ControlUnit,
                ControlRiskLevel = riskList.ControlRiskLevel,
                EvaluationMethod = riskList.EvaluationMethod,
                Cancelled = riskList.Cancelled,
                States = riskList.States,
                ControlUnitId = riskList.ControlUnitId,
                ControlInstallationId = riskList.ControlInstallationId,
                RiskManId = riskList.RiskManId,
                PatrolFrequency = riskList.PatrolFrequency,
                LECItemId = riskList.LECItemId,
                SCLItemId = riskList.SCLItemId,
                JHAItemId = riskList.JHAItemId,
                EvaluationTime = riskList.EvaluationTime,
                EuipmentId = riskList.EuipmentId,
                QRCodePosition=riskList.QRCodePosition,
                IsUsed = riskList.IsUsed,
        };

            db.Hazard_RiskList.InsertOnSubmit(newRiskList);
            db.SubmitChanges();           
        }

        /// <summary>
        /// 修改RiskList信息
        /// </summary>
        /// <param name="riskList"></param>
        public static void UpdateRiskList(Model.Hazard_RiskList riskList)
        {
            Model.HSSEDB_ENN db = Funs.DB;          
            Model.Hazard_RiskList updateRiskList = db.Hazard_RiskList.FirstOrDefault(e => e.RiskListId == riskList.RiskListId);
            if (updateRiskList != null)
            {
                updateRiskList.HiddenHazardId = riskList.HiddenHazardId;
                updateRiskList.InstallationId = riskList.InstallationId;
                updateRiskList.RiskPlace = riskList.RiskPlace;
                updateRiskList.TaskActivity = riskList.TaskActivity;
                updateRiskList.HazardDescription = riskList.HazardDescription;
                updateRiskList.PossibleAccidents = riskList.PossibleAccidents;
                updateRiskList.RiskLevel = riskList.RiskLevel;
                updateRiskList.RiskValue = riskList.RiskValue;
                updateRiskList.ControlMeasures = riskList.ControlMeasures;
                updateRiskList.ManagementMeasures = riskList.ManagementMeasures;
                updateRiskList.ProtectiveMeasures = riskList.ProtectiveMeasures;
                updateRiskList.OtherMeasures = riskList.OtherMeasures;
                updateRiskList.ControlUnit = riskList.ControlUnit;
                updateRiskList.ControlRiskLevel = riskList.ControlRiskLevel;
                updateRiskList.EvaluationMethod = riskList.EvaluationMethod;
                updateRiskList.Cancelled = riskList.Cancelled;
                updateRiskList.States = riskList.States;
                updateRiskList.ControlUnitId = riskList.ControlUnitId;
                updateRiskList.ControlInstallationId = riskList.ControlInstallationId;
                updateRiskList.RiskManId = riskList.RiskManId;
                updateRiskList.PatrolFrequency = riskList.PatrolFrequency;
                updateRiskList.LECItemId = riskList.LECItemId;
                updateRiskList.SCLItemId = riskList.SCLItemId;
                updateRiskList.JHAItemId = riskList.JHAItemId;
                updateRiskList.EvaluationTime = riskList.EvaluationTime;
                updateRiskList.EuipmentId = riskList.EuipmentId;
                updateRiskList.RiskOwnerIds = riskList.RiskOwnerIds;
                updateRiskList.RiskOwnerNames = riskList.RiskOwnerNames;
                updateRiskList.StartDate = riskList.StartDate;
                updateRiskList.QRCodePosition = riskList.QRCodePosition;
                updateRiskList.IsUsed = riskList.IsUsed;
                db.SubmitChanges();
            }
            if (riskList.IsUsed == true)
            {
                ////风险等级变化时 将岗位巡检人写入明细表
                //RiskListItemService.getRiskListItemByRiskList(updateRiskList);
                var riskListItem = from x in db.Hazard_RiskListItem where x.RiskListId == riskList.RiskListId select x;
                foreach (var item in riskListItem)
                {
                    PatrolPlanService.GetPatrolPlanByRiskListItem(item);
                }              
            }
            else
            {
                var deletePatrolPlan = from x in db.Hazard_PatrolPlan
                                       where x.RiskListId == riskList.RiskListId && x.EndTime >= System.DateTime.Now && !x.CheckDate.HasValue
                                       select x;
                if (deletePatrolPlan.Count() > 0)
                {
                    db.Hazard_PatrolPlan.DeleteAllOnSubmit(deletePatrolPlan);
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 根据主键删除信息(需要更新)
        /// </summary>
        /// <param name="riskListId"></param>
        public static void DeleteRiskListById(string riskListId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_RiskList getRiskList = db.Hazard_RiskList.FirstOrDefault(e => e.RiskListId == riskListId && (e.Cancelled == false || e.Cancelled == null));
            if (getRiskList != null)
            {
                ///删除风险明细信息
                BLL.RiskListItemService.DeleteRiskListItemByRiskListId(riskListId);
                ///删除风险巡检计划表
                BLL.PatrolPlanService.DeletePatrolPlanByRiskListId(riskListId);

                string lecItemId = getRiskList.LECItemId;
                string sclItemId = getRiskList.SCLItemId;
                var routingInspection = from x in db.Hazard_RoutingInspection where x.RiskListId == riskListId select x;
                if (routingInspection.Count() > 0)
                {
                    foreach (var item in routingInspection)
                    {
                        RoutingInspectionService.DeleteRoutingInspectionById(item.RoutingInspectionId);
                    }
                }

                db.Hazard_RiskList.DeleteOnSubmit(getRiskList);
                db.SubmitChanges();

                string dataType = string.Empty;
                string dataId = string.Empty;
                if (!string.IsNullOrEmpty(lecItemId))
                {
                    var lecItem = BLL.LECItemService.GetLECItemById(lecItemId);
                    if (lecItem != null)
                    {
                        dataType = lecItem.DataType;
                        dataId = lecItem.DataId;
                        BLL.LECItemService.DeleteLECItemById(lecItem.LECItemId);
                    }
                }
                if (!string.IsNullOrEmpty(sclItemId))
                {
                    var sclItem = BLL.SCLItemService.GetSCLItemById(sclItemId);
                    if (sclItem != null)
                    {
                        dataType = "0";
                        dataId = sclItem.EuipmentId;
                        BLL.SCLItemService.DeleteSCLItemById(getRiskList.SCLItemId);
                    }
                }
                ////更新基础表中的等级 关联厂区地图显示
                BLL.RiskListService.SetEuipmentJobEnvironmentRiskLevel(dataType, dataId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataId"></param>
        public static void SetEuipmentJobEnvironmentRiskLevel(string type, string dataId)
        {
            ////更新基础表中的等级 关联厂区地图显示
            string minLevel = string.Empty;
            minLevel = (from x in Funs.DB.Hazard_LECItem                           
                              where x.DataId == dataId
                              select x).Select(x => x.RiskLevel).Min();

            var minsclItem = (from x in Funs.DB.Hazard_SCLItem                           
                              where x.SCLItemId == dataId
                              select x).Select(x => x.RiskLevel).Min();
            if (Funs.GetNewIntOrZero(minsclItem) > Funs.GetNewIntOrZero(minLevel))
            {
                minLevel = minsclItem;
            }

            if (type == "0")
            {
                var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(dataId);
                if (euipment != null)
                {
                    euipment.RiskLevel = minLevel;
                    BLL.EuipmentService.UpdateEuipment(euipment);
                }
            }
            else
            {
                var jobEnvironment = BLL.JobEnvironmentService.GetJobEnvironmentByJobEnvironmentId(dataId);
                if (jobEnvironment != null)
                {
                    jobEnvironment.RiskLevel = minLevel;
                    BLL.JobEnvironmentService.UpdateJobEnvironment(jobEnvironment);
                }
            }
        }
    }
}
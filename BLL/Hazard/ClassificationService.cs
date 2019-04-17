using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class ClassificationService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        public static List<Model.SpClassificationList> getClassificationLists(string userId)
        {
            ///按责任人 巡检
            var riskOwnerLists = getRiskOwnerClassificationLists(userId);
            ///按岗位 巡检
            var workPostLists = getWorkPostClassificationLists(userId);
            foreach (var item in workPostLists) ////存在责任人巡检的 岗位巡检不计入。
            {
                if (riskOwnerLists.FirstOrDefault(x => x.NewId == item.NewId) == null)
                {
                    riskOwnerLists.Add(item);
                }
            }

            return riskOwnerLists;
        }

        #region 获取岗位下巡检风险及人员巡检时间
        /// <summary>
        /// 获取岗位下巡检风险及人员巡检时间
        /// </summary>
        /// <returns></returns>
        public static List<Model.SpClassificationList> getWorkPostClassificationLists(string userId)
        {
            List<Model.SpClassificationList> getWorkPostClassificationLists = new List<Model.SpClassificationList>();

            var ViewRiskLists = from x in Funs.DB.View_Hazard_RiskList
                                where (x.Cancelled == false || x.Cancelled == null)
                                && x.StartDate.HasValue
                                select x;
            ///取存在风险等级、巡检频次的岗位
            var workPosts = from x in Funs.DB.Base_WorkPost
                            where x.RiskLevelId != null && x.Frequency.HasValue
                            select x;
            foreach (var itemWorkPosts in workPosts)
            {
                var userLists = (from x in Funs.DB.View_Sys_User where x.WorkPostId.Contains(itemWorkPosts.WorkPostId) select x).ToList();
                if (!string.IsNullOrEmpty(userId) && userId != BLL.Const._Null)
                {
                    userLists = userLists.Where(x => x.UserId == userId).ToList();
                }

                foreach (var itemUser in userLists)
                {
                    ///当前岗位可巡检的风险等级
                    List<string> riskLevelLists = Funs.GetStrListByStr(itemWorkPosts.RiskLevelId, ',');
                    foreach (var itemRiskLevel in riskLevelLists)
                    {
                        ///取当前等级下 有多少风险
                        var riskLists = ViewRiskLists.Where(x => x.RiskLevel == itemRiskLevel);                          
                        foreach (var item in riskLists)
                        {
                            ///风险巡检频次有值，且风险不在列表中
                            if ((string.IsNullOrEmpty(itemUser.InstallationId) || itemUser.InstallationId.Contains(item.InstallationId))
                                && (getWorkPostClassificationLists.FirstOrDefault(x => x.RiskListId == item.RiskListId) == null))
                            {
                                Model.SpClassificationList newSpClassificationList = new Model.SpClassificationList
                                {
                                    NewId= item.RiskListId+'|'+ itemUser.UserId,
                                    RiskListId = item.RiskListId,                                    
                                    DepartName = itemUser.DepartName,
                                    InstallationName = itemUser.InstallationName,
                                    UserId = itemUser.UserId,
                                    UserName = itemUser.UserName,
                                    UserCode = itemUser.UserCode,
                                    HazardInstallationName = item.InstallationName,
                                    TaskActivity = item.TaskActivity,
                                    HazardDescription = item.HazardDescription,
                                    RiskLevelName = item.RiskLevelName,
                                    Frequency = itemWorkPosts.Frequency,
                                };
                                ///取最近一次巡检时间
                                var patrolTimeMax = Funs.DB.Hazard_RoutingInspection.Where(x => x.RiskListId == item.RiskListId && x.PatrolManId == itemUser.UserId).Max(x => x.PatrolTime);
                                if (patrolTimeMax != null)
                                {
                                    newSpClassificationList.PatrolTime = patrolTimeMax;
                                    newSpClassificationList.NextPatrolTime = patrolTimeMax.Value.AddDays(itemWorkPosts.Frequency.Value);
                                }
                                else
                                {
                                    if (System.DateTime.Now > item.StartDate)
                                        newSpClassificationList.NextPatrolTime = System.DateTime.Now;
                                }
                                /// 
                                getWorkPostClassificationLists.Add(newSpClassificationList);
                            }
                        }
                    }
                }
            }

            return getWorkPostClassificationLists;
        }
        #endregion

        #region 获取责任人巡检风险及巡检时间
        /// <summary>
        /// 获取责任人巡检风险及巡检时间
        /// </summary>
        /// <returns></returns>
        public static List<Model.SpClassificationList> getRiskOwnerClassificationLists(string userId)
        {
            List<Model.SpClassificationList> getRiskOwnerClassificationLists = new List<Model.SpClassificationList>();
            var riskListItems = from x in Funs.DB.Hazard_RiskListItem select x;
            if (!string.IsNullOrEmpty(userId) && userId != BLL.Const._Null)
            {
                riskListItems = riskListItems.Where(x => x.RiskOwnerId == userId);
            }
            var ViewRiskLists=  from x in Funs.DB.View_Hazard_RiskList
                                where  (x.Cancelled == false || x.Cancelled == null) && x.StartDate.HasValue
                                select x;
            foreach (var itemRisk in riskListItems)
            {
                ///取当前等级下 有多少风险
                var riskLists = ViewRiskLists.Where(x => x.RiskListId == itemRisk.RiskListId);                 
                foreach (var item in riskLists)
                {
                    var userView = Funs.DB.View_Sys_User.FirstOrDefault(x => x.UserId == itemRisk.RiskOwnerId);
                    ///风险巡检频次有值，且风险不在列表中
                    if (getRiskOwnerClassificationLists.FirstOrDefault(x => x.RiskListId == item.RiskListId && x.UserId == itemRisk.RiskOwnerId) == null && userView != null)
                    {
                        Model.SpClassificationList newSpClassificationList = new Model.SpClassificationList
                        {
                            NewId = item.RiskListId + '|' + userView.UserId,
                            RiskListId = item.RiskListId,
                            DepartName = userView.DepartName,
                            InstallationName = userView.InstallationName,
                            UserId = userView.UserId,
                            UserName = userView.UserName,
                            HazardInstallationName = item.InstallationName,
                            TaskActivity = item.TaskActivity,
                            HazardDescription = item.HazardDescription,
                            RiskLevelName = item.RiskLevelName,
                            Frequency = itemRisk.Frequency,
                        };
                        ///取最近一次巡检时间
                        var patrolTimeMax = Funs.DB.Hazard_RoutingInspection.Where(x => x.RiskListId == item.RiskListId && x.PatrolManId == userView.UserId).Max(x => x.PatrolTime);
                        if (patrolTimeMax != null)
                        {
                            newSpClassificationList.PatrolTime = patrolTimeMax;
                            newSpClassificationList.NextPatrolTime = patrolTimeMax.Value.AddDays(itemRisk.Frequency.Value);
                        }
                        else
                        {
                            if (System.DateTime.Now > item.StartDate)
                                newSpClassificationList.NextPatrolTime = System.DateTime.Now;
                        }
                        /// 
                        getRiskOwnerClassificationLists.Add(newSpClassificationList);
                    }
                }
            }

            return getRiskOwnerClassificationLists;
        }
        #endregion

        /// <summary>
        /// 获取巡检明细
        /// </summary>
        public static void getRiskPlanItem()
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var getRiskListItem = from x in db.Hazard_RiskListItem
                                  join y in db.Hazard_RiskList on x.RiskListId equals y.RiskListId
                                  where (x.IsRiskOwner == null || x.IsRiskOwner == false) 
                                  && y.StartDate.HasValue && (y.Cancelled == null || y.Cancelled == false)
                                  select x;
            if (getRiskListItem.Count() > 0)
            {
                db.Hazard_RiskListItem.DeleteAllOnSubmit(getRiskListItem);
                db.SubmitChanges();
            }

            var viewRiskPlan = from x in Funs.DB.View_RiskPlanRiskListItem
                               select x;
            foreach (var item in viewRiskPlan)
            {
                var riskListItem = db.Hazard_RiskListItem.FirstOrDefault(x => x.RiskListId == item.RiskListId && x.RiskOwnerId == item.UserId);
                if (riskListItem == null)
                {
                    Model.Hazard_RiskListItem newRiskListItem = new Model.Hazard_RiskListItem
                    {
                        RiskItemId = SQLHelper.GetNewID(typeof(Model.Hazard_RiskListItem)),
                        RiskListId = item.RiskListId,
                        RiskOwnerId = item.UserId,
                        Frequency = item.Frequency,
                        IsRiskOwner = false,
                    };

                    db.Hazard_RiskListItem.InsertOnSubmit(newRiskListItem);
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 得到巡检计划表
        /// </summary>
        public static void getPatrolPlanByRiskListItem()
        {
            Model.HSSEDB_ENN db = Funs.DB;
            ////设置计划结束时间为当年最后一天
            DateTime endDate = new DateTime(DateTime.Now.Year, 1, 1).AddYears(1).AddDays(-1);
            var viewRiskPlan = from x in db.View_RiskPlanRiskListItemTime                               
                               select x;           
            foreach (var itemView in viewRiskPlan)
            {
                if (itemView.Frequency > 0)
                {
                    ///开始时间
                    DateTime startDate = itemView.StartDate.Value;
                    ////已巡检 风险最大时间   
                    var maxPatrolPlan = db.Hazard_PatrolPlan.Where(x => x.RiskListId == itemView.RiskListId
                                     && x.UserId == itemView.RiskOwnerId && x.CheckDate.HasValue).Max(x => x.EndTime);
                    if (maxPatrolPlan.HasValue)
                    {
                        startDate = maxPatrolPlan.Value.AddDays(1);
                    }

                    for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(itemView.Frequency.Value))
                    {
                        Model.Hazard_PatrolPlan newPatrolPlan = new Model.Hazard_PatrolPlan
                        {
                            UserId = itemView.RiskOwnerId,
                            RiskListId = itemView.RiskListId,
                            IsRiskOwner = itemView.IsRiskOwner,
                            Frequency = itemView.Frequency,
                            StartDate = dt,
                            EndTime = dt.AddDays(itemView.Frequency.Value - 1),
                        };

                        BLL.PatrolPlanService.AddPatrolPlan(newPatrolPlan);
                    }
                }
            }
        }
    }
}
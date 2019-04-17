using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 风险巡检计划表
    /// </summary>
    public static class PatrolPlanService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="patrolPlanId"></param>
        /// <returns></returns>
        public static Model.Hazard_PatrolPlan GetPatrolPlanById(string patrolPlanId)
        {
            return Funs.DB.Hazard_PatrolPlan.FirstOrDefault(e => e.PatrolPlanId == patrolPlanId);
        }

        /// <summary>
        /// 根据用户ID获取当前人巡检信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Hazard_PatrolPlan> GetPatrolPlanListByUserId(string userId)
        {
            return (from x in Funs.DB.Hazard_PatrolPlan where x.UserId == userId select x).ToList();
        }

        /// <summary>
        /// 根据风险ID获取巡检信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Hazard_PatrolPlan> GetPatrolPlanListByRiskListId(string riskListId)
        {
            return (from x in Funs.DB.Hazard_PatrolPlan where x.RiskListId == riskListId select x).ToList();
        }

        /// <summary>
        /// 添加PatrolPlan信息
        /// </summary>
        /// <param name="PatrolPlan"></param>
        public static void AddPatrolPlan(Model.Hazard_PatrolPlan PatrolPlan)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_PatrolPlan newPatrolPlan = new Model.Hazard_PatrolPlan
            {
                PatrolPlanId = SQLHelper.GetNewID(typeof(Model.Hazard_PatrolPlan)),
                UserId = PatrolPlan.UserId,
                RiskListId = PatrolPlan.RiskListId,
                IsRiskOwner = PatrolPlan.IsRiskOwner,
                Frequency = PatrolPlan.Frequency,
                StartDate = PatrolPlan.StartDate,
                EndTime = PatrolPlan.EndTime,
                CheckDate = PatrolPlan.CheckDate,
            };
            db.Hazard_PatrolPlan.InsertOnSubmit(newPatrolPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改PatrolPlan信息
        /// </summary>
        /// <param name="patrolPlan"></param>
        public static void UpdatePatrolPlan(Model.Hazard_PatrolPlan patrolPlan)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_PatrolPlan newPatrolPlan = db.Hazard_PatrolPlan.FirstOrDefault(e => e.PatrolPlanId == patrolPlan.PatrolPlanId);
            if (newPatrolPlan != null)
            {
                newPatrolPlan.UserId = patrolPlan.UserId;
                newPatrolPlan.RiskListId = patrolPlan.RiskListId;
                newPatrolPlan.IsRiskOwner = patrolPlan.IsRiskOwner;
                newPatrolPlan.Frequency = patrolPlan.Frequency;
                newPatrolPlan.StartDate = patrolPlan.StartDate;
                newPatrolPlan.EndTime = patrolPlan.EndTime;
                newPatrolPlan.CheckDate = patrolPlan.CheckDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="patrolPlanId"></param>
        public static void DeletePatrolPlanById(string patrolPlanId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_PatrolPlan PatrolPlan = db.Hazard_PatrolPlan.FirstOrDefault(e => e.PatrolPlanId == patrolPlanId);
            if (PatrolPlan != null)
            {
                db.Hazard_PatrolPlan.DeleteOnSubmit(PatrolPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据用户ID删除信息
        /// </summary>
        /// <param name="PatrolPlanId"></param>
        public static void DeletePatrolPlanByUserId(string userId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var patrolPlans = from x in db.Hazard_PatrolPlan where x.UserId == userId select x;
            if (patrolPlans.Count() > 0)
            {
                db.Hazard_PatrolPlan.DeleteAllOnSubmit(patrolPlans);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据风险ID删除信息
        /// </summary>
        /// <param name="riskListId"></param>
        public static void DeletePatrolPlanByRiskListId(string riskListId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var patrolPlans = from x in db.Hazard_PatrolPlan where x.RiskListId == riskListId select x;
            if (patrolPlans.Count() > 0)
            {
                db.Hazard_PatrolPlan.DeleteAllOnSubmit(patrolPlans);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据风险ID删除信息
        /// </summary>
        /// <param name="riskListId"></param>
        public static void DeletePatrolPlanByRiskListIdDate(string riskListId,string userId, DateTime date)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var patrolPlans = from x in db.Hazard_PatrolPlan
                              where x.RiskListId == riskListId && x.UserId == userId && x.StartDate >= date
                              select x;
            if (patrolPlans.Count() > 0)
            {
                db.Hazard_PatrolPlan.DeleteAllOnSubmit(patrolPlans);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据删除未巡检的信息
        /// </summary>
        /// <param name="riskListId"></param>
        public static void DeletePatrolPlanCheckDateNull()
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var patrolPlans = from x in db.Hazard_PatrolPlan
                              where !x.CheckDate.HasValue
                              select x;
            if (patrolPlans.Count() > 0)
            {
                db.Hazard_PatrolPlan.DeleteAllOnSubmit(patrolPlans);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按风险巡检明细生成计划考核表（近两年）
        /// </summary>
        /// <param name="userId"></param>
        public static void GetPatrolPlanByRiskListItem(Model.Hazard_RiskListItem riskListItem)
        {
            ////设置计划结束时间为当年最后一天
            DateTime endDate = new DateTime(DateTime.Now.Year, 1, 1).AddYears(1).AddDays(-1);
            var riskList = Funs.DB.Hazard_RiskList.FirstOrDefault(x => x.RiskListId == riskListItem.RiskListId && x.IsUsed == true);
            if (riskList != null && riskList.StartDate.HasValue)
            {
                ///开始时间
                DateTime startDate = riskList.StartDate.Value;
                var patrolPlan = Funs.DB.Hazard_PatrolPlan.FirstOrDefault(x => x.RiskListId == riskListItem.RiskListId && x.UserId == riskListItem.RiskOwnerId);
                if (patrolPlan != null)
                {
                    if (patrolPlan.Frequency == riskListItem.Frequency)
                    {
                        ////巡检风险最大时间   
                        var maxPatrolPlan = Funs.DB.Hazard_PatrolPlan.Where(x => x.RiskListId == riskListItem.RiskListId && x.UserId == riskListItem.RiskOwnerId).Max(x => x.EndTime);
                        if (maxPatrolPlan.HasValue)
                        {
                            startDate = maxPatrolPlan.Value.AddDays(1);
                        }
                    }
                    else
                    {
                        ////已巡检 风险最大时间   
                        var maxPatrolPlan = Funs.DB.Hazard_PatrolPlan.Where(x => x.RiskListId == riskListItem.RiskListId && x.UserId == riskListItem.RiskOwnerId && x.CheckDate.HasValue).Max(x => x.EndTime);
                        if (maxPatrolPlan.HasValue)
                        {
                            startDate = maxPatrolPlan.Value.AddDays(1);
                        }
                        ////删除未巡检的计划
                        DeletePatrolPlanByRiskListIdDate(riskListItem.RiskListId, riskListItem.RiskOwnerId, startDate);
                    }
                }
                if (riskListItem.Frequency > 0)
                {
                    for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(riskListItem.Frequency.Value))
                    {
                        Model.Hazard_PatrolPlan newPatrolPlan = new Model.Hazard_PatrolPlan
                        {
                            UserId = riskListItem.RiskOwnerId,
                            RiskListId = riskListItem.RiskListId,
                            IsRiskOwner = riskListItem.IsRiskOwner,
                            Frequency = riskListItem.Frequency,
                            StartDate = dt,
                            EndTime = dt.AddDays(riskListItem.Frequency.Value - 1),
                        };

                        AddPatrolPlan(newPatrolPlan);
                    }
                }
            }
        }
    }
}
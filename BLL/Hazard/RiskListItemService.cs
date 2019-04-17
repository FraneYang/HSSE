using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class RiskListItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="RiskItemId"></param>
        /// <returns></returns>
        public static Model.Hazard_RiskListItem GetRiskListItemById(string RiskItemId)
        {
            return Funs.DB.Hazard_RiskListItem.FirstOrDefault(e => e.RiskItemId == RiskItemId);
        }

        /// <summary>
        /// 添加RiskListItem信息
        /// </summary>
        /// <param name="riskListItem"></param>
        public static void AddRiskListItem(Model.Hazard_RiskListItem riskListItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_RiskListItem newRiskListItem = new Model.Hazard_RiskListItem
            {
                RiskItemId = SQLHelper.GetNewID(typeof(Model.Hazard_RiskListItem)),
                RiskListId = riskListItem.RiskListId,
                RiskOwnerId = riskListItem.RiskOwnerId,
                Frequency = riskListItem.Frequency,
                IsRiskOwner = riskListItem.IsRiskOwner,
            };

            db.Hazard_RiskListItem.InsertOnSubmit(newRiskListItem);
            db.SubmitChanges();
            ///计划表
            PatrolPlanService.GetPatrolPlanByRiskListItem(newRiskListItem);
        }

        /// <summary>
        /// 修改RiskListItem信息
        /// </summary>
        /// <param name="riskListItem"></param>
        public static void UpdateRiskListItem(Model.Hazard_RiskListItem riskListItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_RiskListItem updateRiskListItem = db.Hazard_RiskListItem.FirstOrDefault(e => e.RiskItemId == riskListItem.RiskItemId);
            if (updateRiskListItem != null)
            {
                //newRiskListItem.RiskOwnerId = riskListItem.RiskOwnerId;
                updateRiskListItem.IsRiskOwner = riskListItem.IsRiskOwner;
                updateRiskListItem.Frequency = riskListItem.Frequency;
                db.SubmitChanges();
            }

            ///计划表
            PatrolPlanService.GetPatrolPlanByRiskListItem(updateRiskListItem);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="riskItemId"></param>
        public static void DeleteRiskListItemById(string riskItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_RiskListItem getRiskListItem = db.Hazard_RiskListItem.FirstOrDefault(e => e.RiskItemId == riskItemId);
            if (getRiskListItem != null)
            {
                db.Hazard_RiskListItem.DeleteOnSubmit(getRiskListItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据风险主键删除
        /// </summary>
        /// <param name="RiskItemId"></param>
        public static void DeleteRiskListItemByRiskListId(string riskListId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var getRiskListItem = from x in db.Hazard_RiskListItem where x.RiskListId == riskListId select x;
            if (getRiskListItem.Count() > 0)
            {
                db.Hazard_RiskListItem.DeleteAllOnSubmit(getRiskListItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据风险主键删除
        /// </summary>
        /// <param name="RiskItemId"></param>
        public static void DeleteRiskListItemByUserId(string userId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var getRiskListItem = from x in db.Hazard_RiskListItem where x.RiskOwnerId == userId select x;
            if (getRiskListItem.Count() > 0)
            {
                db.Hazard_RiskListItem.DeleteAllOnSubmit(getRiskListItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据风险主键删除岗位巡检人
        /// </summary>
        /// <param name="RiskItemId"></param>
        public static void DeleteRiskListItemWorkPostByRiskListId(string riskListId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var getRiskListItem = from x in db.Hazard_RiskListItem
                                  where x.RiskListId == riskListId && (x.IsRiskOwner == null || x.IsRiskOwner == false)
                                  select x;
            if (getRiskListItem.Count() > 0)
            {
                db.Hazard_RiskListItem.DeleteAllOnSubmit(getRiskListItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据用户主键删除岗位巡检人
        /// </summary>
        /// <param name="RiskItemId"></param>
        public static void DeleteRiskListItemWorkPostByUserId(string userId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var getRiskListItem = from x in db.Hazard_RiskListItem
                                  where x.RiskOwnerId == userId && (x.IsRiskOwner == null || x.IsRiskOwner == false)
                                  select x;
            if (getRiskListItem.Count() > 0)
            {
                db.Hazard_RiskListItem.DeleteAllOnSubmit(getRiskListItem);
                db.SubmitChanges();
            }
        }

        #region 根据 人员、岗位、风险变化时 将岗位巡检人写入明细表
       /// <summary>
       /// 
       /// </summary>
       /// <param name="riskList"></param>
        public static void getRiskListItemByRiskList(Model.Hazard_RiskList riskList)
        {
            ////删除 风险 岗位巡检人
            DeleteRiskListItemWorkPostByRiskListId(riskList.RiskListId);

            var viewRiskPlan = from x in Funs.DB.View_RiskPlanRiskListItem
                               where x.RiskListId == riskList.RiskListId
                               select x;
            foreach (var item in viewRiskPlan)
            {
                var riskListItem = Funs.DB.Hazard_RiskListItem.FirstOrDefault(x => x.RiskListId == riskList.RiskListId && x.RiskOwnerId == item.UserId);
                if (riskListItem == null)
                {
                    Model.Hazard_RiskListItem newRiskListItem = new Model.Hazard_RiskListItem
                    {
                        RiskListId = riskList.RiskListId,
                        RiskOwnerId = item.UserId,
                        Frequency = item.Frequency,
                        IsRiskOwner = false,
                    };

                    AddRiskListItem(newRiskListItem);
                }
            }
        }

        /// <summary>
        /// 人员新增或岗位变化时 将岗位巡检人 加入风险巡检表
        /// </summary>
        /// <param name="userId"></param>
        public static void getRiskListItemByUser(Model.Sys_User user)
        {
            ////删除 风险 岗位巡检人
            DeleteRiskListItemWorkPostByUserId(user.UserId);
            var riskLists = Funs.DB.View_RiskPlanRiskListItem.Where(x => x.UserId == user.UserId);           
            foreach (var itemRiskList in riskLists)
            {
                var riskListItem = Funs.DB.Hazard_RiskListItem.FirstOrDefault(x => x.RiskListId == itemRiskList.RiskListId && x.RiskOwnerId == user.UserId);
                if (riskListItem == null)
                {
                    Model.Hazard_RiskListItem newRiskListItem = new Model.Hazard_RiskListItem
                    {
                        RiskListId = itemRiskList.RiskListId,
                        RiskOwnerId = user.UserId,
                        Frequency = itemRiskList.Frequency,
                        IsRiskOwner = false,
                    };

                    AddRiskListItem(newRiskListItem);
                }
            }
        }

        /// <summary>
        /// 岗位巡检级别变化时 将岗位巡检人 加入风险巡检表
        /// </summary>
        /// <param name="userId"></param>
        public static void getRiskListItemByWorkPost(Model.Base_WorkPost workPost)
        {
            var users = Funs.DB.Sys_User.Where(x => x.WorkPostId.Contains(workPost.WorkPostId));
            foreach (var item in users)
            {
                getRiskListItemByUser(item);
            }
        }
        #endregion
    }
}
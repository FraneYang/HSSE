using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class WorkPostService
   {
       public static Model.HSSEDB_ENN db = Funs.DB;

       /// <summary>
       /// 根据主键获取信息
       /// </summary>
       /// <param name="groupId"></param>
       /// <returns></returns>
       public static Model.Base_WorkPost GetWorkPostById(string workPostId)
       {
           return Funs.DB.Base_WorkPost.FirstOrDefault(e => e.WorkPostId == workPostId);
       }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddWorkPost(Model.Base_WorkPost workPost)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_WorkPost newWorkPost = new Model.Base_WorkPost
            {
                WorkPostId = workPost.WorkPostId,
                WorkPostCode = workPost.WorkPostCode,
                WorkPostName = workPost.WorkPostName,
                IsHsse = workPost.IsHsse,
                Remark = workPost.Remark,
                PostType = workPost.PostType,
                IsAuditFlow = workPost.IsAuditFlow,
                IsShowChart = workPost.IsShowChart,
                RiskLevelId = workPost.RiskLevelId,
                RiskLevelName = workPost.RiskLevelName,
                Frequency = workPost.Frequency
            };

            db.Base_WorkPost.InsertOnSubmit(newWorkPost);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateWorkPost(Model.Base_WorkPost workPost)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            string newRiskLevelId = workPost.RiskLevelId;
            string oldRiskLevelId = string.Empty;
            Model.Base_WorkPost updateWorkPost = db.Base_WorkPost.FirstOrDefault(e => e.WorkPostId == workPost.WorkPostId);
            if (updateWorkPost != null)
            {
                oldRiskLevelId = workPost.RiskLevelId;
                updateWorkPost.WorkPostCode = workPost.WorkPostCode;
                updateWorkPost.WorkPostName = workPost.WorkPostName;
                updateWorkPost.PostType = workPost.PostType;
                updateWorkPost.IsHsse = workPost.IsHsse;
                updateWorkPost.Remark = workPost.Remark;
                updateWorkPost.IsAuditFlow = workPost.IsAuditFlow;
                updateWorkPost.IsShowChart = workPost.IsShowChart;
                updateWorkPost.RiskLevelId = workPost.RiskLevelId;
                updateWorkPost.RiskLevelName = workPost.RiskLevelName;
                updateWorkPost.Frequency = workPost.Frequency;
                db.SubmitChanges();
            }
            if (newRiskLevelId != oldRiskLevelId)
            {
                ////岗位巡检级别变化时 将岗位巡检人写入明细表
                RiskListItemService.getRiskListItemByWorkPost(updateWorkPost);
            }
        }

       /// <summary>
       /// 根据主键删除信息
       /// </summary>
       /// <param name="workPostId"></param>
       public static void DeleteWorkPostById(string workPostId)
       {
           Model.HSSEDB_ENN db = Funs.DB;
           Model.Base_WorkPost workPost = db.Base_WorkPost.FirstOrDefault(e => e.WorkPostId == workPostId);
           {
               db.Base_WorkPost.DeleteOnSubmit(workPost);
               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 获取类别下拉项
       /// </summary>
       /// <returns></returns>
       public static List<Model.Base_WorkPost> GetWorkPostList()
       {
           var list = (from x in Funs.DB.Base_WorkPost orderby x.WorkPostCode select x).ToList();
           return list;
       }

       #region 表下拉框
       /// <summary>
       ///  表下拉框
       /// </summary>
       /// <param name="dropName">下拉框名字</param>
       /// <param name="isShowPlease">是否显示请选择</param>
       public static void InitWorkPostDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
       {
           dropName.DataValueField = "WorkPostId";
           dropName.DataTextField = "WorkPostName";
           dropName.DataSource = BLL.WorkPostService.GetWorkPostList();
           dropName.DataBind();
           if (isShowPlease)
           {
               Funs.FineUIPleaseSelect(dropName);
           }
       }
       #endregion

       /// <summary>
       /// 得到岗位名称字符串
       /// </summary>
       /// <param name="bigType"></param>
       /// <returns></returns>
       public static string getWorkPostNamesWorkPostIds(object workPostIds)
       {
           string workPostName = string.Empty;
           if (workPostIds != null)
           {
               string[] roles = workPostIds.ToString().Split(',');
               foreach (string roleId in roles)
               {
                   var q = GetWorkPostById(roleId);
                   if (q != null)
                   {
                       workPostName += q.WorkPostName + ",";
                   }
               }
               if (workPostName != string.Empty)
               {
                   workPostName = workPostName.Substring(0, workPostName.Length - 1); ;
               }
           }

           return workPostName;
       }
   }
}
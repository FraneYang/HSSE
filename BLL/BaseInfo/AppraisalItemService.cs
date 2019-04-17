using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 测评考核项
    /// </summary>
    public static class AppraisalItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取测评考核项
        /// </summary>
        /// <param name="appraisalItemId"></param>
        /// <returns></returns>
        public static Model.Base_AppraisalItem GetAppraisalItemById(string appraisalItemId)
        {
            return Funs.DB.Base_AppraisalItem.FirstOrDefault(e => e.AppraisalItemId == appraisalItemId);
        }

        /// <summary>
        /// 添加测评考核项
        /// </summary>
        /// <param name="appraisalItem"></param>
        public static void AddAppraisalItem(Model.Base_AppraisalItem appraisalItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_AppraisalItem newAppraisalItem = new Model.Base_AppraisalItem
            {
                AppraisalItemId = appraisalItem.AppraisalItemId,
                Code = appraisalItem.Code,
                CheckItem = appraisalItem.CheckItem,
                Score = appraisalItem.Score,
                Remark = appraisalItem.Remark
            };

            db.Base_AppraisalItem.InsertOnSubmit(newAppraisalItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改测评考核项
        /// </summary>
        /// <param name="appraisalItem"></param>
        public static void UpdateAppraisalItem(Model.Base_AppraisalItem appraisalItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_AppraisalItem newAppraisalItem = db.Base_AppraisalItem.FirstOrDefault(e => e.AppraisalItemId == appraisalItem.AppraisalItemId);
            if (newAppraisalItem != null)
            {
                newAppraisalItem.Code = appraisalItem.Code;
                newAppraisalItem.CheckItem = appraisalItem.CheckItem;
                newAppraisalItem.Score = appraisalItem.Score;
                newAppraisalItem.Remark = appraisalItem.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除测评考核项
        /// </summary>
        /// <param name="appraisalItemId"></param>
        public static void DeleteAppraisalItemById(string appraisalItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_AppraisalItem AppraisalItem = db.Base_AppraisalItem.FirstOrDefault(e => e.AppraisalItemId == appraisalItemId);
            if (AppraisalItem != null)
            {
                db.Base_AppraisalItem.DeleteOnSubmit(AppraisalItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取测评考核项列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_AppraisalItem> GetAppraisalItemList()
        {
            return (from x in Funs.DB.Base_AppraisalItem orderby x.Code select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitAppraisalItemDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "AppraisalItemId";
            dropName.DataTextField = "CheckItem";
            dropName.DataSource = BLL.AppraisalItemService.GetAppraisalItemList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

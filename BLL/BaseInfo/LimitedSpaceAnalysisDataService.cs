using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 受限分析点数据
    /// </summary>
    public static class LimitedSpaceAnalysisDataService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取受限分析点数据
        /// </summary>
        /// <param name="AnalysisDataId"></param>
        /// <returns></returns>
        public static Model.Base_LimitedSpaceAnalysisData GetLimitedSpaceAnalysisDataById(string AnalysisDataId)
        {
            return Funs.DB.Base_LimitedSpaceAnalysisData.FirstOrDefault(e => e.AnalysisDataId == AnalysisDataId);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="AnalysisDataId"></param>
        /// <param name="DataValue"></param>
        /// <returns></returns>
        public static bool IsExistDataValue(string AnalysisDataId, string DataValue)
        {
            bool isExist = false;
            Model.Base_LimitedSpaceAnalysisData LimitedSpaceAnalysisData = new Model.Base_LimitedSpaceAnalysisData();
            if (!string.IsNullOrEmpty(AnalysisDataId))
            {
                LimitedSpaceAnalysisData = Funs.DB.Base_LimitedSpaceAnalysisData.FirstOrDefault(x => x.AnalysisPoint == DataValue && x.AnalysisDataId != AnalysisDataId);
            }
            else
            {
                LimitedSpaceAnalysisData = Funs.DB.Base_LimitedSpaceAnalysisData.FirstOrDefault(x => x.AnalysisPoint == DataValue);
            }
            if (LimitedSpaceAnalysisData != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加受限分析点数据
        /// </summary>
        /// <param name="LimitedSpaceAnalysisData"></param>
        public static void AddLimitedSpaceAnalysisData(Model.Base_LimitedSpaceAnalysisData LimitedSpaceAnalysisData)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_LimitedSpaceAnalysisData newLimitedSpaceAnalysisData = new Model.Base_LimitedSpaceAnalysisData
            {
                AnalysisDataId = LimitedSpaceAnalysisData.AnalysisDataId,
                SortIndex = LimitedSpaceAnalysisData.SortIndex,
                AnalysisPoint = LimitedSpaceAnalysisData.AnalysisPoint,
                Category = LimitedSpaceAnalysisData.Category,
                MinData = LimitedSpaceAnalysisData.MinData,
                MaxData = LimitedSpaceAnalysisData.MaxData,
                Measure = LimitedSpaceAnalysisData.Measure,
            };
            db.Base_LimitedSpaceAnalysisData.InsertOnSubmit(newLimitedSpaceAnalysisData);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改受限分析点数据
        /// </summary>
        /// <param name="LimitedSpaceAnalysisData"></param>
        public static void UpdateLimitedSpaceAnalysisData(Model.Base_LimitedSpaceAnalysisData LimitedSpaceAnalysisData)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_LimitedSpaceAnalysisData newLimitedSpaceAnalysisData = db.Base_LimitedSpaceAnalysisData.FirstOrDefault(e => e.AnalysisDataId == LimitedSpaceAnalysisData.AnalysisDataId);
            if (newLimitedSpaceAnalysisData != null)
            {
                //newLimitedSpaceAnalysisData.SortIndex = LimitedSpaceAnalysisData.SortIndex;
                //newLimitedSpaceAnalysisData.AnalysisPoint = LimitedSpaceAnalysisData.AnalysisPoint;
                //newLimitedSpaceAnalysisData.Category = LimitedSpaceAnalysisData.Category;
                newLimitedSpaceAnalysisData.MinData = LimitedSpaceAnalysisData.MinData;
                newLimitedSpaceAnalysisData.MaxData = LimitedSpaceAnalysisData.MaxData;
                newLimitedSpaceAnalysisData.Measure = LimitedSpaceAnalysisData.Measure;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除受限分析点数据
        /// </summary>
        /// <param name="AnalysisDataId"></param>
        public static void DeleteLimitedSpaceAnalysisDataById(string AnalysisDataId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_LimitedSpaceAnalysisData LimitedSpaceAnalysisData = db.Base_LimitedSpaceAnalysisData.FirstOrDefault(e => e.AnalysisDataId == AnalysisDataId);
            if (LimitedSpaceAnalysisData != null)
            {
                db.Base_LimitedSpaceAnalysisData.DeleteOnSubmit(LimitedSpaceAnalysisData);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取受限分析点数据列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_LimitedSpaceAnalysisData> GetLimitedSpaceAnalysisDataList()
        {
            return (from x in Funs.DB.Base_LimitedSpaceAnalysisData orderby x.SortIndex select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitLimitedSpaceAnalysisDataDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "AnalysisDataId";
            dropName.DataTextField = "AnalysisPoint";
            dropName.DataSource = BLL.LimitedSpaceAnalysisDataService.GetLimitedSpaceAnalysisDataList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 动火分析点数据
    /// </summary>
    public static class FireWorkAnalysisDataService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取动火分析点数据
        /// </summary>
        /// <param name="AnalysisDataId"></param>
        /// <returns></returns>
        public static Model.Base_FireWorkAnalysisData GetFireWorkAnalysisDataById(string AnalysisDataId)
        {
            return Funs.DB.Base_FireWorkAnalysisData.FirstOrDefault(e => e.AnalysisDataId == AnalysisDataId);
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
            Model.Base_FireWorkAnalysisData FireWorkAnalysisData = new Model.Base_FireWorkAnalysisData();
            if (!string.IsNullOrEmpty(AnalysisDataId))
            {
                FireWorkAnalysisData = Funs.DB.Base_FireWorkAnalysisData.FirstOrDefault(x => x.AnalysisPoint == DataValue && x.AnalysisDataId != AnalysisDataId);
            }
            else
            {
                FireWorkAnalysisData = Funs.DB.Base_FireWorkAnalysisData.FirstOrDefault(x => x.AnalysisPoint == DataValue);
            }
            if (FireWorkAnalysisData != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加动火分析点数据
        /// </summary>
        /// <param name="FireWorkAnalysisData"></param>
        public static void AddFireWorkAnalysisData(Model.Base_FireWorkAnalysisData FireWorkAnalysisData)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_FireWorkAnalysisData newFireWorkAnalysisData = new Model.Base_FireWorkAnalysisData
            {
                AnalysisDataId = FireWorkAnalysisData.AnalysisDataId,
                SortIndex = FireWorkAnalysisData.SortIndex,
                AnalysisPoint = FireWorkAnalysisData.AnalysisPoint,
                MinData = FireWorkAnalysisData.MinData,
                MaxData = FireWorkAnalysisData.MaxData,
                Measure = FireWorkAnalysisData.Measure,

            };
            db.Base_FireWorkAnalysisData.InsertOnSubmit(newFireWorkAnalysisData);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改动火分析点数据
        /// </summary>
        /// <param name="FireWorkAnalysisData"></param>
        public static void UpdateFireWorkAnalysisData(Model.Base_FireWorkAnalysisData FireWorkAnalysisData)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_FireWorkAnalysisData newFireWorkAnalysisData = db.Base_FireWorkAnalysisData.FirstOrDefault(e => e.AnalysisDataId == FireWorkAnalysisData.AnalysisDataId);
            if (newFireWorkAnalysisData != null)
            {
                //newFireWorkAnalysisData.SortIndex = FireWorkAnalysisData.SortIndex;
                //newFireWorkAnalysisData.AnalysisPoint = FireWorkAnalysisData.AnalysisPoint;
                newFireWorkAnalysisData.MinData = FireWorkAnalysisData.MinData;
                newFireWorkAnalysisData.MaxData = FireWorkAnalysisData.MaxData;
                newFireWorkAnalysisData.Measure = FireWorkAnalysisData.Measure;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除动火分析点数据
        /// </summary>
        /// <param name="AnalysisDataId"></param>
        public static void DeleteFireWorkAnalysisDataById(string AnalysisDataId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_FireWorkAnalysisData FireWorkAnalysisData = db.Base_FireWorkAnalysisData.FirstOrDefault(e => e.AnalysisDataId == AnalysisDataId);
            if (FireWorkAnalysisData != null)
            {
                db.Base_FireWorkAnalysisData.DeleteOnSubmit(FireWorkAnalysisData);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取动火分析点数据列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_FireWorkAnalysisData> GetFireWorkAnalysisDataList()
        {
            return (from x in Funs.DB.Base_FireWorkAnalysisData orderby x.SortIndex select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitFireWorkAnalysisDataDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "AnalysisDataId";
            dropName.DataTextField = "AnalysisPoint";
            dropName.DataSource = BLL.FireWorkAnalysisDataService.GetFireWorkAnalysisDataList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

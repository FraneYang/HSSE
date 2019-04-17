using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class ConstValue
    {

        #region 常量表下拉框
        /// <summary>
        /// 常量表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitConstValueDropDownList(FineUIPro.DropDownList dropName, string groupId, bool isShowPlease)
        {
            dropName.DataValueField = "ConstValue";
            dropName.DataTextField = "ConstText";
            dropName.DataSource = BLL.ConstValue.drpConstItemList(groupId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 常量表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitConstValueRadioButtonList(FineUIPro.RadioButtonList rblName, string groupId, string selectValue)
        {
            rblName.DataValueField = "ConstValue";
            rblName.DataTextField = "ConstText";
            rblName.DataSource = BLL.ConstValue.drpConstItemList(groupId);
            rblName.DataBind();
            if (!string.IsNullOrEmpty(selectValue))
            {
                rblName.SelectedValue = selectValue;
            }
        }
        #endregion

        /// <summary>
        /// 获取常量下拉框 根据常量组id
        /// </summary>
        /// <param name="groupId">常量组id</param>
        /// <returns>常量集合</returns>
        public static List<Model.Sys_Const> drpConstItemList(string groupId)
        {
            var list = (from x in Funs.DB.Sys_Const
                        where x.GroupId == groupId
                        orderby x.SortIndex
                        select x).ToList();
            return list;
        }

        /// <summary>
        /// 根据值、组ID获取常量信息
        /// </summary>
        /// <param name="constValue"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Sys_Const GetConstByConstValueAndGroupId(string constValue, string groupId)
        {
            return Funs.DB.Sys_Const.FirstOrDefault(e => e.ConstValue == constValue && e.GroupId == groupId);
        }

        #region 常量组
        /// <summary>
        /// -菜单类型：组id
        /// </summary>
        public const string Group_MenuType = "MenuType";
        /// <summary>
        /// 菜单操作
        /// </summary>
        public const string Group_MenuOperation = "MenuOperation";
        /// <summary>
        /// 是/否 组id
        /// </summary>
        public const string Group_Y_N = "Y/N";
        /// <summary>
        /// 性别：男/女 组id
        /// </summary>
        public const string Group_Gender = "Gender";
        /// <summary>
        /// 婚姻状况 组id
        /// </summary>
        public const string Group_Marriage = "Marriage";
        /// <summary>
        /// 文化程度 组id
        /// </summary>
        public const string Group_Education = "Education";
        /// <summary>
        /// 民族 组id
        /// </summary>
        public const string Group_Nation = "Nation";      
        /// <summary>
        /// 年度 组id
        /// </summary>
        public const string Group_Year = "Year";
        /// <summary>
        /// 月份 组id
        /// </summary>
        public const string Group_Month = "Month";
        /// <summary>
        /// 季度 组id
        /// </summary>
        public const string Group_Quarter = "Quarter";        
        /// <summary>
        /// 图表类型：组id
        /// </summary>
        public const string Group_ChartType = "ChartType";       
        /// <summary>
        /// 报表类型：组ID
        /// </summary>
        public const string Group_ReportType = "ReportType";   
        /// <summary>
        /// 天气 组id
        /// </summary>
        public const string Group_Weather = "Weather";          
        /// <summary>
        /// 岗位类型 组id
        /// </summary>
        public const string Group_PostType = "PostType";
        /// <summary>
        /// -安全作业证类型：组id
        /// </summary>
        public const string Group_LicenseType = "LicenseType";
        /// <summary>
        /// -动火作业级别：组id
        /// </summary>
        public const string Group_FireWorkLevel = "FireWorkLevel";
        /// <summary>
        /// -作业级（类）别：组id
        /// </summary>
        public const string Group_JobLevel = "JobLevel";
        /// <summary>
        /// -风险等级：组id
        /// </summary>
        public const string Group_RiskLevel = "RiskLevel";

        /// <summary>
        /// -风险评价方法：组id
        /// </summary>
        public const string Group_EvaluationMethod = "EvaluationMethod";
        #endregion
    }
}
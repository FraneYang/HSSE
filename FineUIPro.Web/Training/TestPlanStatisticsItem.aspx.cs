using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Training
{
    public partial class TestPlanStatisticsItem : PageBase
    {
        #region 定义项
        /// <summary>
        /// 考试计划主键
        /// </summary>
        public string TestPlanId
        {
            get
            {
                return (string)ViewState["TestPlanId"];
            }
            set
            {
                ViewState["TestPlanId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.TestPlanId = Request.Params["TestPlanId"];
                // 绑定表格
                BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (this.ckStates.SelectedValue == "0")
            {
                strSql = @"SELECT V.UserId,V.SortIndex,V.UserCode,V.UserName,V.DepartName,V.InstallationName,V.WorkPostName,V.Telephone"
                        + @" FROM Training_TestPlan AS Plans"
                        + @" LEFT JOIN Sys_PushRecord AS Push ON Plans.TestPlanId = Push.DataId"
                        + @" LEFT JOIN View_Sys_User AS V ON Push.ReceiveManId =V.UserId"
                        + @" WHERE 1=1 ";
            }
            else if (this.ckStates.SelectedValue == "1")
            {
                strSql = @"SELECT V.UserId,V.SortIndex,V.UserCode,V.UserName,V.DepartName,V.InstallationName,V.WorkPostName,V.Telephone"
                        + @" FROM Training_TestPlan AS Plans"
                        + @" LEFT JOIN Training_TestRecord AS Test ON Plans.TestPlanId = Test.TestPlanId"
                        + @" LEFT JOIN View_Sys_User AS V ON Test.TestManId =V.UserId"
                        + @" WHERE 1=1 ";
            }
            else
            {
                strSql = @"SELECT V.UserId,V.SortIndex,V.UserCode,V.UserName,V.DepartName,V.InstallationName,V.WorkPostName,V.Telephone"
                    + @" FROM Training_TestPlan AS Plans"
                    + @" LEFT JOIN Sys_PushRecord AS Push ON Plans.TestPlanId = Push.DataId"
                    + @" LEFT JOIN View_Sys_User AS V ON Push.ReceiveManId =V.UserId"
                    + @" LEFT JOIN Training_TestRecord AS Test ON Plans.TestPlanId = Test.TestPlanId AND V.UserId=Test.TestManId"
                    + @" WHERE Test.TestRecordId IS NULL ";
            }
            strSql += " AND V.UserId !='" + BLL.Const.sysglyId + "' AND Plans.TestPlanId ='" + this.TestPlanId + "'";
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (V.UserCode LIKE @name OR V.UserName LIKE @name OR V.DepartName LIKE @name OR V.InstallationName LIKE @name OR V.WorkPostName LIKE @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            }
            
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页、关闭窗口
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }        
        #endregion
        
        #region 输入框查询事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 导出按钮  
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(this.ckStates.SelectedItem.Text + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}
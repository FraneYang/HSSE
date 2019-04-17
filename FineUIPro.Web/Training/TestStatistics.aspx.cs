using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.Training
{
    public partial class TestStatistics : PageBase
    {
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
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
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
            string strSql = @"SELECT UserId,UserCode,UserName,sysUser.UnitId,Unit.UnitCode,Unit.UnitName,sysUser.DepartId,Depart.DepartCode,Depart.DepartName"
                        + @",InstallationId,InstallationName,WorkPostId,WorkPostName,ISNULL(TestCount,0) AS TestCount,ISNULL(TestQualifyCount,0) AS TestQualifyCount "
                        + @" FROM Sys_User AS sysUser "
                        + @" LEFT JOIN Base_Unit AS Unit ON sysUser.UnitId=Unit.UnitId"
                        + @" LEFT JOIN Base_Depart AS Depart ON sysUser.DepartId=Depart.DepartId"
                        + @" LEFT JOIN (SELECT COUNT(TestRecordId) AS TestCount,TestManId  FROM Training_TestRecord GROUP BY TestManId) AS TestCount"
                        + @" ON sysUser.UserId=TestCount.TestManId"
                        + @" LEFT JOIN (SELECT COUNT(TestRecordId) AS TestQualifyCount,TestManId  FROM Training_TestRecord WHERE TestScores< 80 GROUP BY TestManId) AS TestQualifyCount"
                        + @" ON sysUser.UserId=TestQualifyCount.TestManId"
                        + @" WHERE UserId <> '" + BLL.Const.sysglyId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (UserName LIKE @name OR UserCode LIKE @name OR Unit.UnitName LIKE @name OR Depart.DepartName LIKE @name OR InstallationName LIKE @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            } 

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页排序
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
    }
}
namespace FineUIPro.Web.ShowDialog
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class PushRecordMessage : PageBase
    {
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
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT PushRecordId,MenuId,DataId,FlowStep,AuditFlowName,PushDateTime,ReceiveManId,sysUser.UserName AS ReceiveManName,PushContent,IsResponse,ResponseTime"
                          + @" FROM Sys_PushRecord"
                          + @" LEFT JOIN Sys_User AS sysUser ON ReceiveManId=sysUser.UserId"
                          + @" WHERE DataId= '" + Request.Params["DataId"] + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtContent.Text.Trim()))
            {
                strSql += " AND (FlowStep LIKE @Content OR AuditFlowName LIKE @Content OR sysUser.UserName LIKE @Content )";
                listStr.Add(new SqlParameter("@Content", "%" + this.txtContent.Text.Trim() + "%"));
            }
            string menuTyp = String.Join(", ", this.ckMenuType.SelectedValueArray);
            if (!string.IsNullOrEmpty(menuTyp))
            {
                if (menuTyp == "0")
                {
                    strSql += " AND (IsResponse is null OR  IsResponse= 0)";
                }
                else
                {
                    strSql += " AND IsResponse= 1";
                }
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {            
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
            BindGrid();
        }
        #endregion

        #region 查询
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
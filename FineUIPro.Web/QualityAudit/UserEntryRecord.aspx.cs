using System;
using System.Collections.Generic;
using BLL;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.QualityAudit
{
    public partial class UserEntryRecord : PageBase
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
                GetButtonPower();
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.BindGrid();
            }
        }
        #endregion

        #region 分页下拉选择
        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Funs.GetNewIntOrZero(this.ddlPageSize.SelectedValue);
            this.BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region BindGrid
        /// <summary>
        ///  gv 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT UserEntryRecord.EntryRecordId,
                                     UserEntryRecord.UserId,
                                     UserEntryRecord.InstallationId,
                                     UserEntryRecord.Place,
                                     (CASE UserEntryRecord.IntoOut WHEN '0' THEN '入场' WHEN '1' THEN '出场' END) AS IntoOut,
                                     UserEntryRecord.IntoOutTime,
                                     Users.UserName,
                                     Installation.InstallationName "
                          + @" FROM dbo.Sys_UserEntryRecord AS UserEntryRecord "
                          + @" LEFT JOIN Sys_User AS Users ON Users.UserId = UserEntryRecord.UserId "
                          + @" LEFT JOIN Base_Installation AS Installation ON Installation.InstallationId = UserEntryRecord.InstallationId "
                          + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            
            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " AND Users.UserName LIKE @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtInstallationName.Text.Trim()))
            {
                strSql += " AND Installation.InstallationName LIKE @InstallationName";
                listStr.Add(new SqlParameter("@InstallationName", "%" + this.txtInstallationName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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

        #region 删除事件
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.UserEntryRecordService.DeleteUserEntryRecordById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除进出场管理");
                }
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.UserEntryRecordMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}
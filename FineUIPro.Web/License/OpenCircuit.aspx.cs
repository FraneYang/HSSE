namespace FineUIPro.Web.License
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class OpenCircuit : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
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
            string strSql = @"SELECT OpenCircuitId,LicenseCode,(CASE WHEN ApplyInstallation.InstallationName IS NULL THEN ApplyUnit.UnitName ELSE ApplyInstallation.InstallationName END) AS ApplyUintName"
                            + @" ,Users.UserName AS ApplyManName,JobStartTime,JobEndTime,JobUnit.UnitName AS JobUnitName,OpenCircuitCause,JobManagerUsers.UserName AS JobManagerName"
                            + @" ,(CASE WHEN States =1 THEN '待审核' WHEN  States =2 THEN '待验收' WHEN  States =3 THEN '已验收' WHEN  States =-1 THEN '已作废' ELSE '待提交' END )  AS StatesName "
                            + @" FROM License_OpenCircuit AS OpenCircuit "
                            + @" LEFT JOIN Base_Unit AS ApplyUnit ON OpenCircuit.UnitId =ApplyUnit.UnitId "
                            + @" LEFT JOIN Base_Installation AS ApplyInstallation ON OpenCircuit.InstallationId =ApplyInstallation.InstallationId "
                            + @" LEFT JOIN Base_Unit AS JobUnit ON OpenCircuit.JobUnitId =JobUnit.UnitId "
                            + @" LEFT JOIN Sys_User AS Users ON OpenCircuit.ApplyManId =Users.UserId"
                            + @" LEFT JOIN Sys_User AS JobManagerUsers ON OpenCircuit.JobManagerId =JobManagerUsers.UserId"                            
                            + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtLicenseCode.Text.Trim()))
            {
                strSql += " AND LicenseCode LIKE @LicenseCode";
                listStr.Add(new SqlParameter("@LicenseCode", "%" + this.txtLicenseCode.Text.Trim() + "%"));
            }           
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        
        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
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
                    BLL.OpenCircuitService.DeleteOpenCircuitById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除断路安全作业证");
                    //var OpenCircuits = BLL.OpenCircuitService.GetOpenCircuitById(rowID);
                    //if (OpenCircuits != null)
                    //{                        
                    //}
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

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
        
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (this.CurrUser.UserId == BLL.Const.sysglyId)
            {
                this.btnMenuDelete.Hidden = false;
            }
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
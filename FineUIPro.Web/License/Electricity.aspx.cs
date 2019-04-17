namespace FineUIPro.Web.License
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class Electricity : PageBase
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
            string strSql = @"SELECT ElectricityId,LicenseCode,(CASE WHEN ApplyInstallation.InstallationName IS NULL THEN ApplyUnit.UnitName ELSE ApplyInstallation.InstallationName END) AS ApplyUintName"
                            + @" ,Users.UserName AS ApplyManName,JobStartTime,JobEndTime ,(CASE WHEN ElectricityInstallation.InstallationName IS NULL THEN JobPalce ELSE ElectricityInstallation.InstallationName+':' +JobPalce END) AS JobPalce"
                            + @" ,(CASE WHEN States =1 THEN '待审核' WHEN  States =2 THEN '待验收' WHEN  States =3 THEN '已验收' WHEN  States =-1 THEN '已作废' ELSE '待提交' END )  AS StatesName "
                            + @" FROM License_Electricity AS Electricity "
                            + @" LEFT JOIN Base_Unit AS ApplyUnit ON Electricity.UnitId =ApplyUnit.UnitId "
                            + @" LEFT JOIN Base_Installation AS ApplyInstallation ON Electricity.InstallationId =ApplyInstallation.InstallationId "
                            + @" LEFT JOIN Sys_User AS Users ON Electricity.ApplicantManId =Users.UserId"
                            + @" LEFT JOIN Base_Installation AS ElectricityInstallation ON Electricity.JobInstallationId =ElectricityInstallation.InstallationId "                            
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
                    BLL.ElectricityService.DeleteElectricityById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除临时用电安全作业证");
                    //var Electricitys = BLL.ElectricityService.GetElectricityById(rowID);
                    //if (Electricitys != null)
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
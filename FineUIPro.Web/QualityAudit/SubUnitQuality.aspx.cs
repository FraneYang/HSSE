using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class SubUnitQuality : PageBase
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
                GetButtonPower();//权限设置
                this.BindGrid();
            }
        }
        #endregion

        #region BindGrid
        /// <summary>
        ///  gv 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Unit.UnitId,
                            Unit.UnitCode,
                            Unit.UnitName,
                            UnitType.UnitTypeName,
                            SubUnitQuality.SubUnitQualityCode,
                            SubUnitQuality.SubUnitQualityName,
                            SubUnitQuality.BusinessLicense,
                            SubUnitQuality.BL_EnableDate,
                            SubUnitQuality.BL_ScanUrl,
                            SubUnitQuality.OrganCode,
                            SubUnitQuality.O_EnableDate,
                            SubUnitQuality.O_ScanUrl,
                            SubUnitQuality.Certificate,
                            SubUnitQuality.C_EnableDate,
                            SubUnitQuality.C_ScanUrl,
                            SubUnitQuality.QualityLicense,
                            SubUnitQuality.QL_EnableDate,
                            SubUnitQuality.QL_ScanUrl,
                            SubUnitQuality.HSELicense,
                            SubUnitQuality.H_EnableDate,
                            SubUnitQuality.H_ScanUrl,
                            SubUnitQuality.SecurityLicense,
                            SubUnitQuality.SL_EnableDate,
                            SubUnitQuality.SL_ScanUrl"
                          + @" FROM Base_Unit AS Unit"
                          + @" LEFT JOIN Base_UnitType AS UnitType ON UnitType.UnitTypeId=Unit.UnitTypeId"
                          + @" LEFT JOIN QualityAudit_SubUnitQuality AS SubUnitQuality ON SubUnitQuality.UnitId = Unit.UnitId"
                          + @" WHERE IsThisUnit<>1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

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

        #region 编辑事件
        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        private void EditData()
        {
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SubUnitQualityEdit.aspx?unitId={0}", Id, "编辑外委单位资质 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.SubUnitQualityMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnMenuEdit.Hidden = false;
                }
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("外委单位资质" + filename, System.Text.Encoding.UTF8) + ".xls");
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
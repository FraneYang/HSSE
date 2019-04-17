using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.Data.SqlClient;
using System.Data;

namespace FineUIPro.Web.QualityAudit
{
    public partial class EquipmentQuality : PageBase
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
            string strSql = @"SELECT EquipmentQuality.EquipmentQualityId, 
                            EquipmentQuality.InstallationId, 
                            EquipmentQuality.UnitId, 
                            EquipmentQuality.EuipmentId, 
                            EquipmentQuality.EquipmentQualityCode, 
                            EquipmentQuality.EquipmentQualityName, 
                            EquipmentQuality.SizeModel, 
                            EquipmentQuality.FactoryCode, 
                            EquipmentQuality.CertificateCode, 
                            EquipmentQuality.CheckDate, 
                            EquipmentQuality.LimitDate, 
                            EquipmentQuality.InDate, 
                            EquipmentQuality.OutDate, 
                            EquipmentQuality.ApprovalPerson, 
                            EquipmentQuality.CarNum, 
                            EquipmentQuality.Remark, 
                            EquipmentQuality.CompileMan, 
                            EquipmentQuality.CompileDate,
                            Euipment.EuipmentName,
                            EuipmentType.EuipmentTypeName,
                            Installation.InstallationName,
                            Unit.UnitName"
                           + @" FROM dbo.QualityAudit_EquipmentQuality AS EquipmentQuality"
                           + @" LEFT JOIN dbo.Base_Euipment AS Euipment ON Euipment.EuipmentId = EquipmentQuality.EuipmentId"
                           + @" LEFT JOIN dbo.Base_EuipmentType AS EuipmentType ON EuipmentType.EuipmentTypeId = Euipment.EuipmentTypeId"
                           + @" LEFT JOIN dbo.Base_Installation AS Installation ON Installation.InstallationId = EquipmentQuality.InstallationId"
                          + @"  LEFT JOIN dbo.Base_Unit AS Unit ON Unit.UnitId= EquipmentQuality.UnitId WHERE 1=1 ";
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

        #region 增加
        /// <summary>
        /// 新增按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentQualityEdit.aspx", "编辑特种设备资质 - ")));
        }
        #endregion

        #region 右键编辑事件
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentQualityEdit.aspx?equipmentQualityId={0}", Id, "编辑特种设备资质 - ")));
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
                    BLL.EquipmentQualityService.DeleteEquipmentQualityById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除特种设备资质");
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.EquipmentQualityMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion      

        #region 双击Grid行事件
        /// <summary>
        /// 双击行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentQualityEdit.aspx?equipmentQualityId={0}", Id, "编辑特种设备资质 - ")));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("设备资质" + filename, System.Text.Encoding.UTF8) + ".xls");
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
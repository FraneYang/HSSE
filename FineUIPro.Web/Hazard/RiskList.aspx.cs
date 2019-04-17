namespace FineUIPro.Web.Hazard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class RiskList : PageBase
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
            string strSql = @"SELECT RiskList.RiskListId,RiskList.HiddenHazardId,HiddenHazard.HiddenHazardCode,HiddenHazard.HiddenHazardName,RiskList.InstallationId,Installation.InstallationName,RiskList.RiskPlace,RiskList.TaskActivity,RiskList.HazardDescription,RiskList.PossibleAccidents,RiskList.RiskLevel,sysConst.ConstText AS RiskLevelName,RiskList.RiskValue,RiskList.ControlMeasures,RiskList.ManagementMeasures,RiskList.ProtectiveMeasures,RiskList.OtherMeasures,RiskList.ControlUnit,RiskList.ControlRiskLevel"
                        + @" ,RiskList.EvaluationMethod,RiskList.Cancelled,RiskList.States,RiskList.ControlUnitId,Unit.UnitName AS ControlUnitName,RiskList.ControlInstallationId,ControlInstallation.InstallationName AS ControlInstallationName,RiskList.PatrolFrequency,RiskList.LECItemId,RiskList.SCLItemId,RiskList.JHAItemId,RiskList.RiskManId,RiskMan.UserName AS RiskManName,RiskList.EvaluationTime,RiskList.StartDate "
                        + @" ,(CASE WHEN RiskList.LECItemId IS NOT NULL AND DataType='1' THEN '作业环境' ELSE '设备设施' END) AS TypeName,RiskList.IsUsed"
                        + @" ,(select stuff((select '|'+ u.UserName from Hazard_RiskListItem i left join Sys_User as u on  i.RiskOwnerId=u.UserId where i.IsRiskOwner=1 AND i.RiskListId=RiskList.RiskListId for xml path ('')),1,1,'')) as RiskOwnerNames"
                        + @" ,(CASE WHEN LECItem.LECItemId IS NOT NULL THEN 'L:'+CAST(LECItem.HazardJudge_L AS varchar(10))+ ',E:'+CAST(LECItem.HazardJudge_E AS varchar(10))+ ',C:'+CAST(LECItem.HazardJudge_C AS varchar(10))+ ',D:'+CAST(LECItem.HazardJudge_D AS varchar(10))"
                        + @" ELSE 'L:'+CAST(SCLItem.HazardJudge_L AS varchar(10))+ ',S:'+CAST(SCLItem.HazardJudge_S AS varchar(10))+ ',R:'+CAST(SCLItem.HazardJudge_R AS varchar(10)) END) AS HazardJudgeName"
                        + @" FROM Hazard_RiskList AS RiskList"
                        + @" LEFT JOIN Base_Installation AS Installation ON RiskList.InstallationId=Installation.InstallationId"
                        + @" LEFT JOIN Hazard_HiddenHazard AS HiddenHazard ON RiskList.HiddenHazardId=HiddenHazard.HiddenHazardId"
                        + @" LEFT JOIN Base_Unit AS Unit ON RiskList.ControlUnitId=Unit.UnitId"
                        + @" LEFT JOIN Base_Installation AS ControlInstallation ON RiskList.ControlInstallationId=ControlInstallation.InstallationId"
                        + @" LEFT JOIN Sys_User AS RiskMan ON RiskList.RiskManId=RiskMan.UserId"
                        + @" LEFT JOIN Sys_Const AS sysConst ON RiskList.RiskLevel=sysConst.ConstValue AND sysConst.GroupId='" + BLL.ConstValue.Group_RiskLevel + "'"
                        + @" LEFT JOIN Hazard_LECItem AS LECItem ON RiskList.LECItemId=LECItem.LECItemId"
                        + @" LEFT JOIN Hazard_SCLItem AS SCLItem ON RiskList.SCLItemId=SCLItem.SCLItemId"
                        + @" WHERE RiskList.RiskLevel='" + this.rbRiskLevel.SelectedValue + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.ckStates.SelectedValue))
            {
                if (this.ckStates.SelectedValue == "1")
                {
                    strSql += " AND RiskList.Cancelled = 1";
                }
                else
                {
                    strSql += " AND (RiskList.Cancelled = 0 OR RiskList.Cancelled IS NULL)";
                }
            }
            if (ckIsUsed.SelectedValue == "1")
            {
                 strSql += " AND RiskList.IsUsed = 1";
            }
            else if (ckIsUsed.SelectedValue == "2")
            {
                strSql += " AND (RiskList.IsUsed = 0 OR RiskList.IsUsed IS NULL)";
            }

            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (RiskList.TaskActivity LIKE @Name OR RiskList.HazardDescription LIKE @Name OR RiskList.PossibleAccidents LIKE @Name OR RiskList.ControlMeasures LIKE @Name OR RiskList.ManagementMeasures LIKE @Name OR RiskList.ProtectiveMeasures LIKE @Name OR RiskList.OtherMeasures LIKE @Name OR RiskList.EvaluationMethod LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }   

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();           
        }

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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.RiskListMenuId);
            if (buttonList.Count() > 0)
            {    
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSurvey.Hidden = false;
                }
            }
        }
        #endregion
        
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
                    if (judgementDelete(rowID, false))
                    {
                        BLL.RiskListService.DeleteRiskListById(rowID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除风险信息库");
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!");
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

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RiskListView.aspx?RiskListId={0}", Id, "查看 - ")));
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            if (Funs.DB.Hazard_RoutingInspection.FirstOrDefault(x=>x.RiskListId == id) != null)
            {
                content = "该风险信息库已在【巡检记录】中使用，不能删除。可在巡检时消除！";
            }
            
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("风险信息库" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }        
        #endregion

        #region 二维码生成事件
        /// <summary>
        /// 二维码生成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            var risk = BLL.RiskListService.GetRiskListById(Grid1.SelectedRowID);
            if (risk != null)
            {
                if (!string.IsNullOrEmpty(risk.EuipmentId))
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../Controls/SeeQRImage.aspx?menuId={0}&dataId={1}", BLL.Const.EuipmentMenuId, risk.EuipmentId, "编辑 - "), "二维码查看", 400, 400));
                }
                else
                {
                    var lecItem = BLL.LECItemService.GetLECItemById(risk.LECItemId);
                    if (lecItem != null && !string.IsNullOrEmpty(lecItem.DataId))
                    {
                        if (lecItem.DataType == "1")
                        {
                            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../Controls/SeeQRImage.aspx?menuId={0}&dataId={1}", BLL.Const.JobEnvironmentMenuId, lecItem.DataId, "编辑 - "), "二维码查看", 400, 400));
                        }
                        else
                        {
                            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../Controls/SeeQRImage.aspx?menuId={0}&dataId={1}", BLL.Const.EuipmentMenuId, lecItem.DataId, "编辑 - "), "二维码查看", 400, 400));
                        }
                    }
                    else
                    {
                        var sclItem = BLL.SCLItemService.GetSCLItemById(risk.SCLItemId);
                        if (sclItem != null && !string.IsNullOrEmpty(sclItem.EuipmentId))
                        {
                            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../Controls/SeeQRImage.aspx?menuId={0}&dataId={1}", BLL.Const.EuipmentMenuId, sclItem.EuipmentId, "编辑 - "), "二维码查看", 400, 400));
                        }
                    }
                }
            }
        }
        #endregion

        #region 二次评估事件
        /// <summary>
        /// 二次评估事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSurvey_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            var risk = BLL.RiskListService.GetRiskListById(Grid1.SelectedRowID);
            if (risk != null)
            {
                if (!string.IsNullOrEmpty(risk.LECItemId))
                {
                    var lec = BLL.LECItemService.GetLECItemById(risk.LECItemId);
                    if (lec != null)
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("LECSave.aspx?LECItemId={0}&DataId={1}&DataType={2}", lec.LECItemId, lec.DataId, lec.DataType), "二次评估", 900, 520));
                    }
                }
                else
                {
                    var scl = BLL.SCLItemService.GetSCLItemById(risk.SCLItemId);
                    if (scl != null)
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SCLSave.aspx?SCLItemId={0}", scl.SCLItemId), "二次评估", 900, 540));
                    }
                }
            }
        }
        #endregion
    }
}
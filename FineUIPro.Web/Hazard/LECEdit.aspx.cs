namespace FineUIPro.Web.Hazard
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class LECEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 评价对象主键
        /// </summary>
        public string DataId
        {
            get
            {
                return (string)ViewState["DataId"];
            }
            set
            {
                ViewState["DataId"] = value;
            }
        }
        /// <summary>
        /// 评价对象类型
        /// </summary>
        public string DataType
        {
            get
            {
                return (string)ViewState["DataType"];
            }
            set
            {
                ViewState["DataType"] = value;
            }
        }
        #endregion

        /// <summary>
        /// LEC评价编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ///权限
                this.GetButtonPower();
                this.DataId = Request.Params["DataId"];
                this.DataType = Request.Params["DataType"];
                if (this.DataType == "0")
                {
                    this.txtDataName.Label = "设备设施";
                    this.txtEuipmentType.Hidden = false;
                    var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(this.DataId);
                    if (euipment != null)
                    {
                        this.txtDataName.Text = euipment.EuipmentName;
                        var type = BLL.EuipmentTypeService.GetEuipmentTypeByEuipmentTypeId(euipment.EuipmentTypeId);
                        if (type != null)
                        {
                            this.txtEuipmentType.Text = type.EuipmentTypeName;
                        }
                        var installation = BLL.InstallationService.GetInstallationByInstallationId(euipment.InstallationId);
                        if (installation != null)
                        {
                            this.txtInstallation.Text = installation.InstallationName;
                        }
                        var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(euipment.WorkAreaId);
                        if (workArea != null)
                        {
                            this.txtWorkArea.Text = workArea.WorkAreaName;
                        }
                    }
                }
                else
                {
                    this.txtDataName.Label = "作业环境";
                    this.txtEuipmentType.Hidden = true;
                    var jobEnvironment = BLL.JobEnvironmentService.GetJobEnvironmentByJobEnvironmentId(this.DataId);
                    if (jobEnvironment != null)
                    {
                        this.txtDataName.Text = jobEnvironment.JobEnvironmentName;
                        var installation = BLL.InstallationService.GetInstallationByInstallationId(jobEnvironment.InstallationId);
                        if (installation != null)
                        {
                            this.txtInstallation.Text = installation.InstallationName;
                        }
                        var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(jobEnvironment.WorkAreaId);
                        if (workArea != null)
                        {
                            this.txtWorkArea.Text = workArea.WorkAreaName;
                        }
                    }
                }

                this.BindGrid();
            }
        }
            
        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.DataId))
            {
                string strSql = @"SELECT LECItem.LECItemId,LECItem.DataId,LECItem.SortIndex,LECItem.HazardDescription,LECItem.PossibleAccidents,LECItem.HazardJudge_L,LECItem.HazardJudge_E,LECItem.HazardJudge_C,LECItem.HazardJudge_D"
                        + @",LECItem.RiskLevel,sysConst.ConstText AS RiskLevelName,LECItem.ControlMeasures,LECItem.ManagementMeasures,LECItem.ProtectiveMeasures,LECItem.OtherMeasures,LECItem.Remark"
                        + @" FROM Hazard_LECItem AS LECItem"
                        + @" LEFT JOIN Sys_Const AS sysConst ON LECItem.RiskLevel=sysConst.ConstValue AND sysConst.GroupId='" + BLL.ConstValue.Group_RiskLevel + "'"
                        + @" WHERE LECItem.DataId=@DataId ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@DataId", this.DataId));
                if (!string.IsNullOrEmpty(this.txtName.Text))
                {
                    strSql += " AND (HazardDescription LIKE @Name OR PossibleAccidents LIKE @Name OR ControlMeasures LIKE @Name)";
                    listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
                }
                strSql += " ORDER BY LECItem.SortIndex";
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);                
                Grid1.DataSource = tb;
                Grid1.DataBind();
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    var lecItem = BLL.LECItemService.GetLECItemById(Grid1.Rows[i].DataKeys[0].ToString());
                    if (lecItem != null)
                    {
                        if (lecItem.RiskLevel == "1")
                        {
                            Grid1.Rows[i].RowCssClass = "Red";
                        }
                        else if (lecItem.RiskLevel == "2")
                        {
                            Grid1.Rows[i].RowCssClass = "Orange";
                        }
                        else if (lecItem.RiskLevel == "3")
                        {
                            Grid1.Rows[i].RowCssClass = "Yellow";
                        }                      
                        else if (lecItem.RiskLevel == "4")
                        {
                            Grid1.Rows[i].RowCssClass = "Blue";                            
                        }
                    }
                }
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
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
            this.Grid1.PageIndex = 0;
        }
        #endregion 

        #region 新增LEC评价
        /// <summary>
        /// 新增LEC评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("LECSave.aspx?DataId={0}&DataType={1}", this.DataId, this.DataType)));
        }
        #endregion

        #region 删除LEC评价
        /// <summary>
        /// 删除LEC评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string messages = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var riskList = Funs.DB.Hazard_RiskList.FirstOrDefault(x => x.LECItemId == rowID);
                    if (riskList == null)
                    {
                        BLL.LECItemService.DeleteLECItemById(rowID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除LEC评价明细！");
                    }
                    else
                    {
                        messages += (rowIndex + 1).ToString() + "行已存在风险信息。";
                    }
                }

                BindGrid();
                if (!string.IsNullOrEmpty(messages))
                {
                    Alert.ShowInTop(messages, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
        }
        #endregion

        #region 数据编辑
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
        protected void btnMenuEdit_Click(object sender, EventArgs e)
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

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("LECSave.aspx?LECItemId={0}&DataId={1}&DataType={2}", Grid1.SelectedRowID, this.DataId, this.DataType, "编辑 - ")));
        }
        #endregion

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }

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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.SCLMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnAdd.Hidden = false;
                }
                //if (buttonList.Contains(BLL.Const.BtnDelete))
                //{
                //    this.btnMenuDelete.Hidden = false;
                //}
            }
        }
        #endregion
    }
}
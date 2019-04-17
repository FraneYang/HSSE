namespace FineUIPro.Web.Hazard
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class SCLEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 设备设施主键
        /// </summary>
        public string EuipmentId
        {
            get
            {
                return (string)ViewState["EuipmentId"];
            }
            set
            {
                ViewState["EuipmentId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// SCL评价编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                ///权限
                this.GetButtonPower();
                this.EuipmentId = Request.Params["EuipmentId"];             
                this.loadData();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        private void loadData()
        {         
            if (!string.IsNullOrEmpty(this.EuipmentId))
            {
                var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(this.EuipmentId);
                if (euipment != null)
                {
                    this.txtEuipmentName.Text = euipment.EuipmentName;
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

                this.BindGrid();
            }           
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.EuipmentId))
            {
                string strSql = @"SELECT SCLItem.SCLItemId,SCLItem.EuipmentId,SCLItem.SortIndex,SCLItem.CheckItem,SCLItem.Standard,SCLItem.Consequence,SCLItem.NowControlMeasures"
                        + @" ,SCLItem.HazardJudge_L,SCLItem.HazardJudge_S,SCLItem.HazardJudge_R,SCLItem.RiskLevel,sysConst.ConstText AS RiskLevelName,SCLItem.ControlMeasures,SCLItem.ManagementMeasures,SCLItem.ProtectiveMeasures,SCLItem.OtherMeasures"
                        + @" FROM Hazard_SCLItem AS SCLItem"                      
                        + @" LEFT JOIN Sys_Const AS sysConst ON SCLItem.RiskLevel=sysConst.ConstValue AND sysConst.GroupId='" + BLL.ConstValue.Group_RiskLevel + "'"
                        + @" WHERE SCLItem.EuipmentId=@EuipmentId ORDER BY SCLItem.SortIndex";
                SqlParameter[] parameter = new SqlParameter[]
                        {
                            new SqlParameter("@EuipmentId",this.EuipmentId),
                        };
                DataTable table = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.DataSource = table;
                Grid1.DataBind();
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    var sclItem = BLL.SCLItemService.GetSCLItemById(Grid1.Rows[i].DataKeys[0].ToString());
                    if (sclItem != null)
                    {
                        if (sclItem.RiskLevel == "1")
                        {
                            Grid1.Rows[i].RowCssClass = "Red";
                        }
                        else if (sclItem.RiskLevel == "2")
                        {
                            Grid1.Rows[i].RowCssClass = "Orange";
                        }
                        else if (sclItem.RiskLevel == "3")
                        {
                            Grid1.Rows[i].RowCssClass = "Yellow";
                        }                      
                        else if (sclItem.RiskLevel == "4")
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

        #region 新增SCL评价
        /// <summary>
        /// 新增SCL评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnAdd_Click(object sender, EventArgs e)
        //{          
        //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SCLSave.aspx?EuipmentId={0}", this.EuipmentId)));
        //}
        #endregion
       
        #region 删除SCL评价
        /// <summary>
        /// 删除SCL评价
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
                    var riskList = Funs.DB.Hazard_RiskList.FirstOrDefault(x => x.SCLItemId == rowID);
                    if (riskList == null)
                    {
                        BLL.SCLItemService.DeleteSCLItemById(rowID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除SCL评价明细！");
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

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SCLSave.aspx?SCLItemId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
                //if (buttonList.Contains(BLL.Const.BtnAdd))
                //{
                //    this.btnAdd.Hidden = false;
                //}
                //if (buttonList.Contains(BLL.Const.BtnDelete))
                //{
                //    this.btnMenuDelete.Hidden = false;
                //}
            }
        }
        #endregion        
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class RiskListView : PageBase
    {
        #region 定义项
        /// <summary>
        /// LEC评价主键
        /// </summary>
        public string RiskListId
        {
            get
            {
                return (string)ViewState["RiskListId"];
            }
            set
            {
                ViewState["RiskListId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 风险信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                this.RiskListId = Request.Params["RiskListId"];
                BLL.UserService.InitUserDropDownList(this.drpRiskOwnerIds, true);
                
                this.loadData();
                this.BindGrid();
                this.BindGrid2();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        private void loadData()
        {
            if (!string.IsNullOrEmpty(this.RiskListId))
            {
                var view = BLL.RiskListService.GetViewRiskListById(this.RiskListId);
                if (view != null)
                {
                    BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpRiskOwner, null,null, view.InstallationId, true);
                    this.txtInstallationName.Text = view.InstallationName;
                    this.txtTaskActivity.Text = view.TaskActivity;
                    this.txtHazardDescription.Text = view.HazardDescription;
                    this.txtPossibleAccidents.Text = view.PossibleAccidents;
                    this.txtRiskLevelName.Text = view.RiskLevelName;
                    this.txtEvaluationMethod.Text = view.EvaluationMethod;
                    this.txtControlMeasures.Text = view.ControlMeasures;
                    this.txtManagementMeasures.Text = view.ManagementMeasures;
                    this.txtProtectiveMeasures.Text = view.ProtectiveMeasures;
                    this.txtOtherMeasures.Text = view.OtherMeasures;
                    if (!string.IsNullOrEmpty(view.RiskOwnerIds))
                    {
                        this.drpRiskOwnerIds.SelectedValueArray = view.RiskOwnerIds.Split(',');
                    }
                    if (view.StartDate != null)
                    {
                        this.dpkStartDate.Text = string.Format("{0:yyyy-MM-dd hh:mm:ss}", view.StartDate);
                    }
                    //this.txtControlInstallationName.Text = view.ControlInstallationName;
                    //this.txtRiskManName.Text = view.RiskManName;
                    //this.txtPatrolFrequency.Text = view.PatrolFrequency.ToString();
                    if (!string.IsNullOrEmpty(view.LECItemId))
                    {
                        this.txtTaskActivity.Label = "设备设施/作业环境";
                    }
                    else if (!string.IsNullOrEmpty(view.SCLItemId))
                    {
                        this.txtTaskActivity.Label = "设备设施";
                    }
                    else if (!string.IsNullOrEmpty(view.JHAItemId))
                    {
                        this.txtTaskActivity.Label = "作业活动";
                    }
                    this.txtQRCodePosition.Text = view.QRCodePosition;
                    if(view.IsUsed == true)
                    {
                        this.ckIsUsed.Checked = true;
                    }
                }
            }
        }

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var riskList = BLL.RiskListService.GetRiskListById(this.RiskListId);
            if (riskList != null)
            {               
                riskList.StartDate = Funs.GetNewDateTime(this.dpkStartDate.Text);
                riskList.QRCodePosition = this.txtQRCodePosition.Text.Trim();
                riskList.IsUsed = this.ckIsUsed.Checked;
                BLL.RiskListService.UpdateRiskList(riskList);
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnSure.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
            }
        }
        #endregion

        #region 巡检人列表绑定
        /// <summary>
        /// 巡检人列表绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Item.RiskItemId,Item.RiskListId,Item.RiskOwnerId,Item.Frequency,SysUser.UserName AS RiskOwnerName ,SysUser.WorkPostName"
                            + @" FROM Hazard_RiskListItem AS Item"
                            + @" LEFT JOIN View_Sys_User AS SysUser ON Item.RiskOwnerId=SysUser.UserId"
                            + @" WHERE Item.IsRiskOwner =1 AND Item.RiskListId=@RiskListId";
            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@RiskListId", this.RiskListId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 巡检人列表绑定
        /// </summary>
        private void BindGrid2()
        {
            string strSql2 = @"SELECT Item.RiskItemId,Item.RiskListId,Item.RiskOwnerId,Item.Frequency,SysUser.UserName AS RiskOwnerName,SysUser.WorkPostName "
                            + @" FROM Hazard_RiskListItem AS Item"
                            + @" LEFT JOIN View_Sys_User AS SysUser ON Item.RiskOwnerId=SysUser.UserId"
                            + @" WHERE (Item.IsRiskOwner =0 OR Item.IsRiskOwner is null) AND Item.RiskListId=@RiskListId";
            List<SqlParameter> listStr2 = new List<SqlParameter>();

            listStr2.Add(new SqlParameter("@RiskListId", this.RiskListId));
            SqlParameter[] parameter2 = listStr2.ToArray();
            DataTable tb2 = SQLHelper.GetDataTableRunText(strSql2, parameter2);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid2.RecordCount = tb2.Rows.Count;            
            var table2 = this.GetPagedDataTable(Grid2, tb2);
            Grid2.DataSource = table2;
            Grid2.DataBind();
        }
        #endregion        

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            if (this.drpRiskOwner.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.txtFrequency.Text))
            {
                var riskItem = Funs.DB.Hazard_RiskListItem.FirstOrDefault(x => x.RiskListId == this.RiskListId && x.RiskOwnerId == this.drpRiskOwner.SelectedValue);
                if (riskItem != null)
                {
                    riskItem.IsRiskOwner = true;
                    riskItem.Frequency = Funs.GetNewInt(this.txtFrequency.Text);
                    BLL.RiskListItemService.UpdateRiskListItem(riskItem);
                }
                else
                {
                    Model.Hazard_RiskListItem newRiskListItem = new Model.Hazard_RiskListItem
                    {
                        RiskListId = this.RiskListId,
                        RiskOwnerId = this.drpRiskOwner.SelectedValue,
                        Frequency = Funs.GetNewInt(this.txtFrequency.Text),
                        IsRiskOwner = true,
                    };

                    BLL.RiskListItemService.AddRiskListItem(newRiskListItem);
                }

                this.BindGrid();

                this.drpRiskOwner.SelectedIndex = 0;
                this.txtFrequency.Text = string.Empty;
            }
            else
            {
                Alert.ShowInTop("责任人和巡检频率不能为空！", MessageBoxIcon.Warning);
            }
        }

        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.RiskListItemService.DeleteRiskListItemById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除风险责任人!");
                }
                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 修改
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
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            var riskItem = Funs.DB.Hazard_RiskListItem.FirstOrDefault(x => x.RiskItemId == Grid1.SelectedRowID);
            if (riskItem != null)
            {
                this.drpRiskOwner.SelectedValue = riskItem.RiskOwnerId;
                if (riskItem.Frequency.HasValue)
                {
                    this.txtFrequency.Text = riskItem.Frequency.ToString();
                }
            }
        }
        #endregion
    }
}
using System;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class MenuFlowOperateEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId
        {
            get
            {
                return (string)ViewState["MenuId"];
            }
            set
            {
                ViewState["MenuId"] = value;
            }
        }

        /// <summary>
        /// 流程ID
        /// </summary>
        public string FlowOperateId
        {
            get
            {
                return (string)ViewState["FlowOperateId"];
            }
            set
            {
                ViewState["FlowOperateId"] = value;
            }
        }
        #endregion

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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.MenuId = Request.Params["MenuId"];
                this.FlowOperateId = Request.Params["FlowOperateId"];
                this.BindDropDownBox();
                BLL.ConstValue.InitConstValueDropDownList(this.drpIsNeed, ConstValue.Group_Y_N, false);

                BLL.DepartService.InitDepartDropDownList(this.drpDepartIds, true);
                if (!string.IsNullOrEmpty(this.FlowOperateId))
                {
                    var menuFlowOperate = BLL.MenuFlowOperateService.GetMenuFlowOperateByFlowOperateId(this.FlowOperateId);
                    if (menuFlowOperate != null)
                    {
                        this.txtFlowStep.Text = menuFlowOperate.FlowStep.ToString();
                        this.txtPushGroup.Text = menuFlowOperate.PushGroup.ToString();
                        this.txtAuditFlowName.Text = menuFlowOperate.AuditFlowName;
                        if (menuFlowOperate.IsNeed.HasValue)
                        {
                            this.drpIsNeed.SelectedValue = Convert.ToString(menuFlowOperate.IsNeed);
                        }
                        else
                        {
                            this.drpIsNeed.SelectedValue = "False";
                        }
                        if (menuFlowOperate.IsFlowEnd != null)
                        {
                            this.IsFlowEnd.Checked = Convert.ToBoolean(menuFlowOperate.IsFlowEnd);
                        }
                        drpWorkPosts.Value = menuFlowOperate.WorkPostIds;
                        //if (!string.IsNullOrEmpty(menuFlowOperate.MatchesValue))
                        //{
                        //    this.drpMatchesValue.SelectedValue = menuFlowOperate.MatchesValue;
                        //}
                        if (!string.IsNullOrEmpty(menuFlowOperate.DepartIds))
                        {
                            this.drpDepartIds.SelectedValueArray = menuFlowOperate.DepartIds.Split(',');
                            this.drpInstallationIds.Items.Clear();
                            BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallationIds, this.drpDepartIds.SelectedValue, true);
                            if (!string.IsNullOrEmpty(menuFlowOperate.InstallationIds))
                            {
                                this.drpInstallationIds.SelectedValueArray = menuFlowOperate.InstallationIds.Split(',');
                            }
                        }
                       
                    }
                }
                else
                {
                    this.txtFlowStep.Text =Funs.GetMaxIndex("Sys_MenuFlowOperate", "FlowStep", "MenuId", this.MenuId ).ToString();
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var menu = BLL.SysMenuService.GetSysMenuByMenuId(this.MenuId);
            if (menu != null)
            {
                if (menu.SuperMenu == BLL.Const.SuperLicenseMenuId && string.IsNullOrEmpty(this.txtPushGroup.Text))
                {
                    Alert.ShowInTop("特殊作业票必须输入组别!", MessageBoxIcon.Error);
                    return;
                }

                Model.Sys_MenuFlowOperate newMenuFlowOperate = new Model.Sys_MenuFlowOperate
                {
                    MenuId = this.MenuId,
                    FlowStep = Funs.GetNewIntOrZero(this.txtFlowStep.Text),
                    PushGroup = Funs.GetNewIntOrZero(this.txtPushGroup.Text),
                    AuditFlowName = this.txtAuditFlowName.Text.Trim()
                };
                string[] WorkPostIds = drpWorkPosts.Values;
                newMenuFlowOperate.WorkPostIds = Funs.ConvertString(WorkPostIds);
                newMenuFlowOperate.IsFlowEnd = this.IsFlowEnd.Checked;
                newMenuFlowOperate.IsNeed = Convert.ToBoolean(this.drpIsNeed.SelectedValue);
                //newMenuFlowOperate.MatchesValue = this.drpMatchesValue.SelectedValue;
                ///部门
                string departIds = Funs.ConvertString(this.drpDepartIds.SelectedValueArray);
                if(departIds != BLL.Const._Null && !string.IsNullOrEmpty(departIds))
                {
                    newMenuFlowOperate.DepartIds = departIds;
                }
                string installationIds = Funs.ConvertString(this.drpInstallationIds.SelectedValueArray);
                if(installationIds != BLL.Const._Null && !string.IsNullOrEmpty(installationIds))
                {
                    ///装置/科室
                    newMenuFlowOperate.InstallationIds = installationIds;
                }
                
                if (string.IsNullOrEmpty(this.FlowOperateId))
                {
                    BLL.MenuFlowOperateService.AddAuditFlow(newMenuFlowOperate);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "增加菜单流程信息");
                }
                else
                {
                    newMenuFlowOperate.FlowOperateId = this.FlowOperateId;
                    BLL.MenuFlowOperateService.UpdateAuditFlow(newMenuFlowOperate);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改菜单流程信息");
                }

                ShowNotify("设置成功!", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        
        /// <summary>
        /// 加载岗位下拉框
        /// </summary>
        private void BindDropDownBox()
        {
            string strSql = @"SELECT WorkPostId,WorkPostName,(WorkPostCode+'-'+WorkPostName) AS WorkPostText  FROM Base_WorkPost WHERE IsAuditFlow =1 ORDER BY WorkPostCode";
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, null);
            rbWorkPosts.DataSource = tb;
            this.rbWorkPosts.DataTextField = "WorkPostText";
            this.rbWorkPosts.DataValueField = "WorkPostId";
            rbWorkPosts.DataBind();
        }

        /// <summary>
        /// 部门下拉框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDepartIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpInstallationIds.Items.Clear();
            BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallationIds, Funs.ConvertString(this.drpDepartIds.SelectedValueArray), true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class TeamGroupEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string TeamGroupId
        {
            get
            {
                return (string)ViewState["TeamGroupId"];
            }
            set
            {
                ViewState["TeamGroupId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                this.txtTeamGroupCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId,true);
                BLL.DepartService.InitDepartDropDownList(this.drpDepart, true);
                
                BLL.UserService.InitUserDropDownList(this.drpLeaders, true);
                this.TeamGroupId = Request.Params["TeamGroupId"];
                if (!string.IsNullOrEmpty(this.TeamGroupId))
                {
                    Model.Base_TeamGroup teamGroup = BLL.TeamGroupService.GetTeamGroupById(this.TeamGroupId);
                    if (teamGroup != null)
                    {
                        this.txtTeamGroupCode.Text = teamGroup.TeamGroupCode;
                        this.txtTeamGroupName.Text = teamGroup.TeamGroupName;
                        if (!string.IsNullOrEmpty(teamGroup.UnitId))
                        {
                            this.drpUnitId.SelectedValue = teamGroup.UnitId;
                        }
                        if (!string.IsNullOrEmpty(teamGroup.DepartId))
                        {
                            this.drpDepart.SelectedValue = teamGroup.DepartId;
                            BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallation, this.drpDepart.SelectedValue, true);
                            if (!string.IsNullOrEmpty(teamGroup.InstallationId))
                            {
                                this.drpInstallation.SelectedValue = teamGroup.InstallationId;
                            }
                        }
                        
                        if (!string.IsNullOrEmpty(teamGroup.TeamType))
                        {
                            this.drpTeamType.SelectedValue = teamGroup.TeamType;
                        }
                        if (!string.IsNullOrEmpty(teamGroup.LeaderIds))
                        {
                            this.drpLeaders.SelectedValue = teamGroup.LeaderIds;
                        }
                        this.txtRemark.Text = teamGroup.Remark;
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Base_TeamGroup teamGroup = new Model.Base_TeamGroup
            {
                TeamGroupCode = this.txtTeamGroupCode.Text.Trim(),
                TeamGroupName = this.txtTeamGroupName.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue!=BLL.Const._Null &&  !string.IsNullOrEmpty(this.drpUnitId.SelectedValue))
            {
                teamGroup.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpDepart.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpDepart.SelectedValue))
            {
                teamGroup.DepartId = this.drpDepart.SelectedValue;
            }
            if (this.drpInstallation.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpInstallation.SelectedValue))
            {
                teamGroup.InstallationId = this.drpInstallation.SelectedValue;
            }
            if (this.drpLeaders.SelectedValue != BLL.Const._Null)
            {
                teamGroup.LeaderIds = this.drpLeaders.SelectedValue;
                teamGroup.LeaderNames = this.drpLeaders.SelectedText;
            }
            if (this.drpTeamType.SelectedValue != BLL.Const._Null)
            {
                teamGroup.TeamType = this.drpTeamType.SelectedValue;
            }
            teamGroup.Remark = this.txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.TeamGroupId))
            {
                teamGroup.TeamGroupId = this.TeamGroupId;
                BLL.TeamGroupService.UpdateTeamGroup(teamGroup);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改班组信息");
            }
            else
            {
                this.TeamGroupId = SQLHelper.GetNewID(typeof(Model.Base_TeamGroup));
                teamGroup.TeamGroupId = this.TeamGroupId;
                BLL.TeamGroupService.AddTeamGroup(teamGroup);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加班组信息");
            }
            ShowNotify("保存数据成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 验证班组名称、项目编号是否存在
        /// <summary>
        /// 验证班组名称、项目编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_TeamGroup.FirstOrDefault(x => x.TeamGroupCode == this.txtTeamGroupCode.Text.Trim() && (x.TeamGroupId != this.TeamGroupId || (this.TeamGroupId == null && x.TeamGroupId != null)));
            if (q != null)
            {
                ShowNotify("输入的班组编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_TeamGroup.FirstOrDefault(x => x.TeamGroupName == this.txtTeamGroupName.Text.Trim() && (x.TeamGroupId != this.TeamGroupId || (this.TeamGroupId == null && x.TeamGroupId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的班组名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 部门下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpInstallation.Items.Clear();
            BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallation, this.drpDepart.SelectedValue, true);
        }
    }
}
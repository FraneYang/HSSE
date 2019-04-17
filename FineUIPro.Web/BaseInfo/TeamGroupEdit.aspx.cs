﻿using System;
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
            Model.Base_TeamGroup teamGroup = new Model.Base_TeamGroup();
            teamGroup.TeamGroupCode = this.txtTeamGroupCode.Text.Trim();
            teamGroup.TeamGroupName = this.txtTeamGroupName.Text.Trim();
            if (this.drpUnitId.SelectedValue!=BLL.Const._Null)
            {
                teamGroup.UnitId = this.drpUnitId.SelectedValue;
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
    }
}
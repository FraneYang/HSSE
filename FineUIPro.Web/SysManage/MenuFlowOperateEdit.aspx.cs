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
                    }
                }
                else
                {
                    int maxId = 0;
                    string str = "SELECT (ISNULL(MAX(FlowStep),0)+1) FROM Sys_MenuFlowOperate WHERE MenuId='" + this.MenuId + "'";
                    maxId = BLL.SQLHelper.getIntValue(str);
                    this.txtFlowStep.Text = maxId.ToString();
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

                Model.Sys_MenuFlowOperate newMenuFlowOperate = new Model.Sys_MenuFlowOperate();
                newMenuFlowOperate.MenuId = this.MenuId;
                newMenuFlowOperate.FlowStep = Funs.GetNewIntOrZero(this.txtFlowStep.Text);
                newMenuFlowOperate.PushGroup = Funs.GetNewIntOrZero(this.txtPushGroup.Text);
                newMenuFlowOperate.AuditFlowName = this.txtAuditFlowName.Text.Trim();
                string[] WorkPostIds = drpWorkPosts.Values;
                newMenuFlowOperate.WorkPostIds = this.ConvertWorkPost(WorkPostIds);
                newMenuFlowOperate.IsFlowEnd = this.IsFlowEnd.Checked;
                newMenuFlowOperate.IsNeed = Convert.ToBoolean(this.drpIsNeed.SelectedValue);
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
            string strSql = @"SELECT WorkPostId,WorkPostName FROM Base_WorkPost WHERE IsAuditFlow =1 ORDER BY WorkPostName";
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, null);
            rbWorkPosts.DataSource = tb;
            this.rbWorkPosts.DataTextField = "WorkPostName";
            this.rbWorkPosts.DataValueField = "WorkPostId";
            rbWorkPosts.DataBind();
        }
       
        /// <summary>
        /// 得到岗位ID字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertWorkPost(string[] WorkPostIds)
        {
            string WorkPosts = null;
            if (WorkPostIds != null && WorkPostIds.Count() > 0)
            {
                foreach (string WorkPostId in WorkPostIds)
                {
                    WorkPosts += WorkPostId + ",";
                }
                if (WorkPosts != string.Empty)
                {
                    WorkPosts = WorkPosts.Substring(0, WorkPosts.Length - 1); ;
                }
            }

            return WorkPosts;
        }

        #region 是否审核结束
        ///// <summary>
        ///// 是否审核结束
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void IsFlowEnd_CheckedChanged(object sender, CheckedEventArgs e)
        //{
        //    if (IsFlowEnd.Checked)
        //    {
        //        this.drpWorkPosts.Value = null;
        //        this.drpWorkPosts.Text = string.Empty;
        //        this.drpWorkPosts.Hidden = true;
        //        if (string.IsNullOrEmpty(this.txtAuditFlowName.Text))
        //        {
        //            this.txtAuditFlowName.Text = "完成";
        //        }
        //    }
        //    else
        //    {
        //        this.drpWorkPosts.Hidden = false;
        //    }
        //}
        #endregion
    }
}
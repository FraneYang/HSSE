using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class InformationCollectionEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 人员ＩＤ
        /// </summary>
        private string UserId
        {
            get
            {
                return (string)ViewState["UserId"];
            }
            set
            {
                ViewState["UserId"] = value;
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
                GetButtonPower();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, true);
                this.UserId = Request.Params["UserId"];
                if (!string.IsNullOrEmpty(this.UserId))
                {
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(this.UserId);
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.UnitId))
                        {
                            this.drpUnit.SelectedValue = user.UnitId;
                        }
                        this.txtUserName.Text = user.UserName;
                        if (!string.IsNullOrEmpty(user.Sex))
                        {
                            this.rblSex.SelectedValue = user.Sex;
                        }
                        if (user.BirthDay != null)
                        {
                            this.txtBirthDay.Text = string.Format("{0:yyyy-MM-dd}", user.BirthDay);
                        }
                        this.txtIdentityCard.Text = user.IdentityCard;
                        this.txtTelephone.Text = user.Telephone;
                        this.txtEntryTime.Text = string.Format("{0:yyyy-MM-dd}", user.EntryTime);
                    }
                }
                else
                {
                    this.txtEntryTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
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
            Model.Sys_User user = new Model.Sys_User
            {
                UserName = this.txtUserName.Text.Trim(),
                EntryTime = Funs.GetNewDateTime(this.txtEntryTime.Text),
            };
            if (!string.IsNullOrEmpty(this.rblSex.SelectedValue))
            {
                user.Sex = this.rblSex.SelectedValue;
            }
            user.BirthDay = Funs.GetNewDateTime(this.txtBirthDay.Text.Trim());
            user.IdentityCard = this.txtIdentityCard.Text.Trim();
            user.Telephone = this.txtTelephone.Text.Trim();
            user.IsPost = false;
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                user.UnitId = this.drpUnit.SelectedValue;
            }
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是本单位或者管理员
            {
                user.UnitId = this.CurrUser.UnitId;
            }
            if (!string.IsNullOrEmpty(this.UserId))
            {
                user.UserId = this.UserId;
                BLL.UserService.UpdateUser(user);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改信息采集");
            }
            else
            {
                BLL.UserService.AddUser(user);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加信息采集");
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.InformationCollectionMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}
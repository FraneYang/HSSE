using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class UserListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId
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

        /// <summary>
        /// 用户编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///权限
                this.GetButtonPower();
                this.UserId = Request.Params["userId"];
               
                BLL.ConstValue.InitConstValueDropDownList(this.drpIsPost, ConstValue.Group_Y_N, false);
                BLL.UnitService.InitUnitDropDownList(this.drpUnit,true);
                BLL.WorkPostService.InitWorkPostDropDownList(this.drpWorkPost, false);
                BLL.DepartService.InitDepartDropDownList(this.drpDepart, false);
               
                if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
                {
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                }
                if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是本单位或者管理员
                {
                    this.drpUnit.Enabled = false;
                }

                ///角色下拉框
                BLL.RoleService.InitRoleDropDownList(this.drpRole, string.Empty, true);
                if (!string.IsNullOrEmpty(this.UserId))
                {
                    var user = BLL.UserService.GetUserByUserId(this.UserId);
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.UnitId))
                        {
                            this.drpUnit.SelectedValue = user.UnitId;
                        }
                        this.txtUserCode.Text = user.UserCode;
                        this.txtUserName.Text = user.UserName;
                        this.txtAccount.Text = user.Account;
                        this.txtTelephone.Text = user.Telephone;
                        if (!string.IsNullOrEmpty(user.RoleId))
                        {
                            this.drpRole.SelectedValue = user.RoleId;
                        }
                        if (!string.IsNullOrEmpty(user.DepartId))
                        {
                            this.drpDepart.SelectedValue = user.DepartId;                            
                            
                            BLL.InstallationService.InitInstallationDropDownList(this.drpInstallation, this.drpDepart.SelectedValue, true);
                            if (!string.IsNullOrEmpty(user.InstallationId))
                            {
                                this.drpInstallation.SelectedValue = user.InstallationId;  
                            }
                        }
                        if (!string.IsNullOrEmpty(user.WorkPostId))
                        {
                            this.drpWorkPost.SelectedValue = user.WorkPostId;
                        } 
                        if (user.IsPost.HasValue)
                        {
                            this.drpIsPost.SelectedValue = Convert.ToString(user.IsPost);
                        }
                        this.txtIdentityCard.Text = user.IdentityCard;
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == Const._Null)
            {
                Alert.ShowInParent("请选择单位！", MessageBoxIcon.Warning);
                return;
            }
            if (BLL.UserService.IsExistUserAccount(this.UserId, this.txtAccount.Text.Trim()))
            {
                Alert.ShowInParent("用户账号已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.UserId, this.txtIdentityCard.Text.Trim()) == true)
            {
                ShowNotify("身份证号码已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            //if (this.txtIdentityCard.Text.Trim().Length!=18)
            //{
            //    ShowNotify("身份证号码必须是18位！", MessageBoxIcon.Warning);
            //    return;
            //}
            Model.Sys_User newUser = new Model.Sys_User();
            newUser.UserCode = this.txtUserCode.Text.Trim();
            newUser.UserName = this.txtUserName.Text.Trim();
            newUser.Account = this.txtAccount.Text.Trim();
            newUser.Telephone = this.txtTelephone.Text.Trim();
            newUser.IdentityCard = this.txtIdentityCard.Text.Trim();
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                newUser.UnitId = this.drpUnit.SelectedValue;
            }
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是本单位或者管理员
            {
                newUser.UnitId = this.CurrUser.UnitId;
            }
            if (this.drpWorkPost.SelectedValue != Const._Null)
            {
                newUser.WorkPostId = this.drpWorkPost.SelectedValue;
            }
            if (this.drpDepart.SelectedValue != Const._Null)
            {
                newUser.DepartId = this.drpDepart.SelectedValue;
            }
            if (this.drpInstallation.SelectedValue != Const._Null)
            {
                newUser.InstallationId = this.drpInstallation.SelectedValue;
            }
            if (this.drpRole.SelectedValue != Const._Null)
            {
                newUser.RoleId = this.drpRole.SelectedValue;
            }

            newUser.IsPost = Convert.ToBoolean(this.drpIsPost.SelectedValue);
            if (string.IsNullOrEmpty(this.UserId))
            {
                newUser.Password = Funs.EncryptionPassword(Const.Password);
                newUser.UserId = SQLHelper.GetNewID(typeof(Model.Sys_User));
                BLL.UserService.AddUser(newUser);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加用户信息");
            }
            else
            {
                newUser.UserId = this.UserId;
                BLL.UserService.UpdateUser(newUser);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改用户信息");
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.UserMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证用户编号、账号是否存在
        /// <summary>
        /// 验证用户编号、账号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Sys_User.FirstOrDefault(x => x.Account == this.txtAccount.Text.Trim() && (x.UserId != this.UserId || (this.UserId == null && x.UserId != null)));
            if (q != null)
            {
                ShowNotify("输入的账号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Sys_User.FirstOrDefault(x => x.UserCode == this.txtUserCode.Text.Trim() && (x.UserId != this.UserId || (this.UserId == null && x.UserId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.UserId, this.txtIdentityCard.Text.Trim()) == true)
            {
                ShowNotify("输入的身份证号码已存在！", MessageBoxIcon.Warning);                
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpInstallation.Items.Clear();
            BLL.InstallationService.InitInstallationDropDownList(this.drpInstallation, this.drpDepart.SelectedValue, true);
        }
    }
}
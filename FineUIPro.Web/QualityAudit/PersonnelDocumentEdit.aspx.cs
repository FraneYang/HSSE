using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class PersonnelDocumentEdit : PageBase
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

        #region 加载
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
                this.UserId = Request.Params["UserId"];

                BLL.ConstValue.InitConstValueDropDownList(this.drpIsPost, ConstValue.Group_Y_N, false);
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, true);
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
                        this.txtSortIndex.Text = user.SortIndex;
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

                            BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallation, this.drpDepart.SelectedValue, true);
                            if (!string.IsNullOrEmpty(user.InstallationId))
                            {
                                this.drpInstallation.SelectedValueArray = user.InstallationId.Split(',');
                            }
                        }
                        if (!string.IsNullOrEmpty(user.WorkPostId))
                        {
                            this.drpWorkPost.SelectedValueArray = user.WorkPostId.Split(',');
                        }
                        if (user.IsPost.HasValue)
                        {
                            this.drpIsPost.SelectedValue = Convert.ToString(user.IsPost);
                        }
                        this.txtIdentityCard.Text = user.IdentityCard;
                    }
                    this.txtEntryTime.Text = string.Format("{0:yyyy-MM-dd}", user.EntryTime);
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
            Model.Sys_User newUser = new Model.Sys_User
            {
                SortIndex = this.txtSortIndex.Text.Trim(),
                UserName = this.txtUserName.Text.Trim(),
                Account = this.txtAccount.Text.Trim(),
                UserCode = this.txtUserCode.Text.Trim(),
                Telephone = this.txtTelephone.Text.Trim(),
                IdentityCard = this.txtIdentityCard.Text.Trim(),
                EntryTime = Funs.GetNewDateTime(this.txtEntryTime.Text),
            };
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                newUser.UnitId = this.drpUnit.SelectedValue;
            }
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是本单位或者管理员
            {
                newUser.UnitId = this.CurrUser.UnitId;
            }
            //岗位
            string workPostId = string.Empty;
            string workPostName = string.Empty;
            foreach (var item in this.drpWorkPost.SelectedValueArray)
            {
                var workPost = BLL.WorkPostService.GetWorkPostById(item);
                if (workPost != null)
                {
                    workPostId += workPost.WorkPostId + ",";
                    workPostName += workPost.WorkPostName + ",";
                }
            }
            if (!string.IsNullOrEmpty(workPostId))
            {
                newUser.WorkPostId = workPostId.Substring(0, workPostId.LastIndexOf(","));
                newUser.WorkPostName = workPostName.Substring(0, workPostName.LastIndexOf(","));
            }

            if (this.drpDepart.SelectedValue != Const._Null)
            {
                newUser.DepartId = this.drpDepart.SelectedValue;
            }
            //装置
            string installationId = string.Empty;
            string installationName = string.Empty;
            foreach (var item in this.drpInstallation.SelectedValueArray)
            {
                var Installation = BLL.InstallationService.GetInstallationByInstallationId(item);
                if (Installation != null)
                {
                    installationId += Installation.InstallationId + ",";
                    installationName += Installation.InstallationName + ",";
                }
            }
            if (!string.IsNullOrEmpty(installationId))
            {
                newUser.InstallationId = installationId.Substring(0, installationId.LastIndexOf(","));
                newUser.InstallationName = installationName.Substring(0, installationName.LastIndexOf(","));
            }
            if (this.drpRole.SelectedValue != Const._Null)
            {
                newUser.RoleId = this.drpRole.SelectedValue;
            }
            newUser.IsPost = Convert.ToBoolean(this.drpIsPost.SelectedValue);
            if (!string.IsNullOrEmpty(this.UserId))
            {
                newUser.UserId = this.UserId;
                BLL.UserService.UpdateUser(newUser);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改人员建档信息");
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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

        #region 下拉选择事件
        /// <summary>
        /// 部门下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpInstallation.Items.Clear();
            BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallation, this.drpDepart.SelectedValue, true);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.PersonnelDocumentMenuId);
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
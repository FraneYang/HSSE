using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class InstallationEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 装置/科室主键
        /// </summary>
        public string InstallationId
        {
            get
            {
                return (string)ViewState["InstallationId"];
            }
            set
            {
                ViewState["InstallationId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 装置/科室编辑页面
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
                this.InstallationId = Request.Params["InstallationId"];
               
                BLL.ConstValue.InitConstValueDropDownList(this.drpIsUsed, ConstValue.Group_Y_N, false);
                BLL.DepartService.InitDepartDropDownList(this.drpDepart,true);
                ///负责人下拉框
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpManagerIds, null, null, this.InstallationId, true);
                if (!string.IsNullOrEmpty(this.InstallationId))
                {
                    var Installation = BLL.InstallationService.GetInstallationByInstallationId(this.InstallationId);
                    if (Installation != null)
                    {
                        if (!string.IsNullOrEmpty(Installation.DepartId))
                        {
                            this.drpDepart.SelectedValue = Installation.DepartId;
                        }
                        this.txtInstallationCode.Text = Installation.InstallationCode;
                        this.txtInstallationName.Text = Installation.InstallationName;
                        this.txtDef.Text = Installation.Def;
                        if (Installation.IsUsed.HasValue)
                        {
                            this.drpIsUsed.SelectedValue = Convert.ToString(Installation.IsUsed);
                        }
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
            if (this.drpDepart.SelectedValue == Const._Null)
            {
                Alert.ShowInParent("请选择部门！", MessageBoxIcon.Warning);
                return;
            }
            if (BLL.InstallationService.IsExistInstallationName(this.InstallationId, this.txtInstallationName.Text.Trim()))
            {
                Alert.ShowInParent("装置/科室名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Installation newInstallation = new Model.Base_Installation();
            newInstallation.InstallationCode = this.txtInstallationCode.Text.Trim();
            newInstallation.InstallationName = this.txtInstallationName.Text.Trim();
            newInstallation.Def = this.txtDef.Text.Trim();
            if (this.drpDepart.SelectedValue != Const._Null)
            {
                newInstallation.DepartId = this.drpDepart.SelectedValue;
            }
            newInstallation.IsUsed = Convert.ToBoolean(this.drpIsUsed.SelectedValue);
           
            ///装置负责人
            string managerIds = string.Empty;
            string managerNames = string.Empty;
            foreach (var item in this.drpManagerIds.SelectedValueArray)
            {
                var users = BLL.UserService.GetUserByUserId(item);
                if (users != null)
                {
                    managerIds += users.UserId + ",";
                    managerNames += users.UserName + ",";
                }
            }
            if (!string.IsNullOrEmpty(managerIds))
            {
                newInstallation.ManagerIds = managerIds.Substring(0, managerIds.LastIndexOf(","));
                newInstallation.ManagerNames = managerNames.Substring(0, managerNames.LastIndexOf(","));
            }

            if (string.IsNullOrEmpty(this.InstallationId))
            {
                newInstallation.InstallationId = SQLHelper.GetNewID(typeof(Model.Base_Installation));
                BLL.InstallationService.AddInstallation(newInstallation);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加装置/科室");
            }
            else
            {
                newInstallation.InstallationId = this.InstallationId;
                BLL.InstallationService.UpdateInstallation(newInstallation);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改装置/科室");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.InstallationMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证装置/科室编号、名称是否存在
        /// <summary>
        /// 验证装置/科室编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_Installation.FirstOrDefault(x => x.InstallationName == this.txtInstallationName.Text.Trim() && (x.InstallationId != this.InstallationId || (this.InstallationId == null && x.InstallationId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_Installation.FirstOrDefault(x => x.InstallationCode == this.txtInstallationCode.Text.Trim() && (x.InstallationId != this.InstallationId || (this.InstallationId == null && x.InstallationId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
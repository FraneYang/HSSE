using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class DepartEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 部门信息主键
        /// </summary>
        public string DepartId
        {
            get
            {
                return (string)ViewState["DepartId"];
            }
            set
            {
                ViewState["DepartId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 部门信息编辑页面
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
                this.DepartId = Request.Params["DepartId"];
               
                BLL.ConstValue.InitConstValueDropDownList(this.drpIsUsed, ConstValue.Group_Y_N, false);            
                ///负责人下拉框
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpManagerIds, null, null, this.DepartId, true);
                if (!string.IsNullOrEmpty(this.DepartId))
                {
                    var depart = BLL.DepartService.GetDepartById(this.DepartId);
                    if (depart != null)
                    {                        
                        this.txtDepartCode.Text = depart.DepartCode;
                        this.txtDepartName.Text = depart.DepartName;
                        this.txtRemark.Text = depart.Remark;
                        if (depart.IsUsed.HasValue)
                        {
                            this.drpIsUsed.SelectedValue = Convert.ToString(depart.IsUsed);
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
            Model.Base_Depart newDepart = new Model.Base_Depart
            {
                DepartCode = this.txtDepartCode.Text.Trim(),
                DepartName = this.txtDepartName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),

                IsUsed = Convert.ToBoolean(this.drpIsUsed.SelectedValue)
            };

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
                newDepart.ManagerIds = managerIds.Substring(0, managerIds.LastIndexOf(","));
                newDepart.ManagerNames = managerNames.Substring(0, managerNames.LastIndexOf(","));
            }

            if (string.IsNullOrEmpty(this.DepartId))
            {
                newDepart.DepartId = SQLHelper.GetNewID(typeof(Model.Base_Depart));
                BLL.DepartService.AddDepart(newDepart);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加部门信息");
            }
            else
            {
                newDepart.DepartId = this.DepartId;
                BLL.DepartService.UpdateDepart(newDepart);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改部门信息");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.DepartMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证部门信息编号、名称是否存在
        /// <summary>
        /// 验证部门信息编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_Depart.FirstOrDefault(x => x.DepartName == this.txtDepartName.Text.Trim() && (x.DepartId != this.DepartId || (this.DepartId == null && x.DepartId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_Depart.FirstOrDefault(x => x.DepartCode == this.txtDepartCode.Text.Trim() && (x.DepartId != this.DepartId || (this.DepartId == null && x.DepartId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
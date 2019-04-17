using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class EuipmentTypeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 设备设施类型主键
        /// </summary>
        public string EuipmentTypeId
        {
            get
            {
                return (string)ViewState["EuipmentTypeId"];
            }
            set
            {
                ViewState["EuipmentTypeId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 设备设施类型编辑页面
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
                this.EuipmentTypeId = Request.Params["EuipmentTypeId"];          
                if (!string.IsNullOrEmpty(this.EuipmentTypeId))
                {
                    var EuipmentType = BLL.EuipmentTypeService.GetEuipmentTypeByEuipmentTypeId(this.EuipmentTypeId);
                    if (EuipmentType != null)
                    {
                        this.txtEuipmentTypeCode.Text = EuipmentType.EuipmentTypeCode;
                        this.txtEuipmentTypeName.Text = EuipmentType.EuipmentTypeName;
                        this.txtRemark.Text = EuipmentType.Remark;
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
            if (BLL.EuipmentTypeService.IsExistEuipmentTypeName(this.EuipmentTypeId, this.txtEuipmentTypeName.Text.Trim()))
            {
                Alert.ShowInParent("设备设施类型名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_EuipmentType newEuipmentType = new Model.Base_EuipmentType
            {
                EuipmentTypeCode = this.txtEuipmentTypeCode.Text.Trim(),
                EuipmentTypeName = this.txtEuipmentTypeName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(this.EuipmentTypeId))
            {
                newEuipmentType.EuipmentTypeId = SQLHelper.GetNewID(typeof(Model.Base_EuipmentType));
                BLL.EuipmentTypeService.AddEuipmentType(newEuipmentType);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加设备设施类型");
            }
            else
            {
                newEuipmentType.EuipmentTypeId = this.EuipmentTypeId;
                BLL.EuipmentTypeService.UpdateEuipmentType(newEuipmentType);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改设备设施类型");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.EuipmentTypeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证设备设施类型编号、名称是否存在
        /// <summary>
        /// 验证设备设施类型编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_EuipmentType.FirstOrDefault(x => x.EuipmentTypeName == this.txtEuipmentTypeName.Text.Trim() && (x.EuipmentTypeId != this.EuipmentTypeId || (this.EuipmentTypeId == null && x.EuipmentTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_EuipmentType.FirstOrDefault(x => x.EuipmentTypeCode == this.txtEuipmentTypeCode.Text.Trim() && (x.EuipmentTypeId != this.EuipmentTypeId || (this.EuipmentTypeId == null && x.EuipmentTypeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
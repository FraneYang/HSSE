using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class EmergencyTypeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 应急救援类型主键
        /// </summary>
        public string EmergencyTypeId
        {
            get
            {
                return (string)ViewState["EmergencyTypeId"];
            }
            set
            {
                ViewState["EmergencyTypeId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 应急救援类型编辑页面
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
                this.EmergencyTypeId = Request.Params["EmergencyTypeId"];
                if (!string.IsNullOrEmpty(this.EmergencyTypeId))
                {
                    var EmergencyType = BLL.EmergencyTypeService.GetEmergencyTypeById(this.EmergencyTypeId);
                    if (EmergencyType != null)
                    {                        
                        this.txtEmergencyTypeCode.Text = EmergencyType.EmergencyTypeCode;
                        this.txtEmergencyTypeName.Text = EmergencyType.EmergencyTypeName;
                        this.txtRemark.Text = EmergencyType.Remark;                       
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
            Model.Base_EmergencyType newEmergencyType = new Model.Base_EmergencyType
            {
                EmergencyTypeCode = this.txtEmergencyTypeCode.Text.Trim(),
                EmergencyTypeName = this.txtEmergencyTypeName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
            };

            if (string.IsNullOrEmpty(this.EmergencyTypeId))
            {
                newEmergencyType.EmergencyTypeId = SQLHelper.GetNewID(typeof(Model.Base_EmergencyType));
                BLL.EmergencyTypeService.AddEmergencyType(newEmergencyType);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加应急救援类型");
            }
            else
            {
                newEmergencyType.EmergencyTypeId = this.EmergencyTypeId;
                BLL.EmergencyTypeService.UpdateEmergencyType(newEmergencyType);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改应急救援类型");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.EmergencyTypeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证应急救援类型编号、名称是否存在
        /// <summary>
        /// 验证应急救援类型编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_EmergencyType.FirstOrDefault(x => x.EmergencyTypeName == this.txtEmergencyTypeName.Text.Trim() && (x.EmergencyTypeId != this.EmergencyTypeId || (this.EmergencyTypeId == null && x.EmergencyTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_EmergencyType.FirstOrDefault(x => x.EmergencyTypeCode == this.txtEmergencyTypeCode.Text.Trim() && (x.EmergencyTypeId != this.EmergencyTypeId || (this.EmergencyTypeId == null && x.EmergencyTypeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
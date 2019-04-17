using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class HiddenHazardTypeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 隐患类别主键
        /// </summary>
        public string HiddenHazardTypeId
        {
            get
            {
                return (string)ViewState["HiddenHazardTypeId"];
            }
            set
            {
                ViewState["HiddenHazardTypeId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 隐患类别编辑页面
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
                this.HiddenHazardTypeId = Request.Params["HiddenHazardTypeId"];          
                if (!string.IsNullOrEmpty(this.HiddenHazardTypeId))
                {
                    var HiddenHazardType = BLL.HiddenHazardTypeService.GetHiddenHazardTypeByHiddenHazardTypeId(this.HiddenHazardTypeId);
                    if (HiddenHazardType != null)
                    {
                        this.txtHiddenHazardTypeCode.Text = HiddenHazardType.HiddenHazardTypeCode;
                        this.txtHiddenHazardTypeName.Text = HiddenHazardType.HiddenHazardTypeName;
                        this.txtRemark.Text = HiddenHazardType.Remark;
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
            if (BLL.HiddenHazardTypeService.IsExistHiddenHazardTypeName(this.HiddenHazardTypeId, this.txtHiddenHazardTypeName.Text.Trim()))
            {
                Alert.ShowInParent("隐患类别名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_HiddenHazardType newHiddenHazardType = new Model.Base_HiddenHazardType();
            newHiddenHazardType.HiddenHazardTypeCode = this.txtHiddenHazardTypeCode.Text.Trim();
            newHiddenHazardType.HiddenHazardTypeName = this.txtHiddenHazardTypeName.Text.Trim();
            newHiddenHazardType.Remark = this.txtRemark.Text.Trim();
            if (string.IsNullOrEmpty(this.HiddenHazardTypeId))
            {
                newHiddenHazardType.HiddenHazardTypeId = SQLHelper.GetNewID(typeof(Model.Base_HiddenHazardType));
                BLL.HiddenHazardTypeService.AddHiddenHazardType(newHiddenHazardType);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加隐患类别");
            }
            else
            {
                newHiddenHazardType.HiddenHazardTypeId = this.HiddenHazardTypeId;
                BLL.HiddenHazardTypeService.UpdateHiddenHazardType(newHiddenHazardType);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改隐患类别");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.HiddenHazardTypeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证隐患类别编号、名称是否存在
        /// <summary>
        /// 验证隐患类别编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_HiddenHazardType.FirstOrDefault(x => x.HiddenHazardTypeName == this.txtHiddenHazardTypeName.Text.Trim() && (x.HiddenHazardTypeId != this.HiddenHazardTypeId || (this.HiddenHazardTypeId == null && x.HiddenHazardTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_HiddenHazardType.FirstOrDefault(x => x.HiddenHazardTypeCode == this.txtHiddenHazardTypeCode.Text.Trim() && (x.HiddenHazardTypeId != this.HiddenHazardTypeId || (this.HiddenHazardTypeId == null && x.HiddenHazardTypeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class StandardEdit : PageBase
    {
        #region 定义项        
        /// <summary>
        /// 标准主键
        /// </summary>
        public string StandardId
        {
            get
            {
                return (string)ViewState["StandardId"];
            }
            set
            {
                ViewState["StandardId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 标准名称编辑页面
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
                this.StandardId = Request.Params["StandardId"];
                BLL.SpecialtyService.InitSpecialtyDropDownList(this.drpSpecialtyId, false);
                if (!string.IsNullOrEmpty(this.StandardId))
                {
                    var Standard = BLL.StandardService.GetStandardById(this.StandardId);
                    if (Standard != null)
                    {
                        this.drpSpecialtyId.SelectedValue = Standard.SpecialtyId;
                        this.txtStandardName.Text = Standard.StandardName;
                        this.txtStandardCode.Text = Standard.StandardCode;
                        this.txtRemark.Text = Standard.Remark;                        
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
            SaveData(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        private void SaveData(bool isColose)
        {
            if (isColose)
            {
                if (BLL.StandardService.IsExistStandardName(this.drpSpecialtyId.SelectedValue, this.StandardId, this.txtStandardName.Text.Trim()))
                {
                    Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.drpSpecialtyId.SelectedValue))
                {
                    Alert.ShowInParent("请先选择专业，再保存！", MessageBoxIcon.Warning);
                    return;
                }
            }

            Model.Base_Standard newStandard = new Model.Base_Standard
            {
                StandardCode = this.txtStandardCode.Text.Trim(),
                StandardName = this.txtStandardName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                SpecialtyId = this.drpSpecialtyId.SelectedValue
            };
            if (string.IsNullOrEmpty(this.StandardId))
            {
                this.StandardId = SQLHelper.GetNewID(typeof(Model.Base_Standard));
                newStandard.StandardId = this.StandardId;
                BLL.StandardService.AddStandard(newStandard);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加标准名称");
            }
            else
            {
                newStandard.StandardId = this.StandardId;
                BLL.StandardService.UpdateStandard(newStandard);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改标准名称");
            }
            if (isColose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.StandardMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 检验名称不能重复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtStandardName_TextChanged(object sender, EventArgs e)
        {
            if (BLL.StandardService.IsExistStandardName(this.drpSpecialtyId.SelectedValue,this.StandardId, this.txtStandardName.Text.Trim()))
            {
                Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.StandardId))
            {
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Standard&menuId={1}", StandardId, BLL.Const.StandardMenuId)));
        }
        #endregion
    }
}
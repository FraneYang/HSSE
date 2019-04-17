using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SpecialtyEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 专业信息主键
        /// </summary>
        public string SpecialtyId
        {
            get
            {
                return (string)ViewState["SpecialtyId"];
            }
            set
            {
                ViewState["SpecialtyId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 专业信息编辑页面
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
                this.SpecialtyId = Request.Params["SpecialtyId"];
                if (!string.IsNullOrEmpty(this.SpecialtyId))
                {
                    var Specialty = BLL.SpecialtyService.GetSpecialtyById(this.SpecialtyId);
                    if (Specialty != null)
                    {
                        this.txtSpecialtyName.Text = Specialty.SpecialtyName;
                        this.txtSpecialtyCode.Text = Specialty.SpecialtyCode;
                        this.txtRemark.Text = Specialty.Remark;                        
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
            if (BLL.SpecialtyService.IsExistSpecialtyName(this.SpecialtyId, this.txtSpecialtyName.Text.Trim()))
            {
                Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            Model.Base_Specialty newSpecialty = new Model.Base_Specialty
            {
                SpecialtyCode = this.txtSpecialtyCode.Text.Trim(),
                SpecialtyName = this.txtSpecialtyName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            if (string.IsNullOrEmpty(this.SpecialtyId))
            {
                newSpecialty.SpecialtyId = SQLHelper.GetNewID(typeof(Model.Base_Specialty));
                BLL.SpecialtyService.AddSpecialty(newSpecialty);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加专业信息");
            }
            else
            {
                newSpecialty.SpecialtyId = this.SpecialtyId;
                BLL.SpecialtyService.UpdateSpecialty(newSpecialty);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改专业信息");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.SpecialtyMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        protected void txtSpecialtyName_TextChanged(object sender, EventArgs e)
        {
            if (BLL.SpecialtyService.IsExistSpecialtyName(this.SpecialtyId, this.txtSpecialtyName.Text.Trim()))
            {
                Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
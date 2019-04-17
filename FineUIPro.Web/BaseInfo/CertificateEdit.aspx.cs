using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class CertificateEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 特岗证书主键
        /// </summary>
        public string CertificateId
        {
            get
            {
                return (string)ViewState["CertificateId"];
            }
            set
            {
                ViewState["CertificateId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 特岗证书编辑页面
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
                this.CertificateId = Request.Params["CertificateId"];               
                if (!string.IsNullOrEmpty(this.CertificateId))
                {
                    var Certificate = BLL.CertificateService.GetCertificateById(this.CertificateId);
                    if (Certificate != null)
                    {                        
                        this.txtCertificateCode.Text = Certificate.CertificateCode;
                        this.txtCertificateName.Text = Certificate.CertificateName;
                        this.txtRemark.Text = Certificate.Remark;                       
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
            Model.Base_Certificate newCertificate = new Model.Base_Certificate
            {
                CertificateCode = this.txtCertificateCode.Text.Trim(),
                CertificateName = this.txtCertificateName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
            };

            if (string.IsNullOrEmpty(this.CertificateId))
            {
                newCertificate.CertificateId = SQLHelper.GetNewID(typeof(Model.Base_Certificate));
                BLL.CertificateService.AddCertificate(newCertificate);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加特岗证书");
            }
            else
            {
                newCertificate.CertificateId = this.CertificateId;
                BLL.CertificateService.UpdateCertificate(newCertificate);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改特岗证书");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.CertificateMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证特岗证书编号、名称是否存在
        /// <summary>
        /// 验证特岗证书编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_Certificate.FirstOrDefault(x => x.CertificateName == this.txtCertificateName.Text.Trim() && (x.CertificateId != this.CertificateId || (this.CertificateId == null && x.CertificateId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_Certificate.FirstOrDefault(x => x.CertificateCode == this.txtCertificateCode.Text.Trim() && (x.CertificateId != this.CertificateId || (this.CertificateId == null && x.CertificateId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
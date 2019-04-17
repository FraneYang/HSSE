using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class ProspectiveEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 准操项目主键
        /// </summary>
        public string ProspectiveId
        {
            get
            {
                return (string)ViewState["ProspectiveId"];
            }
            set
            {
                ViewState["ProspectiveId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 准操项目编辑页面
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
                this.ProspectiveId = Request.Params["ProspectiveId"];               
                if (!string.IsNullOrEmpty(this.ProspectiveId))
                {
                    var Prospective = BLL.ProspectiveService.GetProspectiveById(this.ProspectiveId);
                    if (Prospective != null)
                    {                        
                        this.txtProspectiveCode.Text = Prospective.ProspectiveCode;
                        this.txtProspectiveName.Text = Prospective.ProspectiveName;
                        this.txtRemark.Text = Prospective.Remark;                       
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
            Model.Base_Prospective newProspective = new Model.Base_Prospective
            {
                ProspectiveCode = this.txtProspectiveCode.Text.Trim(),
                ProspectiveName = this.txtProspectiveName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
            };

            if (string.IsNullOrEmpty(this.ProspectiveId))
            {
                newProspective.ProspectiveId = SQLHelper.GetNewID(typeof(Model.Base_Prospective));
                BLL.ProspectiveService.AddProspective(newProspective);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加准操项目");
            }
            else
            {
                newProspective.ProspectiveId = this.ProspectiveId;
                BLL.ProspectiveService.UpdateProspective(newProspective);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改准操项目");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.ProspectiveMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证准操项目编号、名称是否存在
        /// <summary>
        /// 验证准操项目编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_Prospective.FirstOrDefault(x => x.ProspectiveName == this.txtProspectiveName.Text.Trim() && (x.ProspectiveId != this.ProspectiveId || (this.ProspectiveId == null && x.ProspectiveId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_Prospective.FirstOrDefault(x => x.ProspectiveCode == this.txtProspectiveCode.Text.Trim() && (x.ProspectiveId != this.ProspectiveId || (this.ProspectiveId == null && x.ProspectiveId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class AppraisalItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 测评考核项主键
        /// </summary>
        public string AppraisalItemId
        {
            get
            {
                return (string)ViewState["AppraisalItemId"];
            }
            set
            {
                ViewState["AppraisalItemId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 测评考核项编辑页面
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
                this.AppraisalItemId = Request.Params["AppraisalItemId"];               
                if (!string.IsNullOrEmpty(this.AppraisalItemId))
                {
                    var AppraisalItem = BLL.AppraisalItemService.GetAppraisalItemById(this.AppraisalItemId);
                    if (AppraisalItem != null)
                    {
                        this.txtCode.Text = AppraisalItem.Code;
                        this.txtCheckItem.Text = AppraisalItem.CheckItem;
                        this.txtRemark.Text = AppraisalItem.Remark;
                        if (AppraisalItem.Score.HasValue)
                        {
                            this.txtScore.Text = AppraisalItem.Score.ToString();
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
            Model.Base_AppraisalItem newAppraisalItem = new Model.Base_AppraisalItem
            {
                Code = this.txtCode.Text.Trim(),
                CheckItem = this.txtCheckItem.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                Score=Funs.GetNewDecimal(this.txtScore.Text),
            };

            if (string.IsNullOrEmpty(this.AppraisalItemId))
            {
                newAppraisalItem.AppraisalItemId = SQLHelper.GetNewID(typeof(Model.Base_AppraisalItem));
                BLL.AppraisalItemService.AddAppraisalItem(newAppraisalItem);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加测评考核项");
            }
            else
            {
                newAppraisalItem.AppraisalItemId = this.AppraisalItemId;
                BLL.AppraisalItemService.UpdateAppraisalItem(newAppraisalItem);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改测评考核项");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.AppraisalItemMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证测评考核项编号、名称是否存在
        /// <summary>
        /// 验证测评考核项编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_AppraisalItem.FirstOrDefault(x => x.CheckItem == this.txtCheckItem.Text.Trim() && (x.AppraisalItemId != this.AppraisalItemId || (this.AppraisalItemId == null && x.AppraisalItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

    }
}
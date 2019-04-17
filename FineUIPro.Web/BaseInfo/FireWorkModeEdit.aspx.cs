using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class FireWorkModeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 动火方式主键
        /// </summary>
        public string FireWorkModeId
        {
            get
            {
                return (string)ViewState["FireWorkModeId"];
            }
            set
            {
                ViewState["FireWorkModeId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 动火方式编辑页面
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
                this.FireWorkModeId = Request.Params["FireWorkModeId"];
                if (!string.IsNullOrEmpty(this.FireWorkModeId))
                {
                    var FireWorkMode = BLL.FireWorkModeService.GetFireWorkModeById(this.FireWorkModeId);
                    if (FireWorkMode != null)
                    {
                        this.txtFireWorkModeName.Text = FireWorkMode.FireWorkModeName;
                        this.txtFireWorkModeCode.Text = FireWorkMode.FireWorkModeCode;
                        this.txtRemark.Text = FireWorkMode.Remark;                        
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
            Model.Base_FireWorkMode newFireWorkMode = new Model.Base_FireWorkMode
            {
                FireWorkModeCode = this.txtFireWorkModeCode.Text.Trim(),
                FireWorkModeName = this.txtFireWorkModeName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            if (string.IsNullOrEmpty(this.FireWorkModeId))
            {
                newFireWorkMode.FireWorkModeId = SQLHelper.GetNewID(typeof(Model.Base_FireWorkMode));
                BLL.FireWorkModeService.AddFireWorkMode(newFireWorkMode);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加动火方式");
            }
            else
            {
                newFireWorkMode.FireWorkModeId = this.FireWorkModeId;
                BLL.FireWorkModeService.UpdateFireWorkMode(newFireWorkMode);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改动火方式");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.FireWorkModeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

    }
}
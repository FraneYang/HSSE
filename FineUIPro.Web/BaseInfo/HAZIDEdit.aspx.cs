using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class HAZIDEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 危害辨识主键
        /// </summary>
        public string HAZIDId
        {
            get
            {
                return (string)ViewState["HAZIDId"];
            }
            set
            {
                ViewState["HAZIDId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 危害辨识编辑页面
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
                this.HAZIDId = Request.Params["HAZIDId"];          
                if (!string.IsNullOrEmpty(this.HAZIDId))
                {
                    var HAZID = BLL.HAZIDService.GetHAZIDByHAZIDId(this.HAZIDId);
                    if (HAZID != null)
                    {
                        this.txtHAZIDCode.Text = HAZID.HAZIDCode;
                        this.txtHAZIDName.Text = HAZID.HAZIDName;
                        this.txtRemark.Text = HAZID.Remark;
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
            if (BLL.HAZIDService.IsExistHAZIDName(this.HAZIDId, this.txtHAZIDName.Text.Trim()))
            {
                Alert.ShowInParent("危害辨识名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_HAZID newHAZID = new Model.Base_HAZID();
            newHAZID.HAZIDCode = this.txtHAZIDCode.Text.Trim();
            newHAZID.HAZIDName = this.txtHAZIDName.Text.Trim();
            newHAZID.Remark = this.txtRemark.Text.Trim();
            if (string.IsNullOrEmpty(this.HAZIDId))
            {
                newHAZID.HAZIDId = SQLHelper.GetNewID(typeof(Model.Base_HAZID));
                BLL.HAZIDService.AddHAZID(newHAZID);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加危害辨识");
            }
            else
            {
                newHAZID.HAZIDId = this.HAZIDId;
                BLL.HAZIDService.UpdateHAZID(newHAZID);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改危害辨识");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.HAZIDMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证危害辨识编号、名称是否存在
        /// <summary>
        /// 验证危害辨识编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_HAZID.FirstOrDefault(x => x.HAZIDName == this.txtHAZIDName.Text.Trim() && (x.HAZIDId != this.HAZIDId || (this.HAZIDId == null && x.HAZIDId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_HAZID.FirstOrDefault(x => x.HAZIDCode == this.txtHAZIDCode.Text.Trim() && (x.HAZIDId != this.HAZIDId || (this.HAZIDId == null && x.HAZIDId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
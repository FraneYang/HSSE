using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.Standard
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HSSEStandardEdit : PageBase
    {
        /// <summary>
        /// 专家辅助表ID
        /// </summary>
        public string HSSEStandardId
        {
            get
            {
                return (string)ViewState["HSSEStandardId"];
            }
            set
            {
                ViewState["HSSEStandardId"] = value;
            }
        }

        /// <summary>
        /// 管理项目ID
        /// </summary>
        public string ManagedItemId
        {
            get
            {
                return (string)ViewState["ManagedItemId"];
            }
            set
            {
                ViewState["ManagedItemId"] = value;
            }
        }

        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ManagedItemId = Request.Params["ManagedItemId"];
                this.HSSEStandardId = Request.Params["HSSEStandardId"];
                if (!string.IsNullOrEmpty(this.HSSEStandardId))
                {
                    var hsseStandard = BLL.HSSEStandardService.GetHSSEStandardById(this.HSSEStandardId);
                    if (hsseStandard != null)
                    {
                        this.ManagedItemId = hsseStandard.ManagedItemId;
                        this.txtHSSEStandardName.Text = hsseStandard.HSSEStandardName;
                        this.txtHSSEStandardCode.Text = hsseStandard.HSSEStandardCode;
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(hsseStandard.FileContent);
                    }
                }

                if (Request.Params["type"] == "0")
                {
                    this.btnSave.Hidden = true;
                }
            }
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData();
           
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData()
        {
            if (!string.IsNullOrEmpty(this.txtHSSEStandardName.Text))
            {
                if (BLL.HSSEStandardService.IsExistHSSEStandardName(this.ManagedItemId, this.HSSEStandardId, this.txtHSSEStandardName.Text.Trim()))
                {
                    Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                    return;
                }
                Model.Standard_HSSEStandard newHSSEStandard = new Model.Standard_HSSEStandard
                {
                    ManagedItemId = this.ManagedItemId,
                    HSSEStandardName = this.txtHSSEStandardName.Text.Trim(),
                    HSSEStandardCode = this.txtHSSEStandardCode.Text.Trim(),
                    FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text)
            };
                if (string.IsNullOrEmpty(this.HSSEStandardId))
                {
                    this.HSSEStandardId = SQLHelper.GetNewID(typeof(Model.Standard_ManagedItem));
                    newHSSEStandard.HSSEStandardId = this.HSSEStandardId;
                    BLL.HSSEStandardService.AddHSSEStandard(newHSSEStandard);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "增加专家辅助");
                }
                else
                {
                    newHSSEStandard.HSSEStandardId = this.HSSEStandardId;
                    BLL.HSSEStandardService.UpdateHSSEStandard(newHSSEStandard);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改专家辅助");
                }

                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("具体要求不能为空！");
                return;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.HSSEStandardMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}
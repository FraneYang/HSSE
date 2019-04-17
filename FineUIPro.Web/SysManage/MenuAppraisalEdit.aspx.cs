using System;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class MenuAppraisalEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId
        {
            get
            {
                return (string)ViewState["MenuId"];
            }
            set
            {
                ViewState["MenuId"] = value;
            }
        }

        /// <summary>
        /// 流程ID
        /// </summary>
        public string AppraisalId
        {
            get
            {
                return (string)ViewState["AppraisalId"];
            }
            set
            {
                ViewState["AppraisalId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.MenuId = Request.Params["MenuId"];
                this.AppraisalId = Request.Params["AppraisalId"];

                if (!string.IsNullOrEmpty(this.AppraisalId))
                {
                    var menuAppraisal = BLL.MenuAppraisalService.GetMenuAppraisalByAppraisalId(this.AppraisalId);
                    if (menuAppraisal != null)
                    {
                        this.MenuId = menuAppraisal.MenuId;
                        this.txtMenuOperation.Text = menuAppraisal.MenuOperation.ToString();
                        this.txtScore.Text = menuAppraisal.Score.ToString();
                        this.txtMenuOperationName.Text = menuAppraisal.MenuOperationName;
                        this.txtOutTime.Text = menuAppraisal.OutTime.ToString();
                    }
                }
                else
                {
                    this.txtMenuOperation.Text= Funs.GetMaxIndex("Sys_MenuAppraisal", "MenuOperation", "MenuId", this.MenuId).ToString();
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var menu = BLL.SysMenuService.GetSysMenuByMenuId(this.MenuId);
            if (menu != null)
            {
                Model.Sys_MenuAppraisal newMenuAppraisal = new Model.Sys_MenuAppraisal
                {
                    MenuId = this.MenuId,
                    Score = Funs.GetNewDecimalOrZero(this.txtScore.Text),
                    MenuOperation = Funs.GetNewIntOrZero(this.txtMenuOperation.Text),
                    OutTime = Funs.GetNewInt(this.txtOutTime.Text),
                    MenuOperationName = this.txtMenuOperationName.Text.Trim(),
                };

                if (string.IsNullOrEmpty(this.AppraisalId))
                {
                    BLL.MenuAppraisalService.AddMenuAppraisal(newMenuAppraisal);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "增加测评分值信息");
                }
                else
                {
                    newMenuAppraisal.AppraisalId = this.AppraisalId;
                    BLL.MenuAppraisalService.UpdateMenuAppraisal(newMenuAppraisal);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改测评分值信息");
                }

                ShowNotify("设置成功!", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
    }
}
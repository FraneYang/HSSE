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

                drpMenuOperation.DataValueField = "ConstValue";
                drpMenuOperation.DataTextField = "ConstText";
                var listCont = (from x in Funs.DB.Sys_MenuAppraisal where x.MenuId == this.MenuId && x.AppraisalId != this.AppraisalId select x.MenuOperation.ToString()).ToList();
                var list = (from x in Funs.DB.Sys_Const
                            where x.GroupId == BLL.ConstValue.Group_MenuOperation && !listCont.Contains(x.ConstValue.ToString())
                            orderby x.SortIndex
                            select x).ToList();
                drpMenuOperation.DataSource = list;
                drpMenuOperation.DataBind();

                if (!string.IsNullOrEmpty(this.AppraisalId))
                {
                    var menuAppraisal = BLL.MenuAppraisalService.GetMenuAppraisalByAppraisalId(this.AppraisalId);
                    if (menuAppraisal != null)
                    {
                        if (menuAppraisal.MenuOperation.HasValue)
                        {
                            this.drpMenuOperation.SelectedValue = menuAppraisal.MenuOperation.ToString();
                        }
                        this.txtScore.Text = menuAppraisal.Score.ToString();
                       
                    }
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
                Model.Sys_MenuAppraisal newMenuAppraisal = new Model.Sys_MenuAppraisal();
                newMenuAppraisal.MenuId = this.MenuId;
                newMenuAppraisal.Score = Funs.GetNewIntOrZero(this.txtScore.Text);
                if (!string.IsNullOrEmpty(this.drpMenuOperation.SelectedValue))
                {
                    newMenuAppraisal.MenuOperation =Funs.GetNewInt(this.drpMenuOperation.SelectedValue);
                }

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
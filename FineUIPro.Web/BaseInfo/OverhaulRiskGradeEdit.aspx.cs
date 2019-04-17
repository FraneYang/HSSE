using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class OverhaulRiskGradeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 作业风险对应值主键
        /// </summary>
        public string OverhaulRiskGradeId
        {
            get
            {
                return (string)ViewState["OverhaulRiskGradeId"];
            }
            set
            {
                ViewState["OverhaulRiskGradeId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 作业风险对应值编辑页面
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
                this.OverhaulRiskGradeId = Request.Params["OverhaulRiskGradeId"];
                if (!string.IsNullOrEmpty(this.OverhaulRiskGradeId))
                {
                    var OverhaulRiskGrade = BLL.OverhaulRiskGradeService.GetOverhaulRiskGradeById(this.OverhaulRiskGradeId);
                    if (OverhaulRiskGrade != null)
                    {
                        this.txtMinValue.Text = OverhaulRiskGrade.MinValue.ToString();
                        this.txtMaxValue.Text = OverhaulRiskGrade.MaxValue.ToString();
                        this.txtRemark.Text = OverhaulRiskGrade.Remark;
                        this.drpRiskGrade.SelectedValue = OverhaulRiskGrade.RiskGrade;
                        
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
            Model.Base_OverhaulRiskGrade newOverhaulRiskGrade = new Model.Base_OverhaulRiskGrade
            {
                RiskGrade = this.drpRiskGrade.SelectedValue,
                MinValue = Funs.GetNewInt(this.txtMinValue.Text),
                MaxValue = Funs.GetNewInt(this.txtMaxValue.Text),
                Remark = this.txtRemark.Text.Trim()
            };

            if (string.IsNullOrEmpty(this.OverhaulRiskGradeId))
            {
                newOverhaulRiskGrade.OverhaulRiskGradeId = SQLHelper.GetNewID(typeof(Model.Base_OverhaulRiskGrade));
                BLL.OverhaulRiskGradeService.AddOverhaulRiskGrade(newOverhaulRiskGrade);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加作业风险对应值");
            }
            else
            {
                newOverhaulRiskGrade.OverhaulRiskGradeId = this.OverhaulRiskGradeId;
                BLL.OverhaulRiskGradeService.UpdateOverhaulRiskGrade(newOverhaulRiskGrade);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改作业风险对应值");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.OverhaulRiskGradeMenuId);
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
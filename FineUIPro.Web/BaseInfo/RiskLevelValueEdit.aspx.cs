using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class RiskLevelValueEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 风险等级对应值主键
        /// </summary>
        public string RiskLevelValueId
        {
            get
            {
                return (string)ViewState["RiskLevelValueId"];
            }
            set
            {
                ViewState["RiskLevelValueId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 风险等级对应值编辑页面
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
                this.RiskLevelValueId = Request.Params["RiskLevelValueId"];
               
                BLL.ConstValue.InitConstValueDropDownList(this.drpRiskLevelId, ConstValue.Group_RiskLevel, false);                
                if (!string.IsNullOrEmpty(this.RiskLevelValueId))
                {
                    var riskLevelValue = BLL.RiskLevelValueService.GetRiskLevelValueById(this.RiskLevelValueId);
                    if (riskLevelValue != null)
                    {
                        this.txtMinValue.Text = riskLevelValue.MinValue.ToString();
                        this.txtMaxValue.Text = riskLevelValue.MaxValue.ToString();
                        this.txtRemark.Text = riskLevelValue.Remark;
                        this.drpRiskLevelId.SelectedValue = riskLevelValue.RiskLevelId;
                        this.txtControlMeasures.Text = riskLevelValue.ControlMeasures;
                        this.txtLimitTime.Text = riskLevelValue.LimitTime;
                        this.txtIdentification.Text = riskLevelValue.Identification;
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
            Model.Base_RiskLevelValue newRiskLevelValue = new Model.Base_RiskLevelValue
            {
                RiskLevelId = this.drpRiskLevelId.SelectedValue,
                Identification=this.txtIdentification.Text,
                MinValue = Funs.GetNewInt(this.txtMinValue.Text),
                MaxValue = Funs.GetNewInt(this.txtMaxValue.Text),
                Remark = this.txtRemark.Text.Trim(),
                ControlMeasures = this.txtControlMeasures.Text.Trim(),
                LimitTime = this.txtLimitTime.Text.Trim(),
            };

            if (string.IsNullOrEmpty(this.RiskLevelValueId))
            {
                newRiskLevelValue.RiskLevelValueId = SQLHelper.GetNewID(typeof(Model.Base_RiskLevelValue));
                BLL.RiskLevelValueService.AddRiskLevelValue(newRiskLevelValue);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加风险等级对应值");
            }
            else
            {
                newRiskLevelValue.RiskLevelValueId = this.RiskLevelValueId;
                BLL.RiskLevelValueService.UpdateRiskLevelValue(newRiskLevelValue);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改风险等级对应值");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.RiskLevelValueMenuId);
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
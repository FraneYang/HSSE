using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SCLInfoEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// SCL评价主键
        /// </summary>
        public string SCLId
        {
            get
            {
                return (string)ViewState["SCLId"];
            }
            set
            {
                ViewState["SCLId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// SCL评价编辑页面
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
                this.SCLId = Request.Params["SCLId"];
               
                BLL.ConstValue.InitConstValueDropDownList(this.drpRiskLevelId, ConstValue.Group_RiskLevel, true);
                BLL.EuipmentTypeService.InitEuipmentTypeDropDownList(this.drpEuipmentTypeId, true);
                if (!string.IsNullOrEmpty(this.SCLId))
                {
                    var SCL = BLL.SCLInfoService.GetSCLInfoById(this.SCLId);
                    if (SCL != null)
                    {                        
                        this.txtSortIndex.Text = SCL.SortIndex.ToString();
                        this.txtCheckItem.Text = SCL.CheckItem;
                        this.txtStandard.Text = SCL.Standard;
                        this.txtConsequence.Text = SCL.Consequence;
                        this.txtNowControlMeasures.Text = SCL.NowControlMeasures;
                        this.drpHazardJudge_L.SelectedValue = SCL.HazardJudge_L.ToString();
                        this.drpHazardJudge_S.SelectedValue = SCL.HazardJudge_S.ToString();
                        this.txtHazardJudge_R.Text = SCL.HazardJudge_R.ToString();
                        this.txtControlMeasures.Text = SCL.ControlMeasures;
                        if (!String.IsNullOrEmpty( SCL.RiskLevel))
                        {
                            this.drpRiskLevelId.SelectedValue = SCL.RiskLevel;
                        }
                        if (!string.IsNullOrEmpty(SCL.EuipmentTypeId))
                        {
                            this.drpEuipmentTypeId.SelectedValue = SCL.EuipmentTypeId;
                        }
                    }
                }
                else
                {
                    this.txtSortIndex.Text = Funs.GetMaxIndex("Base_SCL", "SortIndex", "EuipmentTypeId", this.drpEuipmentTypeId.SelectedValue).ToString();
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
            Model.Base_SCL newSCL = new Model.Base_SCL
            {
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text),
                CheckItem = this.txtCheckItem.Text.Trim(),
                Standard = this.txtStandard.Text.Trim(),
                Consequence = this.txtConsequence.Text.Trim(),
                NowControlMeasures = this.txtNowControlMeasures.Text.Trim(),
                ControlMeasures = this.txtControlMeasures.Text.Trim(),               
               
                HazardJudge_R = Funs.GetNewDecimal(this.txtHazardJudge_R.Text)
            };
            if (this.drpHazardJudge_L.SelectedValue != BLL.Const._Null)
            {
                newSCL.HazardJudge_L = Funs.GetNewDecimal(this.drpHazardJudge_L.SelectedValue);                
            }
            if (this.drpHazardJudge_S.SelectedValue != BLL.Const._Null)
            {
                newSCL.HazardJudge_S = Funs.GetNewDecimal(this.drpHazardJudge_S.SelectedValue);
            }
            if (this.drpRiskLevelId.SelectedValue != BLL.Const._Null)
            {
                newSCL.RiskLevel = this.drpRiskLevelId.SelectedValue;
            }
            if (this.drpEuipmentTypeId.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpEuipmentTypeId.SelectedValue))
            {
                newSCL.EuipmentTypeId = this.drpEuipmentTypeId.SelectedValue;
            }
            if (string.IsNullOrEmpty(this.SCLId))
            {
                newSCL.SCLId = SQLHelper.GetNewID(typeof(Model.Base_SCL));
                BLL.SCLInfoService.AddSCLInfo(newSCL);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加SCL评价基础表信息");
            }
            else
            {
                newSCL.SCLId = this.SCLId;
                BLL.SCLInfoService.UpdateSCLInfo(newSCL);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改SCL评价基础表信息");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.SCLInfoMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion        

        /// <summary>
        /// 风险评估值变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtHazardJudge_TextChanged(object sender, EventArgs e)
        {
            var mathValue = Funs.GetNewDecimalOrZero(this.drpHazardJudge_L.SelectedValue) * Funs.GetNewDecimalOrZero(this.drpHazardJudge_S.SelectedValue);
            this.txtHazardJudge_R.Text = mathValue.ToString();
            var riskLevelValue = Funs.DB.Base_RiskLevelValue.FirstOrDefault(x => x.Identification == "SCL" && x.MinValue <= mathValue && x.MaxValue >= mathValue);
            if (riskLevelValue != null)
            {
                this.drpRiskLevelId.SelectedValue = riskLevelValue.RiskLevelId;
            }
            else
            {
                this.drpRiskLevelId.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpEuipmentTypeId_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.SCLId))
            {
                this.txtSortIndex.Text = Funs.GetMaxIndex("Base_SCL", "SortIndex", "EuipmentTypeId", this.drpEuipmentTypeId.SelectedValue).ToString();
            }
        }
    }
}
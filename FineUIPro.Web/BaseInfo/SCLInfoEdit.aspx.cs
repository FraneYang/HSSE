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
               
                BLL.ConstValue.InitConstValueDropDownList(this.drpRiskLevel, ConstValue.Group_RiskLevel, true);            
                
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
                        this.txtHazardJudge_L.Text = SCL.HazardJudge_L.ToString();
                        this.txtHazardJudge_S.Text = SCL.HazardJudge_S.ToString();
                        this.txtHazardJudge_R.Text = SCL.HazardJudge_R.ToString();
                        this.txtControlMeasures.Text = SCL.ControlMeasures;
                        if (!String.IsNullOrEmpty( SCL.RiskLevel))
                        {
                            this.drpRiskLevel.SelectedValue = SCL.RiskLevel;
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
            Model.Base_SCL newSCL = new Model.Base_SCL();
            newSCL.SortIndex =Funs.GetNewInt( this.txtSortIndex.Text);
            newSCL.CheckItem = this.txtCheckItem.Text.Trim();
            newSCL.Standard = this.txtStandard.Text.Trim();
            newSCL.Consequence = this.txtConsequence.Text.Trim();
            newSCL.NowControlMeasures = this.txtNowControlMeasures.Text.Trim();
            newSCL.ControlMeasures = this.txtControlMeasures.Text.Trim();
            newSCL.HazardJudge_L =Funs.GetNewDecimal(this.txtHazardJudge_L.Text);
            newSCL.HazardJudge_S = Funs.GetNewDecimal(this.txtHazardJudge_S.Text);
            newSCL.HazardJudge_R = Funs.GetNewDecimal(this.txtHazardJudge_R.Text);
            if (this.drpRiskLevel.SelectedValue != BLL.Const._Null)
            {
                newSCL.RiskLevel = this.drpRiskLevel.SelectedValue;
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
    }
}
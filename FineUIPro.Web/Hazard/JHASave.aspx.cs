using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class JHASave : PageBase
    {
        #region 定义项
        /// <summary>
        /// JobActivityId主键
        /// </summary>
        public string JobActivityId
        {
            get
            {
                return (string)ViewState["JobActivityId"];
            }
            set
            {
                ViewState["JobActivityId"] = value;
            }
        }
        /// <summary>
        /// JHA明细主键
        /// </summary>
        public string JHAItemId
        {
            get
            {
                return (string)ViewState["JHAItemId"];
            }
            set
            {
                ViewState["JHAItemId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 装置/科室编辑页面
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
                this.JHAItemId = Request.Params["JHAItemId"];
                this.JobActivityId = Request.Params["JobActivityId"];
                BLL.ConstValue.InitConstValueDropDownList(this.drpRiskLevelId, ConstValue.Group_RiskLevel, true);
                
                if (!string.IsNullOrEmpty(this.JHAItemId))
                {
                    var jhaItem = BLL.JHAItemService.GetJHAItemById(this.JHAItemId);
                    if (jhaItem != null)
                    {
                        this.JobActivityId = jhaItem.JobActivityId;
                        this.txtSortIndex.Text = jhaItem.SortIndex.ToString();
                        this.txtJobStep.Text = jhaItem.JobStep;
                        this.txtPossibleAccidents.Text = jhaItem.PossibleAccidents;
                        this.txtNowControlMeasures.Text = jhaItem.NowControlMeasures;
                       
                        this.tbxHazardJudge_L.Text = jhaItem.HazardJudge_L.ToString();
                        if (jhaItem.HazardJudge_L1.HasValue)
                        {
                            this.drpHazardJudge_L1.SelectedValue = jhaItem.HazardJudge_L1.ToString();
                        }
                        if (jhaItem.HazardJudge_L2.HasValue)
                        {
                            this.drpHazardJudge_L2.SelectedValue = jhaItem.HazardJudge_L2.ToString();
                        }
                        if (jhaItem.HazardJudge_L3.HasValue)
                        {
                            this.drpHazardJudge_L3.SelectedValue = jhaItem.HazardJudge_L3.ToString();
                        }
                        if (jhaItem.HazardJudge_L4.HasValue)
                        {
                            this.drpHazardJudge_L4.SelectedValue = jhaItem.HazardJudge_L4.ToString();
                        }                        
                        this.tbxHazardJudge_S.Text = jhaItem.HazardJudge_S.ToString();
                        if (jhaItem.HazardJudge_S1.HasValue)
                        {
                            this.drpHazardJudge_S1.SelectedValue = jhaItem.HazardJudge_S1.ToString();
                        }
                        if (jhaItem.HazardJudge_S2.HasValue)
                        {
                            this.drpHazardJudge_S2.SelectedValue = jhaItem.HazardJudge_S2.ToString();
                        }
                        if (jhaItem.HazardJudge_S3.HasValue)
                        {
                            this.drpHazardJudge_S3.SelectedValue = jhaItem.HazardJudge_S3.ToString();
                        }
                        if (jhaItem.HazardJudge_S4.HasValue)
                        {
                            this.drpHazardJudge_S4.SelectedValue = jhaItem.HazardJudge_S4.ToString();
                        }
                        if (jhaItem.HazardJudge_S5.HasValue)
                        {
                            this.drpHazardJudge_S5.SelectedValue = jhaItem.HazardJudge_S5.ToString();
                        }
                        this.txtHazardJudge_R.Text = jhaItem.HazardJudge_R.ToString();
                        this.txtControlMeasures.Text = jhaItem.ControlMeasures;
                        this.txtManagementMeasures.Text = jhaItem.ManagementMeasures;
                        this.txtProtectiveMeasures.Text = jhaItem.ProtectiveMeasures;
                        this.txtOtherMeasures.Text = jhaItem.OtherMeasures;

                        if (!string.IsNullOrEmpty(jhaItem.RiskLevel))
                        {
                            this.drpRiskLevelId.SelectedValue = jhaItem.RiskLevel;
                        }                      
                    }
                    this.BindGrid();
                }
                else
                {
                    this.txtSortIndex.Text = Funs.GetMaxIndex("Hazard_JHAItem", "SortIndex", "JobActivityId", this.JobActivityId).ToString();
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
            Model.Hazard_JHAItem newJHAItem = new Model.Hazard_JHAItem
            {
                JobActivityId = this.JobActivityId,
                JobStep = this.txtJobStep.Text.Trim(),
                PossibleAccidents = this.txtPossibleAccidents.Text.Trim(),
                NowControlMeasures = this.txtNowControlMeasures.Text.Trim(),
                HazardJudge_L = Funs.GetNewDecimal(this.tbxHazardJudge_L.Text),
                HazardJudge_L1 = Funs.GetNewDecimal(this.drpHazardJudge_L1.SelectedValue),
                HazardJudge_L2 = Funs.GetNewDecimal(this.drpHazardJudge_L2.SelectedValue),
                HazardJudge_L3 = Funs.GetNewDecimal(this.drpHazardJudge_L3.SelectedValue),
                HazardJudge_L4 = Funs.GetNewDecimal(this.drpHazardJudge_L4.SelectedValue),
                HazardJudge_S = Funs.GetNewDecimal(this.tbxHazardJudge_S.Text),
                HazardJudge_S1 = Funs.GetNewDecimal(this.drpHazardJudge_S1.SelectedValue),
                HazardJudge_S2 = Funs.GetNewDecimal(this.drpHazardJudge_S2.SelectedValue),
                HazardJudge_S3 = Funs.GetNewDecimal(this.drpHazardJudge_S3.SelectedValue),
                HazardJudge_S4 = Funs.GetNewDecimal(this.drpHazardJudge_S4.SelectedValue),
                HazardJudge_S5 = Funs.GetNewDecimal(this.drpHazardJudge_S5.SelectedValue),
                HazardJudge_R = Funs.GetNewDecimal(this.txtHazardJudge_R.Text),
                ControlMeasures = this.txtControlMeasures.Text.Trim(),
                ManagementMeasures = this.txtManagementMeasures.Text.Trim(),
                ProtectiveMeasures = this.txtProtectiveMeasures.Text.Trim(),
                OtherMeasures = this.txtOtherMeasures.Text.Trim(),
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text.Trim()),                
            };
            if (this.drpRiskLevelId.SelectedValue != Const._Null)
            {
                newJHAItem.RiskLevel = this.drpRiskLevelId.SelectedValue;
            }

            if (string.IsNullOrEmpty(this.JHAItemId))
            {
                newJHAItem.JHAItemId = SQLHelper.GetNewID(typeof(Model.Hazard_JHAItem));
                BLL.JHAItemService.AddJHAItem(newJHAItem);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加JHA评价明细");
            }
            else
            {
                newJHAItem.JHAItemId = this.JHAItemId;
                BLL.JHAItemService.UpdateJHAItem(newJHAItem);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改JHA评价明细");
            }

            Model.Hazard_JHAItemRecord newJHAItemRecord = new Model.Hazard_JHAItemRecord
            {
                JHAItemRecordId = BLL.SQLHelper.GetNewID(typeof(Model.Hazard_JHAItemRecord)),
                JHAItemId = newJHAItem.JHAItemId,
                EvaluationTime = System.DateTime.Now,
                EvaluatorId = this.CurrUser.UserId,
                RiskLevel = newJHAItem.RiskLevel,
            };

            BLL.JHAItemRecordService.AddJHAItemRecord(newJHAItemRecord);
            ///插入综合测评
            BLL.AppraisalScoreService.GetAppraisalScore(this.CurrUser.UserId, BLL.Const.JHAMenuId, 1, this.JHAItemId);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.JHAMenuId);
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
            this.SetRValues();
        }

        private void SetRValues()
        {
            var mathValue = Funs.GetNewDecimalOrZero(this.tbxHazardJudge_L.Text) * Funs.GetNewDecimalOrZero(this.tbxHazardJudge_S.Text);
            this.txtHazardJudge_R.Text = mathValue.ToString();
            var riskLevelValue = Funs.DB.Base_RiskLevelValue.FirstOrDefault(x => x.Identification == "JHA" && x.MinValue <= mathValue && x.MaxValue >= mathValue);
            if (riskLevelValue != null)
            {
                this.drpRiskLevelId.SelectedValue = riskLevelValue.RiskLevelId;
            }
            else
            {
                this.drpRiskLevelId.SelectedValue = "5";
            }
        }

        #region 风险评价记录
        /// <summary>
        /// 流程列表绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Record.JHAItemRecordId,Record.JHAItemId,Record.EvaluatorId,Record.EvaluationTime,Record.RiskLevel,sysUser.UserName AS EvaluatorName,sysConst.ConstText AS RiskLevelName"
                            + @" FROM Hazard_JHAItemRecord AS Record"
                            + @" LEFT JOIN Sys_User AS sysUser ON Record.EvaluatorId=sysUser.UserId"
                            + @" LEFT JOIN Sys_Const AS sysConst ON Record.RiskLevel=sysConst.ConstValue AND sysConst.GroupId='" + BLL.ConstValue.Group_RiskLevel + "'"
                            + @" WHERE JHAItemId=@JHAItemId";
            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@JHAItemId", this.JHAItemId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            //
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region S值
        // 点击 TriggerBox 弹出窗口
        protected void tbxHazardJudge_S_TriggerClick(object sender, EventArgs e)
        {
            WindowS.Hidden = false;
        }

        // 关闭弹出窗口
        protected void btnCloseWindowS_Click(object sender, EventArgs e)
        {
            WindowS.Hidden = true;
            this.tbxHazardJudge_S.Text = this.drpHazardJudge_S1.SelectedValue;
            if (Funs.GetNewDecimalOrZero(this.drpHazardJudge_S1.SelectedValue) < Funs.GetNewDecimalOrZero(this.drpHazardJudge_S2.SelectedValue))
            {
                this.tbxHazardJudge_S.Text = this.drpHazardJudge_S2.SelectedValue;
            }
            if (Funs.GetNewDecimalOrZero(this.drpHazardJudge_S2.SelectedValue) < Funs.GetNewDecimalOrZero(this.drpHazardJudge_S3.SelectedValue))
            {
                this.tbxHazardJudge_S.Text = this.drpHazardJudge_S3.SelectedValue;
            }
            if (Funs.GetNewDecimalOrZero(this.drpHazardJudge_S3.SelectedValue) < Funs.GetNewDecimalOrZero(this.drpHazardJudge_S4.SelectedValue))
            {
                this.tbxHazardJudge_S.Text = this.drpHazardJudge_S4.SelectedValue;
            }
            if (Funs.GetNewDecimalOrZero(this.drpHazardJudge_S4.SelectedValue) < Funs.GetNewDecimalOrZero(this.drpHazardJudge_S5.SelectedValue))
            {
                this.tbxHazardJudge_S.Text = this.drpHazardJudge_S5.SelectedValue;
            }
            
            this.SetRValues();
        }
        #endregion

        #region L值
        // 点击 TriggerBox 弹出窗口
        protected void tbxHazardJudge_L_TriggerClick(object sender, EventArgs e)
        {
            WindowL.Hidden = false;
        }

        // 关闭弹出窗口
        protected void btnCloseWindowL_Click(object sender, EventArgs e)
        {
            WindowL.Hidden = true;
            this.tbxHazardJudge_L.Text = this.drpHazardJudge_L1.SelectedValue;
            if (Funs.GetNewDecimalOrZero(this.drpHazardJudge_L1.SelectedValue) < Funs.GetNewDecimalOrZero(this.drpHazardJudge_L2.SelectedValue))
            {
                this.tbxHazardJudge_L.Text = this.drpHazardJudge_L2.SelectedValue;
            }
            if (Funs.GetNewDecimalOrZero(this.drpHazardJudge_L2.SelectedValue) < Funs.GetNewDecimalOrZero(this.drpHazardJudge_L3.SelectedValue))
            {
                this.tbxHazardJudge_L.Text = this.drpHazardJudge_L3.SelectedValue;
            }
            if (Funs.GetNewDecimalOrZero(this.drpHazardJudge_L3.SelectedValue) < Funs.GetNewDecimalOrZero(this.drpHazardJudge_L4.SelectedValue))
            {
                this.tbxHazardJudge_L.Text = this.drpHazardJudge_L4.SelectedValue;
            }

            this.SetRValues();
        }
        #endregion
    }
}
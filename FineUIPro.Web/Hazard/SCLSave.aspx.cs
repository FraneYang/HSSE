using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class SCLSave : PageBase
    {
        #region 定义项
        /// <summary>
        /// EuipmentId主键
        /// </summary>
        public string EuipmentId
        {
            get
            {
                return (string)ViewState["EuipmentId"];
            }
            set
            {
                ViewState["EuipmentId"] = value;
            }
        }
        /// <summary>
        /// SCL明细主键
        /// </summary>
        public string SCLItemId
        {
            get
            {
                return (string)ViewState["SCLItemId"];
            }
            set
            {
                ViewState["SCLItemId"] = value;
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
                this.SCLItemId = Request.Params["SCLItemId"];
                this.EuipmentId = Request.Params["EuipmentId"];
                BLL.ConstValue.InitConstValueDropDownList(this.drpRiskLevelId, ConstValue.Group_RiskLevel, true);
                
                if (!string.IsNullOrEmpty(this.SCLItemId))
                {
                    var sclItem = BLL.SCLItemService.GetSCLItemById(this.SCLItemId);
                    if (sclItem != null)
                    {
                        this.EuipmentId = sclItem.EuipmentId;
                        this.txtSortIndex.Text = sclItem.SortIndex.ToString();
                        this.txtCheckItem.Text = sclItem.CheckItem;
                        this.txtStandard.Text = sclItem.Standard;
                        this.txtConsequence.Text = sclItem.Consequence;
                        this.txtNowControlMeasures.Text = sclItem.NowControlMeasures;                       
                        this.drpHazardJudge_L.SelectedValue = sclItem.HazardJudge_L.ToString();
                        this.tbxHazardJudge_S.Text = sclItem.HazardJudge_S.ToString();
                        if(sclItem.HazardJudge_S1.HasValue)
                        {
                            this.drpHazardJudge_S1.SelectedValue = sclItem.HazardJudge_S1.ToString();
                        }
                        if (sclItem.HazardJudge_S2.HasValue)
                        {
                            this.drpHazardJudge_S2.SelectedValue = sclItem.HazardJudge_S2.ToString();
                        }
                        if (sclItem.HazardJudge_S3.HasValue)
                        {
                            this.drpHazardJudge_S3.SelectedValue = sclItem.HazardJudge_S3.ToString();
                        }
                        if (sclItem.HazardJudge_S4.HasValue)
                        {
                            this.drpHazardJudge_S4.SelectedValue = sclItem.HazardJudge_S4.ToString();
                        }
                        if (sclItem.HazardJudge_S5.HasValue)
                        {
                            this.drpHazardJudge_S5.SelectedValue = sclItem.HazardJudge_S5.ToString();
                        }

                        this.txtHazardJudge_R.Text = sclItem.HazardJudge_R.ToString();
                        this.txtControlMeasures.Text = sclItem.ControlMeasures;
                        this.txtManagementMeasures.Text = sclItem.ManagementMeasures;
                        this.txtProtectiveMeasures.Text = sclItem.ProtectiveMeasures;
                        this.txtOtherMeasures.Text = sclItem.OtherMeasures;
                        if (!string.IsNullOrEmpty(sclItem.RiskLevel))
                        {
                            this.drpRiskLevelId.SelectedValue = sclItem.RiskLevel;
                        }
                        var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(this.EuipmentId);
                        if (euipment != null)
                        {
                            this.lbEuiqment.Text = "设备设施名称：" + euipment.EuipmentName;
                            var type = BLL.EuipmentTypeService.GetEuipmentTypeByEuipmentTypeId(euipment.EuipmentTypeId);
                            if (type != null)
                            {
                                this.lbEuiqment.Text += "；设备类型：" + type.EuipmentTypeName;
                            }
                            var installation = BLL.InstallationService.GetInstallationByInstallationId(euipment.InstallationId);
                            if (installation != null)
                            {
                                this.lbEuiqment.Text += "；所属单位：" + installation.InstallationName;
                            }
                            var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(euipment.WorkAreaId);
                            if (workArea != null)
                            {
                                this.lbEuiqment.Text += "；所在单元：" + workArea.WorkAreaName;
                            }
                        }
                    }

                    this.BindGrid();
                }
                else
                {
                    this.txtSortIndex.Text = Funs.GetMaxIndex("Hazard_SCLItem", "SortIndex", "EuipmentId", this.EuipmentId).ToString();
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
            Model.Hazard_SCLItem newSCLItem = new Model.Hazard_SCLItem
            {
                EuipmentId = this.EuipmentId,
                CheckItem = this.txtCheckItem.Text.Trim(),
                Standard = this.txtStandard.Text.Trim(),
                Consequence = this.txtConsequence.Text.Trim(),
                NowControlMeasures = this.txtNowControlMeasures.Text.Trim(),
                HazardJudge_L = Funs.GetNewDecimal(this.drpHazardJudge_L.SelectedValue),
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
                newSCLItem.RiskLevel = this.drpRiskLevelId.SelectedValue;
            }

            if (string.IsNullOrEmpty(this.SCLItemId))
            {
                newSCLItem.SCLItemId = SQLHelper.GetNewID(typeof(Model.Hazard_SCLItem));
                BLL.SCLItemService.AddSCLItem(newSCLItem);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加SCL评价明细");
            }
            else
            {
                newSCLItem.SCLItemId = this.SCLItemId;
                BLL.SCLItemService.UpdateSCLItem(newSCLItem);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改SCL评价明细");
            }

            Model.Hazard_SCLItemRecord newSCLItemRecord = new Model.Hazard_SCLItemRecord
            {
                SCLItemRecordId = BLL.SQLHelper.GetNewID(typeof(Model.Hazard_SCLItemRecord)),
                SCLItemId = newSCLItem.SCLItemId,
                EvaluationTime = System.DateTime.Now,
                EvaluatorId = this.CurrUser.UserId,
                RiskLevel = newSCLItem.RiskLevel,
            };

            BLL.SCLItemRecordService.AddSCLItemRecord(newSCLItemRecord);
            ///插入综合测评
            BLL.AppraisalScoreService.GetAppraisalScore(this.CurrUser.UserId, BLL.Const.SCLMenuId, 1, this.SCLItemId);
            /////评价提交时，更新设备设施、作业环境的风险等级。同时写入风险信息库。
            BLL.SCLItemService.SetSubmitInfo(newSCLItem, this.CurrUser.UserId);

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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.SCLMenuId);
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
            var mathValue = Funs.GetNewDecimalOrZero(this.drpHazardJudge_L.SelectedValue) * Funs.GetNewDecimalOrZero(this.tbxHazardJudge_S.Text);
            this.txtHazardJudge_R.Text = mathValue.ToString();
            var riskLevelValue = Funs.DB.Base_RiskLevelValue.FirstOrDefault(x => x.Identification == "SCL" && x.MinValue <= mathValue && x.MaxValue >= mathValue);
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
            string strSql = @"SELECT Record.SCLItemRecordId,Record.SCLItemId,Record.EvaluatorId,Record.EvaluationTime,Record.RiskLevel,sysUser.UserName AS EvaluatorName,sysConst.ConstText AS RiskLevelName"
                            + @" FROM Hazard_SCLItemRecord AS Record"
                            + @" LEFT JOIN Sys_User AS sysUser ON Record.EvaluatorId=sysUser.UserId"
                            + @" LEFT JOIN Sys_Const AS sysConst ON Record.RiskLevel=sysConst.ConstValue AND sysConst.GroupId='" + BLL.ConstValue.Group_RiskLevel + "'"
                            + @" WHERE SCLItemId=@SCLItemId";
            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@SCLItemId", this.SCLItemId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

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
    }
}
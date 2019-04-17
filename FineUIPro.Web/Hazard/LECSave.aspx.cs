using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class LECSave : PageBase
    {
        #region 定义项
        /// <summary>
        /// LEC主键
        /// </summary>
        public string DataId
        {
            get
            {
                return (string)ViewState["DataId"];
            }
            set
            {
                ViewState["DataId"] = value;
            }
        }
        /// <summary>
        /// 评价对象类型
        /// </summary>
        public string DataType
        {
            get
            {
                return (string)ViewState["DataType"];
            }
            set
            {
                ViewState["DataType"] = value;
            }
        }
        /// <summary>
        /// LEC明细主键
        /// </summary>
        public string LECItemId
        {
            get
            {
                return (string)ViewState["LECItemId"];
            }
            set
            {
                ViewState["LECItemId"] = value;
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
                this.LECItemId = Request.Params["LECItemId"];
                this.DataId = Request.Params["DataId"];
                this.DataType = Request.Params["DataType"];
                BLL.ConstValue.InitConstValueDropDownList(this.drpRiskLevelId, ConstValue.Group_RiskLevel, true);
                if (!string.IsNullOrEmpty(this.LECItemId))
                {
                    var lecItem = BLL.LECItemService.GetLECItemById(this.LECItemId);
                    if (lecItem != null)
                    {
                        this.DataId = lecItem.DataId;
                        this.DataType = lecItem.DataType;
                        this.txtSortIndex.Text = lecItem.SortIndex.ToString();
                        this.txtHazardDescription.Text = lecItem.HazardDescription;
                        this.txtPossibleAccidents.Text = lecItem.PossibleAccidents;
                        this.txtControlMeasures.Text = lecItem.ControlMeasures;
                        this.txtManagementMeasures.Text = lecItem.ManagementMeasures;
                        this.txtProtectiveMeasures.Text = lecItem.ProtectiveMeasures;
                        this.txtOtherMeasures.Text = lecItem.OtherMeasures;
                        this.drpHazardJudge_L.SelectedValue = lecItem.HazardJudge_L.ToString();
                        this.drpHazardJudge_E.SelectedValue = lecItem.HazardJudge_E.ToString();
                        this.drpHazardJudge_C.SelectedValue = lecItem.HazardJudge_C.ToString();
                        this.txtHazardJudge_D.Text = lecItem.HazardJudge_D.ToString();
                        if (!string.IsNullOrEmpty(lecItem.RiskLevel))
                        {
                            this.drpRiskLevelId.SelectedValue = lecItem.RiskLevel;
                        }

                        if (this.DataType == "0")
                        {
                            var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(this.DataId);
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
                        else
                        {                            
                            var jobEnvironment = BLL.JobEnvironmentService.GetJobEnvironmentByJobEnvironmentId(this.DataId);
                            if (jobEnvironment != null)
                            {
                                this.lbEuiqment.Text = "作业环境：" + jobEnvironment.JobEnvironmentName;
                                var installation = BLL.InstallationService.GetInstallationByInstallationId(jobEnvironment.InstallationId);
                                if (installation != null)
                                {
                                    this.lbEuiqment.Text += "；所属单位：" + installation.InstallationName;
                                }
                                var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(jobEnvironment.WorkAreaId);
                                if (workArea != null)
                                {
                                    this.lbEuiqment.Text += "；所在单元：" + workArea.WorkAreaName;
                                }
                            }
                        }
                    }

                    this.BindGrid();
                }
                else
                {
                    this.txtSortIndex.Text = Funs.GetMaxIndex("Hazard_LECItem", "SortIndex", "DataId", this.DataId).ToString();
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
            Model.Hazard_LECItem newLECItem = new Model.Hazard_LECItem
            {
                DataId = this.DataId,
                DataType=this.DataType,
                HazardDescription = this.txtHazardDescription.Text.Trim(),
                PossibleAccidents = this.txtPossibleAccidents.Text.Trim(),
                HazardJudge_L = Funs.GetNewDecimal(this.drpHazardJudge_L.SelectedValue),
                HazardJudge_E = Funs.GetNewDecimal(this.drpHazardJudge_E.SelectedValue),
                HazardJudge_C = Funs.GetNewDecimal(this.drpHazardJudge_C.SelectedValue),
                HazardJudge_D = Funs.GetNewDecimal(this.txtHazardJudge_D.Text),
                ControlMeasures = this.txtControlMeasures.Text.Trim(),
                ManagementMeasures = this.txtManagementMeasures.Text.Trim(),
                ProtectiveMeasures = this.txtProtectiveMeasures.Text.Trim(),
                OtherMeasures = this.txtOtherMeasures.Text.Trim(),
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text.Trim()),
            };
            if (this.drpRiskLevelId.SelectedValue != Const._Null)
            {
                newLECItem.RiskLevel = this.drpRiskLevelId.SelectedValue;
            }
         
            if (string.IsNullOrEmpty(this.LECItemId))
            {
                newLECItem.LECItemId = SQLHelper.GetNewID(typeof(Model.Hazard_LECItem));
                BLL.LECItemService.AddLECItem(newLECItem);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加LEC评价明细");
            }
            else
            {
                newLECItem.LECItemId = this.LECItemId;
                BLL.LECItemService.UpdateLECItem(newLECItem);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改LEC评价明细");
            }

            Model.Hazard_LECItemRecord newLECItemRecord = new Model.Hazard_LECItemRecord
            {
                LECItemRecordId = BLL.SQLHelper.GetNewID(typeof(Model.Hazard_LECItemRecord)),
                LECItemId= newLECItem.LECItemId,
                EvaluationTime=System.DateTime.Now,
                EvaluatorId=this.CurrUser.UserId,
                RiskLevel= newLECItem.RiskLevel,
            };
            ///插入评价记录表
            BLL.LECItemRecordService.AddLECItemRecord(newLECItemRecord);
            ///插入综合测评
            BLL.AppraisalScoreService.GetAppraisalScore(this.CurrUser.UserId, BLL.Const.LECMenuId, 1, this.LECItemId);
            /////评价提交时，更新设备设施、作业环境的风险等级。同时写入风险信息库。
            BLL.LECItemService.SetSubmitInfo(newLECItem, this.CurrUser.UserId);
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.LECMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 风险评估值变化事件
        /// <summary>
        /// 风险评估值变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtHazardJudge_TextChanged(object sender, EventArgs e)
        {
            var mathValue =Funs.GetNewDecimalOrZero(this.drpHazardJudge_L.SelectedValue) * Funs.GetNewDecimalOrZero(this.drpHazardJudge_E.SelectedValue) * Funs.GetNewDecimalOrZero(this.drpHazardJudge_C.SelectedValue);
            this.txtHazardJudge_D.Text = mathValue.ToString();
            
            var riskLevelValue = Funs.DB.Base_RiskLevelValue.FirstOrDefault(x => x.Identification == "LEC" && x.MinValue <= mathValue && x.MaxValue >= mathValue);
            if (riskLevelValue != null)
            {
                this.drpRiskLevelId.SelectedValue = riskLevelValue.RiskLevelId;                
            }
            else
            {
                this.drpRiskLevelId.SelectedValue = "5";
            }
        }
        #endregion

        #region 风险评价记录
        /// <summary>
        /// 流程列表绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Record.LECItemRecordId,Record.LECItemId,Record.EvaluatorId,Record.EvaluationTime,Record.RiskLevel,sysUser.UserName AS EvaluatorName,sysConst.ConstText AS RiskLevelName"                            
                            + @" FROM Hazard_LECItemRecord AS Record"
                            + @" LEFT JOIN Sys_User AS sysUser ON Record.EvaluatorId=sysUser.UserId"
                            + @" LEFT JOIN Sys_Const AS sysConst ON Record.RiskLevel=sysConst.ConstValue AND sysConst.GroupId='" + BLL.ConstValue.Group_RiskLevel + "'"
                            + @" WHERE LECItemId=@LECItemId";
            List<SqlParameter> listStr = new List<SqlParameter>();
         
            listStr.Add(new SqlParameter("@LECItemId", this.LECItemId));
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
    }
}
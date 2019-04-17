using System;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class HeightWorkView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HeightWorkId
        {
            get
            {
                return (string)ViewState["HeightWorkId"];
            }
            set
            {
                ViewState["HeightWorkId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SimpleForm1.Title = "高处安全作业证";
                var thisUnits = BLL.CommonService.GetIsThisUnit();
                if (thisUnits != null)
                {
                    this.SimpleForm1.Title = thisUnits.UnitName + this.Title;
                }
                this.HeightWorkId = Request.Params["HeightWorkId"];
                if (!string.IsNullOrEmpty(this.HeightWorkId))
                {
                    Model.View_License_HeightWork heightWork = BLL.HeightWorkService.GetViewHeightWorkById(this.HeightWorkId);
                    if (heightWork != null)
                    {
                        this.txtApplyUnit.Text = heightWork.ApplyUnit;
                        this.txtApplyManName.Text = heightWork.ApplyManName;
                        this.txtLicenseCode.Text = heightWork.LicenseCode;
                        this.txtStartDate.Text = heightWork.JobTime;
                        this.txtJobPalce.Text = heightWork.JobPalce;
                        this.txtJobContent.Text = heightWork.JobContent;
                        this.txtWorkingHeight.Text = heightWork.WorkingHeight;
                        this.cbJobLevel.SelectedValueArray = new string[] { heightWork.JobLevelId };
                        this.txtJobUnit.Text = heightWork.JobUnit;
                        this.txtGuardian.Text = heightWork.Guardian;
                        this.txtJobMan.Text = heightWork.JobMan;
                        this.txtOtherSpecial.Text = heightWork.OtherSpecial;
                        this.txtHAZIDName.Text = heightWork.HAZIDName;
                        if (heightWork.SIsUsed1.HasValue)
                        {
                            this.txtSIsUsed1.Checked = heightWork.SIsUsed1.Value;
                        }
                        this.txtSafetyMeasures1.Text = heightWork.SafetyMeasures1;
                        string measuresManName = string.Empty;
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 1);
                        if (getPushRecord2 != null)
                        {
                            measuresManName = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                        }
                        this.txtMeasuresManName1.Text = measuresManName;
                        if (heightWork.SIsUsed2.HasValue)
                        {
                            this.txtSIsUsed2.Checked = heightWork.SIsUsed2.Value;
                        }
                        this.txtSafetyMeasures2.Text = heightWork.SafetyMeasures2;
                        this.txtMeasuresManName2.Text = measuresManName;
                        if (heightWork.SIsUsed3.HasValue)
                        {
                            this.txtSIsUsed3.Checked = heightWork.SIsUsed3.Value;
                        }
                        this.txtSafetyMeasures3.Text = heightWork.SafetyMeasures3;
                        this.txtMeasuresManName3.Text = measuresManName;
                        if (heightWork.SIsUsed4.HasValue)
                        {
                            this.txtSIsUsed4.Checked = heightWork.SIsUsed4.Value;
                        }
                        this.txtSafetyMeasures4.Text = heightWork.SafetyMeasures4;
                        this.txtMeasuresManName4.Text = measuresManName;
                        if (heightWork.SIsUsed5.HasValue)
                        {
                            this.txtSIsUsed5.Checked = heightWork.SIsUsed5.Value;
                        }
                        this.txtSafetyMeasures5.Text = heightWork.SafetyMeasures5;
                        this.txtMeasuresManName5.Text = measuresManName;
                        if (heightWork.SIsUsed6.HasValue)
                        {
                            this.txtSIsUsed6.Checked = heightWork.SIsUsed6.Value;
                        }
                        this.txtSafetyMeasures6.Text = heightWork.SafetyMeasures6;
                        this.txtMeasuresManName6.Text = measuresManName;
                        if (heightWork.SIsUsed7.HasValue)
                        {
                            this.txtSIsUsed7.Checked = heightWork.SIsUsed7.Value;
                        }
                        this.txtSafetyMeasures7.Text = heightWork.SafetyMeasures7;
                        this.txtMeasuresManName7.Text = measuresManName;
                        if (heightWork.SIsUsed8.HasValue)
                        {
                            this.txtSIsUsed8.Checked = heightWork.SIsUsed8.Value;
                        }
                        this.txtSafetyMeasures8.Text = heightWork.SafetyMeasures8;
                        this.txtMeasuresManName8.Text = measuresManName;
                        if (heightWork.SIsUsed9.HasValue)
                        {
                            this.txtSIsUsed9.Checked = heightWork.SIsUsed9.Value;
                        }
                        this.txtSafetyMeasures9.Text = heightWork.SafetyMeasures9;
                        this.txtMeasuresManName9.Text = measuresManName;
                        if (heightWork.SIsUsed10.HasValue)
                        {
                            this.txtSIsUsed10.Checked = heightWork.SIsUsed10.Value;
                        }
                        this.txtSafetyMeasures10.Text = heightWork.SafetyMeasures10;
                        this.txtMeasuresManName10.Text = measuresManName;
                        if (heightWork.SIsUsed11.HasValue)
                        {
                            this.txtSIsUsed11.Checked = heightWork.SIsUsed11.Value;
                        }
                        this.txtSafetyMeasures11.Text = heightWork.SafetyMeasures11;
                        this.txtMeasuresManName11.Text = measuresManName;
                        if (heightWork.SIsUsed12.HasValue)
                        {
                            this.txtSIsUsed12.Checked = heightWork.SIsUsed12.Value;
                        }
                        this.txtSafetyMeasures12.Text = heightWork.SafetyMeasures12;
                        this.txtMeasuresManName12.Text = measuresManName;
                        if (heightWork.SIsUsed13.HasValue)
                        {
                            this.txtSIsUsed13.Checked = heightWork.SIsUsed13.Value;
                        }
                        this.txtSafetyMeasures13.Text = heightWork.SafetyMeasures13;
                        this.txtMeasuresManName13.Text = measuresManName;
                        if (heightWork.SIsUsed14 == 0)
                        {
                            this.txtSIsUsed14.Checked = false;
                        }
                        else
                        {
                            this.txtSIsUsed14.Checked = true;
                        }
                        this.txtSafetyMeasures14.Text = heightWork.OtherMeasures;
                        this.txtMeasuresManName14.Text = measuresManName;

                        var getPushRecord3 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 3);
                        if (getPushRecord3 != null)
                        {
                            this.txtSafeEduManName.Text = UserService.GetUserNameByUserId(getPushRecord3.ReceiveManId);
                        }
                        var getPushRecord4 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 4);
                        if (getPushRecord4 != null && getPushRecord4.SigningTime.HasValue)
                        {
                            this.txtApplyUnitManOpinion.Text = getPushRecord4.Opinion;
                            this.txtApplyUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord4.ReceiveManId);
                            this.txtApplyUnitManTime.Text = getPushRecord4.SigningTime.Value.ToString("f");
                        }
                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 5);
                        if (getPushRecord5 != null && getPushRecord5.SigningTime.HasValue)
                        {
                            this.txtJobUnitOpinion.Text = getPushRecord5.Opinion;
                            this.txtJobUnitMan.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                            this.txtJobUnitTime.Text = getPushRecord5.SigningTime.Value.ToString("f");
                        }
                        //4-1作业所在装置审核（1-3级）
                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 6);
                        if (getPushRecord6 != null && getPushRecord6.SigningTime.HasValue)
                        {
                            this.txtAuditOpinion.Text = getPushRecord6.Opinion;
                            this.txtAuditMan.Text = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                            this.txtAuditTime.Text = getPushRecord6.SigningTime.Value.ToString("f");
                        }
                        ///4-2安全环保部审核（4级）
                        var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 7);
                        if (getPushRecord7 != null && getPushRecord6.SigningTime.HasValue)
                        {
                            this.txtAuditOpinion.Text = getPushRecord7.Opinion;
                            this.txtAuditMan.Text = UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId);
                            this.txtAuditTime.Text = getPushRecord7.SigningTime.Value.ToString("f");
                        }
                        //5-1设备组审批（1级）
                        var getPushRecord8 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 8);
                        if (getPushRecord8 != null && getPushRecord8.SigningTime.HasValue)
                        {
                            this.txtApproveOpinion.Text = getPushRecord8.Opinion;
                            this.txtApproveMan.Text = UserService.GetUserNameByUserId(getPushRecord8.ReceiveManId);
                            this.txtApproveTime.Text = getPushRecord8.SigningTime.Value.ToString("f");
                        }
                        //5-2设备组审批（2-3级）
                        var getPushRecord9 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 9);
                        if (getPushRecord9 != null && getPushRecord9.SigningTime.HasValue)
                        {
                            this.txtApproveOpinion.Text = getPushRecord9.Opinion;
                            this.txtApproveMan.Text = UserService.GetUserNameByUserId(getPushRecord9.ReceiveManId);
                            this.txtApproveTime.Text = getPushRecord9.SigningTime.Value.ToString("f");
                        }
                        //5-3公司领导审批（4级）
                        var getPushRecord10 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 10);
                        if (getPushRecord10 != null && getPushRecord10.SigningTime.HasValue)
                        {
                            this.txtApproveOpinion.Text = getPushRecord10.Opinion;
                            this.txtApproveMan.Text = UserService.GetUserNameByUserId(getPushRecord10.ReceiveManId);
                            this.txtApproveTime.Text = getPushRecord9.SigningTime.Value.ToString("f");
                        }
                        var getPushRecord11 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.HeightWorkId, 11);
                        if (getPushRecord11 != null && getPushRecord11.SigningTime.HasValue)
                        {
                            this.txtAcceptanceOpinion.Text = getPushRecord11.Opinion;
                            this.txtAcceptanceMan.Text = UserService.GetUserNameByUserId(getPushRecord11.ReceiveManId);
                            this.txtAcceptanceTime.Text = getPushRecord11.SigningTime.Value.ToString("f");
                        }
                    }
                }
            }
        }
        #endregion
    }
}
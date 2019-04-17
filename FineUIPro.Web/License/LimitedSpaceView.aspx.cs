using System;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class LimitedSpaceView : PageBase
    {
        /// <summary>
        /// 
        /// </summary>
        private string LimitedSpaceId
        {
            get
            {
                return (string)ViewState["LimitedSpaceId"];
            }
            set
            {
                ViewState["LimitedSpaceId"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SimpleForm1.Title = "受限空间安全作业证";
                this.LimitedSpaceId = Request.Params["LimitedSpaceId"];
                if (!string.IsNullOrEmpty(this.LimitedSpaceId))
                {
                    Model.View_License_LimitedSpace limitedSpace = BLL.LimitedSpaceService.GetViewLimitedSpaceById(this.LimitedSpaceId);
                    if (limitedSpace != null)
                    {
                        this.txtApplyUnit.Text = limitedSpace.ApplyUnitName;
                        this.txtApplyManName.Text = limitedSpace.ApplyManName;
                        this.txtLicenseCode.Text = limitedSpace.LicenseCode;
                        this.txtLimitedSpaceUnitName.Text = limitedSpace.LimitedSpaceUnitName;
                        this.txtLimitedSpaceName.Text = limitedSpace.LimitedSpaceName;
                        this.txtJobContent.Text = limitedSpace.JobContent;
                        this.txtMedium.Text = limitedSpace.Medium;
                        this.txtJobTime.Text = limitedSpace.StartDate;
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LimitedSpaceId, 2);
                        if (getPushRecord2 != null)
                        {
                            this.txtJobUnitHead.Text = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                        }
                        this.txtJobMan.Text = limitedSpace.JobMans;
                        this.txtGuardian.Text = limitedSpace.Guardian;
                        if (!string.IsNullOrEmpty(limitedSpace.OtherSpecial))
                        {
                            this.txtOtherSpecial.Text = limitedSpace.OtherSpecial;
                        }
                        if (!string.IsNullOrEmpty(limitedSpace.HAZIDName))
                        {
                            this.txtHAZName.Text = limitedSpace.HAZIDName;
                        }
                        if (limitedSpace.SIsUsed1.HasValue)
                        {
                            this.txtSIsUsed1.Checked = limitedSpace.SIsUsed1.Value;
                        }
                        this.txtSafetyMeasures1.Text = limitedSpace.SafetyMeasures1;
                        string measuresManName = string.Empty;
                        var getPushRecord4 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LimitedSpaceId, 4);
                        if (getPushRecord4 != null)
                        {
                            measuresManName = UserService.GetUserNameByUserId(getPushRecord4.ReceiveManId);
                        }
                        this.txtMeasuresManName1.Text = measuresManName;
                        if (limitedSpace.SIsUsed2.HasValue)
                        {
                            this.txtSIsUsed2.Checked = limitedSpace.SIsUsed2.Value;
                        }
                        this.txtSafetyMeasures2.Text = limitedSpace.SafetyMeasures2;
                        this.txtMeasuresManName2.Text = measuresManName;

                        if (limitedSpace.SIsUsed3.HasValue)
                        {
                            this.txtSIsUsed3.Checked = limitedSpace.SIsUsed3.Value;
                        }
                        this.txtSafetyMeasures3.Text = limitedSpace.SafetyMeasures3;
                        this.txtMeasuresManName3.Text = measuresManName;

                        if (limitedSpace.SIsUsed4.HasValue)
                        {
                            this.txtSIsUsed4.Checked = limitedSpace.SIsUsed4.Value;
                        }
                        this.txtSafetyMeasures4.Text = limitedSpace.SafetyMeasures4;
                        this.txtMeasuresManName4.Text = measuresManName;
                        if (limitedSpace.SIsUsed5.HasValue)
                        {
                            this.txtSIsUsed5.Checked = limitedSpace.SIsUsed5.Value;
                        }
                        this.txtSafetyMeasures5.Text = limitedSpace.SafetyMeasures5;
                        this.txtMeasuresManName5.Text = measuresManName;
                        if (limitedSpace.SIsUsed6.HasValue)
                        {
                            this.txtSIsUsed6.Checked = limitedSpace.SIsUsed6.Value;
                        }
                        this.txtSafetyMeasures6.Text = limitedSpace.SafetyMeasures6;
                        this.txtMeasuresManName6.Text = measuresManName;
                        if (limitedSpace.SIsUsed7.HasValue)
                        {
                            this.txtSIsUsed7.Checked = limitedSpace.SIsUsed7.Value;
                        }
                        this.txtSafetyMeasures7.Text = limitedSpace.SafetyMeasures7;
                        this.txtMeasuresManName7.Text = measuresManName;
                        if (limitedSpace.SIsUsed8.HasValue)
                        {
                            this.txtSIsUsed8.Checked = limitedSpace.SIsUsed8.Value;
                        }
                        this.txtSafetyMeasures8.Text = limitedSpace.SafetyMeasures8;
                        this.txtMeasuresManName8.Text = measuresManName;
                        if (limitedSpace.SIsUsed9.HasValue)
                        {
                            this.txtSIsUsed9.Checked = limitedSpace.SIsUsed9.Value;
                        }
                        this.txtSafetyMeasures9.Text = limitedSpace.SafetyMeasures9;
                        this.txtMeasuresManName9.Text = measuresManName;
                        if (limitedSpace.SIsUsed10.HasValue)
                        {
                            this.txtSIsUsed10.Checked = limitedSpace.SIsUsed10.Value;
                        }
                        this.txtSafetyMeasures10.Text = limitedSpace.SafetyMeasures10;
                        this.txtMeasuresManName10.Text = measuresManName;
                        if (limitedSpace.SIsUsed11 == 0)
                        {
                            this.txtSIsUsed11.Checked = false;
                        }
                        else
                        {
                            this.txtSIsUsed11.Checked = true;
                        }
                        this.txtSafetyMeasures11.Text = limitedSpace.OtherMeasures;
                        this.txtMeasuresManName11.Text = limitedSpace.ApplyManName;

                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LimitedSpaceId, 5);
                        if (getPushRecord5 != null)
                        {
                            this.txtSafeEduManName.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                        }
                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LimitedSpaceId, 6);
                        if (getPushRecord6 != null)
                        {
                            this.txtApplyUnitOpinion.Text = getPushRecord6.Opinion;
                            this.txtApplyUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                            if (getPushRecord6.SigningTime.HasValue)
                            {
                                this.txtApplyUnitTime.Text = getPushRecord6.SigningTime.Value.ToString("f");
                            }
                        }
                        var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LimitedSpaceId, 7);
                        if (getPushRecord7 != null)
                        {
                            this.txtApproveUnitOpinion.Text = getPushRecord7.Opinion;
                            this.txtApproveUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId);
                            if (getPushRecord7.SigningTime.HasValue)
                            {
                                this.txtApproveUnitTime.Text = getPushRecord7.SigningTime.Value.ToString("f");
                            }
                        }
                        var getPushRecord8 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LimitedSpaceId, 8);
                        if (getPushRecord8 != null)
                        {
                            this.txtAcceptanceOperationMan.Text = UserService.GetUserNameByUserId(getPushRecord8.ReceiveManId);
                        }
                        var getPushRecord9 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LimitedSpaceId, 9);
                        if (getPushRecord9 != null)
                        {
                            this.txtAcceptanceOpinion.Text = getPushRecord9.Opinion;
                            this.txtAcceptanceJobMan.Text = UserService.GetUserNameByUserId(getPushRecord9.ReceiveManId);
                            if (getPushRecord9.SigningTime.HasValue)
                            {
                                this.txtAcceptanceTime.Text = getPushRecord9.SigningTime.Value.ToString("f");
                            }
                        }
                    }
                }
            }
        }
    }
}
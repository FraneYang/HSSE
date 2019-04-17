using System;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class LiftingWorkView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 吊装安全作业证主键
        /// </summary>
        private string LiftingWorkId
        {
            get
            {
                return (string)ViewState["LiftingWorkId"];
            }
            set
            {
                ViewState["LiftingWorkId"] = value;
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
                this.SimpleForm1.Title = "吊装安全作业证";
                var thisUnits = BLL.CommonService.GetIsThisUnit();
                if (thisUnits != null)
                {
                    this.SimpleForm1.Title = thisUnits.UnitName + this.Title;
                }
                this.LiftingWorkId = Request.Params["LiftingWorkId"];
                if (!string.IsNullOrEmpty(this.LiftingWorkId))
                {
                    Model.View_License_LiftingWork liftingWork = BLL.LiftingWorkService.GetViewLiftingWorkById(this.LiftingWorkId);
                    if (liftingWork != null)
                    {
                        this.txtJobPalce.Text = liftingWork.JobPalce;
                        this.txtLiftingTools.Text = liftingWork.LiftingTools;
                        this.txtLicenseCode.Text = liftingWork.LicenseCode;
                        this.txtWorkerCertificate.Text = liftingWork.WorkerCertificate;
                        this.txtGuardian.Text = liftingWork.Guardian;
                        this.txtCommandCertificate.Text = liftingWork.CommandCertificate;
                        if (liftingWork.LiftingQuality.HasValue)
                        {
                            this.txtLiftingQuality.Text = liftingWork.LiftingQuality.ToString();
                        }
                        this.cbLiftingLevel.SelectedValueArray = new string[] { liftingWork.LiftingLevel };
                        this.txtJobTime.Text = liftingWork.StartDate;
                        this.txtLiftingContent.Text = liftingWork.LiftingContent;
                        this.txtHAZIDName.Text = liftingWork.HAZIDName;
                        if (liftingWork.SIsUsed1.HasValue)
                        {
                            this.txtSIsUsed1.Checked = liftingWork.SIsUsed1.Value;
                        }
                        this.txtSafetyMeasures1.Text = liftingWork.SafetyMeasures1;
                        string measuresManName = string.Empty;
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LiftingWorkId, 2);
                        if (getPushRecord2 != null)
                        {
                            measuresManName = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                        }
                        this.txtMeasuresManName1.Text = measuresManName;
                        if (liftingWork.SIsUsed2.HasValue)
                        {
                            this.txtSIsUsed2.Checked = liftingWork.SIsUsed2.Value;
                        }
                        this.txtSafetyMeasures2.Text = liftingWork.SafetyMeasures2;
                        this.txtMeasuresManName2.Text = measuresManName;
                        if (liftingWork.SIsUsed3.HasValue)
                        {
                            this.txtSIsUsed3.Checked = liftingWork.SIsUsed3.Value;
                        }
                        this.txtSafetyMeasures3.Text = liftingWork.SafetyMeasures3;
                        this.txtMeasuresManName3.Text = measuresManName;
                        if (liftingWork.SIsUsed4.HasValue)
                        {
                            this.txtSIsUsed4.Checked = liftingWork.SIsUsed4.Value;
                        }
                        this.txtSafetyMeasures4.Text = liftingWork.SafetyMeasures4;
                        this.txtMeasuresManName4.Text = measuresManName;
                        if (liftingWork.SIsUsed5.HasValue)
                        {
                            this.txtSIsUsed5.Checked = liftingWork.SIsUsed5.Value;
                        }
                        this.txtSafetyMeasures5.Text = liftingWork.SafetyMeasures5;
                        this.txtMeasuresManName5.Text = measuresManName;
                        if (liftingWork.SIsUsed6.HasValue)
                        {
                            this.txtSIsUsed6.Checked = liftingWork.SIsUsed6.Value;
                        }
                        this.txtSafetyMeasures6.Text = liftingWork.SafetyMeasures6;
                        this.txtMeasuresManName6.Text = measuresManName;
                        if (liftingWork.SIsUsed7.HasValue)
                        {
                            this.txtSIsUsed7.Checked = liftingWork.SIsUsed7.Value;
                        }
                        this.txtSafetyMeasures7.Text = liftingWork.SafetyMeasures7;
                        this.txtMeasuresManName7.Text = measuresManName;
                        if (liftingWork.SIsUsed8.HasValue)
                        {
                            this.txtSIsUsed8.Checked = liftingWork.SIsUsed8.Value;
                        }
                        this.txtSafetyMeasures8.Text = liftingWork.SafetyMeasures8;
                        this.txtMeasuresManName8.Text = measuresManName;
                        if (liftingWork.SIsUsed9.HasValue)
                        {
                            this.txtSIsUsed9.Checked = liftingWork.SIsUsed9.Value;
                        }
                        this.txtSafetyMeasures9.Text = liftingWork.SafetyMeasures9;
                        this.txtMeasuresManName9.Text = measuresManName;
                        if (liftingWork.SIsUsed10.HasValue)
                        {
                            this.txtSIsUsed10.Checked = liftingWork.SIsUsed10.Value;
                        }
                        this.txtSafetyMeasures10.Text = liftingWork.SafetyMeasures10;
                        this.txtMeasuresManName10.Text = measuresManName;
                        if (liftingWork.SIsUsed11.HasValue)
                        {
                            this.txtSIsUsed11.Checked = liftingWork.SIsUsed11.Value;
                        }
                        this.txtSafetyMeasures11.Text = liftingWork.SafetyMeasures11;
                        this.txtMeasuresManName11.Text = measuresManName;
                        if (liftingWork.SIsUsed12.HasValue)
                        {
                            this.txtSIsUsed12.Checked = liftingWork.SIsUsed12.Value;
                        }
                        this.txtSafetyMeasures12.Text = liftingWork.SafetyMeasures12;
                        this.txtMeasuresManName12.Text = measuresManName;
                        if (liftingWork.SIsUsed13.HasValue)
                        {
                            this.txtSIsUsed13.Checked = liftingWork.SIsUsed13.Value;
                        }
                        this.txtSafetyMeasures13.Text = liftingWork.SafetyMeasures13;
                        this.txtMeasuresManName13.Text = measuresManName;
                        if (liftingWork.SIsUsed14.HasValue)
                        {
                            this.txtSIsUsed14.Checked = liftingWork.SIsUsed14.Value;
                        }
                        this.txtSafetyMeasures14.Text = liftingWork.SafetyMeasures14;
                        this.txtMeasuresManName14.Text = measuresManName;
                        if (liftingWork.SIsUsed15.HasValue)
                        {
                            this.txtSIsUsed15.Checked = liftingWork.SIsUsed15.Value;
                        }
                        this.txtSafetyMeasures15.Text = liftingWork.SafetyMeasures15;
                        this.txtMeasuresManName15.Text = measuresManName;
                        if (liftingWork.SIsUsed16.HasValue)
                        {
                            this.txtSIsUsed16.Checked = liftingWork.SIsUsed16.Value;
                        }
                        this.txtSafetyMeasures16.Text = liftingWork.SafetyMeasures16;
                        this.txtMeasuresManName16.Text = measuresManName;
                        if (liftingWork.SIsUsed17.HasValue)
                        {
                            this.txtSIsUsed17.Checked = liftingWork.SIsUsed17.Value;
                        }
                        this.txtSafetyMeasures17.Text = liftingWork.SafetyMeasures17;
                        this.txtMeasuresManName17.Text = measuresManName;
                        if (liftingWork.SIsUsed18.HasValue)
                        {
                            this.txtSIsUsed18.Checked = liftingWork.SIsUsed18.Value;
                        }
                        this.txtSafetyMeasures18.Text = liftingWork.SafetyMeasures18;
                        this.txtMeasuresManName18.Text = measuresManName;
                        if (liftingWork.SIsUsed19.HasValue)
                        {
                            this.txtSIsUsed19.Checked = liftingWork.SIsUsed19.Value;
                        }
                        this.txtSafetyMeasures19.Text = liftingWork.SafetyMeasures19;
                        this.txtMeasuresManName19.Text = measuresManName;
                        if (liftingWork.SIsUsed20.HasValue)
                        {
                            this.txtSIsUsed20.Checked = liftingWork.SIsUsed20.Value;
                        }
                        this.txtSafetyMeasures20.Text = liftingWork.SafetyMeasures20;
                        this.txtMeasuresManName20.Text = measuresManName;
                        if (liftingWork.SIsUsed21.HasValue)
                        {
                            this.txtSIsUsed21.Checked = liftingWork.SIsUsed21.Value;
                        }
                        this.txtSafetyMeasures21.Text = liftingWork.SafetyMeasures21;
                        this.txtMeasuresManName21.Text = measuresManName;
                        if (liftingWork.SIsUsed22.HasValue)
                        {
                            this.txtSIsUsed22.Checked = liftingWork.SIsUsed22.Value;
                        }
                        this.txtSafetyMeasures22.Text = liftingWork.SafetyMeasures22;
                        this.txtMeasuresManName22.Text = measuresManName;
                        if (liftingWork.SIsUsed23.HasValue)
                        {
                            this.txtSIsUsed23.Checked = liftingWork.SIsUsed23.Value;
                        }
                        this.txtSafetyMeasures23.Text = liftingWork.SafetyMeasures23;
                        this.txtMeasuresManName23.Text = measuresManName;
                        if (liftingWork.SIsUsed24.HasValue)
                        {
                            this.txtSIsUsed24.Checked = liftingWork.SIsUsed24.Value;
                        }
                        this.txtSafetyMeasures24.Text = liftingWork.SafetyMeasures24;
                        this.txtMeasuresManName24.Text = measuresManName;
                        if (liftingWork.SIsUsed25.HasValue)
                        {
                            this.txtSIsUsed25.Checked = liftingWork.SIsUsed25.Value;
                        }
                        this.txtSafetyMeasures25.Text = liftingWork.SafetyMeasures25;
                        this.txtMeasuresManName25.Text = measuresManName;
                        if (liftingWork.SIsUsed26.HasValue)
                        {
                            this.txtSIsUsed26.Checked = liftingWork.SIsUsed26.Value;
                        }
                        this.txtSafetyMeasures26.Text = liftingWork.SafetyMeasures26;
                        this.txtMeasuresManName26.Text = measuresManName;
                        if (liftingWork.SIsUsed28.HasValue)
                        {
                            this.txtSIsUsed28.Checked = liftingWork.SIsUsed28.Value;
                        }
                        this.txtSafetyMeasures28.Text = liftingWork.SafetyMeasures28;
                        this.txtMeasuresManName28.Text = measuresManName;
                        if (liftingWork.SIsUsed29.HasValue)
                        {
                            this.txtSIsUsed29.Checked = liftingWork.SIsUsed29.Value;
                        }
                        this.txtSafetyMeasures29.Text = liftingWork.SafetyMeasures29;
                        this.txtMeasuresManName29.Text = measuresManName;
                        if (liftingWork.SIsUsed30.HasValue)
                        {
                            this.txtSIsUsed30.Checked = liftingWork.SIsUsed30.Value;
                        }
                        this.txtSafetyMeasures30.Text = liftingWork.SafetyMeasures30;
                        this.txtMeasuresManName30.Text = measuresManName;
                        if (liftingWork.SIsUsed31.HasValue)
                        {
                            this.txtSIsUsed31.Checked = liftingWork.SIsUsed31.Value;
                        }
                        this.txtSafetyMeasures31.Text = liftingWork.SafetyMeasures31;
                        this.txtMeasuresManName31.Text = measuresManName;
                        if (liftingWork.SIsUsed32.HasValue)
                        {
                            this.txtSIsUsed32.Checked = liftingWork.SIsUsed32.Value;
                        }
                        this.txtSafetyMeasures32.Text = liftingWork.SafetyMeasures32;
                        this.txtMeasuresManName32.Text = measuresManName;
                        if (liftingWork.SIsUsed33 == 0)
                        {
                            this.txtSIsUsed33.Checked = false;
                        }
                        else
                        {
                            this.txtSIsUsed33.Checked = true;
                        }
                        this.txtSafetyMeasures33.Text = liftingWork.OtherMeasures;
                        this.txtMeasuresManName33.Text = measuresManName;
                        var getPushRecord4 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LiftingWorkId, 4);
                        if (getPushRecord4 != null)
                        {
                            this.txtSafeEduManName.Text = UserService.GetUserNameByUserId(getPushRecord4.ReceiveManId);
                        }
                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LiftingWorkId, 5);
                        if (getPushRecord5 != null)
                        {
                            this.txtProduceDepManName.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                        }
                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LiftingWorkId, 6);
                        if (getPushRecord6 != null)
                        {
                            this.txtProduceUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                        }
                        var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LiftingWorkId, 7);
                        if (getPushRecord7 != null)
                        {
                            this.txtJobDepManName.Text = UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId);
                        }
                        var getPushRecord8 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LiftingWorkId, 8);
                        if (getPushRecord8 != null)
                        {
                            this.txtJobUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord8.ReceiveManId);
                        }
                        var getPushRecord9 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.LiftingWorkId, 9);
                        if (getPushRecord9 != null)
                        {
                            this.txtApprovalDepOpinion.Text = getPushRecord9.Opinion;
                            this.txtApprovalDepManName.Text = UserService.GetUserNameByUserId(getPushRecord9.ReceiveManId);
                            if (getPushRecord9.SigningTime.HasValue)
                            {
                                this.txtApprovalDepTime.Text = getPushRecord9.SigningTime.Value.ToString("f");
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
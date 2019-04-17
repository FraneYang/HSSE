using System;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class BreakGroundView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 动土安全作业证主键
        /// </summary>
        private string BreakGroundId
        {
            get
            {
                return (string)ViewState["BreakGroundId"];
            }
            set
            {
                ViewState["BreakGroundId"] = value;
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
                this.SimpleForm1.Title = "动土安全作业证";
                var thisUnits = BLL.CommonService.GetIsThisUnit();
                if (thisUnits != null)
                {
                    this.SimpleForm1.Title = thisUnits.UnitName + this.Title;
                }
                this.BreakGroundId = Request.Params["BreakGroundId"];
                if (!string.IsNullOrEmpty(this.BreakGroundId))
                {
                    Model.View_License_BreakGround breakGround = BLL.BreakGroundService.GetViewBreakGroundById(this.BreakGroundId);
                    if (breakGround != null)
                    {
                        this.txtApplyUnit.Text = breakGround.ApplyUintName;
                        this.txtApplyManName.Text = breakGround.ApplyManName;
                        this.txtLicenseCode.Text = breakGround.LicenseCode;
                        this.txtJobMans.Text = breakGround.JobMans;
                        this.txtJobTime.Text = breakGround.JobTime;
                        this.txtJobPalce.Text = breakGround.JobPalce;
                        this.txtJobUnit.Text = breakGround.JobUnit;
                        this.txtOtherMeasures.Text = breakGround.OtherMeasures;
                        var getPushRecord1 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 1);
                        if (getPushRecord1 != null)
                        {
                            this.txtReceiveManName.Text = UserService.GetUserNameByUserId(getPushRecord1.ReceiveManId);
                            if (getPushRecord1.SigningTime.HasValue)
                            {
                                this.txtSigningTime.Text = getPushRecord1.SigningTime.Value.ToString("f");
                            }
                        }
                        this.txtHAZIDName.Text = breakGround.HAZIDName;
                        if (breakGround.SIsUsed1.HasValue)
                        {
                            this.txtSIsUsed1.Checked = breakGround.SIsUsed1.Value;
                        }
                        this.txtSafetyMeasures1.Text = breakGround.SafetyMeasures1;
                        string measuresManName = string.Empty;
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 2);
                        if (getPushRecord2 != null)
                        {
                            measuresManName = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                        }
                        this.txtMeasuresManName1.Text = measuresManName;
                        if (breakGround.SIsUsed2.HasValue)
                        {
                            this.txtSIsUsed2.Checked = breakGround.SIsUsed2.Value;
                        }
                        this.txtSafetyMeasures2.Text = breakGround.SafetyMeasures2;
                        this.txtMeasuresManName2.Text = measuresManName;
                        if (breakGround.SIsUsed3.HasValue)
                        {
                            this.txtSIsUsed3.Checked = breakGround.SIsUsed3.Value;
                        }
                        this.txtSafetyMeasures3.Text = breakGround.SafetyMeasures3;
                        this.txtMeasuresManName3.Text = measuresManName;
                        if (breakGround.SIsUsed4.HasValue)
                        {
                            this.txtSIsUsed4.Checked = breakGround.SIsUsed4.Value;
                        }
                        this.txtSafetyMeasures4.Text = breakGround.SafetyMeasures4;
                        this.txtMeasuresManName4.Text = measuresManName;
                        if (breakGround.SIsUsed5.HasValue)
                        {
                            this.txtSIsUsed5.Checked = breakGround.SIsUsed5.Value;
                        }
                        this.txtSafetyMeasures5.Text = breakGround.SafetyMeasures5;
                        this.txtMeasuresManName5.Text = measuresManName;
                        if (breakGround.SIsUsed6.HasValue)
                        {
                            this.txtSIsUsed6.Checked = breakGround.SIsUsed6.Value;
                        }
                        this.txtSafetyMeasures6.Text = breakGround.SafetyMeasures6;
                        this.txtMeasuresManName6.Text = measuresManName;
                        if (breakGround.SIsUsed7.HasValue)
                        {
                            this.txtSIsUsed7.Checked = breakGround.SIsUsed7.Value;
                        }
                        this.txtSafetyMeasures7.Text = breakGround.SafetyMeasures7;
                        this.txtMeasuresManName7.Text = measuresManName;
                        if (breakGround.SIsUsed8.HasValue)
                        {
                            this.txtSIsUsed8.Checked = breakGround.SIsUsed8.Value;
                        }
                        this.txtSafetyMeasures8.Text = breakGround.SafetyMeasures8;
                        this.txtMeasuresManName8.Text = measuresManName;
                        if (breakGround.SIsUsed9.HasValue)
                        {
                            this.txtSIsUsed9.Checked = breakGround.SIsUsed9.Value;
                        }
                        this.txtSafetyMeasures9.Text = breakGround.SafetyMeasures9;
                        this.txtMeasuresManName9.Text = measuresManName;
                        if (breakGround.SIsUsed10.HasValue)
                        {
                            this.txtSIsUsed10.Checked = breakGround.SIsUsed10.Value;
                        }
                        this.txtSafetyMeasures10.Text = breakGround.SafetyMeasures10;
                        this.txtMeasuresManName10.Text = measuresManName;
                        if (breakGround.SIsUsed11.HasValue)
                        {
                            this.txtSIsUsed11.Checked = breakGround.SIsUsed11.Value;
                        }
                        this.txtSafetyMeasures11.Text = breakGround.SafetyMeasures11;
                        this.txtMeasuresManName11.Text = measuresManName;
                        if (breakGround.SIsUsed12.HasValue)
                        {
                            this.txtSIsUsed12.Checked = breakGround.SIsUsed12.Value;
                        }
                        this.txtSafetyMeasures12.Text = breakGround.SafetyMeasures12;
                        this.txtMeasuresManName12.Text = measuresManName;
                        if (breakGround.SIsUsed13.HasValue)
                        {
                            this.txtSIsUsed13.Checked = breakGround.SIsUsed13.Value;
                        }
                        this.txtSafetyMeasures13.Text = breakGround.SafetyMeasures13;
                        this.txtMeasuresManName13.Text = measuresManName;
                        if (breakGround.SIsUsed14.HasValue)
                        {
                            this.txtSIsUsed14.Checked = breakGround.SIsUsed14.Value;
                        }
                        this.txtSafetyMeasures14.Text = breakGround.SafetyMeasures14;
                        this.txtMeasuresManName14.Text = measuresManName;
                        if (breakGround.SIsUsed15.HasValue)
                        {
                            this.txtSIsUsed15.Checked = breakGround.SIsUsed15.Value;
                        }
                        this.txtSafetyMeasures15.Text = breakGround.SafetyMeasures15;
                        this.txtMeasuresManName15.Text = measuresManName;
                        if (breakGround.SIsUsed16 == 0)
                        {
                            this.txtSIsUsed16.Checked = false;
                        }
                        else
                        {
                            this.txtSIsUsed16.Checked = true;
                        }
                        this.txtSafetyMeasures16.Text = breakGround.SafetyMeasures16;
                        this.txtMeasuresManName16.Text = measuresManName;

                        var getPushRecord4 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 4);
                        if (getPushRecord4 != null)
                        {
                            this.txtSafeEduManName.Text = UserService.GetUserNameByUserId(getPushRecord4.ReceiveManId);
                        }

                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 5);
                        if (getPushRecord5 != null)
                        {
                            this.txtApplyUnitOpinion.Text = getPushRecord5.Opinion;
                            this.txtApplyUnitMan.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                            if (getPushRecord5.SigningTime.HasValue)
                            {
                                this.txtApplyUnitTime.Text = getPushRecord5.SigningTime.Value.ToString("f");
                            }
                        }
                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 6);
                        if (getPushRecord6 != null)
                        {
                            this.txtJobUnitOpinion.Text = getPushRecord6.Opinion;
                            this.txtJobUnitMan.Text = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                            if (getPushRecord6.SigningTime.HasValue)
                            {
                                this.txtJobUnitTime.Text = getPushRecord6.SigningTime.Value.ToString("f");
                            }
                        }
                        string equipmentOpinion = string.Empty;
                        string equipmentMan = string.Empty;
                        string equipmentTime = string.Empty;
                        var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 7);
                        if (getPushRecord7 != null)
                        {
                            equipmentOpinion += getPushRecord7.Opinion + "；";
                            equipmentMan += UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId) + "；";
                            if (getPushRecord7.SigningTime.HasValue)
                            {
                                equipmentTime += getPushRecord7.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord8 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 8);
                        if (getPushRecord8 != null)
                        {
                            equipmentOpinion += getPushRecord8.Opinion + "；";
                            equipmentMan += UserService.GetUserNameByUserId(getPushRecord8.ReceiveManId) + "；";
                            if (getPushRecord8.SigningTime.HasValue)
                            {
                                equipmentTime += getPushRecord8.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord9 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 9);
                        if (getPushRecord9 != null)
                        {
                            equipmentOpinion += getPushRecord9.Opinion + "；";
                            equipmentMan += UserService.GetUserNameByUserId(getPushRecord9.ReceiveManId) + "；";
                            if (getPushRecord9.SigningTime.HasValue)
                            {
                                equipmentTime += getPushRecord9.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord10 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 10);
                        if (getPushRecord10 != null)
                        {
                            equipmentOpinion += getPushRecord10.Opinion + "；";
                            equipmentMan += UserService.GetUserNameByUserId(getPushRecord10.ReceiveManId) + "；";
                            if (getPushRecord10.SigningTime.HasValue)
                            {
                                equipmentTime += getPushRecord10.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord11 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 11);
                        if (getPushRecord11 != null)
                        {
                            equipmentOpinion += getPushRecord11.Opinion + "；";
                            equipmentMan += UserService.GetUserNameByUserId(getPushRecord11.ReceiveManId) + "；";
                            if (getPushRecord11.SigningTime.HasValue)
                            {
                                equipmentTime += getPushRecord11.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord12 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 12);
                        if (getPushRecord12 != null)
                        {
                            equipmentOpinion += getPushRecord12.Opinion + "；";
                            equipmentMan += UserService.GetUserNameByUserId(getPushRecord12.ReceiveManId) + "；";
                            if (getPushRecord12.SigningTime.HasValue)
                            {
                                equipmentTime += getPushRecord12.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord13 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 13);
                        if (getPushRecord13 != null)
                        {
                            equipmentOpinion += getPushRecord13.Opinion + "；";
                            equipmentMan += UserService.GetUserNameByUserId(getPushRecord13.ReceiveManId) + "；";
                            if (getPushRecord13.SigningTime.HasValue)
                            {
                                equipmentTime += getPushRecord13.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        if (!string.IsNullOrEmpty(equipmentOpinion))
                        {
                            this.txtEquipmentOpinion.Text = equipmentOpinion.Substring(0, equipmentOpinion.LastIndexOf("；"));
                        }
                        if (!string.IsNullOrEmpty(equipmentMan))
                        {
                            this.txtEquipmentMan.Text = equipmentMan.Substring(0, equipmentMan.LastIndexOf("；"));
                        }
                        if (!string.IsNullOrEmpty(equipmentTime))
                        {
                            this.txtEquipmentTime.Text = equipmentTime.Substring(0, equipmentTime.LastIndexOf("；"));
                        }
                        string acceptanceOpinion = string.Empty;
                        string acceptanceMan = string.Empty;
                        string acceptanceTime = string.Empty;
                        var getPushRecord14 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 14);
                        if (getPushRecord14 != null)
                        {
                            acceptanceOpinion += getPushRecord14.Opinion + "；";
                            acceptanceMan += UserService.GetUserNameByUserId(getPushRecord14.ReceiveManId) + "；";
                            if (getPushRecord14.SigningTime.HasValue)
                            {
                                acceptanceTime += getPushRecord14.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord15 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 15);
                        if (getPushRecord15 != null)
                        {
                            acceptanceOpinion += getPushRecord15.Opinion + "；";
                            acceptanceMan += UserService.GetUserNameByUserId(getPushRecord15.ReceiveManId) + "；";
                            if (getPushRecord15.SigningTime.HasValue)
                            {
                                acceptanceTime += getPushRecord15.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord16 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 16);
                        if (getPushRecord16 != null)
                        {
                            acceptanceOpinion += getPushRecord16.Opinion + "；";
                            acceptanceMan += UserService.GetUserNameByUserId(getPushRecord16.ReceiveManId) + "；";
                            if (getPushRecord16.SigningTime.HasValue)
                            {
                                acceptanceTime += getPushRecord16.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        var getPushRecord17 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 17);
                        if (getPushRecord17 != null)
                        {
                            acceptanceOpinion += getPushRecord17.Opinion + "；";
                            acceptanceMan += UserService.GetUserNameByUserId(getPushRecord17.ReceiveManId) + "；";
                            if (getPushRecord17.SigningTime.HasValue)
                            {
                                acceptanceTime += getPushRecord17.SigningTime.Value.ToString("f") + "；";
                            }
                        } 
                        var getPushRecord18 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BreakGroundId, 18);
                        if (getPushRecord18 != null)
                        {
                            acceptanceOpinion += getPushRecord18.Opinion + "；";
                            acceptanceMan += UserService.GetUserNameByUserId(getPushRecord18.ReceiveManId) + "；";
                            if (getPushRecord18.SigningTime.HasValue)
                            {
                                acceptanceTime += getPushRecord18.SigningTime.Value.ToString("f") + "；";
                            }
                        }
                        if (!string.IsNullOrEmpty(acceptanceOpinion))
                        {
                            this.txtAcceptanceOpinion.Text = acceptanceOpinion.Substring(0, acceptanceOpinion.LastIndexOf("；"));
                        }
                        if (!string.IsNullOrEmpty(acceptanceMan))
                        {
                            this.txtAcceptanceMan.Text = acceptanceMan.Substring(0, acceptanceMan.LastIndexOf("；"));
                        }
                        if (!string.IsNullOrEmpty(acceptanceTime))
                        {
                            this.txtAcceptanceTime.Text = acceptanceTime.Substring(0, acceptanceTime.LastIndexOf("；"));
                        }
                    }
                }
            }
        }
        #endregion
    }
}
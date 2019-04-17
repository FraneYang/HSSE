using System;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class BlindPlateView : PageBase
    {
        /// <summary>
        /// 
        /// </summary>
        private string BlindPlateId
        {
            get
            {
                return (string)ViewState["BlindPlateId"];
            }
            set
            {
                ViewState["BlindPlateId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SimpleForm1.Title = "盲板抽堵安全作业票";
                var thisUnits = BLL.CommonService.GetIsThisUnit();
                if (thisUnits != null)
                {
                    this.SimpleForm1.Title = thisUnits.UnitName + this.Title;
                }
                this.BlindPlateId = Request.Params["BlindPlateId"];
                if (!string.IsNullOrEmpty(this.BlindPlateId))
                {
                    Model.View_License_BlindPlate blindPlate = BLL.BlindPlateService.GetViewBlindPlateById(this.BlindPlateId);
                    if (blindPlate != null)
                    {
                        this.txtApplyUnit.Text = blindPlate.ApplyUnitName;
                        this.txtApplyManName.Text = blindPlate.ApplyManName;
                        this.txtLicenseCode.Text = blindPlate.LicenseCode;
                        this.txtEquipmentName.Text = blindPlate.EquipmentName;
                        this.txtMedium.Text = blindPlate.Medium;
                        this.txtTemperature.Text = blindPlate.Temperature;
                        this.txtPressure.Text = blindPlate.Pressure;
                        this.txtMaterial.Text = blindPlate.Material;
                        this.txtSpecification.Text = blindPlate.Specification;
                        this.txtNumber.Text = blindPlate.Number;
                        if (blindPlate.EffectiveDate.HasValue)
                        {
                            this.txtEffectiveDate.Text = blindPlate.EffectiveDate.Value.ToString("f");
                        }
                        this.cbImplement.SelectedValueArray = new string[] { blindPlate.Implement };
                        this.txtJobMan.Text = blindPlate.JobManName;
                        this.txtProcessGuardian.Text = blindPlate.ProcessGuardian;
                        var getPushRecord1 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 1);
                        if (getPushRecord1 != null)
                        {
                            this.txtJobHead.Text = UserService.GetUserNameByUserId(getPushRecord1.ReceiveManId);
                        }
                        var getPushRecord3 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 3);
                        if (getPushRecord3 != null)
                        {
                            this.txtUnitHead.Text = UserService.GetUserNameByUserId(getPushRecord3.ReceiveManId);
                        }
                        this.txtOtherSpecial.Text = blindPlate.OtherSpecial;
                        this.txtNumbering.Text = blindPlate.Numbering;
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 2);
                        if (getPushRecord2 != null)
                        {
                            this.txtCompileMan1.Text = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                            this.txtCompileDate1.Text = getPushRecord2.SigningTime.Value.ToString("f");
                        }
                        if (blindPlate.SIsUsed1.HasValue)
                        {
                            this.txtSIsUsed1.Checked = blindPlate.SIsUsed1.Value;
                        }
                        this.txtSafetyMeasures1.Text = blindPlate.SafetyMeasures1;
                        string measuresManName = string.Empty;
                        var getPushRecord4 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 4);
                        if (getPushRecord4 != null)
                        {
                            measuresManName = UserService.GetUserNameByUserId(getPushRecord4.ReceiveManId);
                        }
                        this.txtMeasuresManName1.Text = measuresManName;
                        if (blindPlate.SIsUsed2.HasValue)
                        {
                            this.txtSIsUsed2.Checked = blindPlate.SIsUsed2.Value;
                        }
                        this.txtSafetyMeasures2.Text = blindPlate.SafetyMeasures2;
                        this.txtMeasuresManName2.Text = measuresManName;
                        if (blindPlate.SIsUsed3.HasValue)
                        {
                            this.txtSIsUsed3.Checked = blindPlate.SIsUsed3.Value;
                        }
                        this.txtSafetyMeasures3.Text = blindPlate.SafetyMeasures3;
                        this.txtMeasuresManName3.Text = measuresManName;
                        if (blindPlate.SIsUsed4.HasValue)
                        {
                            this.txtSIsUsed4.Checked = blindPlate.SIsUsed4.Value;
                        }
                        this.txtSafetyMeasures4.Text = blindPlate.SafetyMeasures4;
                        this.txtMeasuresManName4.Text = measuresManName;
                        if (blindPlate.SIsUsed5.HasValue)
                        {
                            this.txtSIsUsed5.Checked = blindPlate.SIsUsed5.Value;
                        }
                        this.txtSafetyMeasures5.Text = blindPlate.SafetyMeasures5;
                        this.txtMeasuresManName5.Text = measuresManName;
                        if (blindPlate.SIsUsed6.HasValue)
                        {
                            this.txtSIsUsed6.Checked = blindPlate.SIsUsed6.Value;
                        }
                        this.txtSafetyMeasures6.Text = blindPlate.SafetyMeasures6;
                        this.txtMeasuresManName6.Text = measuresManName;
                        if (blindPlate.SIsUsed7.HasValue)
                        {
                            this.txtSIsUsed7.Checked = blindPlate.SIsUsed7.Value;
                        }
                        this.txtSafetyMeasures7.Text = blindPlate.SafetyMeasures7;
                        this.txtMeasuresManName7.Text = measuresManName;
                        if (blindPlate.SIsUsed8 == 0)
                        {
                            this.txtSIsUsed8.Checked = false;
                        }
                        else
                        {
                            this.txtSIsUsed8.Checked = true;
                        }
                        this.txtSafetyMeasures8.Text = blindPlate.OtherMeasures;
                        this.txtMeasuresManName8.Text = blindPlate.ApplyManName;
                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 5);
                        if (getPushRecord5 != null)
                        {
                            this.txtSafeEduManName.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                        }
                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 6);
                        if (getPushRecord6 != null)
                        {
                            this.txtProduceUnitOpinion.Text = getPushRecord6.Opinion;
                            this.txtProduceUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                            this.txtProduceUnitManTime.Text = getPushRecord6.SigningTime.Value.ToString("f");
                        }
                        var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 7);
                        if (getPushRecord7 != null)
                        {
                            this.txtOperationUnitOpinion.Text = getPushRecord7.Opinion;
                            this.txtOperationUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId);
                            this.txtOperationUnitManTime.Text = getPushRecord7.SigningTime.Value.ToString("f");
                        }
                        var getPushRecord8 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 8);
                        if (getPushRecord8 != null)
                        {
                            this.txtProductionDepOpinion.Text = getPushRecord8.Opinion;
                            this.txtProductionDepManName.Text = UserService.GetUserNameByUserId(getPushRecord8.ReceiveManId);
                            this.txtProductionDepManTime.Text = getPushRecord8.SigningTime.Value.ToString("f");
                        }
                        var getPushRecord9 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 9);
                        if (getPushRecord9 != null)
                        {
                            this.txtAccOperationUnitOpinion.Text = getPushRecord9.Opinion;
                            this.txtAccOperationUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord9.ReceiveManId);
                            this.txtAccOperationUnitManTime.Text = getPushRecord9.SigningTime.Value.ToString("f");
                        }
                        var getPushRecord10 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.BlindPlateId, 10);
                        if (getPushRecord10 != null)
                        {
                            this.txtAccProduceUnitOpinion.Text = getPushRecord10.Opinion;
                            this.txtAccProduceUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord10.ReceiveManId);
                            this.txtAccProduceUnitManTime.Text = getPushRecord10.SigningTime.Value.ToString("f");
                        }
                    }
                }
            }
        }
    }
}
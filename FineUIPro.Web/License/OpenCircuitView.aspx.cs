using System;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class OpenCircuitView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 断路安全作业证主键
        /// </summary>
        private string OpenCircuitId
        {
            get
            {
                return (string)ViewState["OpenCircuitId"];
            }
            set
            {
                ViewState["OpenCircuitId"] = value;
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
            this.SimpleForm1.Title = "断路安全作业证";
            var thisUnits = BLL.CommonService.GetIsThisUnit();
            if (thisUnits != null)
            {
                this.SimpleForm1.Title = thisUnits.UnitName + this.Title;
            }
            this.OpenCircuitId = Request.Params["OpenCircuitId"];
            if (!string.IsNullOrEmpty(this.OpenCircuitId))
            {
                Model.View_License_OpenCircuit openCircuit = BLL.OpenCircuitService.GetViewOpenCircuitById(this.OpenCircuitId);
                if (openCircuit != null)
                {
                    this.txtApplyUnit.Text = openCircuit.ApplyUnitName;
                    this.txtApplyManName.Text = openCircuit.ApplyManName;
                    this.txtLicenseCode.Text = openCircuit.LicenseCode;
                    this.txtJobUnit.Text = openCircuit.JobUnitName;

                    string measuresManName = string.Empty;
                    var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OpenCircuitId, 6);
                    if (getPushRecord6 != null)
                    {
                        measuresManName = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                    }
                    this.txtJobHead.Text = measuresManName;
                    this.txtRelatedUnitDep.Text = openCircuit.RelatedUnitDep;
                    this.txtOpenCircuitCause.Text = openCircuit.OpenCircuitCause;
                    this.txtOpenCircuitTime.Text = openCircuit.StartDate;
                    this.txtJobContent.Text = openCircuit.JobContent;
                    this.txtJobContentName.Text = openCircuit.CompileManName;
                    if (openCircuit.CompileManTime.HasValue)
                    {
                        this.txtJobContentTime.Text = openCircuit.CompileManTime.Value.ToString("f");
                    }
                    this.txtHAZIDName.Text = openCircuit.HAZIDName;
                    if (openCircuit.SIsUsed1.HasValue)
                    {
                        this.txtSIsUsed1.Checked = openCircuit.SIsUsed1.Value;
                    }
                    this.txtSafetyMeasures1.Text = openCircuit.SafetyMeasures1;
                    this.txtMeasuresManName1.Text = measuresManName;
                    if (openCircuit.SIsUsed2.HasValue)
                    {
                        this.txtSIsUsed2.Checked = openCircuit.SIsUsed2.Value;
                    }
                    this.txtSafetyMeasures2.Text = openCircuit.SafetyMeasures2;
                    this.txtMeasuresManName2.Text = measuresManName;
                    if (openCircuit.SIsUsed3.HasValue)
                    {
                        this.txtSIsUsed3.Checked = openCircuit.SIsUsed3.Value;
                    }
                    this.txtSafetyMeasures3.Text = openCircuit.SafetyMeasures3;
                    this.txtMeasuresManName3.Text = measuresManName;
                    if (openCircuit.SIsUsed4 == 0)
                    {
                        this.txtSIsUsed4.Checked = false;
                    }
                    else
                    {
                        this.txtSIsUsed4.Checked = true;
                    }
                    this.txtSafetyMeasures4.Text = openCircuit.OtherMeasures;
                    this.txtMeasuresManName4.Text = openCircuit.ApplyManName;
                    var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OpenCircuitId, 2);
                    if (getPushRecord2 != null)
                    {
                        this.txtSafeEduManName.Text = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                    }
                    var getPushRecord1 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OpenCircuitId, 1);
                    if (getPushRecord1 != null)
                    {
                        this.txtApplyUnitOpinion.Text = getPushRecord1.Opinion;
                        this.txtApplyUnitMan.Text = UserService.GetUserNameByUserId(getPushRecord1.ReceiveManId);
                        this.txtApplyUnitTime.Text = getPushRecord1.SigningTime.Value.ToString("f");
                    }
                    var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OpenCircuitId, 5);
                    if (getPushRecord5 != null)
                    {
                        this.txtJobUnitOpinion.Text = getPushRecord5.Opinion;
                        this.txtJobUnitMan.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                        this.txtJobUnitTime.Text = getPushRecord5.SigningTime.Value.ToString("f");
                    }
                    string ApprovalDepOpinion = string.Empty;
                    string ApprovalDepMan = string.Empty;
                    string ApprovalDepTime = string.Empty;
                    var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OpenCircuitId, 7);
                    if (getPushRecord7 != null)
                    {
                        ApprovalDepOpinion += getPushRecord7.Opinion + ",";
                        ApprovalDepMan += UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId) + ",";
                        ApprovalDepTime += getPushRecord7.SigningTime.Value.ToString("f") + ",";
                    }
                    var getPushRecord8 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OpenCircuitId, 8);
                    if (getPushRecord8 != null)
                    {
                        ApprovalDepOpinion += getPushRecord8.Opinion + ",";
                        ApprovalDepMan += UserService.GetUserNameByUserId(getPushRecord8.ReceiveManId) + ",";
                        ApprovalDepTime += getPushRecord8.SigningTime.Value.ToString("f") + ",";
                    }
                    if (!string.IsNullOrEmpty(ApprovalDepOpinion))
                    {
                        this.txtApprovalDepOpinion.Text = ApprovalDepOpinion.Substring(0, ApprovalDepOpinion.LastIndexOf(","));
                    }
                    if (!string.IsNullOrEmpty(ApprovalDepMan))
                    {
                        this.txtApprovalDepMan.Text = ApprovalDepMan.Substring(0, ApprovalDepMan.LastIndexOf(","));
                    }
                    if (!string.IsNullOrEmpty(ApprovalDepTime))
                    {
                        this.txtApprovalDepTime.Text = ApprovalDepTime.Substring(0, ApprovalDepTime.LastIndexOf(","));
                    }
                    var getPushRecord10 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OpenCircuitId, 10);
                    if (getPushRecord10 != null)
                    {
                        this.txtAcceptanceOpinion.Text = getPushRecord10.Opinion;
                        this.txtAcceptanceMan.Text = UserService.GetUserNameByUserId(getPushRecord10.ReceiveManId);
                        this.txtAcceptanceTime.Text = getPushRecord7.SigningTime.Value.ToString("f");
                    }
                }
            }
        }
        #endregion
    }
}
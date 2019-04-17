using System;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class AInterlockingView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// A级联锁变更审批单
        /// </summary>
        private string InterlockingId
        {
            get
            {
                return (string)ViewState["InterlockingId"];
            }
            set
            {
                ViewState["InterlockingId"] = value;
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
                this.SimpleForm1.Title = "A级联锁变更审批单";
                this.InterlockingId = Request.Params["InterlockingId"];
                if (!string.IsNullOrEmpty(this.InterlockingId))
                {
                    Model.License_Interlocking interlocking = BLL.InterlockingService.GetInterlockingById(this.InterlockingId);
                    if (interlocking != null)
                    {
                        if (interlocking.ApplyDate.HasValue)
                        {
                            this.txtApplyDate.Text = interlocking.ApplyDate.Value.ToString("f");
                        }
                        this.txtInterlockName.Text = interlocking.InterlockName;
                        if (interlocking.InterlockLevel == "A")
                        {
                            this.txtInterlockLevel.Text = "A级";
                        }
                        if (!string.IsNullOrEmpty(interlocking.ApplyInstallationId))
                        {
                            this.txtApplyUnit.Text = BLL.InstallationService.GetInstallationNameByInstallationId(interlocking.ApplyInstallationId);
                        }
                        else
                        {
                            this.txtApplyUnit.Text = BLL.UnitService.GetUnitNameByUnitId(interlocking.ApplyUnitId);
                        }
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.InterlockingId, 2);
                        if (getPushRecord2 != null)
                        {
                            this.txtUnitHead.Text = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                        }
                        if (!string.IsNullOrEmpty(interlocking.PerformUnitId))
                        {
                            this.txtPerformUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(interlocking.PerformUnitId);
                        }
                        var getPushRecord1 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.InterlockingId, 1);
                        if (getPushRecord1 != null)
                        {
                            this.txtChangeOperator.Text = UserService.GetUserNameByUserId(getPushRecord1.ReceiveManId);
                        }
                        if (interlocking.ChangeTime.HasValue)
                        {
                            this.txtChangeTime.Text = interlocking.ChangeTime.Value.ToString("f");
                        }
                        this.txtLicenseCode.Text = interlocking.LicenseCode;
                        this.txtReason.Text = interlocking.Reason ;
                        if (!string.IsNullOrEmpty(interlocking.SafetyMeasures))
                        {
                            this.txtReason.Text += "\r\n 安全措施：" + interlocking.SafetyMeasures;
                        }
                        if (!string.IsNullOrEmpty(interlocking.ApplyManId))
                        {
                            this.txtApplyManName.Text = BLL.UserService.GetUserNameByUserId(interlocking.ApplyManId);
                        }
                        var getPushRecord3 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.InterlockingId, 3);
                        if (getPushRecord3 != null)
                        {
                            this.txtInstallationOpinion.Text = getPushRecord3.Opinion;
                            this.txtInstallationOpinionName.Text = UserService.GetUserNameByUserId(getPushRecord3.ReceiveManId);
                            if (getPushRecord3.SigningTime.HasValue)
                            {
                                this.txtInstallationOpinionTime.Text = getPushRecord3.SigningTime.Value.ToString("f");
                            }
                        }
                        var getPushRecord4 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.InterlockingId, 4);
                        if (getPushRecord4 != null)
                        {
                            this.txtEquipmentOpinion.Text = getPushRecord4.Opinion;
                            this.txtEquipmentOpinionName.Text = UserService.GetUserNameByUserId(getPushRecord4.ReceiveManId);
                            if (getPushRecord4.SigningTime.HasValue)
                            {
                                this.txtEquipmentOpinionTime.Text = getPushRecord4.SigningTime.Value.ToString("f");
                            }
                        }
                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.InterlockingId, 5);
                        if (getPushRecord5 != null)
                        {
                            this.txtProductionOpinion.Text = getPushRecord5.Opinion;
                            this.txtProductionOpinionName.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                            if (getPushRecord5.SigningTime.HasValue)
                            {
                                this.txtProductionOpinionTime.Text = getPushRecord5.SigningTime.Value.ToString("f");
                            }
                        }
                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.InterlockingId, 6);
                        if (getPushRecord6 != null)
                        {
                            this.txtChiefEngineerOpinion.Text = getPushRecord6.Opinion;
                            this.txtChiefEngineerOpinionName.Text = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                            if (getPushRecord6.SigningTime.HasValue)
                            {
                                this.txtChiefEngineerOpinionTime.Text = getPushRecord6.SigningTime.Value.ToString("f");
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
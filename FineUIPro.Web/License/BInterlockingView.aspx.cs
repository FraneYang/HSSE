using System;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class BInterlockingView : PageBase
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
                this.SimpleForm1.Title = "B级联锁变更审批单";
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
                        if (interlocking.InterlockLevel == "B")
                        {
                            this.txtInterlockLevel.Text = "B级";
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
                        this.txtReason.Text = interlocking.Reason;
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
                            this.txtInstallationMan.Text = BLL.UserService.GetUserNameByUserId(getPushRecord3.ReceiveManId);
                            if (getPushRecord3.SigningTime.HasValue)
                            {
                                this.txtInstallationManTime.Text = getPushRecord3.SigningTime.Value.ToString("f");
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
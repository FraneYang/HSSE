namespace FineUIPro.Web.License
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class ElectricityView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ElectricityId
        {
            get
            {
                return (string)ViewState["ElectricityId"];
            }
            set
            {
                ViewState["ElectricityId"] = value;
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
                this.SimpleForm1.Title = "临时用电安全作业证";
                var thisUnits = BLL.CommonService.GetIsThisUnit();
                if (thisUnits != null)
                {
                    this.SimpleForm1.Title = thisUnits.UnitName + this.Title;
                }
                
                this.ElectricityId = Request.Params["ElectricityId"];
                if (!string.IsNullOrEmpty(this.ElectricityId))
                {
                    var getElectricity = BLL.ElectricityService.GetElectricityById(this.ElectricityId);
                    if (getElectricity != null)
                    {
                        if (!string.IsNullOrEmpty(getElectricity.InstallationId))
                        {
                            this.txtApplyUnit.Text = BLL.InstallationService.GetInstallationNameByInstallationId(getElectricity.InstallationId);
                        }
                        else
                        {
                            this.txtApplyUnit.Text = BLL.UnitService.GetUnitNameByUnitId(getElectricity.UnitId);
                        }
                        this.txtApplyManName.Text = BLL.UserService.GetUserNameByUserId(getElectricity.ApplicantManId);
                        this.txtLicenseCode.Text = getElectricity.LicenseCode;

                        if (getElectricity.JobStartTime.HasValue)
                        {
                            this.txtJobStartTime.Text = "自 " + getElectricity.JobStartTime.Value.ToString("f") + " 至实 ";
                            if (getElectricity.JobEndTime.HasValue)
                            {
                                this.txtJobStartTime.Text += getElectricity.JobEndTime.Value.ToString("f");
                            }
                            this.txtJobStartTime.Text += " 止";
                        }
                        this.txtJobPalce.Text = getElectricity.JobPalce;
                        this.txtAccessPoint.Text = getElectricity.AccessPoint;
                        this.txtWorkingVoltage.Text = getElectricity.WorkingVoltage;
                        this.txtEquipmentPower.Text = getElectricity.EquipmentPower;
                        this.txtMeterReading.Text = getElectricity.MeterReading;
                        this.txtJobManName.Text =getElectricity.JobOtherManName;
                        this.txtJobManCode.Text = getElectricity.JobManCode;
                        if (string.IsNullOrEmpty(getElectricity.HAZIDName))
                        {
                            var hazids = Funs.GetStrListByStr(getElectricity.HAZID, ',');
                            string name = string.Empty;
                            foreach (var ha in hazids)
                            {
                                var haz = BLL.HAZIDService.GetHAZIDByHAZIDId(ha);
                                if (haz != null)
                                {
                                    name += haz.HAZIDName + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(name))
                            {
                                this.txtHAZIDName.Text = name.Substring(0, name.LastIndexOf(","));
                            }
                        }
                        else
                        {
                            this.txtHAZIDName.Text = getElectricity.HAZIDName;
                        }

                        string measuresManName = string.Empty;
                        var getPushRecord1 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.ElectricityId, 2);
                        if (getPushRecord1 != null)
                        {
                            measuresManName = UserService.GetUserNameByUserId(getPushRecord1.ReceiveManId);
                        }

                        var item = from x in Funs.DB.License_LicenseItem where x.DataId == this.ElectricityId select x;
                        if (item.Count() > 0)
                        {
                            var item1 = item.FirstOrDefault(x => x.SortIndex == 1);
                            if (item1 != null)
                            {
                                this.txtSafetyMeasures1.Text = item1.SafetyMeasures;
                                if(item1.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed1.Checked = item1.IsUsed.Value;
                                }
                                
                                this.txtMeasuresManName1.Text = measuresManName;
                            }
                            var item2 = item.FirstOrDefault(x => x.SortIndex == 2);
                            if (item2 != null)
                            {
                                this.txtSafetyMeasures2.Text = item2.SafetyMeasures;
                                if (item2.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed2.Checked = item2.IsUsed.Value;
                                }

                                this.txtMeasuresManName2.Text = measuresManName;
                            }
                            var item3 = item.FirstOrDefault(x => x.SortIndex == 3);
                            if (item3 != null)
                            {
                                this.txtSafetyMeasures3.Text = item3.SafetyMeasures;
                                if (item3.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed3.Checked = item3.IsUsed.Value;
                                }

                                this.txtMeasuresManName3.Text = measuresManName;
                            }
                            var item4 = item.FirstOrDefault(x => x.SortIndex == 4);
                            if (item4 != null)
                            {
                                this.txtSafetyMeasures4.Text = item4.SafetyMeasures;
                                if (item4.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed4.Checked = item4.IsUsed.Value;
                                }

                                this.txtMeasuresManName4.Text = measuresManName;
                            }
                            var item5 = item.FirstOrDefault(x => x.SortIndex == 5);
                            if (item5 != null)
                            {
                                this.txtSafetyMeasures5.Text = item5.SafetyMeasures;
                                if (item5.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed5.Checked = item5.IsUsed.Value;
                                }

                                this.txtMeasuresManName5.Text = measuresManName;
                            }
                            var item6 = item.FirstOrDefault(x => x.SortIndex == 6);
                            if (item6 != null)
                            {
                                this.txtSafetyMeasures6.Text = item6.SafetyMeasures;
                                if (item6.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed6.Checked = item6.IsUsed.Value;
                                }

                                this.txtMeasuresManName6.Text = measuresManName;
                            }
                            var item7 = item.FirstOrDefault(x => x.SortIndex == 7);
                            if (item7 != null)
                            {
                                this.txtSafetyMeasures7.Text = item7.SafetyMeasures;
                                if (item7.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed7.Checked = item7.IsUsed.Value;
                                }

                                this.txtMeasuresManName7.Text = measuresManName;
                            }
                            var item8 = item.FirstOrDefault(x => x.SortIndex == 8);
                            if (item8 != null)
                            {
                                this.txtSafetyMeasures8.Text = item8.SafetyMeasures;
                                if (item8.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed8.Checked = item8.IsUsed.Value;
                                }

                                this.txtMeasuresManName8.Text = measuresManName;
                            }
                            var item9 = item.FirstOrDefault(x => x.SortIndex == 9);
                            if (item9 != null)
                            {
                                this.txtSafetyMeasures9.Text = item9.SafetyMeasures;
                                if (item9.IsUsed.HasValue)
                                {
                                    this.txtSIsUsed9.Checked = item9.IsUsed.Value;
                                }

                                this.txtMeasuresManName9.Text = measuresManName;
                            }

                            
                        }
                        this.txtSafetyMeasures10.Text = getElectricity.OtherMeasures;
                        if (!string.IsNullOrEmpty(getElectricity.OtherMeasures))
                        {
                            this.txtSIsUsed10.Checked = true;
                        }
                        
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.ElectricityId, 2);
                        if (getPushRecord2 != null)
                        {
                            this.txtMeasuresManName10.Text = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                        }

                        var getPushRecord3 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.ElectricityId, 2);
                        if (getPushRecord3 != null)
                        {
                            this.txtSafeEduManName.Text = UserService.GetUserNameByUserId(getPushRecord3.ReceiveManId);
                        }

                        var getPushRecord4 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.ElectricityId, 1);
                        if (getPushRecord4 != null)
                        {
                            this.txtOperationUnitName.Text = UserService.GetUserNameByUserId(getPushRecord4.ReceiveManId);
                            if (getPushRecord4.SigningTime.HasValue)
                            {
                                this.txtOperationUnitTime.Text = getPushRecord4.SigningTime.Value.ToString("f");
                            }
                            this.txtOperationUnitOpinion.Text = getPushRecord4.Opinion;
                        }

                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.ElectricityId, 2);
                        if (getPushRecord5 != null)
                        {
                            this.txtElectricUnitName.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                            if (getPushRecord5.SigningTime.HasValue)
                            {
                                this.txtElectricUnitTime.Text = getPushRecord5.SigningTime.Value.ToString("f");
                            }
                            this.txtElectricUnitOpinion.Text = getPushRecord5.Opinion;
                        }

                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.ElectricityId, 3);
                        if (getPushRecord6 != null)
                        {
                            this.txtAuditDepName.Text = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                            if (getPushRecord6.SigningTime.HasValue)
                            {
                                this.txtAuditDepTime.Text = getPushRecord6.SigningTime.Value.ToString("f");
                            }
                            this.txtAuditDepOpinion.Text = getPushRecord6.Opinion;
                        }
                        var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.ElectricityId, 4);
                        if (getPushRecord7 != null)
                        {
                            this.txtAcceptanceName.Text = UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId);
                            if (getPushRecord7.SigningTime.HasValue)
                            {
                                this.txtAcceptanceTime.Text = getPushRecord7.SigningTime.Value.ToString("f");
                            }
                            this.txtAcceptanceOpinion.Text = getPushRecord7.Opinion;
                        }

                        this.lblBottom.Text = getElectricity.PauseDescribe;
                    }
                }
            }
        }
        #endregion        
    }
}
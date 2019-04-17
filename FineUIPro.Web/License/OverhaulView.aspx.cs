namespace FineUIPro.Web.License
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class OverhaulView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string OverhaulId
        {
            get
            {
                return (string)ViewState["OverhaulId"];
            }
            set
            {
                ViewState["OverhaulId"] = value;
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
                this.SimpleForm1.Title = "检修工作票";
                var thisUnits = BLL.CommonService.GetIsThisUnit();
                if (thisUnits != null)
                {
                    this.SimpleForm1.Title = thisUnits.UnitName + this.Title;
                }
                //this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.OverhaulId = Request.Params["OverhaulId"];
                if (!string.IsNullOrEmpty(this.OverhaulId))
                {
                    var getOverhaul = BLL.OverhaulService.GetOverhaulById(this.OverhaulId);
                    if (getOverhaul != null)
                    {
                        this.lbLicenseCode.Text = getOverhaul.LicenseCode;
                        this.lbSendTicketTime.Text = string.Format("{0:yyyy-MM-dd hh:mm:dd}", getOverhaul.SendTicketTime);
                        this.lbRiskGrade.Text = "作业风险（" + getOverhaul.RiskGrade + ")级";
                        this.txtInstallationName.Text = BLL.InstallationService.GetInstallationNameByInstallationId(getOverhaul.InstallationId);
                        this.txtDevicePositionNum.Text = getOverhaul.DevicePositionNum;
                        this.txtDeviceName.Text = getOverhaul.DeviceName;
                        this.txtUnitName.Text = getOverhaul.OverhaulUnit;
                        this.txtOverhaulCategory.Text = getOverhaul.OverhaulCategory;
                        if (getOverhaul.IsMonthlyPlan == true)
                        {
                            this.txtIsMonthlyPlan.Text = "是";
                        }
                        else
                        {
                            this.txtIsMonthlyPlan.Text = "否";
                        }

                        string estimateTime = "钳工：" + getOverhaul.EstimateTime1+ "            ";
                        estimateTime += "铆焊：" + getOverhaul.EstimateTime2 + "            ";
                        estimateTime += "起重：" + getOverhaul.EstimateTime3 + "            ";
                        estimateTime += "电仪：" + getOverhaul.EstimateTime4 + "            ";
                        this.txtEstimateTime.Text = estimateTime;

                        string actualTime = "钳工：" + getOverhaul.ActualTime1 + "            ";
                        actualTime += "铆焊：" + getOverhaul.ActualTime2 + "            ";
                        actualTime += "起重：" + getOverhaul.ActualTime3 + "            ";
                        actualTime += "电仪：" + getOverhaul.ActualTime4 + "            ";
                        this.txtActualTime.Text = actualTime;

                        this.txtOverhaulContent.Text = getOverhaul.OverhaulContent;
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(getOverhaul.CompileManId);

                        string OverhaulHead = string.Empty;
                        var getPushRecord1 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OverhaulId, 1);
                        if (getPushRecord1 != null)
                        {
                            OverhaulHead = UserService.GetUserNameByUserId(getPushRecord1.ReceiveManId);
                        }
                        this.txtOverhaulHead.Text = OverhaulHead;

                        this.txtOverhaulManIds.Text = getOverhaul.OverhaulManIds;
                        string auditor2 = string.Empty;
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OverhaulId, 2);
                        if (getPushRecord2 != null)
                        {
                            auditor2 = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                        }
                        this.txtAuditor.Text = auditor2;

                        this.txtRemarks.Text = getOverhaul.Remarks;
                        var item = from x in Funs.DB.License_LicenseItem where x.DataId == this.OverhaulId select x;
                        if (item.Count() > 0)
                        {
                            var item1= item.FirstOrDefault(x => x.SortIndex == 1);
                            if(item1 != null && item1.IsUsed == true)
                            {
                                this.CheckBox1.Checked = true;
                            }
                            var item2 = item.FirstOrDefault(x => x.SortIndex == 2);
                            if (item2 != null && item2.IsUsed == true)
                            {
                                this.CheckBox2.Checked = true;
                            }
                            var item3 = item.FirstOrDefault(x => x.SortIndex == 3);
                            if (item3 != null && item3.IsUsed == true)
                            {
                                this.CheckBox3.Checked = true;
                            }
                            var item4 = item.FirstOrDefault(x => x.SortIndex == 4);
                            if (item4 != null && item4.IsUsed == true)
                            {
                                this.CheckBox4.Checked = true;
                            }
                            var item5 = item.FirstOrDefault(x => x.SortIndex == 5);
                            if (item5 != null && item5.IsUsed == true)
                            {
                                this.CheckBox5.Checked = true;
                            }
                            var item6 = item.FirstOrDefault(x => x.SortIndex == 6);
                            if (item6 != null && item6.IsUsed == true)
                            {
                                this.CheckBox6.Checked = true;
                            }
                            var item7 = item.FirstOrDefault(x => x.SortIndex == 7);
                            if (item7 != null && item7.IsUsed == true)
                            {
                                this.CheckBox7.Checked = true;
                            }
                            var item8 = item.FirstOrDefault(x => x.SortIndex == 8);
                            if (item8 != null && item8.IsUsed == true)
                            {
                                this.CheckBox8.Checked = true;
                            }
                            var item9 = item.FirstOrDefault(x => x.SortIndex == 9);
                            if (item9 != null && item9.IsUsed == true)
                            {
                                this.CheckBox9.Checked = true;
                            }
                            var item10 = item.FirstOrDefault(x => x.SortIndex == 10);
                            if (item10 != null && item10.IsUsed == true)
                            {
                                this.CheckBox10.Checked = true;
                            }
                            var item11 = item.FirstOrDefault(x => x.SortIndex == 11);
                            if (item11 != null && item11.IsUsed == true)
                            {
                                this.CheckBox11.Checked = true;
                            }
                            var item12 = item.FirstOrDefault(x => x.SortIndex == 12);
                            if (item12 != null && item12.IsUsed == true)
                            {
                                this.CheckBox12.Checked = true;
                            }
                            var item13 = item.FirstOrDefault(x => x.SortIndex == 13);
                            if (item13 != null && item13.IsUsed == true)
                            {
                                this.CheckBox13.Checked = true;
                            }
                            var item14 = item.FirstOrDefault(x => x.SortIndex == 14);
                            if (item14 != null && item14.IsUsed == true)
                            {
                                this.CheckBox14.Checked = true;
                            }
                            var item15 = item.FirstOrDefault(x => x.SortIndex == 15);
                            if (item15 != null && item15.IsUsed == true)
                            {
                                this.CheckBox15.Checked = true;
                            }
                            var item16 = item.FirstOrDefault(x => x.SortIndex == 16);
                            if (item16 != null && item16.IsUsed == true)
                            {
                                this.CheckBox16.Checked = true;
                            }
                            var item17 = item.FirstOrDefault(x => x.SortIndex == 17);
                            if (item17 != null && item17.IsUsed == true)
                            {
                                this.CheckBox17.Checked = true;
                            }
                        }
                        string processMan3 = string.Empty;
                        var getPushRecord3 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OverhaulId, 3);
                        if (getPushRecord3 != null)
                        {
                            processMan3 = UserService.GetUserNameByUserId(getPushRecord3.ReceiveManId);
                        }
                        this.txtProcessMan.Text = processMan3;

                        this.txtQualifiedTime.Text = "            年            月            日";
                        if (getOverhaul.QualifiedTime.HasValue)
                        {
                            this.txtQualifiedTime.Text = getOverhaul.QualifiedTime.Value.Year.ToString() + "年" + getOverhaul.QualifiedTime.Value.Month.ToString() + "月" + getOverhaul.QualifiedTime.Value.Day.ToString() + "日";
                        }

                        string processMonitor = string.Empty;
                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OverhaulId, 5);
                        if (getPushRecord5 != null)
                        {
                            processMonitor = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                        }
                        this.txtProcessMonitor.Text = processMonitor;

                        string checkOverhaulHead = string.Empty;
                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OverhaulId, 6);
                        if (getPushRecord6 != null)
                        {
                            checkOverhaulHead = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                        }
                        this.txtCheckOverhaulHead.Text = checkOverhaulHead;

                        string checkProcessMonitor = string.Empty;
                        var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.OverhaulId, 7);
                        if (getPushRecord7 != null)
                        {
                            checkProcessMonitor = UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId);
                        }
                        this.txtCheckProcessMonitor.Text = checkProcessMonitor;
                        this.txtTrainEdu.Text = getOverhaul.TrainEdu;

                        //var hazids = Funs.GetStrListByStr(getOverhaul.HAZID, ',');
                        //string name = string.Empty;
                        //foreach (var hitem in hazids)
                        //{
                        //    var haz = BLL.HAZIDService.GetHAZIDByHAZIDId(hitem);
                        //    if (haz != null)
                        //    {
                        //        name += haz.HAZIDName + ",";
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(name))
                        //{
                        //    this.txtHAZIDName.Text = name.Substring(0, name.LastIndexOf(","));
                        //}
                        this.txtHAZIDName.Text = getOverhaul.HAZIDName;
                    }
                }
            }
        }
        #endregion

    }
}
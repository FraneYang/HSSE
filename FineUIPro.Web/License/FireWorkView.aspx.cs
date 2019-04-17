namespace FineUIPro.Web.License
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class FireWorkView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string FireWorkId
        {
            get
            {
                return (string)ViewState["FireWorkId"];
            }
            set
            {
                ViewState["FireWorkId"] = value;
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
                this.SimpleForm1.Title = "动火安全作业证";
                var thisUnits = BLL.CommonService.GetIsThisUnit();
                if (thisUnits != null)
                {
                    this.SimpleForm1.Title = thisUnits.UnitName + this.Title;
                }
                //this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.FireWorkId = Request.Params["FireWorkId"];
                if (!string.IsNullOrEmpty(this.FireWorkId))
                {
                    var getFireWork = BLL.FireWorkService.GetView_License_FireWorkById(this.FireWorkId);
                    if (getFireWork != null)
                    {
                        if (!string.IsNullOrEmpty(getFireWork.ApplyInstallationName))
                        {
                            this.txtApplyUnit.Text = getFireWork.ApplyInstallationName;
                        }
                        else
                        {
                            this.txtApplyUnit.Text = getFireWork.ApplyUnitName;
                        }
                        this.txtLicenseCode.Text = getFireWork.LicenseCode;
                        this.txtApplyManName.Text = getFireWork.ApplyManName;
                        this.cbFireWorkLevel.SelectedValueArray = new string[] { getFireWork.FireWorkLevel };
                        this.txtFireWorkModeName.Text = getFireWork.FireWorkModeName;
                        this.txtFireWorkPalce.Text = getFireWork.FireWorkPalce;
                        if (getFireWork.StartDate.HasValue)
                        {
                            this.txtStartDate.Text = "自 " + getFireWork.StartDate.Value.ToString("f") + " 至实 ";
                            if (getFireWork.EndDate.HasValue)
                            {
                                this.txtStartDate.Text += getFireWork.EndDate.Value.ToString("f");
                            }
                            this.txtStartDate.Text += " 止";
                        }
                        this.txtFireHeaderName.Text = getFireWork.ApplyManName;
                        this.txtFireManName.Text = getFireWork.FireManName;
                        this.txtOtherSpecial.Text = getFireWork.OtherSpecial;
                        //var hazids = Funs.GetStrListByStr(getFireWork.HAZID, ',');
                        //string name = string.Empty;
                        //foreach (var item in hazids)
                        //{
                        //    var haz = BLL.HAZIDService.GetHAZIDByHAZIDId(item);
                        //    if (haz != null)
                        //    {
                        //        name += haz.HAZIDName + ",";
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(name))
                        //{
                        //    this.txtHAZIDName.Text = name.Substring(0, name.LastIndexOf(","));
                        //}
                        this.txtHAZIDName.Text = getFireWork.HAZIDName;
                        if (getFireWork.SIsUsed1.HasValue)
                        {
                            this.txtSIsUsed1.Checked = getFireWork.SIsUsed1.Value;
                        }

                        this.txtSafetyMeasures1.Text = getFireWork.SafetyMeasures1;
                        string measuresManName = string.Empty;
                        var getPushRecord2 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 2);
                        if (getPushRecord2 != null)
                        {
                            measuresManName = UserService.GetUserNameByUserId(getPushRecord2.ReceiveManId);
                        }
                        this.txtMeasuresManName1.Text = measuresManName;
                        if (getFireWork.SIsUsed2.HasValue)
                        {
                            this.txtSIsUsed2.Checked = getFireWork.SIsUsed2.Value;
                        }
                        this.txtSafetyMeasures2.Text = getFireWork.SafetyMeasures2;
                        this.txtMeasuresManName2.Text = measuresManName;
                        if (getFireWork.SIsUsed3.HasValue)
                        {
                            this.txtSIsUsed3.Checked = getFireWork.SIsUsed3.Value;
                        }
                        this.txtSafetyMeasures3.Text = getFireWork.SafetyMeasures3;
                        this.txtMeasuresManName3.Text = measuresManName;
                        if (getFireWork.SIsUsed4.HasValue)
                        {
                            this.txtSIsUsed4.Checked = getFireWork.SIsUsed4.Value;
                        }
                        this.txtSafetyMeasures4.Text = getFireWork.SafetyMeasures4;
                        this.txtMeasuresManName4.Text = measuresManName;
                        if (getFireWork.SIsUsed5.HasValue)
                        {
                            this.txtSIsUsed5.Checked = getFireWork.SIsUsed5.Value;
                        }
                        this.txtSafetyMeasures5.Text = getFireWork.SafetyMeasures5;
                        this.txtMeasuresManName5.Text = getFireWork.ApplyManName;
                        if (getFireWork.SIsUsed6.HasValue)
                        {
                            this.txtSIsUsed6.Checked = getFireWork.SIsUsed6.Value;
                        }
                        this.txtSafetyMeasures6.Text = getFireWork.SafetyMeasures6;
                        this.txtMeasuresManName6.Text = getFireWork.ApplyManName;
                        if (getFireWork.SIsUsed7.HasValue)
                        {
                            this.txtSIsUsed7.Checked = getFireWork.SIsUsed7.Value;
                        }
                        this.txtSafetyMeasures7.Text = getFireWork.SafetyMeasures7;
                        this.txtMeasuresManName7.Text = getFireWork.ApplyManName;
                        if (getFireWork.SIsUsed8.HasValue)
                        {
                            this.txtSIsUsed8.Checked = getFireWork.SIsUsed8.Value;
                        }
                        this.txtSafetyMeasures8.Text = getFireWork.SafetyMeasures8;
                        this.txtMeasuresManName8.Text = getFireWork.ApplyManName;
                        if (getFireWork.SIsUsed9.HasValue)
                        {
                            this.txtSIsUsed9.Checked = getFireWork.SIsUsed9.Value;
                        }
                        this.txtSafetyMeasures9.Text = getFireWork.SafetyMeasures9;
                        this.txtMeasuresManName9.Text = getFireWork.ApplyManName;
                        if (getFireWork.SIsUsed10 == 0)
                        {
                            this.txtSIsUsed10.Checked = false;
                        }
                        else {
                            this.txtSIsUsed10.Checked = true;
                        }
                        this.txtSafetyMeasures10.Text = getFireWork.OtherMeasures;
                        this.txtMeasuresManName10.Text = getFireWork.ApplyManName;

                        var getPushRecord3 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 3);
                        if (getPushRecord3 != null)
                        {
                            this.txtProduceUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord3.ReceiveManId);
                        }


                        this.txtFireWatchName.Text = getFireWork.FireWatchName;
                        var getPushRecord4 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 4);
                        if (getPushRecord4 != null)
                        {
                            this.txtFireFirstManName.Text = UserService.GetUserNameByUserId(getPushRecord4.ReceiveManId);
                        }

                        var getPushRecord5 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 5);
                        if (getPushRecord5 != null)
                        {
                            this.txtSafeEduManName.Text = UserService.GetUserNameByUserId(getPushRecord5.ReceiveManId);
                        }

                        var getPushRecord6 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 6);
                        if (getPushRecord6 != null)
                        {
                            this.txtApplyUnitManName.Text = UserService.GetUserNameByUserId(getPushRecord6.ReceiveManId);
                            if (getPushRecord6.SigningTime.HasValue)
                            {
                                this.txtApplyUnitManTime.Text = getPushRecord6.SigningTime.Value.ToString("f");
                            }
                            this.txtApplyUnitManOpinion.Text = getPushRecord6.Opinion;
                        }


                        var getPushRecord7 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 7);
                        if (getPushRecord7 != null)
                        {
                            this.txtSafeDepManName.Text = UserService.GetUserNameByUserId(getPushRecord7.ReceiveManId);
                            if (getPushRecord7.SigningTime.HasValue)
                            {
                                this.txtSafeDepManTime.Text = getPushRecord7.SigningTime.Value.ToString("f");
                            }
                            this.txtSafeDepManOpinion.Text = getPushRecord7.Opinion;
                        }

                        if (getFireWork.FireWorkLevel == "0")
                        {
                            var getPushRecord8 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 8);
                            if (getPushRecord8 != null)
                            {
                                this.txtFireApprovalManName.Text = UserService.GetUserNameByUserId(getPushRecord8.ReceiveManId);
                                if (getPushRecord8.SigningTime.HasValue)
                                {
                                    this.txtFireApprovalManTime.Text = getPushRecord8.SigningTime.Value.ToString("f");
                                }
                                this.txtFireApprovalManOpinion.Text = getPushRecord8.Opinion;
                            }
                        }
                        else if (getFireWork.FireWorkLevel == "1")
                        {
                            var getPushRecord9 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 9);
                            if (getPushRecord9 != null)
                            {
                                this.txtFireApprovalManName.Text = UserService.GetUserNameByUserId(getPushRecord9.ReceiveManId);
                                if (getPushRecord9.SigningTime.HasValue)
                                {
                                    this.txtFireApprovalManTime.Text = getPushRecord9.SigningTime.Value.ToString("f");
                                }
                                this.txtFireApprovalManOpinion.Text = getPushRecord9.Opinion;
                            }
                        }
                        else
                        {
                            var getPushRecord10 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 10);
                            if (getPushRecord10 != null)
                            {
                                this.txtFireApprovalManName.Text = UserService.GetUserNameByUserId(getPushRecord10.ReceiveManId);
                                if (getPushRecord10.SigningTime.HasValue)
                                {
                                    this.txtFireApprovalManTime.Text = getPushRecord10.SigningTime.Value.ToString("f");
                                }
                                this.txtFireApprovalManOpinion.Text = getPushRecord10.Opinion;
                            }
                        }

                        var getPushRecord11 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 11);
                        if (getPushRecord11 != null)
                        {
                            this.txtInspectTicketManName.Text = UserService.GetUserNameByUserId(getPushRecord11.ReceiveManId);
                            if (getPushRecord11.SigningTime.HasValue)
                            {
                                this.txtInspectTicketManTime.Text = getPushRecord11.SigningTime.Value.ToString("f");
                            }
                            this.txtInspectTicketManOpinion.Text = getPushRecord11.Opinion;
                        }

                        var getPushRecord12 = SysPushRecord.GetPushRecordByDataIdFlowStep(this.FireWorkId, 12);
                        if (getPushRecord12 != null)
                        {
                            this.txtFireWorkManName.Text = UserService.GetUserNameByUserId(getPushRecord12.ReceiveManId);
                            if (getPushRecord12.SigningTime.HasValue)
                            {
                                this.txtFireWorkManTime.Text = getPushRecord12.SigningTime.Value.ToString("f");
                            }
                            this.txtFireWorkManOpinion.Text = getPushRecord12.Opinion;
                        }                        
                        this.lblBottom.Text = getFireWork.PauseDescribe;
                    }
                }
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Analysis.FireWorkAnalysisId,Analysis.FireWorkId,Analysis.SortIndex,Analysis.AnalysisTime,Analysis.AnalysisPoint,"
                         + @"(CAST(Analysis.AnalysisData AS nvarchar(50)) +'('+(CASE WHEN Analysis.IsQualified=0 THEN '不合格' ELSE '合格' END)+')') AS AnalysisData,Analysis.AnalysisMan,Users.UserName AS AnalysisManName"
                         + @" FROM License_FireWorkAnalysis AS Analysis "
                         + @" LEFT JOIN Sys_User AS Users ON Analysis.AnalysisMan =Users.UserId"
                         + @" WHERE Analysis.FireWorkId ='" + this.FireWorkId +"'";
            List<SqlParameter> listStr = new List<SqlParameter>();            
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
    }
}
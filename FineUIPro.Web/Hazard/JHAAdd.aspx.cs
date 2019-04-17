namespace FineUIPro.Web.Hazard
{
    using BLL;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;

    public partial class JHAAdd : PageBase
    {
        #region 定义项
        /// <summary>
        /// 设备设施主键
        /// </summary>
        public string JobActivityId
        {
            get
            {
                return (string)ViewState["JobActivityId"];
            }
            set
            {
                ViewState["JobActivityId"] = value;
            }
        }
        #endregion

        private static List<Model.View_Hazard_JHAItem> viewJHAItems = new List<Model.View_Hazard_JHAItem>();

        /// <summary>
        /// JHA评价编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {          ///权限
                this.GetButtonPower();
                this.JobActivityId = Request.Params["JobActivityId"];
                if (!string.IsNullOrEmpty(this.JobActivityId))
                {
                    var jobActivity = BLL.JobActivityService.GetJobActivityByJobActivityId(this.JobActivityId);
                    if (jobActivity != null)
                    {
                        this.txtJobActivityName.Text = jobActivity.JobActivityName;
                        this.txtJobActivityCode.Text = jobActivity.JobActivityCode;
                        var installation = BLL.InstallationService.GetInstallationByInstallationId(jobActivity.InstallationId);
                        if (installation != null)
                        {
                            this.txtInstallation.Text = installation.InstallationName;
                        }
                        var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(jobActivity.WorkAreaId);
                        if (workArea != null)
                        {
                            this.txtWorkArea.Text = workArea.WorkAreaName;
                        }


                        viewJHAItems = (from x in Funs.DB.View_Hazard_JHAItem where x.JobActivityId == this.JobActivityId orderby x.SortIndex select x).ToList();
                        if (viewJHAItems.Count() == 0)
                        {
                            this.ADDItem();
                        }

                        this.Grid1.DataSource = viewJHAItems;
                        this.Grid1.DataBind();

                    }
                }
            }
        }
                
        #region 增加行
        /// <summary>
        ///  增加行
        /// </summary>
        private void ADDItem()
        {
            Model.View_Hazard_JHAItem newJHAItem = new Model.View_Hazard_JHAItem();
            newJHAItem.JHAItemId = SQLHelper.GetNewID(typeof(Model.Hazard_JHAItem));
            newJHAItem.SortIndex = this.Grid1.Rows.Count() + 1;
            //newJHAItem.HazardJudge_L = 1;
            //newJHAItem.HazardJudge_S = 1;
            //newJHAItem.HazardJudge_R = 1;
            //newJHAItem.HazardJudge_L1 = 1;
            //newJHAItem.HazardJudge_L2 = 1;
            //newJHAItem.HazardJudge_L3 = 1;
            //newJHAItem.HazardJudge_L4 = 1;
            //newJHAItem.HazardJudge_S1 = 1;
            //newJHAItem.HazardJudge_S2 = 1;
            //newJHAItem.HazardJudge_S3 = 1;
            //newJHAItem.HazardJudge_S4 = 1;
            //newJHAItem.HazardJudge_S5 = 1;
            newJHAItem.JobActivityId = this.JobActivityId;
            //newJHAItem.RiskLevel = "1";
            //newJHAItem.RiskLevelName = "一级";
            viewJHAItems.Add(newJHAItem);
        }
        #endregion

        #region 收集页面表格信息
        /// <summary>
        ///  收集页面表格输入信息
        /// </summary>
        /// <param name="drillPlanHalfYearReportId"></param>
        private void ColViewJHAItems()
        {
            viewJHAItems.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                Model.View_Hazard_JHAItem newJHAItem = new Model.View_Hazard_JHAItem
                {
                    JobActivityId = this.JobActivityId
                };
                if (values["JHAItemId"] != null)
                {
                    newJHAItem.JHAItemId = values.Value<string>("JHAItemId");
                }
                if (values["SortIndex"] != null)
                {
                    newJHAItem.SortIndex = values.Value<int?>("SortIndex");
                }
                if (values["JobStep"] != null)
                {
                    newJHAItem.JobStep = values.Value<string>("JobStep");
                }
                //if (values["PossibleAccidents"] != null)
                //{
                //    newJHAItem.PossibleAccidents = values.Value<string>("PossibleAccidents");
                //}
                //if (values["NowControlMeasures"] != null)
                //{
                //    newJHAItem.NowControlMeasures = values.Value<string>("NowControlMeasures");
                //}
                //if (values["HazardJudge_L"] != null)
                //{
                //    newJHAItem.HazardJudge_L = values.Value<decimal?>("HazardJudge_L");
                //}
                //if (values["HazardJudge_L1"] != null)
                //{
                //    newJHAItem.HazardJudge_L1 = values.Value<decimal?>("HazardJudge_L1");
                //}
                //if (values["HazardJudge_L2"] != null)
                //{
                //    newJHAItem.HazardJudge_L2 = values.Value<decimal?>("HazardJudge_L2");
                //}
                //if (values["HazardJudge_L3"] != null)
                //{
                //    newJHAItem.HazardJudge_L3 = values.Value<decimal?>("HazardJudge_L3");
                //}
                //if (values["HazardJudge_L4"] != null)
                //{
                //    newJHAItem.HazardJudge_L4 = values.Value<decimal?>("HazardJudge_L4");
                //}
                //if (values["HazardJudge_S"] != null)
                //{
                //    newJHAItem.HazardJudge_S = values.Value<decimal?>("HazardJudge_S");
                //}
                //if (values["HazardJudge_S1"] != null)
                //{
                //    newJHAItem.HazardJudge_S1 = values.Value<decimal?>("HazardJudge_S1");
                //}
                //if (values["HazardJudge_S2"] != null)
                //{
                //    newJHAItem.HazardJudge_S2 = values.Value<decimal?>("HazardJudge_S2");
                //}
                //if (values["HazardJudge_S3"] != null)
                //{
                //    newJHAItem.HazardJudge_S3 = values.Value<decimal?>("HazardJudge_S3");
                //}
                //if (values["HazardJudge_S4"] != null)
                //{
                //    newJHAItem.HazardJudge_S4 = values.Value<decimal?>("HazardJudge_S4");
                //}
                //if (values["HazardJudge_S5"] != null)
                //{
                //    newJHAItem.HazardJudge_S5 = values.Value<decimal?>("HazardJudge_S5");
                //}
                //if (values["HazardJudge_R"] != null)
                //{
                //    newJHAItem.HazardJudge_R = values.Value<decimal?>("HazardJudge_R");
                //    newJHAItem.RiskLevel = SetRValues(newJHAItem.HazardJudge_R);
                //    var con = ConstValue.GetConstByConstValueAndGroupId(newJHAItem.RiskLevel, ConstValue.Group_RiskLevel);
                //    if (con != null)
                //    {
                //        newJHAItem.RiskLevelName = con.ConstText;
                //    }
                //}

                //if (values["ControlMeasures"] != null)
                //{
                //    newJHAItem.ControlMeasures = values.Value<string>("ControlMeasures");
                //}
                //if (values["ManagementMeasures"] != null)
                //{
                //    newJHAItem.ManagementMeasures = values.Value<string>("ManagementMeasures");
                //}
                //if (values["ProtectiveMeasures"] != null)
                //{
                //    newJHAItem.ProtectiveMeasures = values.Value<string>("ProtectiveMeasures");
                //}
                //if (values["OtherMeasures"] != null)
                //{
                //    newJHAItem.OtherMeasures = values.Value<string>("OtherMeasures");
                //}
                if (!string.IsNullOrEmpty(newJHAItem.JHAItemId))
                {
                    viewJHAItems.Add(newJHAItem);
                }
            }
        }
        #endregion

        #region 根据R值取风险等级
        private string SetRValues(decimal? rValue)
        {
            string riskLevel = "1";
            var riskLevelValue = Funs.DB.Base_RiskLevelValue.FirstOrDefault(x => x.Identification == "JHA" && x.MinValue <= rValue && x.MaxValue >= rValue);
            if (riskLevelValue != null)
            {
                riskLevel = riskLevelValue.RiskLevelId;
            }
            
            return riskLevel;
        }
        #endregion

        # region 增加按钮事件
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.ColViewJHAItems();
            this.ADDItem();
            Grid1.DataSource = viewJHAItems;
            Grid1.DataBind();
        }
        #endregion

        #region Grid1行点击事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();          
            if (e.CommandName == "Delete")
            {
                this.ColViewJHAItems();
                foreach (var item in viewJHAItems)
                {
                    if (item.JHAItemId == rowID)
                    {
                        viewJHAItems.Remove(item);
                        break;
                    }
                }                
                Grid1.DataSource = viewJHAItems;
                Grid1.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.ColViewJHAItems();
            var oldJHAItem = JHAItemService.GetJHAItemListByJobActivityId(this.JobActivityId);
            if (oldJHAItem.Count() > 0)
            {
                foreach (var item in oldJHAItem)
                {
                    if (viewJHAItems.FirstOrDefault(x => x.JHAItemId == item.JHAItemId) == null)
                    {
                        BLL.JHAItemService.DeleteJHAItemById(item.JHAItemId);
                    }
                }
            }

            if (viewJHAItems.Count() > 0)
            {
                foreach (var itemView in viewJHAItems)
                {                    
                    Model.Hazard_JHAItem newJHAItem = new Model.Hazard_JHAItem
                    {
                        JHAItemId = itemView.JHAItemId,
                        JobActivityId = this.JobActivityId,
                        SortIndex = itemView.SortIndex,
                        JobStep = itemView.JobStep,                        
                    };

                    var oldSetItem = BLL.JHAItemService.GetJHAItemById(itemView.JHAItemId);
                    if (oldSetItem != null)
                    {
                        newJHAItem.PossibleAccidents = oldSetItem.PossibleAccidents;
                        newJHAItem.NowControlMeasures = oldSetItem.NowControlMeasures;
                        newJHAItem.HazardJudge_L = oldSetItem.HazardJudge_L;
                        newJHAItem.HazardJudge_L1 = oldSetItem.HazardJudge_L1;
                        newJHAItem.HazardJudge_L2 = oldSetItem.HazardJudge_L2;
                        newJHAItem.HazardJudge_L3 = oldSetItem.HazardJudge_L3;
                        newJHAItem.HazardJudge_L4 = oldSetItem.HazardJudge_L4;
                        newJHAItem.HazardJudge_S = oldSetItem.HazardJudge_S;
                        newJHAItem.HazardJudge_S1 = oldSetItem.HazardJudge_S1;
                        newJHAItem.HazardJudge_S2 = oldSetItem.HazardJudge_S2;
                        newJHAItem.HazardJudge_S3 = oldSetItem.HazardJudge_S3;
                        newJHAItem.HazardJudge_S4 = oldSetItem.HazardJudge_S4;
                        newJHAItem.HazardJudge_S5 = oldSetItem.HazardJudge_S5;
                        newJHAItem.HazardJudge_R = oldSetItem.HazardJudge_R;
                        newJHAItem.ControlMeasures = oldSetItem.ControlMeasures;
                        newJHAItem.ManagementMeasures = oldSetItem.ManagementMeasures;
                        newJHAItem.ProtectiveMeasures = oldSetItem.ProtectiveMeasures;
                        newJHAItem.OtherMeasures = oldSetItem.OtherMeasures;
                        newJHAItem.RiskLevel = oldSetItem.RiskLevel;
                    }

                    if (oldJHAItem.Count() > 0 && oldJHAItem.FirstOrDefault(x => x.JHAItemId == newJHAItem.JHAItemId) != null)
                    {
                        JHAItemService.UpdateJHAItem(newJHAItem);
                        LogService.AddLog(this.CurrUser.UserId, "添加JHA评价明细");
                    }
                    else
                    {
                        JHAItemService.AddJHAItem(newJHAItem);
                        LogService.AddLog(this.CurrUser.UserId, "修改JHA评价明细");
                    }

                    var jobActivity = BLL.JobActivityService.GetJobActivityByJobActivityId(this.JobActivityId);
                    if (jobActivity != null)
                    {
                        jobActivity.States = "1";
                        var jhaItem = Funs.DB.Hazard_JHAItem.FirstOrDefault(x => x.JobActivityId == this.JobActivityId);
                        if (jhaItem == null)
                        {
                            jobActivity.States = "0";
                        }
                        else
                        {
                            jobActivity.States = "1";
                        } BLL.JobActivityService.UpdateJobActivity(jobActivity);
                    }

                    //Model.Hazard_JHAItemRecord newJHAItemRecord = new Model.Hazard_JHAItemRecord
                    //{
                    //    JHAItemRecordId = BLL.SQLHelper.GetNewID(typeof(Model.Hazard_JHAItemRecord)),
                    //    JHAItemId = newJHAItem.JHAItemId,
                    //    EvaluationTime = DateTime.Now,
                    //    EvaluatorId = this.CurrUser.UserId,
                    //    RiskLevel = newJHAItem.RiskLevel,
                    //};

                    //BLL.JHAItemRecordService.AddJHAItemRecord(newJHAItemRecord);
                    /////插入综合测评
                   // BLL.AppraisalScoreService.GetAppraisalScore(this.CurrUser.UserId, BLL.Const.JHAMenuId, 1, newJHAItem.JHAItemId);
                }
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.JHAMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnAdd.Hidden = false;
                }
            }
        }
        #endregion
    }
}
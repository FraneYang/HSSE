using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SafeAnalysis
{
    public partial class HazardAnalyse : PageBase
    {
        #region 定义项
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallation, string.Empty, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpChartType, ConstValue.Group_ChartType, false);
                this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                this.AnalyseData();
            }
        }
        #endregion

        #region 统计
        /// <summary>
        /// 统计方法
        /// </summary>
        private void AnalyseData()
        {
            List<string> installationValues = new List<string>();
            foreach (ListItem item in this.drpInstallation.SelectedItemArray)
            {
                var ins = BLL.InstallationService.GetInstallationByInstallationId(item.Value);
                if (ins != null)
                {
                    installationValues.Add(item.Value);
                }
            }

            if (installationValues.Count() == 0)
            {
                installationValues = BLL.InstallationService.GetInstallationByNoInstallTypeList("1").Select(x => x.InstallationId).ToList();
            }
            ////危险观察等级
            var registrationList = from x in Funs.DB.Hazard_HiddenHazard
                                   where x.States == "4"
                                   select x;
            DateTime? startDate = Funs.GetNewDateTime(this.txtStartDate.Text);
            DateTime? endDate = Funs.GetNewDateTime(this.txtEndDate.Text);
            if (startDate.HasValue)
            {
                registrationList = registrationList.Where(x => x.CorrectTime.Value.AddDays(1) > startDate.Value);
            }
            if (endDate.HasValue)
            {
                registrationList = registrationList.Where(x => x.CorrectTime.Value.AddDays(-1) < endDate.Value);
            }
            var HiddenHazardTypeIdList = registrationList.Select(x => x.HiddenHazardTypeId).Distinct();

            #region 按装置统计
            ///按装置统计           
            DataTable dtHazard = new DataTable();
            dtHazard.Columns.Add("装置", typeof(string));
            foreach (var itemType in HiddenHazardTypeIdList)
            {
                var type = Funs.DB.Base_HiddenHazardType.FirstOrDefault(x => x.HiddenHazardTypeId == itemType);
                if (type != null)
                {
                    dtHazard.Columns.Add(type.HiddenHazardTypeName, typeof(string));
                }
                else
                {
                    dtHazard.Columns.Add("其他", typeof(string));
                }
            }

            foreach (var iteminstallation in installationValues)
            {
                DataRow rowinstallation = dtHazard.NewRow();
                rowinstallation["装置"] = BLL.InstallationService.GetInstallationNameByInstallationId(iteminstallation);
                foreach (var itemType in HiddenHazardTypeIdList)
                {
                    var type = Funs.DB.Base_HiddenHazardType.FirstOrDefault(x => x.HiddenHazardTypeId == itemType);
                    if (type != null)
                    {
                        rowinstallation[type.HiddenHazardTypeName] = registrationList.Where(x => x.InstallationId == iteminstallation && x.HiddenHazardTypeId == itemType).Count();
                    }
                    else
                    {
                        rowinstallation["其他"] = registrationList.Where(x => x.InstallationId == iteminstallation && x.HiddenHazardTypeId == itemType).Count();
                    }                   
                }

                dtHazard.Rows.Add(rowinstallation);
            }

            this.gvHazard.DataSource = dtHazard;
            this.gvHazard.DataBind();
            this.ChartCostTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtHazard, "隐患统计", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));
            #endregion
        }
        #endregion

        #region 清空
        /// <summary>
        /// 清空下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpInstallation_ClearIconClick(object sender, EventArgs e)
        {
            this.drpInstallation.SelectedValue = BLL.Const._Null;
            this.AnalyseData();
        }
        #endregion

        #region 统计查询      
        protected void ckbShow_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.AnalyseData();
        }

        /// <summary>
        /// 统计分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            this.AnalyseData();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SafeAnalysis
{
    public partial class LicenseAnalysis : PageBase
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
                ConstValue.InitConstValueDropDownList(this.drpChartType, ConstValue.Group_ChartType, false);
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
            var licenseTypes = ConstValue.drpConstItemList(BLL.ConstValue.Group_LicenseType);           
            DateTime? startDate = Funs.GetNewDateTime(this.txtStartDate.Text);
            DateTime? endDate = Funs.GetNewDateTime(this.txtEndDate.Text);
            #region 按装置统计
            ///按装置统计           
            DataTable dtHazard = new DataTable();
            dtHazard.Columns.Add("类型", typeof(string));
            dtHazard.Columns.Add("数量", typeof(string));
            foreach (var item in licenseTypes)
            {
                DataRow rowLicenseTypes = dtHazard.NewRow();
                rowLicenseTypes["类型"] = item.ConstText;
                if (item.ConstValue == "656C760C-27A8-402C-B875-E2B80CC6E577") ///检修
                {
                    var License = from x in Funs.DB.License_Overhaul
                                   where startDate.HasValue && x.SendTicketTime > startDate.Value.AddDays(-1) && endDate.HasValue && x.SendTicketTime < endDate.Value.AddDays(1)
                                   select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "68E502C6-3993-4669-8E4C-64E820AD5100") ///临电
                {
                    var License = from x in Funs.DB.License_Electricity
                                   where startDate.HasValue && x.JobStartTime > startDate.Value.AddDays(-1) && endDate.HasValue && x.JobStartTime < endDate.Value.AddDays(1)
                                   select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "6A583619-B499-48A1-8BA1-6BC140741C06") ///盲板抽堵
                {
                    var License = from x in Funs.DB.License_BlindPlate
                                  where startDate.HasValue && x.EffectiveDate > startDate.Value.AddDays(-1) && endDate.HasValue && x.EffectiveDate < endDate.Value.AddDays(1)
                                  select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "27147897-4091-4EC5-96D3-2914ECDE0384") ///动火
                {
                    var License = from x in Funs.DB.License_FireWork
                                   where startDate.HasValue && x.StartDate > startDate.Value.AddDays(-1) && endDate.HasValue && x.StartDate < endDate.Value.AddDays(1)
                                   select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "45A92A2E-0AD7-400F-A309-F70BBEF01617") ///登高
                {
                    var License = from x in Funs.DB.License_HeightWork
                                  where startDate.HasValue && x.JobStartTime > startDate.Value.AddDays(-1) && endDate.HasValue && x.JobStartTime < endDate.Value.AddDays(1)
                                  select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "E8B4B410-74A3-45D7-8A73-9088FCB56959") ///受限空间
                {
                    var License = from x in Funs.DB.License_LimitedSpace
                                  where startDate.HasValue && x.JobStartTime > startDate.Value.AddDays(-1) && endDate.HasValue && x.JobStartTime < endDate.Value.AddDays(1)
                                  select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "20DFBC0F-2D53-4A3F-800F-CC4383A0B328") ///断路
                {
                    var License = from x in Funs.DB.License_OpenCircuit
                                  where startDate.HasValue && x.JobStartTime > startDate.Value.AddDays(-1) && endDate.HasValue && x.JobStartTime < endDate.Value.AddDays(1)
                                  select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "94D5A962-CB10-4392-84ED-E62DF7D1778D") ///动土
                {
                    var License = from x in Funs.DB.License_BreakGround
                                  where startDate.HasValue && x.JobStartTime > startDate.Value.AddDays(-1) && endDate.HasValue && x.JobStartTime < endDate.Value.AddDays(1)
                                  select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "D04F5626-7646-430F-9DBA-811B78401982") ///吊装
                {
                    var License = from x in Funs.DB.License_LiftingWork
                                  where startDate.HasValue && x.JobStartTime > startDate.Value.AddDays(-1) && endDate.HasValue && x.JobStartTime < endDate.Value.AddDays(1)
                                  select x;
                    rowLicenseTypes["数量"] = License.Count();
                }
                if (item.ConstValue == "F12FD53C-FE7F-4145-B138-EC641B138D5E") ///联锁
                {
                    var License = from x in Funs.DB.License_Interlocking
                                  where startDate.HasValue && x.ApplyDate > startDate.Value.AddDays(-1) && endDate.HasValue && x.ApplyDate < endDate.Value.AddDays(1)
                                  select x;
                    rowLicenseTypes["数量"] = License.Count();
                }

                dtHazard.Rows.Add(rowLicenseTypes);
            }

            this.gvHazard.DataSource = dtHazard;
            this.gvHazard.DataBind();
            this.ChartCostTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtHazard, "隐患统计", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));
            #endregion
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
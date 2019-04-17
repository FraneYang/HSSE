namespace FineUIPro.Web.Appraisal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class AppraisalScore : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                         
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_Year, false);
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonth, BLL.ConstValue.Group_Month, false);

                BLL.DepartService.InitDepartDropDownList(this.drpDepart, true);
                BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallation, this.drpDepart.SelectedValue, true);
                this.drpYear.SelectedValue = System.DateTime.Now.Year.ToString();
                this.drpMonth.SelectedValue = System.DateTime.Now.Month.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            //string strSql = @"SELECT sysUser.UserId,depart.DepartName,sysUser.InstallationName, sysUser.UserId,sysUser.UserName,CONVERT(varchar(7),OperationTime,120) AS OperationTime,SUM(Score) AS ToltalScore,Test.TestScores,Person.PersonScore"
            //            + @" FROM Sys_User AS sysUser"
            //            + @" LEFT JOIN dbo.Appraisal_AppraisalScore AS AppraisalScore ON AppraisalScore.UserId = sysUser.UserId"
            //            + @" LEFT JOIN Base_Depart AS depart ON sysUser.DepartId =depart.DepartId"
            //            + @" LEFT JOIN (SELECT MAX(TestScores) AS TestScores,TestManId,CONVERT(varchar(7),TestEndTime,120) AS TestMonth FROM Training_TestRecord GROUP BY TestManId,CONVERT(varchar(7), TestEndTime, 120)) AS Test 
            //                     ON AppraisalScore.UserId=Test.TestManId AND CONVERT(varchar(7),OperationTime,120)=Test.TestMonth"
            //            + @" LEFT JOIN (SELECT SUM(Score) AS PersonScore,ProblemManId,CONVERT(varchar(7),FindTime,120) AS FindTimeMonth FROM Appraisal_PersonAppraisal GROUP BY ProblemManId,CONVERT(varchar(7), FindTime, 120)) AS Person 
            //                     ON AppraisalScore.UserId=Person.ProblemManId AND CONVERT(varchar(7),OperationTime,120)=Person.FindTimeMonth "
            //            + @" WHERE (SUM(Score) IS NOT NULL OR Test.TestScores IS NOT NULL OR Person.PersonScore IS NOT NULL)";
            //List<SqlParameter> listStr = new List<SqlParameter>();
            //if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            //{
            //    strSql += " AND (sysUser.UserName LIKE @Name OR sysUser.InstallationName LIKE @Name OR depart.DepartName LIKE @Name)";
            //    listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            //}
            //if (!string.IsNullOrEmpty(this.drpYear.SelectedValue) && this.drpYear.SelectedValue != BLL.Const._Null)
            //{
            //    strSql += " AND (YEAR(OperationTime) =@year)";
            //    listStr.Add(new SqlParameter("@year", this.drpYear.SelectedValue));
            //}
            //if (!string.IsNullOrEmpty(this.drpYear.SelectedValue) && this.drpYear.SelectedValue != BLL.Const._Null)
            //{
            //    strSql += " AND (Month(OperationTime) =@month)";
            //    listStr.Add(new SqlParameter("@month", this.drpMonth.SelectedValue));
            //}
            //strSql += @" GROUP BY depart.DepartName,sysUser.InstallationName,sysUser.UserId,sysUser.UserName,CONVERT(varchar(7),OperationTime,120),Test.TestScores,Person.PersonScore";
            //SqlParameter[] parameter = listStr.ToArray();
            //DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            //Grid1.RecordCount = tb.Rows.Count;
            //
            //var table = this.GetPagedDataTable(Grid1, tb);
            //Grid1.DataSource = table;
            //Grid1.DataBind();

            string name = null;
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                name = "%" + this.txtName.Text.Trim() + "%";
            }

            string departId = null;
            if (this.drpDepart.SelectedValue != BLL.Const._Null)
            {
                departId = this.drpDepart.SelectedValue;
            }
            string installationId = null;
            if (this.drpInstallation.SelectedValue != BLL.Const._Null)
            {
                installationId = this.drpInstallation.SelectedValue;
            }

            DateTime dateTime = Funs.GetNewDateTimeOrNow(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue);
            SqlParameter[] values = new SqlParameter[]
                {
                    new SqlParameter("@name", name),
                    new SqlParameter("@YearMonth", dateTime),
                    new SqlParameter("@departId", departId),
                    new SqlParameter("@installationId", installationId),
                };

            DataTable tb = SQLHelper.GetDataTableRunProc("Sp_Appraisal_AppraisalScore", values);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);

            Grid1.RecordCount = tb.Rows.Count;
            //
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
            this.Grid1.PageIndex = 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDepart_TextChanged(object sender, EventArgs e)
        {
            BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallation, this.drpDepart.SelectedValue, true);
            this.BindGrid();
            this.Grid1.PageIndex = 0;
        }
        #endregion 

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {           
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 导出按钮  
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("人员测评" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}
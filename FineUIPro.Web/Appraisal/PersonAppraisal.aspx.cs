namespace FineUIPro.Web.Appraisal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class PersonAppraisal : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT P.PersonAppraisalId,P.FindManId,FindUser.UserName AS FindManName,P.FindTime,P.InstallationId,Installation.InstallationName,P.Place,P.ProblemManId"
                          + @" ,ProblemUser.UserName AS ProblemManName,P.PohotoUrl,P.Score,P.AuditManId,AuditUser.UserName AS AuditManName,P.AuditTime,P.CheckItem,P.States,P.GetScore"
                          + @" ,(CASE WHEN P.States=-1 THEN '已作废' WHEN P.States=2 THEN '审核完成'  WHEN P.States=1 THEN '待审核' ELSE '待提交' END) AS StatesName"
                          + @" FROM dbo.Appraisal_PersonAppraisal AS P"
                          + @" LEFT JOIN Base_Installation AS Installation ON P.InstallationId = Installation.InstallationId"
                          + @" LEFT JOIN Sys_User AS FindUser ON P.FindManId = FindUser.UserId"
                          + @" LEFT JOIN Sys_User AS ProblemUser ON P.ProblemManId = ProblemUser.UserId"
                          +@" LEFT JOIN Sys_User AS AuditUser ON P.AuditManId = AuditUser.UserId"
                          + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtContent.Text.Trim()))
            {
                strSql += " AND (FindUser.UserName LIKE @Content OR Installation.InstallationName LIKE @Content OR ProblemUser.UserName LIKE @Content OR AuditUser.UserName LIKE @Content OR P.CheckItem LIKE @Content)";
                listStr.Add(new SqlParameter("@Content", "%" + this.txtContent.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        
        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.PersonAppraisalService.DeletePersonAppraisalById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除测评记录");
                    //var InterEmergencyings = BLL.InterEmergencyingService.GetInterEmergencyingById(rowID);
                    //if (InterEmergencyings != null)
                    //{                        
                    //}
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
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
        
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.PersonAppraisalMenuId);
            if (buttonList.Count() > 0)
            {               
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion 
    }
}
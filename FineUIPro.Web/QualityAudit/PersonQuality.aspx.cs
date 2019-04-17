using System;
using System.Collections.Generic;
using BLL;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.QualityAudit
{
    public partial class PersonQuality : PageBase
    {
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
                GetButtonPower();//权限设置
                this.BindGrid();
            }
        }
        #endregion

        #region BindGrid
        /// <summary>
        ///  gv 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT (CASE WHEN PersonQuality.PersonQualityId IS NULL THEN users.UserId ELSE PersonQuality.PersonQualityId END) AS ID,PersonQuality.PersonQualityId, users.UserId,users.InstallationId,users.UnitId,users.UserCode,users.UserName,users.WorkPostId,users.InstallationName,users.IsPost,"
                         + @" users.WorkPostName,units.UnitName,depart.DepartName,PersonQuality.CertificateNo,cer.CertificateName,Unit.UnitName AS SendUnit,PersonQuality.SendDate,PersonQuality.LimitDate,PersonQuality.CompileDate"
                         + @" FROM dbo.Sys_User AS users"
                         + @" LEFT JOIN Base_Unit AS units ON users.UnitId=units.UnitId"
                         + @" LEFT JOIN Base_WorkPost AS workPost ON workPost.WorkPostId=users.WorkPostId"
                         + @" LEFT JOIN Base_Depart AS depart ON depart.DepartId = users.DepartId"
                         + @" LEFT JOIN QualityAudit_PersonQuality AS PersonQuality ON PersonQuality.PersonId =users.UserId "
                         + @" LEFT JOIN Base_Certificate AS cer ON cer.CertificateId=PersonQuality.CertificateId"
                         + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=PersonQuality.SendUnit"
                         + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.drpPersonQuality.SelectedValue == "1")
            {
                strSql += " AND workPost.PostType=@postType";
                listStr.Add(new SqlParameter("@postType", Const.PostType_2));
            }
            else if (this.drpPersonQuality.SelectedValue == "2")
            {
                strSql += " AND (workPost.PostType<>@postType OR workPost.PostType is null)";
                listStr.Add(new SqlParameter("@postType", Const.PostType_2));
            }
            if (this.isPost.Checked)
            {
                strSql += " AND (users.IsPost = 0 OR users.IsPost IS NULL)";
            }

            if (!string.IsNullOrEmpty(this.txtName.Text))
            {
                strSql += " AND (users.WorkPostName LIKE @Name OR users.UserName LIKE @Name OR users.UserCode LIKE @Name OR PersonQuality.CertificateNo LIKE @Name OR cer.CertificateName LIKE @Name OR Unit.UnitName LIKE @Name OR PersonQuality.ProspectiveNames LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }
            if (this.drpDataType.SelectedValue == "1")
            {
                if (!string.IsNullOrEmpty(this.txtStartDate.Text))
                {
                    strSql += " AND PersonQuality.LimitDate >= @StartDate";
                    listStr.Add(new SqlParameter("@StartDate", Funs.GetNewDateTime(this.txtStartDate.Text)));
                }
                if (!string.IsNullOrEmpty(this.txtEndDate.Text))
                {
                    strSql += " AND PersonQuality.LimitDate <= @EndDate";
                    listStr.Add(new SqlParameter("@EndDate", Funs.GetNewDateTime(this.txtEndDate.Text)));
                }
            }
            else if (this.drpDataType.SelectedValue == "2")
            {
                if (!string.IsNullOrEmpty(this.txtStartDate.Text))
                {
                    strSql += " AND PersonQuality.CompileDate >= @StartDate";
                    listStr.Add(new SqlParameter("@StartDate", Funs.GetNewDateTime(this.txtStartDate.Text)));
                }
                if (!string.IsNullOrEmpty(this.txtEndDate.Text))
                {
                    strSql += " AND PersonQuality.CompileDate <= @EndDate";
                    listStr.Add(new SqlParameter("@EndDate", Funs.GetNewDateTime(this.txtEndDate.Text)));
                }
            }
            else if (this.drpDataType.SelectedValue == "3")
            {
                if (!string.IsNullOrEmpty(this.txtStartDate.Text))
                {
                    strSql += " AND PersonQuality.LateCheckDate >= @StartDate";
                    listStr.Add(new SqlParameter("@StartDate", Funs.GetNewDateTime(this.txtStartDate.Text)));
                }
                if (!string.IsNullOrEmpty(this.txtEndDate.Text))
                {
                    strSql += " AND PersonQuality.LateCheckDate <= @EndDate";
                    listStr.Add(new SqlParameter("@EndDate", Funs.GetNewDateTime(this.txtEndDate.Text)));
                }
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                var personQuality = BLL.PersonQualityService.GetPersonQualityById(Grid1.Rows[i].DataKeys[0].ToString());
                if (personQuality != null && personQuality.LimitDate < System.DateTime.Now)
                {
                    Grid1.Rows[i].RowCssClass = "Red";
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

        #region 分页下拉选择
        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Funs.GetNewIntOrZero(this.ddlPageSize.SelectedValue);
            this.BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.PersonQualityMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnMenuEdit.Hidden = false;
                    this.btnImport.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 关闭窗口事件
        /// <summary>
        /// 关闭窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑事件
        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
        }
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        private void EditData()
        {
            string Id = Grid1.SelectedRowID;
            var personQuality = BLL.PersonQualityService.GetPersonQualityById(Id);
            if (personQuality != null)
            {
                Id = personQuality.PersonId;
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonQualityEdit.aspx?PersonId={0}", Id, "编辑人员资质 - ")));
        }
        #endregion

        protected void isPost_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.BindGrid();
        }

        #region 删除事件
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
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
                    BLL.PersonQualityService.DeletePersonQualityById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除人员资质");
                }
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("人员资质" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonQualityIn.aspx", "导入 - "), "导入", 1024, 560));
        }
        #endregion
    }
}
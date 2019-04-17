namespace FineUIPro.Web.SafeAnalysis
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using AspNet = System.Web.UI.WebControls;

    public partial class HazardTypeAnalyse : PageBase
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
                BLL.HiddenHazardTypeService.InitHiddenHazardTypeRadioButtonList(this.ckType);              
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT HiddenHazardId,HiddenHazardCode,HiddenHazardName,FindTime,CorrectManName,CorrectUnitName,IsMajorName,CorrectMeasures,CorrectMoney,CorrectTime,AcceptanceManName,ReasonAnalysis,HiddenHazardTypeId,HiddenHazardTypeName,'' AS Remark"
                        + @" FROM View_Hazard_HiddenHazard"
                        + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.ckType.SelectedValue) && this.ckType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND HiddenHazardTypeId=@Type";
                listStr.Add(new SqlParameter("@Type", this.ckType.SelectedValue));
            }

            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (HiddenHazardCode LIKE @Name OR HiddenHazardName LIKE @Name OR CorrectManName LIKE @Name OR IsMajorName LIKE @Name OR CorrectUnitName LIKE @Name OR CorrectMeasures LIKE @Name  OR AcceptanceManName LIKE @Name  OR ReasonAnalysis LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
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

        /// <summary>
        /// 类型选择联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckType_SelectedIndexChanged(object sender, EventArgs e)
        {         
            this.BindGrid();
        }


        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(this.ckType.SelectedItem.Text + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }        
    }
}
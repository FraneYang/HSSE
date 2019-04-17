namespace FineUIPro.Web.Standard
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class HSSEStandardView : PageBase
    {
        #region 管理项目主键
        /// <summary>
        /// 管理项目主键
        /// </summary>
        public string ManagedItemId
        {
            get
            {
                return (string)ViewState["ManagedItemId"];
            }
            set
            {
                ViewState["ManagedItemId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ManagedItemId = Request.Params["ManagedItemId"];
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
            string strSql = @"SELECT * FROM dbo.View_Standard_HSSEStandard "                      
                        + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.ManagedItemId))
            {
                strSql += " AND ManagedItemId =@ManagedItemId";
                listStr.Add(new SqlParameter("@ManagedItemId", this.ManagedItemId));
            }

            if (!string.IsNullOrEmpty(this.txtName.Text))
            {
                strSql += " AND (ManagedItemName LIKE @Name OR Standards.StandardName LIKE @Name OR Specialty.SpecialtyName LIKE @Name OR ManagedObject.ManagedObjectName LIKE @Name)";
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

        #region 专家辅助下载原文
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            var view = BLL.Funs.DB.View_Standard_ManagedItem.FirstOrDefault(x => x.ManagedItemId == this.ManagedItemId);
            if(view != null)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Standard&type=-1", view.StandardId)));
            }            
        }
        #endregion

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEStandardEdit.aspx?HSSEStandardId={0}&type=0", Grid1.SelectedRowID, "修改 - ")));
        }
    }
}
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.common
{
    public partial class main : PageBase
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
                BindGridToDoMatter("close");
                BindGridNotice("close");
                BindGridHSSEStandard("close");
                this.ShowNewsPic();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGridHSSEStandard")
                {
                    BindGridHSSEStandard("close");
                }
                if (GetRequestEventArgument() == "reloadGridNotice")
                {
                    BindGridNotice("close");
                }
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGridToDoMatter("close");
                }
            }
        }
        #endregion

        #region 待办事项
        /// <summary>
        /// 绑定数据(待办事项)
        /// </summary>
        private void BindGridToDoMatter(string type)
        {
            //var q = from x in Funs.DB.View_ToDoMatter where x.UserId == this.CurrUser.UserId select x;
            
            //var toDoMatterList = q.ToList();
            //var dataIdList = (from x in Funs.DB.Sys_FlowOperate
            //                  where x.OperaterId == this.CurrUser.UserId && (x.IsClosed == false || !x.IsClosed.HasValue)
            //                  select x).ToList();
            //if (dataIdList.Count() > 0)
            //{
            //    foreach (var item in dataIdList)
            //    {
            //        Model.View_ToDoMatter newTodo = new Model.View_ToDoMatter();
            //        newTodo.Id = item.DataId;
            //        var menu = BLL.SysMenuService.GetSysMenuByMenuId(item.MenuId);
            //        if (menu != null)
            //        {
            //            newTodo.Type = menu.MenuName;
            //            if (!string.IsNullOrEmpty(item.Url))
            //            {
            //                string newUrl = item.Url.Replace("View.aspx", "Edit.aspx");
            //                newTodo.Url = String.Format(newUrl, item.DataId, "审核 - ");
            //            }
            //        }
            //        else
            //        {
            //            newTodo.Type = "项目单据";
            //        }  
            //        newTodo.Date = item.OperaterTime;
            //        newTodo.UserId = item.OperaterId;
            //        toDoMatterList.Add(newTodo);
            //    }
            //}
          
            //if (type != "oper")
            //{          
            //    toDoMatterList = toDoMatterList.Take(6).ToList();
            //}
            //DataTable tb = this.LINQToDataTable(toDoMatterList);
            //// 2.获取当前分页数据
            ////var table = this.GetPagedDataTable(GridHSSEStandard, tb1);
            //GridToDoMatter.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(GridToDoMatter.FilteredData, tb);
            //var table = this.GetPagedDataTable(GridToDoMatter, tb);

            //GridToDoMatter.DataSource = table;
            //GridToDoMatter.DataBind();
        }

        /// <summary>
        /// Grid行双击事件(待办事项)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridToDoMatter_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            //if (GridToDoMatter.SelectedRow != null)
            //{
            //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridToDoMatter.SelectedRow.Values[3].ToString()), "待办事项：" + GridToDoMatter.SelectedRow.Values[0].ToString()));
            //}
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen3_Click(object sender, EventArgs e)
        {
            this.BindGridToDoMatter("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose3_Click(object sender, EventArgs e)
        {
            this.BindGridToDoMatter("close");
        }
        #endregion

        #region 通知通过
        /// <summary>
        /// 绑定数据(通知通过)
        /// </summary>
        private void BindGridNotice(string type)
        {
            ///近三个发布的通知
            var noticeList = (from x in Funs.DB.Resource_Notices
                             where x.ReleaseTime >= System.DateTime.Now.AddMonths(-3)
                             orderby x.ReleaseTime descending
                             select x).ToList();
            if (noticeList.Count() == 0)
            {
                noticeList= (from x in Funs.DB.Resource_Notices
                            orderby x.ReleaseTime descending
                            select x).Take(5).ToList();
            }
            
            DataTable tb = this.LINQToDataTable(noticeList);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridHSSEStandard, tb1);
            GridNotice.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(GridNotice.FilteredData, tb);
            var table = this.GetPagedDataTable(GridNotice, tb);

            GridNotice.DataSource = table;
            GridNotice.DataBind();
        }

        /// <summary>
        /// Grid行双击事件(待办事项)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridNotice_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {

            if (GridNotice.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Resources/NewsEdit.aspx?NewsId={0}", GridNotice.SelectedRowID, "编辑 - ")));
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen2_Click(object sender, EventArgs e)
        {
            this.BindGridNotice("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose2_Click(object sender, EventArgs e)
        {
            this.BindGridNotice("close");
        }
        #endregion

        #region 专家辅助
        /// <summary>
        /// 绑定数据(专家辅助)
        /// </summary>
        private void BindGridHSSEStandard(string type)
        {
            string strSql = string.Empty;
            if (type == "oper")
            {
                strSql = @"SELECT ManagedItemId,ManagedItemCode,ManagedItemName,ManagedObjectId,ManagedObjectCode,ManagedObjectName,StandardId,StandardCode,StandardName,SpecialtyId,SpecialtyCode,SpecialtyName"
                    + @" FROM dbo.View_Standard_ManagedItem "
                    + @" WHERE 1=1 ";

            }
            else
            {
                strSql = @"SELECT TOP 8 ManagedItemId,ManagedItemCode,ManagedItemName,ManagedObjectId,ManagedObjectCode,ManagedObjectName,StandardId,StandardCode,StandardName,SpecialtyId,SpecialtyCode,SpecialtyName"
                    + @" FROM dbo.View_Standard_ManagedItem "
                    + @" WHERE 1=1 ";
            }

            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.txtStandard.Text.Trim()))
            {
                strSql += " AND (SpecialtyName LIKE @values OR StandardName LIKE @values OR ManagedObjectName LIKE @values OR ManagedItemName LIKE @values)";
                listStr.Add(new SqlParameter("@values", "%" + this.txtStandard.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            GridHSSEStandard.RecordCount = tb.Rows.Count;     
            var table = this.GetPagedDataTable(GridHSSEStandard, tb);
            GridHSSEStandard.DataSource = table;
            GridHSSEStandard.DataBind();            
        }

        #region 输入框查询事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGridHSSEStandard("oper");
        }
        #endregion

        /// <summary>
        /// Grid行双击事件(专家辅助)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridHSSEStandard_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            var view =BLL.ManagedItemService.GetManagedItemById(GridHSSEStandard.SelectedRowID);
            if (view != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Standard/HSSEStandardView.aspx?ManagedItemId={0}", GridHSSEStandard.SelectedRowID), "专家辅助:"));
            }
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen1_Click(object sender, EventArgs e)
        {
            this.BindGridHSSEStandard("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose1_Click(object sender, EventArgs e)
        {
            this.BindGridHSSEStandard("close");
        }
        #endregion

        #region 项目图片
        public string pics, links, texts;

        /// <summary>
        /// 项目图片显示
        /// </summary>
        public void ShowNewsPic()
        {
            this.picContent.Visible = false;
            string strSql = @"SELECT TOP 8 NewsId,Title,Url FROM dbo.Resource_News ORDER BY ReleaseTime DESC";
            
            DataSet ds = BLL.SQLHelper.RunSqlString(strSql, "Resource_News");
            DataView dv = ds.Tables[0].DefaultView;
            if (dv.Table.Rows.Count != 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    var q = Funs.DB.AttachFile.FirstOrDefault(e => e.ToKeyId == dv.Table.Rows[i]["NewsId"].ToString());
                    if (q != null && q.AttachUrl != null)
                    {
                        var urls = Funs.GetStrListByStr(q.AttachUrl, ',');
                        foreach (var item in urls)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                links += "../Resources/NewsEdit.aspx?NewsId=" + dv.Table.Rows[i]["NewsId"].ToString() + "|";
                                pics += "../" + item + "|";
                                texts += dv.Table.Rows[i]["Title"].ToString() + "|";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(links) && !string.IsNullOrEmpty(links) && !string.IsNullOrEmpty(links))
                {
                    this.picContent.Visible = true;
                    links = links.Substring(0, links.LastIndexOf("|"));
                    pics = pics.Substring(0, pics.LastIndexOf("|")).Replace("\\", "/");
                    texts = texts.Substring(0, texts.LastIndexOf("|"));
                }
                else
                {
                    this.picContent.Visible = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            this.BindGridHSSEStandard("close");
            this.BindGridToDoMatter("close");
        }
    }
}

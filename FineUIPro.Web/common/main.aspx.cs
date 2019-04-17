using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

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
                //BindGridNewDynamic("close");
                //this.ProjectPic();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGridNewDynamic")
                {
                    //BindGridNewDynamic("close");
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
            ////var table = this.GetPagedDataTable(GridNewDynamic, tb1);
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
            if (GridToDoMatter.SelectedRow != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridToDoMatter.SelectedRow.Values[3].ToString()), "待办事项：" + GridToDoMatter.SelectedRow.Values[0].ToString()));
            }
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
        //    List<Model.View_ToDoMatter> toDoMatterList = new List<Model.View_ToDoMatter>();
        //    ///近三个发布的通知
        //    var noticeList = from x in Funs.DB.InformationProject_Notice
        //                     where x.ReleaseDate >= System.DateTime.Now.AddMonths(-3)
        //                     select x;
        //    if (noticeList.Count() > 0)
        //    {
        //        List<Model.InformationProject_Notice> getNotices = new List<Model.InformationProject_Notice>();
        //        var projectId = from x in Funs.DB.Project_ProjectUser where x.UserId == this.CurrUser.UserId select x.ProjectId;
        //        if (projectId.Count() > 0)
        //        {
        //            foreach (var item in projectId)
        //            {
        //                if (!string.IsNullOrEmpty(item))
        //                {
        //                    noticeList = noticeList.Where(x => x.AccessProjectId.Contains(item));
        //                    if (noticeList.Count() > 0)
        //                    {
        //                        getNotices.AddRange(noticeList.ToList());
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            getNotices = noticeList.Where(x => x.AccessProjectId.Contains("#")).ToList();
        //        }    
        //        foreach (var item in getNotices.Distinct().OrderByDescending(x => x.ReleaseDate).ToList())
        //        {
        //            Model.View_ToDoMatter newTodo = new Model.View_ToDoMatter();
        //            newTodo.Id = item.NoticeId;
        //            newTodo.Type = "通知通告";
        //            newTodo.Name = "[" + item.NoticeCode + "]" + item.NoticeTitle;
        //            newTodo.Date = item.ReleaseDate;
        //            newTodo.Url = String.Format("~/InformationProject/NoticeView.aspx?NoticeId={0}", item.NoticeId);
        //            toDoMatterList.Add(newTodo);
        //        }
        //    }

        //    DataTable tb = this.LINQToDataTable(toDoMatterList);
        //    // 2.获取当前分页数据
        //    //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
        //    GridNotice.RecordCount = tb.Rows.Count;
        //    tb = GetFilteredTable(GridNotice.FilteredData, tb);
        //    var table = this.GetPagedDataTable(GridNotice, tb);

        //    GridNotice.DataSource = table;
        //    GridNotice.DataBind();
        }

        /// <summary>
        /// Grid行双击事件(待办事项)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridNotice_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridNotice.SelectedRow.Values[3].ToString()),  GridNotice.SelectedRow.Values[0].ToString()));
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

        #region 资质预警
        /// <summary>
        /// 绑定数据(资质预警)
        /// </summary>
        private void BindGridNewDynamic(string type)
        {
            string strSql = string.Empty;
            if (type == "oper")
            {
                strSql = "SELECT * FROM View_NewDynamic";
               
            }
            else
            {
                strSql = "SELECT TOP 5 * FROM View_NewDynamic";
            }

            DataTable tb = SQLHelper.GetDataTableRunText(strSql, null); 
            GridNewDynamic.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(GridNewDynamic.FilteredData, tb);
            var table = this.GetPagedDataTable(GridNewDynamic, tb);
            GridNewDynamic.DataSource = table;
            GridNewDynamic.DataBind();
        }

        /// <summary>
        /// Grid行双击事件(资质预警)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridNewDynamic_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            //var view = Funs.DB.View_NewDynamic.FirstOrDefault(x => x.Id == GridNewDynamic.SelectedRowID);   
            //if (view != null)
            //{
            //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(GridNewDynamic.SelectedRow.Values[3].ToString(), GridNewDynamic.SelectedRowID), "资质预警:" + view.Type));
            //}
        }

        /// <summary>
        /// 右键展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOpen1_Click(object sender, EventArgs e)
        {
            this.BindGridNewDynamic("oper");
        }

        /// <summary>
        /// 右键收起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuClose1_Click(object sender, EventArgs e)
        {
            this.BindGridNewDynamic("close");
        }
        #endregion

        #region 项目图片
        public string pics, links, texts;

        /// <summary>
        /// 项目图片显示
        /// </summary>
        public void ProjectPic()
        {
            this.picContent.Visible = false;
            string strSql = @"SELECT DISTINCT TOP 5 A.PictureId,A.Title,A.ContentDef,A.PictureType,A.UploadDate,A.States,A.AttachUrl,A.CompileMan"
                + @" FROM dbo.InformationProject_Picture A" ;
            bool isLeaderManage =BLL.CommonService.IsThisUnitLeaderOrManage(this.CurrUser.UserId);
            if (!isLeaderManage)
            {
                strSql += " LEFT JOIN Project_ProjectUser B ON A.ProjectId =B.ProjectId";
                strSql += " WHERE B.UserId='" + this.CurrUser.UserId + "'";
            }
            strSql += " ORDER BY A.UploadDate DESC";
            
            DataSet ds = BLL.SQLHelper.RunSqlString(strSql, "InformationProject_Picture");
            DataView dv = ds.Tables[0].DefaultView;
            if (dv.Table.Rows.Count != 0)
            {
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    var q = Funs.DB.AttachFile.FirstOrDefault(e => e.ToKeyId == dv.Table.Rows[i]["PictureId"].ToString());
                    if (q != null && q.AttachUrl != null)
                    {
                        var urls = Funs.GetStrListByStr(q.AttachUrl, ',');
                        foreach (var item in urls)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                links += "../InformationProject/PictureView.aspx?PictureId=" + dv.Table.Rows[i]["PictureId"].ToString() + "|";
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
            this.BindGridNewDynamic("close");
            this.BindGridToDoMatter("close");
        }
    }
}

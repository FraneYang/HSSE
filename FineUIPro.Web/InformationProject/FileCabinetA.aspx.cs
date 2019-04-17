using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class FileCabinetA : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.FileCabinetADataBind();//加载树
                this.GetButtonPower();
            }
        }
        
        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.tvFileCabinetA.SelectedNodeID))
            {
                string strSql = @"SELECT Item.FileCabinetAItemId,Item.FileCabinetAId,Item.Code,Item.Title,Item.CompileMan,UserName AS CompileManName,Item.CompileDate,Item.Remark,Item.AttachUrl"
                              + @" FROM dbo.InformationProject_FileCabinetAItem AS Item"                             
                              + @" LEFT JOIN Sys_User ON CompileMan=UserId WHERE FileCabinetAId='"+ this.tvFileCabinetA.SelectedNodeID+"'";
                
                List<SqlParameter> listStr = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(this.txtGridName.Text.Trim()))
                {
                    strSql += " AND (Item.Code LIKE @Name OR Item.Title LIKE @Name)";
                    listStr.Add(new SqlParameter("@Name", "%" + this.txtGridName.Text.Trim() + "%"));
                }

                if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
                {
                    strSql += " AND Item.CompileDate >= @StartDate";
                    listStr.Add(new SqlParameter("@StartDate", Funs.GetNewDateTime(this.txtStartDate.Text.Trim())));
                }
                if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
                {
                    strSql += " AND Item.CompileDate <= @EndDate";
                    listStr.Add(new SqlParameter("@EndDate", Funs.GetNewDateTime(this.txtEndDate.Text.Trim())));
                }

                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
        
        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void FileCabinetADataBind()
        {
            this.tvFileCabinetA.Nodes.Clear();
            this.tvFileCabinetA.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = "文件柜";
            rootNode.Expanded = true;
            rootNode.NodeID = "0";
            this.tvFileCabinetA.Nodes.Add(rootNode);
            var fileCabinetAList = BLL.FileCabinetAService.GetFileCabinetAList();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                fileCabinetAList = fileCabinetAList.Where(x => x.Title.Contains(this.txtName.Text.Trim())).ToList();

            }
            if (fileCabinetAList.Count() > 0)
            {
                this.GetNodes(fileCabinetAList, rootNode);
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(List<Model.InformationProject_FileCabinetA> fileCabinetAList, TreeNode node)
        {
            var FileCabinetAs = fileCabinetAList.Where(x => x.SupFileCabinetAId == node.NodeID);
            foreach (var item in FileCabinetAs)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = item.Title,
                    NodeID = item.FileCabinetAId
                };
                newNode.EnableClickEvent = true;
                node.Nodes.Add(newNode);
                GetNodes(fileCabinetAList, newNode);
            }
        }
        #endregion
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvFileCabinetA_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region gv排序翻页
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {          
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion

        #region 右键增加、修改、删除文件柜方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvFileCabinetA.SelectedNode != null)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("FileCabinetAEdit.aspx?SupFileCabinetAId={0}", this.tvFileCabinetA.SelectedNodeID, "新增 - ")));
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 右键修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (this.tvFileCabinetA.SelectedNode != null && this.tvFileCabinetA.SelectedNodeID != "0")
            {
                var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByID(this.tvFileCabinetA.SelectedNodeID);
                if (fileCabinetA != null)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("FileCabinetAEdit.aspx?FileCabinetAId={0}", fileCabinetA.FileCabinetAId, "编辑 - ")));
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (this.tvFileCabinetA.SelectedNode != null)
            {
                var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByID(this.tvFileCabinetA.SelectedNodeID);
                if (fileCabinetA != null && BLL.FileCabinetAService.IsDeleteFileCabinetA(fileCabinetA.FileCabinetAId))
                {
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除文件柜信息");
                    BLL.FileCabinetAService.DeleteFileCabinetAByID(fileCabinetA.FileCabinetAId);
                    this.FileCabinetADataBind();
                    Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
                }
                else
                {
                    Alert.ShowInTop("存在下级菜单或已增加明细，不允许删除！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        /// <summary>
        /// 双击查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            var fileCabinetA = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(Grid1.SelectedRowID);
            if (fileCabinetA != null && !string.IsNullOrEmpty(fileCabinetA.Url))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(fileCabinetA.Url, Grid1.SelectedRowID), "查看", 1100, 620));
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            var fileCabinetA = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(Grid1.SelectedRowID);
            if (fileCabinetA != null && !string.IsNullOrEmpty(fileCabinetA.Url))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(fileCabinetA.Url, Grid1.SelectedRowID), "查看", 1100, 620));
            }
        }

        /// <summary>
        /// 删除明细方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelDetail_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    var item = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(Grid1.DataKeys[rowIndex][0].ToString());
                    if (item != null)
                    {
                        var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByID(item.FileCabinetAId);
                        if (fileCabinetA != null)
                        {
                            string info = "删除文件柜文件";
                            ////考试记录
                            var testRecord = BLL.TestRecordService.GetTestRecordById(item.FileCabinetAItemId);
                            if (testRecord != null)
                            {
                                testRecord.IsFiled = null;
                                BLL.TestRecordService.UpdateTestRecord(testRecord);
                                info = "删除文件柜考试记录文件";
                            }

                            ////隐患巡检记录
                            var getHiddenHazard = BLL.HiddenHazardService.GetHiddenHazardById(item.FileCabinetAItemId);
                            if (getHiddenHazard != null)
                            {
                                getHiddenHazard.IsFiled = null;
                                BLL.HiddenHazardService.UpdateHiddenHazard(getHiddenHazard);
                                info = "删除文件柜隐患巡检记录文件";
                            }
                            
                            BLL.FileCabinetAItemService.DeleteFileCabinetAItemByID(item.FileCabinetAItemId);
                            BLL.LogService.AddLog(this.CurrUser.UserId, info);
                        }
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 调档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModifyDetail_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }
            string ids = string.Empty;
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                string rowID = Grid1.DataKeys[rowIndex][0].ToString();

                var fileCabinetA = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(rowID);
                if (fileCabinetA != null)
                {
                    ids += rowID + "|";                    
                }
            }

            if (!string.IsNullOrEmpty(ids))
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("FileCabinetAChange.aspx?FileCabinetAItemId={0}", ids, "查看 - "), "归档", 600, 540));
            }
        }
        
        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            FileCabinetADataBind();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.FileCabinetAMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnMenuNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                    this.btnMenuModifyDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelDetail.Hidden = false;
                    this.btnMenuDel.Hidden = false;
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
        protected void Text_TextChanged(object sender, EventArgs e)
        {
            this.FileCabinetADataBind();//加载树
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();//列表绑定
        }
        #endregion
    }
}
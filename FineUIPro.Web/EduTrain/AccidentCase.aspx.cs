using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.Data.SqlClient;
using System.Data;

namespace FineUIPro.Web.EduTrain
{
    public partial class AccidentCase : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                InitTreeMenu();
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trAccidentCase.Nodes.Clear();
            trAccidentCase.ShowBorder = false;
            trAccidentCase.ShowHeader = false;
            trAccidentCase.EnableIcons = true;
            trAccidentCase.AutoScroll = true;
            trAccidentCase.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "事故案例库",
                NodeID = "0",
                Expanded = true
            };
            this.trAccidentCase.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = GetNewAccidentCase(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.AccidentCaseName,
                        ToolTip = dr.AccidentCaseName,
                        NodeID = dr.AccidentCaseId,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.AccidentCaseId);
                }
            }
        }

        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.EduTrain_AccidentCase> GetNewAccidentCase(string parentId)
        {
            return (from x in Funs.DB.EduTrain_AccidentCase where x.SupAccidentCaseId == parentId orderby x.AccidentCaseCode select x).ToList();
        }
        #endregion        

        #region Tree增加、修改、删除 按钮事件
        /// <summary>
        /// 增加Tree节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trAccidentCase.SelectedNode != null)
            {
                Model.EduTrain_AccidentCase accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(this.trAccidentCase.SelectedNode.NodeID);
                if ((accidentCase != null && accidentCase.IsEndLever == false) || this.trAccidentCase.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentCaseEdit.aspx?SupAccidentCaseId={0}", this.trAccidentCase.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("选择的项已是末级！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑Tree按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.trAccidentCase.SelectedNode != null)
            {
                if (this.trAccidentCase.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentCaseEdit.aspx?AccidentCaseId={0}", this.trAccidentCase.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("根节点无法编辑！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除Tree节点数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.trAccidentCase.SelectedNode != null)
            {
                var q = BLL.AccidentCaseService.GetAccidentCaseById(this.trAccidentCase.SelectedNode.NodeID);

                if (q != null && BLL.AccidentCaseService.IsDeleteAccidentCase(this.trAccidentCase.SelectedNode.NodeID))
                {
                    BLL.AccidentCaseService.DeleteAccidentCaseByAccidentCaseId(this.trAccidentCase.SelectedNode.NodeID);
                    InitTreeMenu();
                }
                else
                {
                    ShowNotify("存在下级菜单或已增加资源数据或者为内置项，不允许删除！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择删除项！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region TreeView行点击事件
        /// <summary>
        /// Tree行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trAccidentCase_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region BindGrid

        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.trAccidentCase.SelectedNode.NodeID))
            {
                string strSql = "select * from View_EduTrain_AccidentCaseItem where AccidentCaseId=@AccidentCaseId ";

                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@AccidentCaseId", this.trAccidentCase.SelectedNode.NodeID));
                if (!string.IsNullOrEmpty(this.Activities.Text.Trim()))
                {
                    strSql += " AND Activities LIKE @Activities";
                    listStr.Add(new SqlParameter("@Activities", "%" + this.Activities.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.AccidentCaseName.Text.Trim()))
                {
                    strSql += " AND AccidentCaseName LIKE @AccidentCaseName";
                    listStr.Add(new SqlParameter("@AccidentCaseName", "%" + this.AccidentCaseName.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.AccidentName.Text.Trim()))
                {
                    strSql += " AND AccidentName LIKE @AccidentName";
                    listStr.Add(new SqlParameter("@AccidentName", "%" + this.AccidentName.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
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

        #region 表头过滤
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 增加明细
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trAccidentCase.SelectedNode != null)
            {
                if (this.trAccidentCase.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCaseItemEdit.aspx?AccidentCaseId={0}", this.trAccidentCase.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("请选择末级节点！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑明细
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string accidentCaseItemId = Grid1.SelectedRowID;

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCaseItemEdit.aspx?AccidentCaseItemId={0}", accidentCaseItemId, "编辑 - ")));
        }
        #endregion

        #region 删除明细数据
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
                    BLL.AccidentCaseItemService.DeleteAccidentCaseItemId(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除事故案例库");
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 排序、分页
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 关闭弹出框
        protected void Window1_Close(object sender, EventArgs e)
        {
            InitTreeMenu();
        }

        protected void Window2_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.AccidentCaseMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainingEdu : PageBase
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
                this.GetButtonPower();
                this.InitTreeMenu();
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trTraining.Nodes.Clear();
            trTraining.ShowBorder = false;
            trTraining.ShowHeader = false;
            trTraining.EnableIcons = true;
            trTraining.AutoScroll = true;
            trTraining.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "培训教材库",
                NodeID = "0",
                Expanded = true
            };
            this.trTraining.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, rootNode.NodeID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        private void BoundTree(TreeNodeCollection nodes, string parentId)
        {
            var dt = (from x in Funs.DB.Training_TrainingEdu
                      where x.SupTrainingEduId == parentId
                      orderby x.TrainingEduCode
                      select x).ToList();
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    string name = dr.TrainingEduName;
                    if (!string.IsNullOrEmpty(dr.TrainingEduCode))
                    {
                        name = "[" + dr.TrainingEduCode + "]" + dr.TrainingEduName;
                    }
                    tn = new TreeNode
                    {
                        Text = name,
                        NodeID = dr.TrainingEduId,
                        EnableClickEvent = true,
                        ToolTip = dr.TrainingEduName
                    };
                    nodes.Add(tn);

                    ///是否存在下级节点
                    var sup = Funs.DB.Training_TrainingEdu.FirstOrDefault(x => x.SupTrainingEduId == tn.NodeID);
                    if (sup != null)
                    {
                        BoundTree(tn.Nodes, tn.NodeID);
                    }
                }
            }
        }

        protected void btnMenuADD_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainingEduSave.aspx?SupTrainingEduId={0}", this.trTraining.SelectedNode.NodeID, "新增 - ")));
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        protected void btnTreeMenuEdit_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null)
            {
                if (this.trTraining.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainingEduSave.aspx?TrainingEduId={0}", this.trTraining.SelectedNode.NodeID, "编辑 - ")));
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

        protected void btnTreeMenuDelete_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null && this.trTraining.SelectedNodeID != "0")
            {
                var edu = Funs.DB.Training_TrainingEdu.FirstOrDefault(x => x.SupTrainingEduId == this.trTraining.SelectedNode.NodeID);
                if (edu == null)
                {
                    BLL.TrainingEduService.DeleteTrainingEduById(this.trTraining.SelectedNode.NodeID);
                    InitTreeMenu();
                    Grid1.DataSource = null;
                    Grid1.DataBind();
                }
                else
                {
                    ShowNotify("存在子目录，不能删除！", MessageBoxIcon.Warning);
                }                
            }
            else
            {
                ShowNotify("请选择删除项！", MessageBoxIcon.Warning);
            }
        }

        protected void trTraining_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            BindGrid();
        }

        #region BindGrid

        private void BindGrid()
        {
            if (this.trTraining.SelectedNode!= null && !string.IsNullOrEmpty(this.trTraining.SelectedNode.NodeID))
            {
                string strSql = @"SELECT TrainingEduItemId,TrainingEduId,TrainingEduItemCode,TrainingEduItemName,Summary,AttachUrl,PictureUrl,InstallationIds"
                                + @",(CASE WHEN InstallationNames IS NULL THEN '通用' ELSE InstallationNames END) AS InstallationNames"
                                + @" FROM dbo.Training_TrainingEduItem"
                                + @" WHERE TrainingEduId=@TrainingEduId ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@TrainingEduId", this.trTraining.SelectedNode.NodeID));
                if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    strSql += " AND (TrainingEduItemCode LIKE @Name OR TrainingEduItemName LIKE @Name OR Summary LIKE @Name OR InstallationNames LIKE @Name)";
                    listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
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

        #region Events
        protected void Window1_Close(object sender, EventArgs e)
        {
            this.InitTreeMenu();
        }

        protected void Window2_Close(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        protected void Window3_Close(object sender, EventArgs e)
        {
            this.InitTreeMenu();
            this.BindGrid();
        }

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
                    BLL.TrainingEduItemService.DeleteTrainingEduItemById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除培训教材库");
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region GV排序页面
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    
        /// <summary>
        /// 编辑教材
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

            string TrainingEduItemId = Grid1.SelectedRowID;

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainingEduItemSave.aspx?TrainingEduItemId={0}", TrainingEduItemId, "编辑 - ")));
        }

        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null && this.trTraining.SelectedNodeID != "0")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainingEduItemSave.aspx?TrainingEduId={0}", this.trTraining.SelectedNode.NodeID, "编辑 - ")));
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        
        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.TrainingEduMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnMenuADD.Hidden = false;
                    this.btnImport.Hidden = false;
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnTreeMenuEdit.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnTreeMenuDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        
        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("TrainingEduItemIn.aspx", "导入 - ")));
        }
        #endregion
    }
}
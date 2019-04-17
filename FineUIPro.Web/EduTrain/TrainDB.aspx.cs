using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainDB : PageBase
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
                Text = "考试试题库",
                NodeID = "0",
                Expanded = true
            };
            this.trTraining.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, rootNode.NodeID);
        }

        private void BoundTree(TreeNodeCollection nodes, string parentId)
        {
            var dt = GetNewTraining(parentId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    string name = dr.TrainingName;
                    if (!string.IsNullOrEmpty(dr.TrainingCode))
                    {
                        name = "[" + dr.TrainingCode + "]" + dr.TrainingName;
                    }
                    tn = new TreeNode
                    {

                        Text = name,
                        NodeID = dr.TrainingId,
                        EnableClickEvent = true,
                        ToolTip = dr.TrainingName
                    };
                    nodes.Add(tn);
                    ///是否存在下级节点
                    var sup = Funs.DB.Training_Training.FirstOrDefault(x => x.SupTrainingId == tn.NodeID);
                    if (sup != null)
                    {
                        BoundTree(tn.Nodes, tn.NodeID);
                    }
                }
            }
        }

        /// <summary>
        /// 得到培训类型
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.Training_Training> GetNewTraining(string parentId)
        {
            return (from x in Funs.DB.Training_Training
                    where x.SupTrainingId == parentId
                    orderby x.TrainingCode
                    select x).ToList();
        }

        //protected void btnNew_Click(object sender, EventArgs e)
        //{
        //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainingSave.aspx", "编辑 - ")));
        //}


        protected void btnMenuADD_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainingSave.aspx?SupTrainingId={0}", this.trTraining.SelectedNode.NodeID, "新增 - ")));
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
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainingSave.aspx?TrainingId={0}", this.trTraining.SelectedNode.NodeID, "编辑 - ")));
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
                var edu = Funs.DB.Training_Training.FirstOrDefault(x => x.SupTrainingId == this.trTraining.SelectedNode.NodeID);
                if (edu == null)
                {
                    BLL.TrainingService.DeleteTrainingById(this.trTraining.SelectedNode.NodeID);
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
                string strSql = @"SELECT TrainingItemId,TrainingId,TrainingItemCode,TrainingItemName,Abstracts,AttachUrl,VersionNum,TestType,InstallationIds "
                                + @" ,(CASE WHEN TestType = '1' THEN '单选题' WHEN TestType = '2' THEN '多选题' ELSE '判断题' END) AS TestTypeName "
                                + @" ,(CASE WHEN InstallationNames IS NULL THEN '通用' ELSE InstallationNames END) AS InstallationNames,AItem,BItem,CItem,DItem,EItem,Score,AnswerItems "
                                + @" FROM dbo.Training_TrainingItem"
                                + @" WHERE TrainingId=@TrainingId ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@TrainingId", this.trTraining.SelectedNode.NodeID));
                if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    strSql += " AND (TrainingItemCode LIKE @Name OR TrainingItemName LIKE @Name OR Abstracts LIKE @Name OR InstallationNames LIKE @Name)";
                    listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
                }
                if (this.drptype.SelectedValue != "0")
                {
                    strSql += " AND TestType=@TestType";
                    listStr.Add(new SqlParameter("@TestType", this.drptype.SelectedValue));
                }
                if (this.ckIsItem.Checked)
                {
                    strSql += " AND (AItem IS NULL OR BItem IS NULL OR AItem ='' OR BItem ='')";
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
                    BLL.TrainingItemService.DeleteTrainingItemById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除考试试题库");
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
        /// 编辑试题
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

            string trainingItemId = Grid1.SelectedRowID;

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainingItemSave.aspx?TrainingItemId={0}", trainingItemId, "编辑 - ")));
        }

        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null && this.trTraining.SelectedNodeID != "0")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("TrainingItemSave.aspx?TrainingId={0}", this.trTraining.SelectedNode.NodeID, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.TrainDBMenuId);
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

            //if (this.CurrUser.UserId == BLL.Const.sysglyId)
            //{
            //    this.btnRefresh.Hidden = false;
            //    this.btnRefresh1.Hidden = false;
            //}
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
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("TrainingItemIn.aspx", "导入 - ")));
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("TrainDBOut.aspx", "导出 - ")));
        }
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut1_Click(object sender, EventArgs e)
        {
            if (this.trTraining.SelectedNode != null && this.trTraining.SelectedNodeID != "0")
            {
                Response.ClearContent();
                string filename = Funs.GetNewFileName();
                Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("培训试题" + filename, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentType = "application/excel";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                this.Grid1.PageSize = Grid1.RecordCount;
                BindGrid();
                Response.Write(GetGridTableHtml(Grid1));
                Response.End();
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        
        #endregion

        #region 刷新装置
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Refresh_Click(object sender, EventArgs e)
        {
            var trainingItems = from x in Funs.DB.Training_TrainingItem select x;
            if (trainingItems.Count() > 0)
            {
                foreach (var trainingItem in trainingItems)
                {
                    if (trainingItem.TestType == "3" && string.IsNullOrEmpty(trainingItem.AItem))
                    {
                        trainingItem.AItem = "对";
                        trainingItem.BItem = "错";
                    }

                    if (!string.IsNullOrEmpty(trainingItem.InstallationIds))
                    {
                        string name = string.Empty;
                        var installList = Funs.GetStrListByStr(trainingItem.InstallationIds, ',');
                        if (installList.Count() > 0)
                        {
                            foreach (var installItem in installList)
                            {
                                var install = BLL.InstallationService.GetInstallationNameByInstallationId(installItem);
                                if (!string.IsNullOrEmpty(install))
                                {
                                    name += install + ",";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(name))
                        {
                            trainingItem.InstallationNames = name.Substring(0, name.LastIndexOf(","));
                        }
                    }

                    BLL.TrainingItemService.UpdateTrainingItem(trainingItem);
                }
            }

            Alert.ShowInTop("操作完成！", MessageBoxIcon.Success);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Refresh1_Click(object sender, EventArgs e)
        {
            var trainingItems = from x in Funs.DB.Training_TrainingItem select x;
            if (trainingItems.Count() > 0)
            {
                foreach (var trainingItem in trainingItems)
                {
                    if (trainingItem.TestType == "3" && string.IsNullOrEmpty(trainingItem.AItem))
                    {
                        trainingItem.AItem = "对";
                        trainingItem.BItem = "错";
                    }

                    if (!string.IsNullOrEmpty(trainingItem.InstallationNames))
                    {
                        string getInstallationId = string.Empty;
                        var installList = Funs.GetStrListByStr(trainingItem.InstallationNames, ',');
                        if (installList.Count() > 0)
                        {
                            foreach (var installItem in installList)
                            {
                                var install = Funs.DB.Base_Installation.FirstOrDefault(x => x.InstallationName == installItem);
                                if (install != null && !string.IsNullOrEmpty(install.InstallationId))
                                {
                                    getInstallationId += install.InstallationId + ",";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(getInstallationId))
                        {
                            trainingItem.InstallationIds = getInstallationId.Substring(0, getInstallationId.LastIndexOf(","));
                        }
                    }

                    BLL.TrainingItemService.UpdateTrainingItem(trainingItem);
                }
            }

            Alert.ShowInTop("操作完成！", MessageBoxIcon.Success);
        }
        #endregion

        /// <summary>
        /// 显示选项不全的题目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckIsItem_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.BindGrid();
        }
    }
}
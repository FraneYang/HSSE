namespace FineUIPro.Web.Hazard
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class JHA : PageBase
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
                this.InitTreeMenu();
                // 绑定表格
                this.BindGrid();
            }
        }

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trInstall.Nodes.Clear();
            var dt = BLL.InstallationService.GetInstallationByInstallTypeList("2");
            if (dt.Count() > 0)
            {
                foreach (var dr in dt)
                {
                    TreeNode rootNode = new TreeNode
                    {
                        Text = dr.InstallationName,
                        NodeID = dr.InstallationId,
                        EnableClickEvent = true,
                        ToolTip = dr.InstallationName
                    };

                    this.trInstall.Nodes.Add(rootNode);
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trInstall_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string installationId = string.Empty;
            if (this.trInstall.SelectedNode != null && this.trInstall.SelectedNode.NodeID != "0")
            {
                installationId = this.trInstall.SelectedNode.NodeID;
            }
            string strSql = @"SELECT DISTINCT JobActivitys.JobActivityId,JobActivitys.JobActivityName,JobActivitys.JobActivityCode,JobActivitys.Remark,WorkAreas.WorkAreaCode,WorkAreas.WorkAreaName,Installation.InstallationCode,Installation.InstallationName,Identification"
                        + @" FROM dbo.Base_JobActivity AS JobActivitys"
                        + @" LEFT JOIN DBO.Base_Installation AS Installation ON JobActivitys.InstallationId =Installation.InstallationId"
                        + @" LEFT JOIN DBO.Base_WorkArea AS WorkAreas ON JobActivitys.WorkAreaId =WorkAreas.WorkAreaId"                        
                        + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(installationId))
            {
                strSql += " AND JobActivitys.InstallationId = @InstallationId";
                listStr.Add(new SqlParameter("@InstallationId", installationId));
            }
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (JobActivitys.JobActivityName LIKE @Name OR JobActivitys.JobActivityCode LIKE @Name OR WorkAreas.WorkAreaName LIKE @Name OR Installation.InstallationName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }

            if (this.ckStates.SelectedValue == "0")
            {
                strSql += " AND (JobActivitys.States='0' OR JobActivitys.States IS NULL)";
            }            
            else
            {
                strSql += " AND JobActivitys.States = @states";
                listStr.Add(new SqlParameter("@states", this.ckStates.SelectedValue));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
           // 
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            this.Grid1.PageIndex = 0;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.JHAMenuId);
            if (buttonList.Count() > 0)
            {              
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }             
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

        /// <summary>
        /// Grid行双击事件
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
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string JobActivityId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("JHAEdit.aspx?JobActivityId={0}", JobActivityId, "编辑 - ")));          
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
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
        #endregion
    }
}
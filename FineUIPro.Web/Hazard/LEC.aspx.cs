namespace FineUIPro.Web.Hazard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class LEC : PageBase
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
           
            //BoundTree(rootNode.Nodes, "0");
        }

        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            
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

        #region 绑定数据BindGrid
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
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.ckDataType.SelectedValue == "1")
            {
                strSql = @"SELECT JobEnvironments.JobEnvironmentId AS DataId,JobEnvironments.JobEnvironmentName AS DataName,JobEnvironments.JobEnvironmentCode AS DataCode,JobEnvironments.Remark,WorkAreas.WorkAreaCode,WorkAreas.WorkAreaName,Installation.InstallationCode,Installation.InstallationName,Identification,NULL AS EuipmentNo, NULL AS EuipmentTypeName"
                        + @" FROM dbo.Base_JobEnvironment AS JobEnvironments"
                        + @" LEFT JOIN DBO.Base_Installation AS Installation ON JobEnvironments.InstallationId =Installation.InstallationId"
                        + @" LEFT JOIN DBO.Base_WorkArea AS WorkAreas ON JobEnvironments.WorkAreaId =WorkAreas.WorkAreaId"
                        + @" WHERE 1 = 1";
                if (!string.IsNullOrEmpty(installationId))
                {
                    strSql += " AND JobEnvironments.InstallationId = @InstallationId";
                    listStr.Add(new SqlParameter("@InstallationId", installationId));
                }
                if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    strSql += " AND (JobEnvironments.JobEnvironmentName LIKE @Name OR JobEnvironments.JobEnvironmentCode LIKE @Name OR WorkAreas.WorkAreaName LIKE @Name OR Installation.InstallationName LIKE @Name)";
                    listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
                }
                if (this.ckStates.SelectedValue == "0")
                {
                    strSql += " AND (SELECT COUNT(LECItemId) FROM  Hazard_LECItem WHERE DataId=JobEnvironments.JobEnvironmentId) =0";
                }
                else
                {
                    strSql += " AND (SELECT COUNT(LECItemId) FROM  Hazard_LECItem WHERE DataId=JobEnvironments.JobEnvironmentId) >0";
                }

            } else
            {
                strSql = @"SELECT Euipments.EuipmentId AS DataId,Euipments.EuipmentName AS DataName,Euipments.EuipmentCode AS DataCode,Euipments.Remark,WorkAreas.WorkAreaCode,WorkAreas.WorkAreaName,Installation.InstallationCode,Installation.InstallationName,EuipmentNo,EuipmentType.EuipmentTypeName,Identification"
                       + @" FROM dbo.Base_Euipment AS Euipments"
                       + @" LEFT JOIN DBO.Base_Installation AS Installation ON Euipments.InstallationId =Installation.InstallationId"
                       + @" LEFT JOIN DBO.Base_WorkArea AS WorkAreas ON Euipments.WorkAreaId =WorkAreas.WorkAreaId"
                       + @" LEFT JOIN DBO.Base_EuipmentType AS EuipmentType ON Euipments.EuipmentTypeId =EuipmentType.EuipmentTypeId"
                       + @" WHERE 1 = 1";
                if (!string.IsNullOrEmpty(installationId))
                {
                    strSql += " AND Euipments.InstallationId = @InstallationId";
                    listStr.Add(new SqlParameter("@InstallationId", installationId));
                }
                if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    strSql += " AND (Euipments.EuipmentName LIKE @Name OR Euipments.EuipmentCode LIKE @Name OR WorkAreas.WorkAreaName LIKE @Name OR Installation.InstallationName LIKE @Name)";
                    listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
                }
                if (this.ckStates.SelectedValue == "0")
                {
                    strSql += " AND (SELECT COUNT(LECItemId) FROM  Hazard_LECItem WHERE DataId=Euipments.EuipmentId) =0";
                }
                else
                {
                    strSql += " AND (SELECT COUNT(LECItemId) FROM  Hazard_LECItem WHERE DataId=Euipments.EuipmentId) >0";
                }
            }           
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            if (this.ckDataType.SelectedValue == "1")
            {
                this.Grid1.Columns[6].Hidden = true;
                this.Grid1.Columns[7].Hidden = true;
            }
            else
            {
                this.Grid1.Columns[6].Hidden = false;
                this.Grid1.Columns[7].Hidden = false;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.LECMenuId);
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
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("LECEdit.aspx?DataId={0}&DataType={1}", Id, this.ckDataType.SelectedValue, "编辑 - ")));
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
    }
}
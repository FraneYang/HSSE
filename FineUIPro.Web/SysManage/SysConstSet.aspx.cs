using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.SysManage
{
    public partial class SysConstSet : PageBase
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
                /// 菜单设置
                this.LoadTab2Data();
                this.drpMenu.Value = BLL.Const.HiddenHazardMenuId;
                this.drpMenu_TextChanged(null, null);
                /// 环境设置
                this.LoadTTab4Data();
            }
        }

        #region 菜单设置
        #region 流程设置
        /// <summary>
        /// TAB2加载页面方法
        /// </summary>
        private void LoadTab2Data()
        {
            this.treeMenu.Nodes.Clear();
            var sysMenu = BLL.SysMenuService.GetIsUsedMenuListByMenuType(BLL.Const.Menu_Project);
            if (sysMenu.Count() > 0)
            {
                this.InitTreeMenu(sysMenu, null);
            }
        }

        #region 加载菜单下拉框树
        /// <summary>
        /// 加载菜单下拉框树
        /// </summary>
        private void InitTreeMenu(List<Model.Sys_Menu> menusList, TreeNode node)
        {
            string supMenu = "0";
            if (node != null)
            {
                supMenu = node.NodeID;
            }
            var menuItemList = menusList.Where(x => x.SuperMenu == supMenu).OrderBy(x => x.SortIndex);    //获取菜单列表
            if (menuItemList.Count() > 0)
            {
                foreach (var item in menuItemList)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = item.MenuName,
                        NodeID = item.MenuId
                    };
                    if (node == null)
                    {
                        this.treeMenu.Nodes.Add(newNode);
                    }
                    else
                    {
                        node.Nodes.Add(newNode);
                    }
                    if (!item.IsEnd.HasValue || item.IsEnd == false)
                    {
                        InitTreeMenu(menusList, newNode);
                    }
                }
            }
        }
        #endregion

        #region 下拉框回发事件
        /// <summary>
        /// 下拉框回发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMenu_TextChanged(object sender, EventArgs e)
        {
            string menuId = this.drpMenu.Value;
            ///加载流程列表
            this.BindGrid();
            this.BindGrid2();
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
            if (sysMenu == null || !sysMenu.IsEnd.HasValue || sysMenu.IsEnd == false)
            {
                this.drpMenu.Text = string.Empty;
                this.drpMenu.Value = string.Empty;
                ShowNotify("请选择末级菜单操作！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #endregion

        #region 流程列表绑定数据
        /// <summary>
        /// 流程列表绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT FlowOperateId,MenuId,FlowStep,AuditFlowName,WorkPostIds,IsNeed,IsFlowEnd,PushGroup,DepartIds,InstallationIds "
                            + @",(CASE WHEN MatchesValue='1' THEN '单位' WHEN MatchesValue='2' THEN '部门' WHEN MatchesValue='3' THEN '装置' ELSE '无' END) AS MatchesValueName "
                            + @" FROM dbo.Sys_MenuFlowOperate "
                            + @" WHERE MenuId=@MenuId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            string menuId = string.Empty;
            if (!string.IsNullOrEmpty(this.drpMenu.Value))
            {
                menuId = this.drpMenu.Value;
            }
            listStr.Add(new SqlParameter("@MenuId", menuId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            //
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 得到岗位名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertWorkPost(object WorkPostIds)
        {
            return BLL.WorkPostService.getWorkPostNamesWorkPostIds(WorkPostIds);
        }
        /// <summary>
        /// 得到部门名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertDepart(object DepartIdIds)
        {
            return BLL.DepartService.getDepartNamesDepartIds(DepartIdIds);
        }
        /// <summary>
        /// 得到装置名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertInstallation(object InstallationIds)
        {
            return BLL.InstallationService.getInstallationNamesInstallationIds(InstallationIds);
        }

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

        #region 增加编辑事件
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFlowOperateNew_Click(object sender, EventArgs e)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MenuFlowOperateEdit.aspx?MenuId={0}&FlowOperateId={1}", sysMenu.MenuId, string.Empty, "增加 - ")));
            }
        }

        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MenuFlowOperateEdit.aspx?MenuId={0}&FlowOperateId={1}", sysMenu.MenuId, Grid1.SelectedRowID, "编辑 - ")));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFlowOperateDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.MenuFlowOperateService.DeleteAuditFlow(rowID);
                }

                BLL.MenuFlowOperateService.SetSortIndex(this.drpMenu.Value);
                BindGrid();
                BLL.LogService.AddLog(this.CurrUser.UserId, "删除审批流程信息");
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Action2")
            {
                BLL.MenuFlowOperateService.DeleteAuditFlow(Grid1.SelectedRowID);
                BLL.MenuFlowOperateService.SetSortIndex(this.drpMenu.Value);
                BindGrid();
                BLL.LogService.AddLog(this.CurrUser.UserId, "删除审批流程信息");
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }


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
        #endregion        

        #region 测评分值列表绑定数据
        /// <summary>
        /// 测评分值列表绑定数据
        /// </summary>
        private void BindGrid2()
        {
            string strSql = @"SELECT AppraisalId,MenuId,Score,MenuOperation,MenuOperationName,OutTime"
                        + @" FROM Sys_MenuAppraisal"
                        + @" WHERE MenuId=@MenuId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            string menuId = string.Empty;
            if (!string.IsNullOrEmpty(this.drpMenu.Value))
            {
                menuId = this.drpMenu.Value;
            }

            listStr.Add(new SqlParameter("@MenuId", menuId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
        }

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid2();
        }
        #endregion

        #region 增加编辑事件
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAppraisalNew_Click(object sender, EventArgs e)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("MenuAppraisalEdit.aspx?MenuId={0}&AppraisalId={1}", sysMenu.MenuId, string.Empty, "增加 - ")));
            }
        }

        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("MenuAppraisalEdit.aspx?MenuId={0}&AppraisalId={1}", sysMenu.MenuId, Grid2.SelectedRowID, "编辑 - ")));
            }
        }
        #endregion

        #region  删除数据
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAppraisalDelete_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndexArray.Count() > 0)
            {
                foreach (int rowIndex in Grid2.SelectedRowIndexArray)
                {
                    string rowID = Grid2.DataKeys[rowIndex][0].ToString();
                    BLL.MenuAppraisalService.DeleteMenuAppraisal(rowID);
                }
                BindGrid2();
                BLL.LogService.AddLog(this.CurrUser.UserId, "删除测评分值信息");
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Action1")
            {
                BLL.MenuAppraisalService.DeleteMenuAppraisal(Grid2.SelectedRowID);
                BindGrid2();
                BLL.LogService.AddLog(this.CurrUser.UserId, "删除测评分值信息");
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid2();
        }
        #endregion

        #endregion
        #endregion

        #region 环境设置
        /// <summary>
        /// 加载
        /// </summary>
        private void LoadTTab4Data()
        {
            this.ckbGPS.Checked = false;
            var sysCongs = from x in Funs.DB.Sys_Set select x;
            if (sysCongs.Count() > 0)
            {
                var sysSet1 = sysCongs.FirstOrDefault(x => x.SetId == 1);
                if (sysSet1 != null && sysSet1.IsAuto.HasValue)
                {
                    this.ckbGPS.Checked = sysSet1.IsAuto.Value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetSave_Click(object sender, EventArgs e)
        {
            var sysSet1 = Funs.DB.Sys_Set.FirstOrDefault(x => x.SetId == 1);
            if (sysSet1 != null)
            {
                sysSet1.IsAuto = this.ckbGPS.Checked;
            }
            else
            {
                Model.Sys_Set newSys_Set = new Model.Sys_Set
                {
                    SetId = 1,
                    SetName = this.ckbGPS.Label,
                    IsAuto = this.ckbGPS.Checked
                };
                Funs.DB.Sys_Set.InsertOnSubmit(newSys_Set);
            }

            Funs.DB.SubmitChanges();
            ShowNotify("保存成功", MessageBoxIcon.Success);
        }
        #endregion        
    }
}
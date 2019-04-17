﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class Register : PageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  ////权限按钮方法
                this.GetButtonPower();
                // 绑定表格
                this.BindGrid();                
            }
        }

        #region 角色下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;                
            if (this.CurrUser.UserId != BLL.Const.sysglyId)
            {
                var role = BLL.RoleService.GetRoleByRoleId(this.CurrUser.RoleId);
                if (role != null && !string.IsNullOrEmpty(role.AuthorizedRoleIds))
                {   ///找出可授权角色
                    List<string> roleIdLists =Funs.GetStrListByStr(role.AuthorizedRoleIds,',');
                    string authorizedRoleIds = "(";
                    foreach(var item in roleIdLists)
                    {
                        authorizedRoleIds += ("'" + item + "',");
                    }
                    authorizedRoleIds = authorizedRoleIds.Substring(0, authorizedRoleIds.LastIndexOf(",")) + ")";
                    strSql = @"SELECT Roles.RoleId,Roles.RoleName,Roles.RoleCode,Roles.Def,Roles.IsAuditFlow,Roles.IsSystemBuilt,Roles.AuthorizedRoleIds,Roles.AuthorizedRoleNames"
                              + @" FROM dbo.Sys_Role AS Roles "                              
                              + @" WHERE Roles.RoleId IN " + authorizedRoleIds;
                }
            }
            else
            {
                strSql = @"SELECT Roles.RoleId,Roles.RoleName,Roles.RoleCode,Roles.Def,Roles.IsAuditFlow,Roles.IsSystemBuilt,Roles.AuthorizedRoleIds,Roles.AuthorizedRoleNames"
                          + @" FROM dbo.Sys_Role AS Roles "                          
                          + @" WHERE 1=1 ";
            }

            if (!string.IsNullOrEmpty(strSql))
            {
                List<SqlParameter> listStr = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(this.txtRoleName.Text.Trim()))
                {
                    strSql += " AND RoleName LIKE @RoleName";
                    listStr.Add(new SqlParameter("@RoleName", "%" + this.txtRoleName.Text.Trim() + "%"));
                }
               
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }
        }
        #endregion

        #region 角色下拉框查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion 

        #region 下拉框选择
        /// <summary>
        ///  角色下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpRoleId_TextChanged(object sender, EventArgs e)
        {
            this.SelectedIndexChanged();
        }

        /// <summary>
        ///  菜单类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckMenuType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedIndexChanged();
        }

        /// <summary>
        /// 选择变化事件
        /// </summary>
        private void SelectedIndexChanged()
        {
            this.InitMenuTree(this.drpRole.Value, String.Join(", ", this.ckMenuType.SelectedValueArray));                  
        }
        #endregion

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="menuList">菜单集合</param>
        private void InitMenuTree(string roleId, string menuType)
        {
            this.tvMenu.Nodes.Clear();
            var menus = BLL.SysMenuService.GetIsUsedMenuListByMenuType(menuType);
            if (menus.Count() > 0)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Text = "菜单";
                rootNode.NodeID = "0";
                rootNode.EnableCheckBox = true;
                rootNode.EnableCheckEvent = true;
                rootNode.Expanded = true;
                this.tvMenu.Nodes.Add(rootNode);
                this.BoundTree(rootNode.Nodes, menus, rootNode.NodeID, roleId);
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, List<Model.Sys_Menu> sysMenus, string superMenuId, string roleId)
        {
            var menus = sysMenus.Where(x => x.SuperMenu == superMenuId).Distinct().OrderBy(x => x.SortIndex);
            if (menus.Count() > 0)
            {
                foreach (var item in menus)
                {
                    TreeNode chidNode = new TreeNode();
                    chidNode.Text = item.MenuName;
                    chidNode.NodeID = item.MenuId;
                    chidNode.EnableCheckBox = true;
                    chidNode.EnableCheckEvent = true;
                    if (BLL.RolePowerService.IsHavePowerByRoleIdMenuId(roleId, chidNode.NodeID))
                    {
                        chidNode.Checked = true;
                        chidNode.Expanded = true;
                        chidNode.Selectable = true;
                    }
                    nodes.Add(chidNode);

                    if (item.IsEnd == true)
                    {
                        var buttons = BLL.ButtonToMenuService.GetButtonToMenuListByMenuId(item.MenuId);
                        if (buttons.Count() > 0)
                        {       ///该菜单、角色下按钮集合
                            string[] buttonList = BLL.ButtonPowerService.GetButtonPowerList(this.drpRole.Value, item.MenuId);
                            foreach (var itemButton in buttons)
                            {
                                TreeNode chidButtonNode = new TreeNode();
                                chidButtonNode.Text = itemButton.ButtonName;
                                chidButtonNode.NodeID = itemButton.ButtonToMenuId;
                                chidButtonNode.EnableCheckBox = true;
                                chidButtonNode.EnableCheckEvent = true;

                                if (buttonList != null && buttonList.Contains(chidButtonNode.Text))
                                {
                                    chidButtonNode.Checked = true;
                                    chidButtonNode.Expanded = true;
                                }
                                chidNode.Nodes.Add(chidButtonNode);
                            }
                        }
                    }
                    else
                    {
                        this.BoundTree(chidNode.Nodes, sysMenus, item.MenuId, roleId);
                    }
                }
            }
        }
        #endregion

        #region 全选、全不选
        /// <summary>
        /// 全选、全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvMenu_NodeCheck(object sender, FineUIPro.TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                this.tvMenu.CheckAllNodes(e.Node.Nodes);               
                SetCheckParentNode(e.Node);
            }
            else
            {
                this.tvMenu.UncheckAllNodes(e.Node.Nodes);
            }
        }

        /// <summary>
        /// 选中父节点
        /// </summary>
        /// <param name="node"></param>
        private void SetCheckParentNode(TreeNode node)
        {
            if (node.ParentNode != null && node.ParentNode.NodeID != "0")
            {
                node.ParentNode.Checked = true;
                if (node.ParentNode.ParentNode.NodeID != "0")
                {
                    SetCheckParentNode(node.ParentNode);
                }
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.RolePowerMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnCancel.Hidden = false;
                }
            }
            if (this.CurrUser.UserId == BLL.Const.sysglyId)
            {
                this.btnSave.Hidden = false;
                this.btnCancel.Hidden = false;
            }
        }
        #endregion

        #region 保存按钮事件
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string roleId = this.drpRole.Value;
            string menuTyp = String.Join(", ", this.ckMenuType.SelectedValueArray);
            if (!String.IsNullOrEmpty(roleId))
            {
                BLL.ButtonPowerService.DeleteButtonPowerByRoleIdMenuType(roleId,menuTyp);
                BLL.RolePowerService.DeleteRolePowerByRoleIdMenuType(roleId,menuTyp);
                TreeNode[] nodes = this.tvMenu.GetCheckedNodes();
                if (nodes.Length > 0)
                {
                    foreach (TreeNode tn in nodes)
                    {
                        if (tn.NodeID != "0")
                        {
                            if (BLL.RolePowerService.IsExistMenu(tn.NodeID))
                            {
                                Model.Sys_RolePower newPower = new Model.Sys_RolePower();
                                newPower.RoleId = roleId;
                                newPower.MenuId = tn.NodeID;
                                BLL.RolePowerService.SaveRolePower(newPower);
                            }

                            if (BLL.ButtonPowerService.isExistButtonToMenu(tn.NodeID))
                            {
                                Model.Sys_ButtonPower btn = new Model.Sys_ButtonPower();
                                btn.RoleId = roleId;
                                btn.MenuId = tn.ParentNode.NodeID;
                                btn.ButtonToMenuId = tn.NodeID;
                                BLL.ButtonPowerService.SaveButtonPower(btn);
                            }
                        }
                    }
                }

                BLL.LogService.AddLog(this.CurrUser.UserId, "保存角色菜单授权");
                ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("请选择角色！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.SelectedIndexChanged();
        }
    }
}
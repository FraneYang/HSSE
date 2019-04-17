using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class SystemMenuSet : PageBase
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
                /// 加载菜单树
                this.InitMenuTree(String.Join(", ", this.ckMenuType.SelectedValueArray));
            }
        }

        #region Tab1菜单更新按钮
        /// <summary>
        /// Tab1保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab1Save_Click(object sender, EventArgs e)
        {            
            this.SaveMenuAndGetXML();
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            BLL.LogService.AddLog( this.CurrUser.UserId, "修改功能菜单设置！");
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        /// <summary>
        /// 生成菜单及XML的方法
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="menuModel"></param>
        private void SaveMenuAndGetXML()
        {
            ////个人设置菜单
            //BLL.CreateMenuXML.getMenuXML(BLL.Const.Menu_Personal);
            ////本部管理菜单
            // BLL.CreateMenuXML.getMenuXML(BLL.Const.Menu_Sever);
            ////项目现场菜单
            BLL.CreateMenuXML.getMenuXML(BLL.Const.Menu_Project);
            ////公共资源菜单
            BLL.CreateMenuXML.getMenuXML(BLL.Const.Menu_Resource);
            ////基础信息菜单
            BLL.CreateMenuXML.getMenuXML(BLL.Const.Menu_BaseInfo);
            ////系统设置菜单
            BLL.CreateMenuXML.getMenuXML(BLL.Const.Menu_SystemSet);   
        }

        #region Tab2
        /// <summary>
        ///  菜单类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckMenuType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitMenuTree(String.Join(", ", this.ckMenuType.SelectedValueArray));
        }

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="menuList">菜单集合</param>
        private void InitMenuTree(string menuType)
        {
            this.tvMenu.Nodes.Clear();
            var menus = BLL.SysMenuService.GetMenuListByMenuType(menuType);
            if (menus.Count() > 0)
            {
                TreeNode rootNode = new TreeNode
                {
                    Text = "菜单",
                    NodeID = "0",
                    EnableCheckBox = true,
                    EnableCheckEvent = true,
                    Expanded = true
                };
                this.tvMenu.Nodes.Add(rootNode);
                this.BoundTree(rootNode.Nodes, menus, rootNode.NodeID);
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, List<Model.Sys_Menu> sysMenus, string superMenuId)
        {
            var menus = sysMenus.Where(x => x.SuperMenu == superMenuId).OrderBy(x => x.SortIndex);
            if (menus.Count() > 0)
            {
                foreach (var item in menus)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.MenuName,
                        NodeID = item.MenuId,
                        EnableCheckBox = true,
                        EnableCheckEvent = true
                    };

                    if (item.IsUsed == true || !item.IsUsed.HasValue)
                    {
                        chidNode.Checked = true;
                        chidNode.Expanded = true;
                        chidNode.Selectable = true;
                    }

                    nodes.Add(chidNode);
                    if (!item.IsEnd.HasValue || item.IsEnd == false)
                    {
                        this.BoundTree(chidNode.Nodes, sysMenus, item.MenuId);
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

        #region Tab2保存按钮
        /// <summary>
        /// Tab2保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab2Save_Click(object sender, EventArgs e)
        {            
            string menuType = String.Join(", ", this.ckMenuType.SelectedValueArray);
            if (!string.IsNullOrEmpty(menuType))
            {
                var updateMenu = BLL.SysMenuService.GetIsUsedMenuListByMenuType(menuType);
                if (updateMenu.Count() > 0)
                {
                    foreach (var item in updateMenu)
                    {
                        item.IsUsed = false;
                        BLL.SysMenuService.UpdateMenu(item);
                    }
                }
 
                TreeNode[] nodes = this.tvMenu.GetCheckedNodes();
                if (nodes.Length > 0)
                {
                    foreach (TreeNode tn in nodes)
                    {
                        if (tn.NodeID != "0")
                        {
                            if (BLL.RolePowerService.IsExistMenu(tn.NodeID))
                            {
                                var menuUsed = BLL.SysMenuService.GetSysMenuByMenuId(tn.NodeID);
                                if (menuUsed != null)
                                {
                                    menuUsed.IsUsed = true;
                                    BLL.SysMenuService.UpdateMenu(menuUsed);
                                }
                            }                       
                        }
                    }
                }

                this.SaveMenuAndGetXML();
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改功能菜单设置！");
            }
        }
        #endregion
        #endregion

        #region Tab3
        #region Tab3保存按钮
        /// <summary>
        /// Tab3保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab3Save_Click(object sender, EventArgs e)
        {
        }
        #endregion
        #endregion
    }
}
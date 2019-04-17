using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace BLL
{
    public static class CreateMenuXML
    {
        /// <summary>
        /// 取菜单创建XMl
        /// </summary>
        /// <param name="menuType">菜单类型</param>
        /// <param name="menuModel">菜单模式</param>
        public static void getMenuXML(string menuType)
        {
            var sysMenus = from x in Funs.DB.Sys_Menu where (x.IsUsed == true || !x.IsUsed.HasValue) && x.MenuType == menuType select x;
            CreateMenuDataXML(menuType, sysMenus.ToList(), "0", null);
        }

        #region 创建菜单信息XML方法
        /// <summary>
        /// 创建菜单信息XML方法
        /// </summary>
        /// <param name="fileName"></param>
        public static void CreateMenuDataXML(string menuType, List<Model.Sys_Menu> menusList, string superMenu, XmlTextWriter writer)
        {           
            try
            {
                if (superMenu == "0")
                {
                    ///xml文件路径名
                    string fileName = Funs.RootPath + "common\\" + menuType + ".xml";
                    FileStream fileStream = new FileStream(fileName, FileMode.Create);
                    writer = new XmlTextWriter(fileStream, Encoding.UTF8)
                    {
                        //使用自动缩进便于阅读
                        Formatting = Formatting.Indented
                    };
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Tree");    //创建父节点
                    var menuItemList = menusList.Where(x => x.SuperMenu == superMenu).OrderBy(x=>x.SortIndex);    //获取菜单列表
                    if (menuItemList.Count() > 0)
                    {
                        foreach (var item in menuItemList)
                        {
                            writer.WriteStartElement("TreeNode");    //创建子节点
                            writer.WriteAttributeString("id", item.MenuId);    //添加属性                          
                            writer.WriteAttributeString("Text", item.MenuName);
                            writer.WriteAttributeString("NavigateUrl", item.Url);
                            if (!string.IsNullOrEmpty(item.Icon))
                            {
                                writer.WriteAttributeString("Icon", item.Icon);
                            }else
                            {
                                writer.WriteAttributeString("Icon", "LayoutContent");
                            }
                            if (!item.IsEnd.HasValue || item.IsEnd == false)
                            {
                                CreateMenuDataXML(menuType, menusList, item.MenuId, writer);
                            }
                            writer.WriteFullEndElement();    //子节点结束
                            //在节点间添加一些空格
                            writer.WriteWhitespace("\n");
                        }
                    }
                    writer.WriteFullEndElement();    //父节点结束
                    writer.Close();
                    fileStream.Close();
                }
                else
                {
                    var subMenuItemList = menusList.Where(x => x.SuperMenu == superMenu).OrderBy(x => x.SortIndex);    //获取菜单集合
                    if (subMenuItemList.Count() > 0)
                    {
                        foreach (var item in subMenuItemList)
                        {
                            //使用自动缩进便于阅读
                            writer.Formatting = Formatting.Indented;
                            writer.WriteStartElement("TreeNode");    //创建子节点
                            writer.WriteAttributeString("id", item.MenuId);    //添加属性
                            writer.WriteAttributeString("Text", item.MenuName);
                            writer.WriteAttributeString("NavigateUrl", item.Url);
                            if (!string.IsNullOrEmpty(item.Icon))
                            {
                                writer.WriteAttributeString("Icon", item.Icon);
                            }
                            else
                            {
                                writer.WriteAttributeString("Icon", "LayoutContent");
                            }
                            if (!item.IsEnd.HasValue || item.IsEnd == false)
                            {
                                CreateMenuDataXML(menuType, menusList, item.MenuId, writer);
                            }
                            writer.WriteFullEndElement();    //子节点结束
                            //在节点间添加一些空格
                            writer.WriteWhitespace("\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
        }
        #endregion
    }
}
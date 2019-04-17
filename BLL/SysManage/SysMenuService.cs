using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
   public static class SysMenuService
    {
       public static Model.HSSEDB_ENN db = Funs.DB;

       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static List<Model.Sys_Menu> GetSupMenuListBySuperMenu(string superMenu)
       {
           var list = (from x in Funs.DB.Sys_Menu where x.SuperMenu == superMenu orderby x.SortIndex select x).ToList();          
           return list;
       }


       /// <summary>
       /// 根据MenuId获取菜单名称项
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static Model.Sys_Menu GetSysMenuByMenuId(string menuId)
       {
           return Funs.DB.Sys_Menu.FirstOrDefault(x => x.MenuId == menuId);
       }

       /// <summary>
       /// 根据MenuType获取菜单集合
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static List<Model.Sys_Menu> GetMenuListByMenuType(string menuType)
       {
           var list = (from x in Funs.DB.Sys_Menu where x.MenuType == menuType orderby x.SortIndex select x).ToList();
           return list;
       }

       /// <summary>
       /// 根据MenuType获取菜单集合
       /// </summary>
       /// <param name="menuId"></param>
       /// <returns></returns>
       public static List<Model.Sys_Menu> GetIsUsedMenuListByMenuType(string menuType)
       {
           var list = (from x in Funs.DB.Sys_Menu                     
                       where x.MenuType == menuType && x.IsUsed == true
                       orderby x.SortIndex select x).ToList();
           return list;
       }

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="teamGroup"></param>
       public static void UpdateMenu(Model.Sys_Menu menu)
       {
           Model.HSSEDB_ENN db = Funs.DB;
           Model.Sys_Menu newMenu = db.Sys_Menu.FirstOrDefault(e => e.MenuId == menu.MenuId);
           if (newMenu != null)
           {
               newMenu.IsUsed = menu.IsUsed;
               newMenu.ModifyDate = System.DateTime.Now;
               db.SubmitChanges();
           }
       }     
    }
}

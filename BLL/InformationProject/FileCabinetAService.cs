using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class FileCabinetAService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据用户id获取文件柜主表列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.InformationProject_FileCabinetA> GetFileCabinetAList()
        {
            var FileCabinetAList = from x in Funs.DB.InformationProject_FileCabinetA 
                                   orderby x.Code
                                   select x;
            return FileCabinetAList.ToList();
        }

        /// <summary>
        /// 根据主键id获取文件柜
        /// </summary>
        /// <returns></returns>
        public static Model.InformationProject_FileCabinetA GetFileCabinetAByID(string FileCabinetAId)
        {
            return Funs.DB.InformationProject_FileCabinetA.FirstOrDefault(x => x.FileCabinetAId == FileCabinetAId);
        }

        /// <summary>
        /// 根据菜单id项目id获取文件柜
        /// </summary>
        /// <returns></returns>
        public static Model.InformationProject_FileCabinetA GetFileCabinetAByMenuId(string MenuId)
        {
            return Funs.DB.InformationProject_FileCabinetA.FirstOrDefault(x => x.MenuId == MenuId);
        }

        /// <summary>
        /// 添加文件柜
        /// </summary>
        /// <param name="FileCabinetA"></param>
        public static void AddFileCabinetA(Model.InformationProject_FileCabinetA FileCabinetA)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.InformationProject_FileCabinetA newFileCabinetA = new Model.InformationProject_FileCabinetA
            {
                FileCabinetAId = FileCabinetA.FileCabinetAId,
                MenuId = FileCabinetA.MenuId,
                Code = FileCabinetA.Code,
                Title = FileCabinetA.Title,
                SupFileCabinetAId = FileCabinetA.SupFileCabinetAId,
            };
            db.InformationProject_FileCabinetA.InsertOnSubmit(newFileCabinetA);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改文件柜
        /// </summary>
        /// <param name="FileCabinetA"></param>
        public static void UpdateFileCabinetA(Model.InformationProject_FileCabinetA FileCabinetA)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.InformationProject_FileCabinetA newFileCabinetA = db.InformationProject_FileCabinetA.FirstOrDefault(e => e.FileCabinetAId == FileCabinetA.FileCabinetAId);
            if (newFileCabinetA != null)
            {
                newFileCabinetA.Code = FileCabinetA.Code;
                newFileCabinetA.Title = FileCabinetA.Title;
                newFileCabinetA.SupFileCabinetAId = FileCabinetA.SupFileCabinetAId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="FileCabinetAId"></param>
        public static void DeleteFileCabinetAByID(string FileCabinetAId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.InformationProject_FileCabinetA FileCabinetA = db.InformationProject_FileCabinetA.FirstOrDefault(e => e.FileCabinetAId == FileCabinetAId);
            {
                db.InformationProject_FileCabinetA.DeleteOnSubmit(FileCabinetA);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否存在文件夹名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistTitle(string FileCabinetAId, string supFileCabinetAId, string title)
        {
            var q = Funs.DB.InformationProject_FileCabinetA.FirstOrDefault(x => x.SupFileCabinetAId == supFileCabinetAId && x.Title == title
                    && x.FileCabinetAId != FileCabinetAId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否可删除节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteFileCabinetA(string FileCabinetAId)
        {
            bool isDelete = true;
            var FileCabinetA = GetFileCabinetAByID(FileCabinetAId);
            if (FileCabinetA != null)
            {
                var detailCout = Funs.DB.InformationProject_FileCabinetAItem.FirstOrDefault(x => x.FileCabinetAId == FileCabinetAId);
                if (detailCout != null)
                {
                    isDelete = false;
                }
                var supItemSetCount = Funs.DB.InformationProject_FileCabinetA.FirstOrDefault(x => x.SupFileCabinetAId == FileCabinetAId);
                if (supItemSetCount != null)
                {
                    isDelete = false;
                }
            }
            return isDelete;
        }

        #region 页面信息添加到文件柜A
        #region 添加到文件柜A中方法
        /// <summary>
        /// 添加到文件柜A中方法
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dataId"></param>
        /// <param name="content"></param>
        public static void AddFileCabinetA(string menuId, string dataId, string content, string url,string itemCode,string fileCabinetAId)
        {
            //var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByMenuId(menuId);
            //if (fileCabinetA != null)   ////判断这个菜单id 是否在文件柜A中 在则插入明细 不在则查找上一级菜单id
            //{
            //    AddDataToFileCabinetAItem(dataId, content, fileCabinetA.FileCabinetAId, url, itemCode);
            //}
            //else   ///增加改菜单到文件柜A主表 然后增加明细
            //{
            //    AddDataToFileCabinetA(menuId, dataId, content, url, itemCode);
            //}

            if (!string.IsNullOrEmpty(fileCabinetAId))
            {
                AddDataToFileCabinetAItem(dataId, content, fileCabinetAId, url, itemCode);
            }
            else
            {
                AddDataToFileCabinetA(menuId, dataId, content, url, itemCode);
            }
        }
        #endregion

        #region 添加到文件柜A中方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dataId"></param>
        /// <param name="content"></param>
        /// <param name="url"></param>
        public static void AddDataToFileCabinetA(string menuId, string dataId, string content, string url,string itemCode)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
            if (sysMenu != null)
            {
                Model.InformationProject_FileCabinetA newFileCabinetA = new Model.InformationProject_FileCabinetA
                {
                    FileCabinetAId = SQLHelper.GetNewID(typeof(Model.InformationProject_FileCabinetA)),                    
                    Code = sysMenu.SortIndex.ToString(),
                    Title = sysMenu.MenuName,
                    MenuId = menuId
                };
                if (menuId == "0")
                {
                    newFileCabinetA.SupFileCabinetAId = "0";
                }
                else
                {
                    newFileCabinetA.SupFileCabinetAId = null;
                }
                BLL.FileCabinetAService.AddFileCabinetA(newFileCabinetA);

                ///查询是否存在下级菜单
                var fileCabinetAList = from x in Funs.DB.InformationProject_FileCabinetA
                                       join y in Funs.DB.Sys_Menu on x.MenuId equals y.MenuId
                                       where x.SupFileCabinetAId == null  && y.SuperMenu == menuId
                                       select x;
                if (fileCabinetAList.Count() > 0)
                {
                    foreach (var item in fileCabinetAList)
                    {
                        item.SupFileCabinetAId = newFileCabinetA.FileCabinetAId;
                        BLL.FileCabinetAService.UpdateFileCabinetA(item);
                    }
                }
                if (sysMenu.IsEnd == true)  ///增加明细
                {
                    AddDataToFileCabinetAItem(dataId, content, newFileCabinetA.FileCabinetAId, url,itemCode);
                }
                var fileCabinetASuper = BLL.FileCabinetAService.GetFileCabinetAByMenuId(sysMenu.SuperMenu);
                if (fileCabinetASuper == null)  ///继续增加上级菜单
                {
                    AddDataToFileCabinetA(sysMenu.SuperMenu, dataId, content, url, itemCode);
                }
                else
                {

                    var fileCabinetASuperList = from x in Funs.DB.InformationProject_FileCabinetA
                                                join y in Funs.DB.Sys_Menu on x.MenuId equals y.MenuId
                                                where y.SuperMenu == fileCabinetASuper.MenuId && x.SupFileCabinetAId == null
                                                select x;
                    if (fileCabinetASuperList.Count() > 0)
                    {
                        foreach (var item in fileCabinetASuperList)
                        {
                            item.SupFileCabinetAId = fileCabinetASuper.FileCabinetAId;
                            BLL.FileCabinetAService.UpdateFileCabinetA(item);
                        }
                    }
                }
            }
            else
            {
                ///查询是否存在下级菜单
                var fileCabinetAList = from x in Funs.DB.InformationProject_FileCabinetA
                                       where x.SupFileCabinetAId == null 
                                       select x;
                if (fileCabinetAList.Count() > 0)
                {
                    foreach (var item in fileCabinetAList)
                    {
                        item.SupFileCabinetAId = "0";
                        BLL.FileCabinetAService.UpdateFileCabinetA(item);
                    }
                }
            }
        }
        #endregion

        #region 增加文件柜明细
        /// <summary>
        /// 增加文件柜明细
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="content"></param>
        /// <param name="fileCabinetAId"></param>
        /// <param name="url"></param>
        public static void AddDataToFileCabinetAItem(string dataId, string content, string fileCabinetAId, string url, string itemCode)
        {
            var fileCabinetAItem = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(dataId); ///明细是否存在
            if (fileCabinetAItem != null)
            {
                fileCabinetAItem.Title = content;
                fileCabinetAItem.Code = itemCode;
                BLL.FileCabinetAItemService.UpdateFileCabinetAItem(fileCabinetAItem);
            }
            else
            {
                Model.InformationProject_FileCabinetAItem newFileCabinetAItem = new Model.InformationProject_FileCabinetAItem
                {
                    FileCabinetAItemId = dataId,
                    FileCabinetAId = fileCabinetAId,
                    Code = itemCode,
                    Title = content,
                    CompileDate = System.DateTime.Now,
                    IsMenu = true,
                    Url = url
                };
                BLL.FileCabinetAItemService.AddFileCabinetAItem(newFileCabinetAItem);
            }
        }
        #endregion

        #endregion
    }
}

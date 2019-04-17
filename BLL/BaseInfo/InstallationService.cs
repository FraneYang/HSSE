using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 装置
    /// </summary>
    public static class InstallationService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取装置
        /// </summary>
        /// <param name="InstallationId"></param>
        /// <returns></returns>
        public static Model.Base_Installation GetInstallationByInstallationId(string InstallationId)
        {
            return Funs.DB.Base_Installation.FirstOrDefault(e => e.InstallationId == InstallationId);
        }

        /// <summary>
        /// 根据主键获取装置
        /// </summary>
        /// <param name="InstallationId"></param>
        /// <returns></returns>
        public static string GetInstallationNameByInstallationId(string InstallationId)
        {
            string name = string.Empty;
            var install = Funs.DB.Base_Installation.FirstOrDefault(e => e.InstallationId == InstallationId);
            if (install != null)
            {
                name = install.InstallationName;
            }
            return name;
        }

        /// <summary>
        /// 获取装置名称是否存在
        /// </summary>
        /// <param name="installationId">装置id</param>
        /// <param name="installationName">名称</param>
        /// <returns>是否存在</returns>
        public static bool IsExistInstallationName(string installationId, string installationName)
        {
            bool isExist = false;
            var role = Funs.DB.Base_Installation.FirstOrDefault(x => x.InstallationName == installationName && x.InstallationId != installationId);
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加装置
        /// </summary>
        /// <param name="Installation"></param>
        public static void AddInstallation(Model.Base_Installation Installation)
        {
            Model.Base_Installation newInstallation = new Model.Base_Installation
            {
                InstallationId = Installation.InstallationId,
                DepartId = Installation.DepartId,
                InstallationCode = Installation.InstallationCode,
                InstallationName = Installation.InstallationName,
                IsUsed = Installation.IsUsed,
                Def = Installation.Def,
                ManagerIds = Installation.ManagerIds,
                ManagerNames = Installation.ManagerNames,
                InstallType = Installation.InstallType
            };
            db.Base_Installation.InsertOnSubmit(newInstallation);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改装置
        /// </summary>
        /// <param name="Installation"></param>
        public static void UpdateInstallation(Model.Base_Installation Installation)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Installation newInstallation = db.Base_Installation.FirstOrDefault(e => e.InstallationId == Installation.InstallationId);
            if (newInstallation != null)
            {
                newInstallation.DepartId = Installation.DepartId;
                newInstallation.InstallationCode = Installation.InstallationCode;
                newInstallation.InstallationName = Installation.InstallationName;
                newInstallation.IsUsed = Installation.IsUsed;
                newInstallation.Def = Installation.Def;
                newInstallation.ManagerIds = Installation.ManagerIds;
                newInstallation.ManagerNames = Installation.ManagerNames;
                newInstallation.InstallType = Installation.InstallType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除装置
        /// </summary>
        /// <param name="InstallationId"></param>
        public static void DeleteInstallationById(string InstallationId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Installation Installation = db.Base_Installation.FirstOrDefault(e => e.InstallationId == InstallationId);
            if (Installation != null)
            {
                db.Base_Installation.DeleteOnSubmit(Installation);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据部门Id获取装置下拉选择项
        /// </summary>
        /// <param name="departId"></param>
        /// <returns></returns>
        public static List<Model.Base_Installation> GetInstallationByDepartIdList(string departId)
        {
            if (!string.IsNullOrEmpty(departId))
            {
                if (departId.Contains(','))
                {
                    List<string> departIdList = Funs.GetStrListByStr(departId, ',');
                    return (from x in Funs.DB.Base_Installation
                            where departIdList.Contains(x.DepartId) && x.IsUsed == true || !x.IsUsed.HasValue
                            orderby x.InstallationCode
                            select x).ToList();
                }
                else
                {
                    return (from x in Funs.DB.Base_Installation
                            where x.DepartId == departId && x.IsUsed == true || !x.IsUsed.HasValue
                            orderby x.InstallationCode
                            select x).ToList();
                }
            }
            else
            {
                return (from x in Funs.DB.Base_Installation
                        where x.IsUsed == true || !x.IsUsed.HasValue
                        orderby x.InstallationCode
                        select x).ToList();
            }            
        }

        /// <summary>
        /// 根据装置类型获取装置下拉选择项
        /// </summary>
        /// <param name="installType"></param>
        /// <returns></returns>
        public static List<Model.Base_Installation> GetInstallationByInstallTypeList(string installType)
        {
            if (!string.IsNullOrEmpty(installType))
            {
                return (from x in Funs.DB.Base_Installation
                        where x.InstallType == installType && x.IsUsed == true || !x.IsUsed.HasValue
                        orderby x.InstallationCode
                        select x).ToList();
            }
            else
            {
                return (from x in Funs.DB.Base_Installation
                        where x.IsUsed == true || !x.IsUsed.HasValue
                        orderby x.InstallationCode
                        select x).ToList();
            }
        }
        
        /// <summary>
        /// 根据装置类型获取装置下拉选择项
        /// </summary>
        /// <param name="installType"></param>
        /// <returns></returns>
        public static List<Model.Base_Installation> GetInstallationByNoInstallTypeList(string installType)
        {
            if (!string.IsNullOrEmpty(installType))
            {
                return (from x in Funs.DB.Base_Installation
                        where x.InstallType != installType && x.IsUsed == true || !x.IsUsed.HasValue
                        orderby x.InstallationCode
                        select x).ToList();
            }
            else
            {
                return (from x in Funs.DB.Base_Installation
                        where x.IsUsed == true || !x.IsUsed.HasValue
                        orderby x.InstallationCode
                        select x).ToList();
            }
        }

        #region 装置表下拉框
        /// <summary>
        ///  装置表（按部门）下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitInstallationByDepartDropDownList(FineUIPro.DropDownList dropName, string departId, bool isShowPlease)
        {
            dropName.DataValueField = "InstallationId";
            dropName.DataTextField = "InstallationName";
            dropName.DataSource = BLL.InstallationService.GetInstallationByDepartIdList(departId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  装置(按类型)表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitInstallationByInstallTypeDropDownList(FineUIPro.DropDownList dropName, string installType, bool isShowPlease)
        {
            dropName.DataValueField = "InstallationId";
            dropName.DataTextField = "InstallationName";
            dropName.DataSource = BLL.InstallationService.GetInstallationByInstallTypeList(installType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  装置(按类型)表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitInstallationByNoInstallTypeDropDownList(FineUIPro.DropDownList dropName, string installType, bool isShowPlease)
        {
            dropName.DataValueField = "InstallationId";
            dropName.DataTextField = "InstallationName";
            dropName.DataSource = BLL.InstallationService.GetInstallationByNoInstallTypeList(installType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion


        /// <summary>
        /// 得到部门名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string getInstallationNamesInstallationIds(object installationIds)
        {
            string installationName = string.Empty;
            if (installationIds != null)
            {
                string[] roles = installationIds.ToString().Split(',');
                foreach (string roleId in roles)
                {
                    var q = GetInstallationByInstallationId(roleId);
                    if (q != null)
                    {
                        installationName += q.InstallationName + ",";
                    }
                }
                if (installationName != string.Empty)
                {
                    installationName = installationName.Substring(0, installationName.Length - 1); ;
                }
            }

            return installationName;
        }

        /// <summary>
        /// 获取装置下拉选择项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Installation> GetInstallationList()
        {
            return (from x in db.Base_Installation orderby x.InstallationCode select x).ToList();
        }
    }
}

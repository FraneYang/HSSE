using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
 /// <summary>
 /// 部门服务类
 /// </summary>
 public static class DepartService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_Depart GetDepartById(string departId)
        {
            var dep = Funs.DB.Base_Depart.FirstOrDefault(x => x.DepartId == departId);
            return dep;
        }

        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="depart"></param>
        public static void AddDepart(Model.Base_Depart depart)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Depart newDepart = new Model.Base_Depart
            {
                DepartId = depart.DepartId,
                DepartCode = depart.DepartCode,
                DepartName = depart.DepartName,
                Remark = depart.Remark,
                ManagerIds = depart.ManagerIds,
                ManagerNames = depart.ManagerNames,
                IsUsed = depart.IsUsed,
            };
            db.Base_Depart.InsertOnSubmit(newDepart);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateDepart(Model.Base_Depart depart)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Depart newDepart = db.Base_Depart.FirstOrDefault(e => e.DepartId == depart.DepartId);
            if (newDepart != null)
            {
                newDepart.DepartCode = depart.DepartCode;
                newDepart.DepartName = depart.DepartName;
                newDepart.Remark = depart.Remark;
                newDepart.IsUsed = depart.IsUsed;
                newDepart.ManagerIds = depart.ManagerIds;
                newDepart.ManagerNames = depart.ManagerNames;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="departId"></param>
        public static void DeleteDepartById(string departId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Depart depart = db.Base_Depart.FirstOrDefault(e => e.DepartId == departId);
            {
                db.Base_Depart.DeleteOnSubmit(depart);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Depart> GetDepartList()
        {
            var list = (from x in Funs.DB.Base_Depart
                        where x.IsUsed == true || !x.IsUsed.HasValue
                        orderby x.DepartCode select x).ToList();
            return list;
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitDepartDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "DepartId";
            dropName.DataTextField = "DepartName";
            dropName.DataSource = BLL.DepartService.GetDepartList();
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
        public static string getDepartNamesDepartIds(object departIds)
        {
            string DepartName = string.Empty;
            if (departIds != null)
            {
                string[] roles = departIds.ToString().Split(',');
                foreach (string roleId in roles)
                {
                    var q = GetDepartById(roleId);
                    if (q != null)
                    {
                        DepartName += q.DepartName + ",";
                    }
                }
                if (DepartName != string.Empty)
                {
                    DepartName = DepartName.Substring(0, DepartName.Length - 1); ;
                }
            }

            return DepartName;
        }
    }
}
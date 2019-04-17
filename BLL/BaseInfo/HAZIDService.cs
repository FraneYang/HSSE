using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 危害辨识
    /// </summary>
    public static class HAZIDService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取危害辨识
        /// </summary>
        /// <param name="HAZIDId"></param>
        /// <returns></returns>
        public static Model.Base_HAZID GetHAZIDByHAZIDId(string HAZIDId)
        {
            return Funs.DB.Base_HAZID.FirstOrDefault(e => e.HAZIDId == HAZIDId);
        }

        /// <summary>
        /// 获取危害辨识名称是否存在
        /// </summary>
        /// <param name="HAZIDId">危害辨识id</param>
        /// <param name="HAZIDName">名称</param>
        /// <returns>是否存在</returns>
        public static bool IsExistHAZIDName(string HAZIDId, string HAZIDName)
        {
            bool isExist = false;
            var role = Funs.DB.Base_HAZID.FirstOrDefault(x => x.HAZIDName == HAZIDName && x.HAZIDId != HAZIDId);
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加危害辨识
        /// </summary>
        /// <param name="HAZID"></param>
        public static void AddHAZID(Model.Base_HAZID HAZID)
        {
            Model.Base_HAZID newHAZID = new Model.Base_HAZID();
            newHAZID.HAZIDId = HAZID.HAZIDId;
            newHAZID.HAZIDCode = HAZID.HAZIDCode;
            newHAZID.HAZIDName = HAZID.HAZIDName;
            db.Base_HAZID.InsertOnSubmit(newHAZID);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改危害辨识
        /// </summary>
        /// <param name="HAZID"></param>
        public static void UpdateHAZID(Model.Base_HAZID HAZID)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_HAZID newHAZID = db.Base_HAZID.FirstOrDefault(e => e.HAZIDId == HAZID.HAZIDId);
            if (newHAZID != null)
            {
                newHAZID.HAZIDCode = HAZID.HAZIDCode;
                newHAZID.HAZIDName = HAZID.HAZIDName;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除危害辨识
        /// </summary>
        /// <param name="HAZIDId"></param>
        public static void DeleteHAZIDById(string HAZIDId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_HAZID HAZID = db.Base_HAZID.FirstOrDefault(e => e.HAZIDId == HAZIDId);
            if (HAZID != null)
            {
                db.Base_HAZID.DeleteOnSubmit(HAZID);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据部门Id获取危害辨识下拉选择项
        /// </summary>
        /// <param name="departId"></param>
        /// <returns></returns>
        public static List<Model.Base_HAZID> GetHAZIDList()
        {
            return (from x in Funs.DB.Base_HAZID
                    orderby x.HAZIDCode
                    select x).ToList();
        }

        #region 危害辨识表下拉框
        /// <summary>
        ///  危害辨识表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitHAZIDDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "HAZIDId";
            dropName.DataTextField = "HAZIDName";
            dropName.DataSource = BLL.HAZIDService.GetHAZIDList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

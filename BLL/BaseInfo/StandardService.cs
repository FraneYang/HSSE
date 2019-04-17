using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
  public static  class StandardService
    {

        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据专业主键获取标准信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Base_Standard> GetStandardListBySpecialtyId(string SpecialtyId)
        {
            return (from x in Funs.DB.Base_Standard where x.SpecialtyId == SpecialtyId orderby x.StandardName select x).ToList();
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="StandardId"></param>
        /// <returns></returns>
        public static Model.Base_Standard GetStandardById(string StandardId)
        {
            return Funs.DB.Base_Standard.FirstOrDefault(e => e.StandardId == StandardId);
        }

        /// <summary>
        /// 根据名称获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_Standard GetStandardByName(string StandardName)
        {
            return Funs.DB.Base_Standard.FirstOrDefault(e => e.StandardName == StandardName);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="StandardId"></param>
        /// <param name="StandardName"></param>
        /// <returns></returns>
        public static bool IsExistStandardName(string SpecialtyId, string StandardId, string StandardName)
        {
            bool isExist = false;
            var standard = new Model.Base_Standard();
            if (!string.IsNullOrEmpty(StandardId))
            {
                standard = Funs.DB.Base_Standard.FirstOrDefault(x => x.SpecialtyId == SpecialtyId && x.StandardName == StandardName && x.StandardId != StandardId);
            }
            else
            {
                standard = Funs.DB.Base_Standard.FirstOrDefault(x => x.SpecialtyId == SpecialtyId && x.StandardName == StandardName );
            }
            
            if (standard != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddStandard(Model.Base_Standard Standard)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Standard newStandard = new Model.Base_Standard
            {
                StandardId = Standard.StandardId,
                StandardCode = Standard.StandardCode,
                StandardName = Standard.StandardName,
                Remark = Standard.Remark,
                SpecialtyId = Standard.SpecialtyId
            };
            db.Base_Standard.InsertOnSubmit(newStandard);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateStandard(Model.Base_Standard Standard)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Standard newStandard = db.Base_Standard.FirstOrDefault(e => e.StandardId == Standard.StandardId);
            if (newStandard != null)
            {
                newStandard.StandardCode = Standard.StandardCode;
                newStandard.StandardName = Standard.StandardName;
                newStandard.Remark = Standard.Remark;
                newStandard.SpecialtyId = Standard.SpecialtyId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="StandardId"></param>
        public static void DeleteStandardById(string StandardId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Standard Standard = db.Base_Standard.FirstOrDefault(e => e.StandardId == StandardId);
            {
                db.Base_Standard.DeleteOnSubmit(Standard);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Standard> GetStandardList()
        {
            var list = (from x in Funs.DB.Base_Standard orderby x.StandardCode select x).ToList();
            return list;
        }
        
        #region 基础表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitStandardDropDownList(FineUIPro.DropDownList dropName,string SpecialtyId,  bool isShowPlease)
        {
            dropName.DataValueField = "StandardId";
            dropName.DataTextField = "StandardName";
            dropName.DataSource = BLL.StandardService.GetStandardListBySpecialtyId(SpecialtyId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion        
    }
}
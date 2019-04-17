using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 隐患类别
    /// </summary>
    public static class HiddenHazardTypeService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取隐患类别
        /// </summary>
        /// <param name="HiddenHazardTypeId"></param>
        /// <returns></returns>
        public static Model.Base_HiddenHazardType GetHiddenHazardTypeByHiddenHazardTypeId(string HiddenHazardTypeId)
        {
            return Funs.DB.Base_HiddenHazardType.FirstOrDefault(e => e.HiddenHazardTypeId == HiddenHazardTypeId);
        }

        /// <summary>
        /// 获取隐患类别名称是否存在
        /// </summary>
        /// <param name="HiddenHazardTypeId">隐患类别id</param>
        /// <param name="HiddenHazardTypeName">名称</param>
        /// <returns>是否存在</returns>
        public static bool IsExistHiddenHazardTypeName(string HiddenHazardTypeId, string HiddenHazardTypeName)
        {
            bool isExist = false;
            var role = Funs.DB.Base_HiddenHazardType.FirstOrDefault(x => x.HiddenHazardTypeName == HiddenHazardTypeName && x.HiddenHazardTypeId != HiddenHazardTypeId);
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加隐患类别
        /// </summary>
        /// <param name="HiddenHazardType"></param>
        public static void AddHiddenHazardType(Model.Base_HiddenHazardType HiddenHazardType)
        {
            Model.Base_HiddenHazardType newHiddenHazardType = new Model.Base_HiddenHazardType();
            newHiddenHazardType.HiddenHazardTypeId = HiddenHazardType.HiddenHazardTypeId;
            newHiddenHazardType.HiddenHazardTypeCode = HiddenHazardType.HiddenHazardTypeCode;
            newHiddenHazardType.HiddenHazardTypeName = HiddenHazardType.HiddenHazardTypeName;
            db.Base_HiddenHazardType.InsertOnSubmit(newHiddenHazardType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改隐患类别
        /// </summary>
        /// <param name="HiddenHazardType"></param>
        public static void UpdateHiddenHazardType(Model.Base_HiddenHazardType HiddenHazardType)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_HiddenHazardType newHiddenHazardType = db.Base_HiddenHazardType.FirstOrDefault(e => e.HiddenHazardTypeId == HiddenHazardType.HiddenHazardTypeId);
            if (newHiddenHazardType != null)
            {
                newHiddenHazardType.HiddenHazardTypeCode = HiddenHazardType.HiddenHazardTypeCode;
                newHiddenHazardType.HiddenHazardTypeName = HiddenHazardType.HiddenHazardTypeName;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除隐患类别
        /// </summary>
        /// <param name="HiddenHazardTypeId"></param>
        public static void DeleteHiddenHazardTypeById(string HiddenHazardTypeId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_HiddenHazardType HiddenHazardType = db.Base_HiddenHazardType.FirstOrDefault(e => e.HiddenHazardTypeId == HiddenHazardTypeId);
            if (HiddenHazardType != null)
            {
                db.Base_HiddenHazardType.DeleteOnSubmit(HiddenHazardType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据部门Id获取隐患类别下拉选择项
        /// </summary>
        /// <param name="departId"></param>
        /// <returns></returns>
        public static List<Model.Base_HiddenHazardType> GetHiddenHazardTypeList()
        {
            return (from x in Funs.DB.Base_HiddenHazardType
                    orderby x.HiddenHazardTypeCode
                    select x).ToList();
        }

        #region 隐患类别表下拉框
        /// <summary>
        ///  隐患类别表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitHiddenHazardTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "HiddenHazardTypeId";
            dropName.DataTextField = "HiddenHazardTypeName";
            dropName.DataSource = BLL.HiddenHazardTypeService.GetHiddenHazardTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

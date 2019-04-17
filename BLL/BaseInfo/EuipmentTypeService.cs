using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 设备设施类型
    /// </summary>
    public static class EuipmentTypeService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取设备设施类型
        /// </summary>
        /// <param name="EuipmentTypeId"></param>
        /// <returns></returns>
        public static Model.Base_EuipmentType GetEuipmentTypeByEuipmentTypeId(string EuipmentTypeId)
        {
            return Funs.DB.Base_EuipmentType.FirstOrDefault(e => e.EuipmentTypeId == EuipmentTypeId);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="EuipmentTypeId"></param>
        /// <param name="EuipmentTypeName"></param>
        /// <returns></returns>
        public static bool IsExistEuipmentTypeName(string EuipmentTypeId, string EuipmentTypeName)
        {
            bool isExist = false;
            Model.Base_EuipmentType EuipmentType = new Model.Base_EuipmentType();
            if (!string.IsNullOrEmpty(EuipmentTypeId))
            {
                EuipmentType = Funs.DB.Base_EuipmentType.FirstOrDefault(x => x.EuipmentTypeName == EuipmentTypeName && x.EuipmentTypeId != EuipmentTypeId);
            }
            else
            {
                EuipmentType = Funs.DB.Base_EuipmentType.FirstOrDefault(x => x.EuipmentTypeName == EuipmentTypeName);
            }
            if (EuipmentType != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加设备设施类型
        /// </summary>
        /// <param name="EuipmentType"></param>
        public static void AddEuipmentType(Model.Base_EuipmentType EuipmentType)
        {
            Model.Base_EuipmentType newEuipmentType = new Model.Base_EuipmentType
            {
                EuipmentTypeId = EuipmentType.EuipmentTypeId,
                EuipmentTypeCode = EuipmentType.EuipmentTypeCode,
                EuipmentTypeName = EuipmentType.EuipmentTypeName,
                Remark = EuipmentType.Remark
            };
            db.Base_EuipmentType.InsertOnSubmit(newEuipmentType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改设备设施类型
        /// </summary>
        /// <param name="EuipmentType"></param>
        public static void UpdateEuipmentType(Model.Base_EuipmentType EuipmentType)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_EuipmentType newEuipmentType = db.Base_EuipmentType.FirstOrDefault(e => e.EuipmentTypeId == EuipmentType.EuipmentTypeId);
            if (newEuipmentType != null)
            {
                newEuipmentType.EuipmentTypeCode = EuipmentType.EuipmentTypeCode;
                newEuipmentType.EuipmentTypeName = EuipmentType.EuipmentTypeName;
                newEuipmentType.Remark = EuipmentType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除设备设施类型
        /// </summary>
        /// <param name="EuipmentTypeId"></param>
        public static void DeleteEuipmentTypeById(string EuipmentTypeId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_EuipmentType EuipmentType = db.Base_EuipmentType.FirstOrDefault(e => e.EuipmentTypeId == EuipmentTypeId);
            if (EuipmentType != null)
            {
                db.Base_EuipmentType.DeleteOnSubmit(EuipmentType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据装置Id获取设备设施类型下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_EuipmentType> GetEuipmentTypeList()
        {
            var EuipmentType = (from x in Funs.DB.Base_EuipmentType orderby x.EuipmentTypeCode select x).ToList();           
            return EuipmentType;
        }

        #region 设备设施类型表下拉框
        /// <summary>
        ///  设备设施类型表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitEuipmentTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "EuipmentTypeId";
            dropName.DataTextField = "EuipmentTypeName";
            dropName.DataSource = BLL.EuipmentTypeService.GetEuipmentTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
  public static  class ManagedObjectService
    {

        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据专业主键获取标准信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Standard_ManagedObject> GetManagedObjectListByStandardId(string StandardId)
        {
            return (from x in Funs.DB.Standard_ManagedObject where x.StandardId == StandardId orderby x.ManagedObjectName select x).ToList();
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="ManagedObjectId"></param>
        /// <returns></returns>
        public static Model.Standard_ManagedObject GetManagedObjectById(string ManagedObjectId)
        {
            return Funs.DB.Standard_ManagedObject.FirstOrDefault(e => e.ManagedObjectId == ManagedObjectId);
        }

        /// <summary>
        /// 根据名称获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Standard_ManagedObject GetManagedObjectByName(string ManagedObjectName)
        {
            return Funs.DB.Standard_ManagedObject.FirstOrDefault(e => e.ManagedObjectName == ManagedObjectName);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="ManagedObjectId"></param>
        /// <param name="ManagedObjectName"></param>
        /// <returns></returns>
        public static bool IsExistManagedObjectName(string StandardId, string ManagedObjectId, string ManagedObjectName)
        {
            bool isExist = false;
            Model.Standard_ManagedObject ManagedObject = new Model.Standard_ManagedObject();
            if (!string.IsNullOrEmpty(ManagedObjectId))
            {
                ManagedObject = Funs.DB.Standard_ManagedObject.FirstOrDefault(x => x.StandardId == StandardId && x.ManagedObjectName == ManagedObjectName && x.ManagedObjectId != ManagedObjectId);
            }
            else
            {
                ManagedObject = Funs.DB.Standard_ManagedObject.FirstOrDefault(x => x.StandardId == StandardId && x.ManagedObjectName == ManagedObjectName);
            }
            if (ManagedObject != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddManagedObject(Model.Standard_ManagedObject ManagedObject)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_ManagedObject newManagedObject = new Model.Standard_ManagedObject
            {
                ManagedObjectId = ManagedObject.ManagedObjectId,
                ManagedObjectCode = ManagedObject.ManagedObjectCode,
                ManagedObjectName = ManagedObject.ManagedObjectName,
                Remark = ManagedObject.Remark,
                StandardId = ManagedObject.StandardId
            };
            db.Standard_ManagedObject.InsertOnSubmit(newManagedObject);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateManagedObject(Model.Standard_ManagedObject ManagedObject)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_ManagedObject newManagedObject = db.Standard_ManagedObject.FirstOrDefault(e => e.ManagedObjectId == ManagedObject.ManagedObjectId);
            if (newManagedObject != null)
            {
                newManagedObject.ManagedObjectCode = ManagedObject.ManagedObjectCode;
                newManagedObject.ManagedObjectName = ManagedObject.ManagedObjectName;
                newManagedObject.Remark = ManagedObject.Remark;
                newManagedObject.StandardId = ManagedObject.StandardId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ManagedObjectId"></param>
        public static void DeleteManagedObjectById(string ManagedObjectId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_ManagedObject ManagedObject = db.Standard_ManagedObject.FirstOrDefault(e => e.ManagedObjectId == ManagedObjectId);
            {
                db.Standard_ManagedObject.DeleteOnSubmit(ManagedObject);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Standard_ManagedObject> GetManagedObjectList()
        {
            var list = (from x in Funs.DB.Standard_ManagedObject orderby x.ManagedObjectCode select x).ToList();
            return list;
        }
        
        #region 基础表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitManagedObjectDropDownList(FineUIPro.DropDownList dropName, string StandardId, bool isShowPlease)
        {
            dropName.DataValueField = "ManagedObjectId";
            dropName.DataTextField = "ManagedObjectName";
            dropName.DataSource = BLL.ManagedObjectService.GetManagedObjectListByStandardId(StandardId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
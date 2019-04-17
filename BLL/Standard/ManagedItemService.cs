using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
  public static  class ManagedItemService
    {

        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据专业主键获取标准信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Standard_ManagedItem> GetManagedItemListByManagedObjectId(string ManagedObjectId)
        {
            return (from x in Funs.DB.Standard_ManagedItem where x.ManagedObjectId == ManagedObjectId orderby x.ManagedItemName select x).ToList();
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="ManagedItemId"></param>
        /// <returns></returns>
        public static Model.Standard_ManagedItem GetManagedItemById(string ManagedItemId)
        {
            return Funs.DB.Standard_ManagedItem.FirstOrDefault(e => e.ManagedItemId == ManagedItemId);
        }

        /// <summary>
        /// 根据名称获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Standard_ManagedItem GetManagedItemByName(string ManagedItemName)
        {
            return Funs.DB.Standard_ManagedItem.FirstOrDefault(e => e.ManagedItemName == ManagedItemName);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="ManagedItemId"></param>
        /// <param name="ManagedItemName"></param>
        /// <returns></returns>
        public static bool IsExistManagedItemName(string ManagedObjectId, string ManagedItemId, string ManagedItemName)
        {
            bool isExist = false;
            Model.Standard_ManagedItem ManagedItem = new Model.Standard_ManagedItem();
            if (!string.IsNullOrEmpty(ManagedItemId))
            {
                ManagedItem = Funs.DB.Standard_ManagedItem.FirstOrDefault(x => x.ManagedObjectId == ManagedObjectId && x.ManagedItemName == ManagedItemName && x.ManagedItemId != ManagedItemId);
            }
            else
            {
                ManagedItem = Funs.DB.Standard_ManagedItem.FirstOrDefault(x => x.ManagedObjectId == ManagedObjectId && x.ManagedItemName == ManagedItemName);
            }
            if (ManagedItem != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddManagedItem(Model.Standard_ManagedItem ManagedItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_ManagedItem newManagedItem = new Model.Standard_ManagedItem
            {
                ManagedItemId = ManagedItem.ManagedItemId,
                ManagedItemCode = ManagedItem.ManagedItemCode,
                ManagedItemName = ManagedItem.ManagedItemName,
                Remark = ManagedItem.Remark,
                ManagedObjectId = ManagedItem.ManagedObjectId
            };
            db.Standard_ManagedItem.InsertOnSubmit(newManagedItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateManagedItem(Model.Standard_ManagedItem ManagedItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_ManagedItem newManagedItem = db.Standard_ManagedItem.FirstOrDefault(e => e.ManagedItemId == ManagedItem.ManagedItemId);
            if (newManagedItem != null)
            {
                newManagedItem.ManagedItemCode = ManagedItem.ManagedItemCode;
                newManagedItem.ManagedItemName = ManagedItem.ManagedItemName;
                newManagedItem.Remark = ManagedItem.Remark;
                newManagedItem.ManagedObjectId = ManagedItem.ManagedObjectId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ManagedItemId"></param>
        public static void DeleteManagedItemById(string ManagedItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_ManagedItem ManagedItem = db.Standard_ManagedItem.FirstOrDefault(e => e.ManagedItemId == ManagedItemId);
            {
                db.Standard_ManagedItem.DeleteOnSubmit(ManagedItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Standard_ManagedItem> GetManagedItemList()
        {
            var list = (from x in Funs.DB.Standard_ManagedItem orderby x.ManagedItemCode select x).ToList();
            return list;
        }
        
        #region 基础表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitManagedItemDropDownList(FineUIPro.DropDownList dropName, string ManagedObjectId, bool isShowPlease)
        {
            dropName.DataValueField = "ManagedItemId";
            dropName.DataTextField = "ManagedItemName";
            dropName.DataSource = BLL.ManagedItemService.GetManagedItemListByManagedObjectId(ManagedObjectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
  public static  class HSSEStandardService
    {

        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据专业主键获取标准信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<Model.Standard_HSSEStandard> GetHSSEStandardListByManagedItemId(string ManagedItemId)
        {
            return (from x in Funs.DB.Standard_HSSEStandard where x.ManagedItemId == ManagedItemId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="HSSEStandardId"></param>
        /// <returns></returns>
        public static Model.Standard_HSSEStandard GetHSSEStandardById(string HSSEStandardId)
        {
            return Funs.DB.Standard_HSSEStandard.FirstOrDefault(e => e.HSSEStandardId == HSSEStandardId);
        }

        /// <summary>
        /// 根据名称获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Standard_HSSEStandard GetHSSEStandardByName(string HSSEStandardName)
        {
            return Funs.DB.Standard_HSSEStandard.FirstOrDefault(e => e.HSSEStandardName == HSSEStandardName);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="HSSEStandardId"></param>
        /// <param name="HSSEStandardName"></param>
        /// <returns></returns>
        public static bool IsExistHSSEStandardName(string ManagedItemId, string HSSEStandardId, string HSSEStandardName)
        {
            bool isExist = false;
            Model.Standard_HSSEStandard hssetandard = new Model.Standard_HSSEStandard();
            if (!string.IsNullOrEmpty(HSSEStandardId))
            {
                hssetandard = Funs.DB.Standard_HSSEStandard.FirstOrDefault(x => x.ManagedItemId == ManagedItemId && x.HSSEStandardName == HSSEStandardName && x.HSSEStandardId != HSSEStandardId);
            }
            else
            {
                hssetandard = Funs.DB.Standard_HSSEStandard.FirstOrDefault(x => x.ManagedItemId == ManagedItemId && x.HSSEStandardName == HSSEStandardName);
            }
            if (hssetandard != null)

            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddHSSEStandard(Model.Standard_HSSEStandard HSSEStandard)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_HSSEStandard newHSSEStandard = new Model.Standard_HSSEStandard
            {
                HSSEStandardId = HSSEStandard.HSSEStandardId,
                HSSEStandardCode = HSSEStandard.HSSEStandardCode,
                HSSEStandardName = HSSEStandard.HSSEStandardName,
                ManagedItemId = HSSEStandard.ManagedItemId,
                FileContent = HSSEStandard.FileContent
            };
            db.Standard_HSSEStandard.InsertOnSubmit(newHSSEStandard);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateHSSEStandard(Model.Standard_HSSEStandard HSSEStandard)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_HSSEStandard newHSSEStandard = db.Standard_HSSEStandard.FirstOrDefault(e => e.HSSEStandardId == HSSEStandard.HSSEStandardId);
            if (newHSSEStandard != null)
            {
                newHSSEStandard.HSSEStandardCode = HSSEStandard.HSSEStandardCode;
                newHSSEStandard.HSSEStandardName = HSSEStandard.HSSEStandardName;
                newHSSEStandard.ManagedItemId = HSSEStandard.ManagedItemId;
                newHSSEStandard.FileContent = HSSEStandard.FileContent;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="HSSEStandardId"></param>
        public static void DeleteHSSEStandardById(string HSSEStandardId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Standard_HSSEStandard HSSEStandard = db.Standard_HSSEStandard.FirstOrDefault(e => e.HSSEStandardId == HSSEStandardId);
            {
                db.Standard_HSSEStandard.DeleteOnSubmit(HSSEStandard);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Standard_HSSEStandard> GetHSSEStandardList()
        {
            var list = (from x in Funs.DB.Standard_HSSEStandard orderby x.HSSEStandardCode select x).ToList();
            return list;
        }
        
        #region 基础表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitHSSEStandardDropDownList(FineUIPro.DropDownList dropName, string ManagedItemId, bool isShowPlease)
        {
            dropName.DataValueField = "HSSEStandardId";
            dropName.DataTextField = "HSSEStandardName";
            dropName.DataSource = BLL.HSSEStandardService.GetHSSEStandardListByManagedItemId(ManagedItemId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
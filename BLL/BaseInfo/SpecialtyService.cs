using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
  public static  class SpecialtyService
    {

        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_Specialty GetSpecialtyById(string SpecialtyId)
        {
            return Funs.DB.Base_Specialty.FirstOrDefault(e => e.SpecialtyId == SpecialtyId);
        }

        /// <summary>
        /// 根据名称获取信息
        /// </summary>
        /// <param name="SpecialtyName"></param>
        /// <returns></returns>
        public static Model.Base_Specialty GetSpecialtyByName(string SpecialtyName)
        {
            return Funs.DB.Base_Specialty.FirstOrDefault(e => e.SpecialtyName == SpecialtyName);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="SpecialtyId"></param>
        /// <param name="SpecialtyName"></param>
        /// <returns></returns>
        public static bool IsExistSpecialtyName(string SpecialtyId, string SpecialtyName)
        {
            bool isExist = false;
            Model.Base_Specialty Specialty = new Model.Base_Specialty();
            if (!string.IsNullOrEmpty(SpecialtyId))
            { Specialty = Funs.DB.Base_Specialty.FirstOrDefault(x => x.SpecialtyName == SpecialtyName && x.SpecialtyId != SpecialtyId); }
            else
            { Specialty = Funs.DB.Base_Specialty.FirstOrDefault(x => x.SpecialtyName == SpecialtyName); }

            if (Specialty != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddSpecialty(Model.Base_Specialty Specialty)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Specialty newSpecialty = new Model.Base_Specialty
            {
                SpecialtyId = Specialty.SpecialtyId,
                SpecialtyCode = Specialty.SpecialtyCode,
                SpecialtyName = Specialty.SpecialtyName,
                Remark = Specialty.Remark
            };
            db.Base_Specialty.InsertOnSubmit(newSpecialty);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateSpecialty(Model.Base_Specialty Specialty)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Specialty newSpecialty = db.Base_Specialty.FirstOrDefault(e => e.SpecialtyId == Specialty.SpecialtyId);
            if (newSpecialty != null)
            {
                newSpecialty.SpecialtyCode = Specialty.SpecialtyCode;
                newSpecialty.SpecialtyName = Specialty.SpecialtyName;
                newSpecialty.Remark = Specialty.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="SpecialtyId"></param>
        public static void DeleteSpecialtyById(string SpecialtyId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Specialty Specialty = db.Base_Specialty.FirstOrDefault(e => e.SpecialtyId == SpecialtyId);
            {
                db.Base_Specialty.DeleteOnSubmit(Specialty);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Specialty> GetSpecialtyList()
        {
            var list = (from x in Funs.DB.Base_Specialty orderby x.SpecialtyName select x).ToList();
            return list;
        }
        
        #region 基础表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitSpecialtyDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "SpecialtyId";
            dropName.DataTextField = "SpecialtyName";
            dropName.DataSource = BLL.SpecialtyService.GetSpecialtyList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 常量表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitSpecialtyRadioButtonList(FineUIPro.RadioButtonList rblName)
        {
            rblName.DataValueField = "SpecialtyId";
            rblName.DataTextField = "SpecialtyName";
            rblName.DataSource = BLL.SpecialtyService.GetSpecialtyList();
            rblName.DataBind();
            rblName.SelectedIndex = 0;
        }
        #endregion
    }
}
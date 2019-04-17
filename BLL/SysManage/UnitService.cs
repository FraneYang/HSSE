using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 单位信息
    /// </summary>
    public static class UnitService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static Model.Base_Unit GetUnitByUnitId(string unitId)
        {
            return Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId == unitId);
        }

        /// <summary>
        /// 添加单位信息
        /// </summary>
        /// <param name="unit"></param>
        public static void AddUnit(Model.Base_Unit unit)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Unit newUnit = new Model.Base_Unit();
            newUnit.UnitId = unit.UnitId;
            newUnit.UnitCode = unit.UnitCode;
            newUnit.UnitName = unit.UnitName;
            newUnit.UnitTypeId = unit.UnitTypeId;
            newUnit.Corporate = unit.Corporate;
            newUnit.Address = unit.Address;
            newUnit.Telephone = unit.Telephone;
            newUnit.Fax = unit.Fax;
            newUnit.EMail = unit.EMail;
            newUnit.ProjectRange = unit.ProjectRange;
            newUnit.IsThisUnit = unit.IsThisUnit;
            newUnit.IsHide = false;
            newUnit.ManagerIds = unit.ManagerIds;
            newUnit.ManagerNames = unit.ManagerNames;
            db.Base_Unit.InsertOnSubmit(newUnit);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改单位信息
        /// </summary>
        /// <param name="unit"></param>
        public static void UpdateUnit(Model.Base_Unit unit)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Unit newUnit = db.Base_Unit.FirstOrDefault(e => e.UnitId == unit.UnitId);
            if (newUnit != null)
            {
                newUnit.UnitCode = unit.UnitCode;
                newUnit.UnitName = unit.UnitName;
                newUnit.UnitTypeId = unit.UnitTypeId;
                newUnit.Corporate = unit.Corporate;
                newUnit.Address = unit.Address;
                newUnit.Telephone = unit.Telephone;
                newUnit.Fax = unit.Fax;
                newUnit.EMail = unit.EMail;
                newUnit.ProjectRange = unit.ProjectRange;
                newUnit.IsThisUnit = unit.IsThisUnit;
                newUnit.IsHide = unit.IsHide;
                newUnit.ManagerIds = unit.ManagerIds;
                newUnit.ManagerNames = unit.ManagerNames;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据单位ID删除单位信息
        /// </summary>
        /// <param name="unitId"></param>
        public static void DeleteUnitById(string unitId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Unit newUnit = db.Base_Unit.FirstOrDefault(e => e.UnitId == unitId);
            if (newUnit != null)
            {
                newUnit.IsHide = true;
                UpdateUnit(newUnit);
            }
        }

        /// <summary>
        /// 获取单位下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitDropDownList()
        {
            var list = (from x in Funs.DB.Base_Unit where (x.IsHide== null || x.IsHide == false) select x).OrderByDescending(x => x.IsThisUnit).ThenBy(x => x.UnitCode).ToList();
            return list;
        }

        /// <summary>
        /// 获取本单位下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetThisUnitDropDownList()
        {
            var list = (from x in Funs.DB.Base_Unit where x.IsThisUnit == true select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static string GetUnitNameByUnitId(string unitId)
        {
            string name = string.Empty;
            var unit = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId == unitId);
            if (unit != null)
            {
                name = unit.UnitName;
            }
            return name;
        }

        #region 单位表下拉框
        /// <summary>
        ///  单位表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitDropDownList(FineUIPro.DropDownList dropName,bool isShowPlease)
        {
            dropName.DataValueField = "UnitId";
            dropName.DataTextField = "UnitName";
            dropName.DataSource = BLL.UnitService.GetUnitDropDownList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

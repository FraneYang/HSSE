using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 动火方式
    /// </summary>
    public static class FireWorkModeService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_FireWorkMode GetFireWorkModeById(string fireWorkModeId)
        {
            return Funs.DB.Base_FireWorkMode.FirstOrDefault(e => e.FireWorkModeId == fireWorkModeId);
        }

        /// <summary>
        /// 根据名称获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_FireWorkMode GetFireWorkModeByName(string fireWorkModeName)
        {
            return Funs.DB.Base_FireWorkMode.FirstOrDefault(e => e.FireWorkModeName == fireWorkModeName);
        }

        /// <summary>
        /// 添加动火方式信息
        /// </summary>
        /// <param name="?"></param>
        public static void AddFireWorkMode(Model.Base_FireWorkMode FireWorkMode)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_FireWorkMode newFireWorkMode = new Model.Base_FireWorkMode
            {
                FireWorkModeId = FireWorkMode.FireWorkModeId,
                FireWorkModeCode = FireWorkMode.FireWorkModeCode,
                FireWorkModeName = FireWorkMode.FireWorkModeName,
                Remark = FireWorkMode.Remark
            };

            db.Base_FireWorkMode.InsertOnSubmit(newFireWorkMode);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改动火方式信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateFireWorkMode(Model.Base_FireWorkMode FireWorkMode)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_FireWorkMode newFireWorkMode = db.Base_FireWorkMode.FirstOrDefault(e => e.FireWorkModeId == FireWorkMode.FireWorkModeId);
            if (newFireWorkMode != null)
            {
                newFireWorkMode.FireWorkModeCode = FireWorkMode.FireWorkModeCode;
                newFireWorkMode.FireWorkModeName = FireWorkMode.FireWorkModeName;
                newFireWorkMode.Remark = FireWorkMode.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据动火方式主键删除对应动火方式信息
        /// </summary>
        /// <param name="FireWorkModeId"></param>
        public static void DeleteFireWorkModeById(string FireWorkModeId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_FireWorkMode FireWorkMode = db.Base_FireWorkMode.FirstOrDefault(e => e.FireWorkModeId == FireWorkModeId);
            {
                db.Base_FireWorkMode.DeleteOnSubmit(FireWorkMode);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取动火方式下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_FireWorkMode> GetFireWorkModeList()
        {
            var list = (from x in Funs.DB.Base_FireWorkMode orderby x.FireWorkModeCode select x).ToList();
            return list;
        }

        #region 动火方式表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitFireWorkModeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "FireWorkModeId";
            dropName.DataTextField = "FireWorkModeName";
            dropName.DataSource = BLL.FireWorkModeService.GetFireWorkModeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
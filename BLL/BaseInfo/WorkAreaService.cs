using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 作业区域
    /// </summary>
    public static class WorkAreaService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取作业区域
        /// </summary>
        /// <param name="workAreaId"></param>
        /// <returns></returns>
        public static Model.Base_WorkArea GetWorkAreaByWorkAreaId(string workAreaId)
        {
            return Funs.DB.Base_WorkArea.FirstOrDefault(e => e.WorkAreaId == workAreaId);
        }

        /// <summary>
        /// 添加作业区域
        /// </summary>
        /// <param name="workArea"></param>
        public static void AddWorkArea(Model.Base_WorkArea workArea)
        {
            Model.Base_WorkArea newWorkArea = new Model.Base_WorkArea();
            newWorkArea.WorkAreaId = workArea.WorkAreaId;
            newWorkArea.WorkAreaCode = workArea.WorkAreaCode;
            newWorkArea.WorkAreaName = workArea.WorkAreaName;
            newWorkArea.Remark = workArea.Remark;
            db.Base_WorkArea.InsertOnSubmit(newWorkArea);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改作业区域
        /// </summary>
        /// <param name="workArea"></param>
        public static void UpdateWorkArea(Model.Base_WorkArea workArea)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_WorkArea newWorkArea = db.Base_WorkArea.FirstOrDefault(e => e.WorkAreaId == workArea.WorkAreaId);
            if (newWorkArea != null)
            {
                newWorkArea.WorkAreaCode = workArea.WorkAreaCode;
                newWorkArea.WorkAreaName = workArea.WorkAreaName;
                newWorkArea.Remark = workArea.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除作业区域
        /// </summary>
        /// <param name="workAreaId"></param>
        public static void DeleteWorkAreaById(string workAreaId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_WorkArea workArea = db.Base_WorkArea.FirstOrDefault(e => e.WorkAreaId == workAreaId);
            if (workArea != null)
            {
                db.Base_WorkArea.DeleteOnSubmit(workArea);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据装置Id获取作业区域下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_WorkArea> GetWorkAreaByInstallationIdList(string installationId)
        {
            return (from x in Funs.DB.Base_WorkArea where x.InstallationId == installationId orderby x.WorkAreaCode select x).ToList();
        }

        #region 单位表下拉框
        /// <summary>
        ///  单位表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitWorkAreaDropDownList(FineUIPro.DropDownList dropName, string installationId, bool isShowPlease)
        {
            dropName.DataValueField = "WorkAreaId";
            dropName.DataTextField = "WorkAreaName";
            dropName.DataSource = BLL.WorkAreaService.GetWorkAreaByInstallationIdList(installationId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

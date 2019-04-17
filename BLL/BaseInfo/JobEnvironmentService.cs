using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 作业环境
    /// </summary>
    public static class JobEnvironmentService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取作业环境
        /// </summary>
        /// <param name="JobEnvironmentId"></param>
        /// <returns></returns>
        public static Model.Base_JobEnvironment GetJobEnvironmentByJobEnvironmentId(string JobEnvironmentId)
        {
            return Funs.DB.Base_JobEnvironment.FirstOrDefault(e => e.JobEnvironmentId == JobEnvironmentId);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="JobEnvironmentId"></param>
        /// <param name="JobEnvironmentName"></param>
        /// <returns></returns>
        public static bool IsExistJobEnvironmentName(string InstallationId, string WorkAreaId, string JobEnvironmentId, string JobEnvironmentName)
        {
            bool isExist = false;
            Model.Base_JobEnvironment JobEnvironment = new Model.Base_JobEnvironment();
            if (!string.IsNullOrEmpty(JobEnvironmentId))
            {
                JobEnvironment = Funs.DB.Base_JobEnvironment.FirstOrDefault(x => x.InstallationId == InstallationId && x.WorkAreaId == WorkAreaId && x.JobEnvironmentName == JobEnvironmentName && x.JobEnvironmentId != JobEnvironmentId);
            }
            else
            {
                JobEnvironment = Funs.DB.Base_JobEnvironment.FirstOrDefault(x => x.InstallationId == InstallationId && x.WorkAreaId == WorkAreaId && x.JobEnvironmentName == JobEnvironmentName);
            }
            if (JobEnvironment != null)
            {
                isExist = true;
            }
            return isExist;
        }


        /// <summary>
        /// 添加作业环境
        /// </summary>
        /// <param name="JobEnvironment"></param>
        public static void AddJobEnvironment(Model.Base_JobEnvironment JobEnvironment)
        {
            Model.Base_JobEnvironment newJobEnvironment = new Model.Base_JobEnvironment
            {
                InstallationId = JobEnvironment.InstallationId,
                WorkAreaId = JobEnvironment.WorkAreaId,
                JobEnvironmentId = JobEnvironment.JobEnvironmentId,
                JobEnvironmentCode = JobEnvironment.JobEnvironmentCode,
                JobEnvironmentName = JobEnvironment.JobEnvironmentName,
                Remark = JobEnvironment.Remark,
                Identification = JobEnvironment.Identification,
                RiskLevel = JobEnvironment.RiskLevel,
                QRCodeUrl = JobEnvironment.QRCodeUrl,
            };
            db.Base_JobEnvironment.InsertOnSubmit(newJobEnvironment);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改作业环境
        /// </summary>
        /// <param name="JobEnvironment"></param>
        public static void UpdateJobEnvironment(Model.Base_JobEnvironment JobEnvironment)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_JobEnvironment newJobEnvironment = db.Base_JobEnvironment.FirstOrDefault(e => e.JobEnvironmentId == JobEnvironment.JobEnvironmentId);
            if (newJobEnvironment != null)
            {
                newJobEnvironment.InstallationId = JobEnvironment.InstallationId;
                newJobEnvironment.WorkAreaId = JobEnvironment.WorkAreaId;
                newJobEnvironment.JobEnvironmentCode = JobEnvironment.JobEnvironmentCode;
                newJobEnvironment.JobEnvironmentName = JobEnvironment.JobEnvironmentName;
                newJobEnvironment.Remark = JobEnvironment.Remark;
                newJobEnvironment.Identification = JobEnvironment.Identification;
                newJobEnvironment.RiskLevel = JobEnvironment.RiskLevel;
                newJobEnvironment.QRCodeUrl = JobEnvironment.QRCodeUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除作业环境
        /// </summary>
        /// <param name="JobEnvironmentId"></param>
        public static void DeleteJobEnvironmentById(string JobEnvironmentId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_JobEnvironment JobEnvironment = db.Base_JobEnvironment.FirstOrDefault(e => e.JobEnvironmentId == JobEnvironmentId);
            if (JobEnvironment != null)
            {
                var lecItems = BLL.LECItemService.GetLECItemListByDataId(JobEnvironmentId);
                if (lecItems.Count() > 0)
                {
                    foreach (var item in lecItems)
                    {
                        var lecItemRecords = from x in db.Hazard_LECItemRecord where x.LECItemId == item.LECItemId select x;
                        if (lecItemRecords.Count() > 0)
                        {
                            db.Hazard_LECItemRecord.DeleteAllOnSubmit(lecItemRecords);
                            db.SubmitChanges();
                        }

                        var riskList = BLL.RiskListService.GetRiskListByLECItemId(item.LECItemId);
                        if (riskList != null)
                        {
                            db.Hazard_RiskList.DeleteOnSubmit(riskList);
                            db.SubmitChanges();
                        }
                    }

                    db.Hazard_LECItem.DeleteAllOnSubmit(lecItems);
                    db.SubmitChanges();
                }

                db.Base_JobEnvironment.DeleteOnSubmit(JobEnvironment);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据装置Id获取作业环境下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_JobEnvironment> GetJobEnvironmentList(string installationId, string workAreaId)
        {
            var JobEnvironment = (from x in Funs.DB.Base_JobEnvironment orderby x.JobEnvironmentCode select x).ToList();
            if (!string.IsNullOrEmpty(installationId))
            {
                JobEnvironment = JobEnvironment.Where(x => x.InstallationId == installationId).ToList();
            }
            if (!string.IsNullOrEmpty(workAreaId))
            {
                JobEnvironment = JobEnvironment.Where(x => x.WorkAreaId == workAreaId).ToList();
            }
            return JobEnvironment;
        }

        #region 作业环境表下拉框
        /// <summary>
        ///  作业环境表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitJobEnvironmentDropDownList(FineUIPro.DropDownList dropName, string installationId, string workAreaId, bool isShowPlease)
        {
            dropName.DataValueField = "JobEnvironmentId";
            dropName.DataTextField = "JobEnvironmentName";
            dropName.DataSource = BLL.JobEnvironmentService.GetJobEnvironmentList(installationId, workAreaId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

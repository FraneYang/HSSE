using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 作业活动
    /// </summary>
    public static class JobActivityService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取作业活动
        /// </summary>
        /// <param name="JobActivityId"></param>
        /// <returns></returns>
        public static Model.Base_JobActivity GetJobActivityByJobActivityId(string JobActivityId)
        {
            return Funs.DB.Base_JobActivity.FirstOrDefault(e => e.JobActivityId == JobActivityId);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="JobActivityId"></param>
        /// <param name="JobActivityName"></param>
        /// <returns></returns>
        public static bool IsExistJobActivityName(string InstallationId, string WorkAreaId, string JobActivityId, string JobActivityName)
        {
            bool isExist = false;
            Model.Base_JobActivity JobActivity = new Model.Base_JobActivity();
            if (!string.IsNullOrEmpty(JobActivityId))
            {
                JobActivity = Funs.DB.Base_JobActivity.FirstOrDefault(x => x.InstallationId == InstallationId && x.WorkAreaId == WorkAreaId && x.JobActivityName == JobActivityName && x.JobActivityId != JobActivityId);
            }
            else
            {
                JobActivity = Funs.DB.Base_JobActivity.FirstOrDefault(x => x.InstallationId == InstallationId && x.WorkAreaId == WorkAreaId && x.JobActivityName == JobActivityName);
            }
            if (JobActivity != null)
            {
                isExist = true;
            }
            return isExist;
        }


        /// <summary>
        /// 添加作业活动
        /// </summary>
        /// <param name="jobActivity"></param>
        public static void AddJobActivity(Model.Base_JobActivity jobActivity)
        {
            Model.Base_JobActivity newJobActivity = new Model.Base_JobActivity
            {
                InstallationId = jobActivity.InstallationId,
                WorkAreaId = jobActivity.WorkAreaId,
                JobActivityId = jobActivity.JobActivityId,
                JobActivityCode = jobActivity.JobActivityCode,
                JobActivityName = jobActivity.JobActivityName,
                Remark = jobActivity.Remark,
                Identification = jobActivity.Identification,
                RiskLevel = jobActivity.RiskLevel,
                QRCodeUrl = jobActivity.QRCodeUrl,
                States = "0",
            };
            db.Base_JobActivity.InsertOnSubmit(newJobActivity);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改作业活动
        /// </summary>
        /// <param name="JobActivity"></param>
        public static void UpdateJobActivity(Model.Base_JobActivity JobActivity)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_JobActivity newJobActivity = db.Base_JobActivity.FirstOrDefault(e => e.JobActivityId == JobActivity.JobActivityId);
            if (newJobActivity != null)
            {
                newJobActivity.InstallationId = JobActivity.InstallationId;
                newJobActivity.WorkAreaId = JobActivity.WorkAreaId;
                newJobActivity.JobActivityCode = JobActivity.JobActivityCode;
                newJobActivity.JobActivityName = JobActivity.JobActivityName;
                newJobActivity.Remark = JobActivity.Remark;
                newJobActivity.Identification = JobActivity.Identification;
                newJobActivity.RiskLevel = JobActivity.RiskLevel;
                newJobActivity.QRCodeUrl = JobActivity.QRCodeUrl;
                newJobActivity.States = JobActivity.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除作业活动
        /// </summary>
        /// <param name="JobActivityId"></param>
        public static void DeleteJobActivityById(string JobActivityId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_JobActivity JobActivity = db.Base_JobActivity.FirstOrDefault(e => e.JobActivityId == JobActivityId);
            if (JobActivity != null)
            {
                var jhaItems = BLL.JHAItemService.GetJHAItemListByJobActivityId(JobActivityId);
                if (jhaItems.Count() > 0)
                {
                    foreach (var item in jhaItems)
                    {
                        var JHAItemRecords = from x in db.Hazard_JHAItemRecord where x.JHAItemId == item.JHAItemId select x;
                        if (JHAItemRecords.Count() > 0)
                        {
                            db.Hazard_JHAItemRecord.DeleteAllOnSubmit(JHAItemRecords);
                            db.SubmitChanges();
                        }
                    }

                    db.Hazard_JHAItem.DeleteAllOnSubmit(jhaItems);
                    db.SubmitChanges();
                }

                db.Base_JobActivity.DeleteOnSubmit(JobActivity);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据装置Id获取作业活动下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_JobActivity> GetJobActivityList(string installationId, string workAreaId)
        {
            var jobActivity = (from x in Funs.DB.Base_JobActivity orderby x.JobActivityCode select x).ToList();
            if (!string.IsNullOrEmpty(installationId))
            {
                jobActivity = jobActivity.Where(x => x.InstallationId == installationId).ToList();
            }
            if (!string.IsNullOrEmpty(workAreaId))
            {
                jobActivity = jobActivity.Where(x => x.WorkAreaId == workAreaId).ToList();
            }
            return jobActivity;
        }

        #region 作业活动表下拉框
        /// <summary>
        ///  作业活动表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitJobActivityDropDownList(FineUIPro.DropDownList dropName, string installationId, string workAreaId, bool isShowPlease)
        {
            dropName.DataValueField = "JobActivityId";
            dropName.DataTextField = "JobActivityName";
            dropName.DataSource = BLL.JobActivityService.GetJobActivityList(installationId, workAreaId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}

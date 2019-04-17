using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 人员培训
    /// </summary>
    public static class TrainRecordService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取人员培训
        /// </summary>
        /// <param name="trainRecordId"></param>
        /// <returns></returns>
        public static Model.Training_TrainRecord GetTrainRecordById(string trainRecordId)
        {
            return db.Training_TrainRecord.FirstOrDefault(e => e.TrainRecordId == trainRecordId);
        }

        /// <summary>
        /// 添加人员培训
        /// </summary>
        /// <param name="trainRecord"></param>
        public static void AddTrainRecord(Model.Training_TrainRecord trainRecord)
        {
            Model.Training_TrainRecord newTrainRecord = new Model.Training_TrainRecord
            {
                TrainRecordId = trainRecord.TrainRecordId,
                TrainingCode = trainRecord.TrainingCode,
                TrainTitle = trainRecord.TrainTitle,
                TrainTypeId = trainRecord.TrainTypeId,
                TrainContent = trainRecord.TrainContent,
                TrainStartDate = trainRecord.TrainStartDate,
                TrainEndDate = trainRecord.TrainEndDate,
                TeachHour = trainRecord.TeachHour,
                TeachMan = trainRecord.TeachMan,
                TeachAddress = trainRecord.TeachAddress,
                Remark = trainRecord.Remark,
                AttachUrl = trainRecord.AttachUrl,
                UnitIds = trainRecord.UnitIds,
                CompileMan = trainRecord.CompileMan,
                TrainPersonNum = trainRecord.TrainPersonNum,
                States = trainRecord.States
            };
            db.Training_TrainRecord.InsertOnSubmit(newTrainRecord);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员培训
        /// </summary>
        /// <param name="trainRecord"></param>
        public static void UpdateTrainRecord(Model.Training_TrainRecord trainRecord)
        {
            Model.Training_TrainRecord newTrainRecord = db.Training_TrainRecord.FirstOrDefault(e => e.TrainRecordId == trainRecord.TrainRecordId);
            if (newTrainRecord != null)
            {
                newTrainRecord.TrainingCode = trainRecord.TrainingCode;
                newTrainRecord.TrainTitle = trainRecord.TrainTitle;
                newTrainRecord.TrainTypeId = trainRecord.TrainTypeId;
                newTrainRecord.TrainContent = trainRecord.TrainContent;
                newTrainRecord.TrainStartDate = trainRecord.TrainStartDate;
                newTrainRecord.TrainEndDate = trainRecord.TrainEndDate;
                newTrainRecord.TeachHour = trainRecord.TeachHour;
                newTrainRecord.TeachMan = trainRecord.TeachMan;
                newTrainRecord.TeachAddress = trainRecord.TeachAddress;
                newTrainRecord.Remark = trainRecord.Remark;
                newTrainRecord.AttachUrl = trainRecord.AttachUrl;
                newTrainRecord.UnitIds = trainRecord.UnitIds;
                newTrainRecord.CompileMan = trainRecord.CompileMan;
                newTrainRecord.TrainPersonNum = trainRecord.TrainPersonNum;
                newTrainRecord.States = trainRecord.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除人员培训
        /// </summary>
        /// <param name="trainRecordId"></param>
        public static void DeleteTrainRecordById(string trainRecordId)
        {
            Model.Training_TrainRecord trainRecord = db.Training_TrainRecord.FirstOrDefault(e => e.TrainRecordId == trainRecordId);
            if (trainRecord != null)
            {
                db.Training_TrainRecord.DeleteOnSubmit(trainRecord);
                db.SubmitChanges();
            }
        }
    }
}

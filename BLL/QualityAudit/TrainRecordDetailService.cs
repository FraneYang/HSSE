using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 人员培训明细
    /// </summary>
    public static class TrainRecordDetailService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取人员培训明细信息
        /// </summary>
        /// <param name="trainRecordDetailId"></param>
        /// <returns></returns>
        public static Model.Training_TrainRecordDetail GetTrainRecordDetailById(string trainRecordDetailId)
        {
            return db.Training_TrainRecordDetail.FirstOrDefault(e => e.TrainDetailId == trainRecordDetailId);
        }

        /// <summary>
        /// 根据人员培训Id获取所有相关明细信息
        /// </summary>
        /// <param name="trainRecordId"></param>
        /// <returns></returns>
        public static List<Model.View_Training_TrainRecordDetail> GetTrainRecordDetailByTrainRecordId(string trainRecordId)
        {
            return (from x in db.View_Training_TrainRecordDetail where x.TrainRecordId == trainRecordId select x).ToList();
        }

        /// <summary>
        /// 添加人员培训明细
        /// </summary>
        /// <param name="trainRecordDetail"></param>
        public static void AddTrainRecordDetail(Model.Training_TrainRecordDetail trainRecordDetail)
        {
            Model.Training_TrainRecordDetail newTrainRecordDetail = new Model.Training_TrainRecordDetail
            {
                TrainDetailId = trainRecordDetail.TrainDetailId,
                TrainRecordId = trainRecordDetail.TrainRecordId,
                PersonId = trainRecordDetail.PersonId,
                CheckScore = trainRecordDetail.CheckScore,
                CheckResult = trainRecordDetail.CheckResult,
                AttachUrl = trainRecordDetail.AttachUrl
            };
            db.Training_TrainRecordDetail.InsertOnSubmit(newTrainRecordDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训明细信息
        /// </summary>
        /// <param name="trainRecordDetail"></param>
        public static void UpdateTrainRecordDetail(Model.Training_TrainRecordDetail trainRecordDetail)
        {
            Model.Training_TrainRecordDetail newTrainRecordDetail = db.Training_TrainRecordDetail.FirstOrDefault(e => e.TrainDetailId == trainRecordDetail.TrainDetailId);
            if (newTrainRecordDetail != null)
            {
                newTrainRecordDetail.TrainRecordId = trainRecordDetail.TrainRecordId;
                newTrainRecordDetail.PersonId = trainRecordDetail.PersonId;
                newTrainRecordDetail.CheckScore = trainRecordDetail.CheckScore;
                newTrainRecordDetail.CheckResult = trainRecordDetail.CheckResult;
                newTrainRecordDetail.AttachUrl = trainRecordDetail.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训记录明细
        /// </summary>
        /// <param name="trainRecordDetailId"></param>
        public static void DeleteTrainRecordDetailById(string trainRecordDetailId)
        {
            Model.Training_TrainRecordDetail detail = db.Training_TrainRecordDetail.FirstOrDefault(e => e.TrainDetailId == trainRecordDetailId);
            if (detail!=null)
            {
                db.Training_TrainRecordDetail.DeleteOnSubmit(detail);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员培训ID删除所有相关明细信息
        /// </summary>
        /// <param name="trainRecordId"></param>
        public static void DeleteTrainRecordDetailByTrainRecordId(string trainRecordId)
        {
            var q = (from x in db.Training_TrainRecordDetail where x.TrainRecordId == trainRecordId select x).ToList();
            if (q!=null)
            {
                db.Training_TrainRecordDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}

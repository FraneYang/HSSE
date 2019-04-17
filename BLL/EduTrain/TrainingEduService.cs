using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class TrainingEduService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Training_TrainingEdu GetTrainingEduById(string trainingEduId)
        {
            return Funs.DB.Training_TrainingEdu.FirstOrDefault(e => e.TrainingEduId == trainingEduId);
        }

        /// <summary>
        /// 添加试题类型信息
        /// </summary>
        /// <param name="trainingEdu"></param>
        public static void AddTrainingEdu(Model.Training_TrainingEdu trainingEdu)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingEdu newTrainingEdu = new Model.Training_TrainingEdu
            {
                TrainingEduId = trainingEdu.TrainingEduId,
                TrainingEduCode = trainingEdu.TrainingEduCode,
                TrainingEduName = trainingEdu.TrainingEduName,
                SupTrainingEduId = trainingEdu.SupTrainingEduId,
            };
            db.Training_TrainingEdu.InsertOnSubmit(newTrainingEdu);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试题类型信息
        /// </summary>
        /// <param name="trainingEdu"></param>
        public static void UpdateTrainingEdu(Model.Training_TrainingEdu trainingEdu)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingEdu newTrainingEdu = db.Training_TrainingEdu.FirstOrDefault(e => e.TrainingEduId == trainingEdu.TrainingEduId);
            if (newTrainingEdu != null)
            {
                newTrainingEdu.TrainingEduCode = trainingEdu.TrainingEduCode;
                newTrainingEdu.TrainingEduName = trainingEdu.TrainingEduName;
                newTrainingEdu.SupTrainingEduId = trainingEdu.SupTrainingEduId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="trainingEduId"></param>
        public static void DeleteTrainingEduById(string trainingEduId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingEdu TrainingEdu = db.Training_TrainingEdu.FirstOrDefault(e => e.TrainingEduId == trainingEduId);
            if (TrainingEdu != null)
            {
                var TrainingEduItem = from x in db.Training_TrainingEduItem where x.TrainingEduId == trainingEduId select x;
                if (TrainingEduItem.Count() > 0)
                {
                    db.Training_TrainingEduItem.DeleteAllOnSubmit(TrainingEduItem);
                }

                db.Training_TrainingEdu.DeleteOnSubmit(TrainingEdu);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取试题类型列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Training_TrainingEdu> GetTrainingEduList()
        {
            return (from x in db.Training_TrainingEdu orderby x.TrainingEduCode select x).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class TrainingService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Training_Training GetTrainingById(string TrainingId)
        {
            return Funs.DB.Training_Training.FirstOrDefault(e => e.TrainingId == TrainingId);
        }

        /// <summary>
        /// 添加试题类型信息
        /// </summary>
        /// <param name="Training"></param>
        public static void AddTraining(Model.Training_Training Training)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_Training newTraining = new Model.Training_Training
            {
                TrainingId = Training.TrainingId,
                TrainingCode = Training.TrainingCode,
                TrainingName = Training.TrainingName,
                SupTrainingId = Training.SupTrainingId,
                IsEndLever = Training.IsEndLever
            };
            db.Training_Training.InsertOnSubmit(newTraining);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试题类型信息
        /// </summary>
        /// <param name="Training"></param>
        public static void UpdateTraining(Model.Training_Training Training)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_Training newTraining = db.Training_Training.FirstOrDefault(e => e.TrainingId == Training.TrainingId);
            if (newTraining != null)
            {
                newTraining.TrainingCode = Training.TrainingCode;
                newTraining.TrainingName = Training.TrainingName;
                newTraining.SupTrainingId = Training.SupTrainingId;
                newTraining.IsEndLever = Training.IsEndLever;
            
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="TrainingId"></param>
        public static void DeleteTrainingById(string TrainingId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_Training Training = db.Training_Training.FirstOrDefault(e => e.TrainingId == TrainingId);
            if (Training != null)
            {
                var TrainingItem = from x in db.Training_TrainingItem where x.TrainingId == TrainingId select x;
                if (TrainingItem.Count() > 0)
                {
                    db.Training_TrainingItem.DeleteAllOnSubmit(TrainingItem);
                }

                db.Training_Training.DeleteOnSubmit(Training);
                db.SubmitChanges();
            }
        }
        
        /// <summary>
        /// 获取试题类型列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Training_Training> GetTrainingList()
        {
            return (from x in db.Training_Training orderby x.TrainingCode select x).ToList();
        }
    }
}

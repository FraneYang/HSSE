using System.Linq;

namespace BLL
{
    public static class TrainingEduItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Training_TrainingEduItem GetTrainingEduItemById(string trainingEduItemId)
        {
            return Funs.DB.Training_TrainingEduItem.FirstOrDefault(e => e.TrainingEduItemId == trainingEduItemId);
        }

        /// <summary>
        /// 添加试题信息
        /// </summary>
        /// <param name="trainingEduItem"></param>
        public static void AddTrainingEduItem(Model.Training_TrainingEduItem trainingEduItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingEduItem newTrainingEduItem = new Model.Training_TrainingEduItem
            {
                TrainingEduItemId = trainingEduItem.TrainingEduItemId,
                TrainingEduId = trainingEduItem.TrainingEduId,
                TrainingEduItemCode = trainingEduItem.TrainingEduItemCode,
                TrainingEduItemName = trainingEduItem.TrainingEduItemName,
                Summary = trainingEduItem.Summary,
                PictureUrl = trainingEduItem.PictureUrl,
                AttachUrl = trainingEduItem.AttachUrl,
                WorkPostIds = trainingEduItem.WorkPostIds,
                WorkPostNames = trainingEduItem.WorkPostNames,
                InstallationIds = trainingEduItem.InstallationIds,
                InstallationNames = trainingEduItem.InstallationNames,
            };
            db.Training_TrainingEduItem.InsertOnSubmit(newTrainingEduItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试题信息
        /// </summary>
        /// <param name="trainingEduItem"></param>
        public static void UpdateTrainingEduItem(Model.Training_TrainingEduItem trainingEduItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingEduItem newTrainingEduItem = db.Training_TrainingEduItem.FirstOrDefault(e => e.TrainingEduItemId == trainingEduItem.TrainingEduItemId);
            if (newTrainingEduItem != null)
            {
                newTrainingEduItem.TrainingEduItemCode = trainingEduItem.TrainingEduItemCode;
                newTrainingEduItem.TrainingEduItemName = trainingEduItem.TrainingEduItemName;
                newTrainingEduItem.Summary = trainingEduItem.Summary;
                newTrainingEduItem.AttachUrl = trainingEduItem.AttachUrl;
                newTrainingEduItem.PictureUrl = trainingEduItem.PictureUrl;
                newTrainingEduItem.WorkPostIds = trainingEduItem.WorkPostIds;
                newTrainingEduItem.WorkPostNames = trainingEduItem.WorkPostNames;
                newTrainingEduItem.InstallationIds = trainingEduItem.InstallationIds;
                newTrainingEduItem.InstallationNames = trainingEduItem.InstallationNames;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="trainingEduItemId"></param>
        public static void DeleteTrainingEduItemById(string trainingEduItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingEduItem TrainingEduItem = db.Training_TrainingEduItem.FirstOrDefault(e => e.TrainingEduItemId == trainingEduItemId);
            if (TrainingEduItem != null)
            {
                db.Training_TrainingEduItem.DeleteOnSubmit(TrainingEduItem);
                db.SubmitChanges();
            }
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class TrainingItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Training_TrainingItem GetTrainingItemById(string TrainingItemId)
        {
            return Funs.DB.Training_TrainingItem.FirstOrDefault(e => e.TrainingItemId == TrainingItemId);
        }

        /// <summary>
        /// 添加试题信息
        /// </summary>
        /// <param name="TrainingItem"></param>
        public static void AddTrainingItem(Model.Training_TrainingItem TrainingItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingItem newTrainingItem = new Model.Training_TrainingItem
            {
                TrainingItemId = TrainingItem.TrainingItemId,
                TrainingId = TrainingItem.TrainingId,
                TrainingItemCode = TrainingItem.TrainingItemCode,
                TrainingItemName = TrainingItem.TrainingItemName,
                Abstracts = TrainingItem.Abstracts,
                AttachUrl = TrainingItem.AttachUrl,
                VersionNum = TrainingItem.VersionNum,
                TestType = TrainingItem.TestType,
                WorkPostIds = TrainingItem.WorkPostIds,
                WorkPostNames = TrainingItem.WorkPostNames,
                InstallationIds = TrainingItem.InstallationIds,
                InstallationNames = TrainingItem.InstallationNames,
                AItem = TrainingItem.AItem,
                BItem = TrainingItem.BItem,
                CItem = TrainingItem.CItem,
                DItem = TrainingItem.DItem,
                EItem = TrainingItem.EItem,
                Score = TrainingItem.Score,
                AnswerItems = TrainingItem.AnswerItems,
            };
            db.Training_TrainingItem.InsertOnSubmit(newTrainingItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试题信息
        /// </summary>
        /// <param name="TrainingItem"></param>
        public static void UpdateTrainingItem(Model.Training_TrainingItem TrainingItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingItem newTrainingItem = db.Training_TrainingItem.FirstOrDefault(e => e.TrainingItemId == TrainingItem.TrainingItemId);
            if (newTrainingItem != null)
            {
                newTrainingItem.TrainingItemCode = TrainingItem.TrainingItemCode;
                newTrainingItem.TrainingItemName = TrainingItem.TrainingItemName;
                newTrainingItem.Abstracts = TrainingItem.Abstracts;
                newTrainingItem.AttachUrl = TrainingItem.AttachUrl;
                newTrainingItem.VersionNum = TrainingItem.VersionNum;
                newTrainingItem.TestType = TrainingItem.TestType;
                newTrainingItem.WorkPostIds = TrainingItem.WorkPostIds;
                newTrainingItem.WorkPostNames = TrainingItem.WorkPostNames;
                newTrainingItem.InstallationIds = TrainingItem.InstallationIds;
                newTrainingItem.InstallationNames = TrainingItem.InstallationNames;
                newTrainingItem.AItem = TrainingItem.AItem;
                newTrainingItem.BItem = TrainingItem.BItem;
                newTrainingItem.CItem = TrainingItem.CItem;
                newTrainingItem.DItem = TrainingItem.DItem;
                newTrainingItem.EItem = TrainingItem.EItem;
                newTrainingItem.Score = TrainingItem.Score;
                newTrainingItem.AnswerItems = TrainingItem.AnswerItems;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="TrainingItemId"></param>
        public static void DeleteTrainingItemById(string TrainingItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TrainingItem TrainingItem = db.Training_TrainingItem.FirstOrDefault(e => e.TrainingItemId == TrainingItemId);
            if (TrainingItem != null)
            {
                db.Training_TrainingItem.DeleteOnSubmit(TrainingItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据教材类型获取正确答案项
        /// </summary>
        /// <param name="testType"></param>
        /// <returns></returns>
        public static ListItem[] GetAnswerItemsList(string testType)
        {
            if (testType == "1")   //单选题
            {
                ListItem[] item = new ListItem[4];
                item[0] = new ListItem("A", "1");
                item[1] = new ListItem("B", "2");
                item[2] = new ListItem("C", "3");
                item[3] = new ListItem("D", "4");
                return item;
            }
            else if (testType == "2")   //多选题
            {
                ListItem[] item = new ListItem[5];
                item[0] = new ListItem("A", "1");
                item[1] = new ListItem("B", "2");
                item[2] = new ListItem("C", "3");
                item[3] = new ListItem("D", "4");
                item[4] = new ListItem("E", "5");
                return item;
            }
            else    //判断题
            {
                ListItem[] item = new ListItem[2];
                item[0] = new ListItem("A", "1");
                item[1] = new ListItem("B", "2");
                return item;
            }
        }
    }
}

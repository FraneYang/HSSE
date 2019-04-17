using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 综合测评
    /// </summary>
    public static class AppraisalScoreService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取综合测评
        /// </summary>
        /// <param name="AppraisalScoreId"></param>
        /// <returns></returns>
        public static Model.Appraisal_AppraisalScore GetAppraisalScoreById(string AppraisalScoreId)
        {
            return Funs.DB.Appraisal_AppraisalScore.FirstOrDefault(e => e.AppraisalScoreId == AppraisalScoreId);
        }

        /// <summary>
        /// 添加综合测评
        /// </summary>
        /// <param name="appraisalScore"></param>
        public static void AddAppraisalScore(Model.Appraisal_AppraisalScore appraisalScore)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Appraisal_AppraisalScore newAppraisalScore = new Model.Appraisal_AppraisalScore
            {
                AppraisalScoreId = SQLHelper.GetNewID(typeof(Model.Appraisal_AppraisalScore)),
                UserId = appraisalScore.UserId,
                MenuId = appraisalScore.MenuId,
                MenuOperation = appraisalScore.MenuOperation,
                MenuOperationName = appraisalScore.MenuOperationName,
                OperationTime = appraisalScore.OperationTime,
                Score = appraisalScore.Score,
                DataId = appraisalScore.DataId,
            };

            db.Appraisal_AppraisalScore.InsertOnSubmit(newAppraisalScore);
            db.SubmitChanges();
        }

        /// <summary>
        /// 获取并插入综合测评
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <param name="menuOperation"></param>
        /// <param name="dataId"></param>
        public static void GetAppraisalScore(string userId, string menuId, int menuOperation, string dataId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var menuAppraisal = db.Sys_MenuAppraisal.FirstOrDefault(x => x.MenuId == menuId && x.MenuOperation == menuOperation);
            if (menuAppraisal != null)
            {
                var appraisal = db.Appraisal_AppraisalScore.FirstOrDefault(x => x.DataId == dataId && x.MenuId == menuId && x.MenuOperation == menuOperation);
                if (appraisal == null)
                {
                    Model.Appraisal_AppraisalScore newAppraisalScore = new Model.Appraisal_AppraisalScore
                    {
                        AppraisalScoreId = SQLHelper.GetNewID(typeof(Model.Appraisal_AppraisalScore)),
                        UserId = userId,
                        MenuId = menuId,
                        MenuOperation = menuOperation,
                        OperationTime = System.DateTime.Now,
                        Score = menuAppraisal.Score,
                        DataId = dataId,
                        MenuOperationName= menuAppraisal.MenuOperationName,
                    };

                    db.Appraisal_AppraisalScore.InsertOnSubmit(newAppraisalScore);
                    db.SubmitChanges();
                }
            }
        }
    }
}

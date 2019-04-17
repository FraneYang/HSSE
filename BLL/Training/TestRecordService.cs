using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 考试记录
    /// </summary>
    public static class TestRecordService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取考试记录
        /// </summary>
        /// <param name="testRecordId"></param>
        /// <returns></returns>
        public static Model.Training_TestRecord GetTestRecordById(string testRecordId)
        {
            return db.Training_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecordId);
        }

        /// <summary>
        /// 修改考试记录信息
        /// </summary>
        /// <param name="Training"></param>
        public static void UpdateTestRecord(Model.Training_TestRecord testRecord)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TestRecord newTestRecord = db.Training_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecord.TestRecordId);
            if (newTestRecord != null)
            {
                newTestRecord.IsFiled = testRecord.IsFiled;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改考试记录信息
        /// </summary>
        /// <param name="Training"></param>
        public static void UpdateTestRecordEndTime(string testRecordId,DateTime testEndTime,decimal testScores)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Training_TestRecord newTestRecord = db.Training_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecordId);
            if (newTestRecord != null)
            {
                newTestRecord.TestEndTime = testEndTime;
                newTestRecord.TestScores = testScores;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训计划信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteTestRecordByTestRecordId(string testRecordId)
        {
            var testRecord = db.Training_TestRecord.FirstOrDefault(e => e.TestRecordId == testRecordId);
            if (testRecord != null)
            {
                CommonService.DeleteSysPushRecordByDataId(testRecordId);
                var testRecordItem = from x in db.Training_TestRecordItem where x.TestRecordId == testRecordId select x;
                if (testRecordItem.Count() > 0)
                {
                    foreach (var item in testRecordItem)
                    {
                        BLL.TestRecordItemService.DeleteTestRecordItemmByTestRecordItemId(item.TestRecordItemId);
                    }
                }
                db.Training_TestRecord.DeleteOnSubmit(testRecord);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 更新没有结束时间且超时的考试记录
        /// </summary>
        public static void UpdateTestEndTimeNull()
        {
            var testRecord = from x in Funs.DB.Training_TestRecord
                             join y in Funs.DB.Training_TestPlan on x.TestPlanId equals y.TestPlanId
                             where !x.TestEndTime.HasValue && x.TestStartTime.Value.AddMinutes(y.Duration.Value) < DateTime.Now
                             select x;
            if (testRecord.Count() == 0)
            {
                testRecord= from x in Funs.DB.Training_TestRecord                           
                            where !x.TestEndTime.HasValue && x.TestStartTime.Value.AddMinutes(100) < DateTime.Now
                            select x;
            }
            foreach (var item in testRecord)
            {
                item.TestEndTime = item.TestStartTime.Value.AddMinutes(100);
                if (!item.TestScores.HasValue)
                {
                    var sumSubjectScore = Funs.DB.Training_TestRecordItem.Where(x => x.TestRecordId == item.TestRecordId).Select(x => x.SubjectScore).Sum();
                    item.TestScores = sumSubjectScore ?? 0;
                }
                Funs.DB.SubmitChanges();
            }
        }
    }
}

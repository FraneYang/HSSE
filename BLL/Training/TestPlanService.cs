﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 考试计划
    /// </summary>
    public static class TestPlanService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取培训计划
        /// </summary>
        /// <param name="TestPlanId"></param>
        /// <returns></returns>
        public static Model.Training_TestPlan GetTestPlanById(string TestPlanId)
        {
            return db.Training_TestPlan.FirstOrDefault(e => e.TestPlanId == TestPlanId);
        }

        /// <summary>
        /// 添加培训计划
        /// </summary>
        /// <param name="TestPlan"></param>
        public static void AddTestPlan(Model.Training_TestPlan TestPlan)
        {
            Model.Training_TestPlan newTestPlan = new Model.Training_TestPlan
            {
                TestPlanId = TestPlan.TestPlanId,
                PlanCode = TestPlan.PlanCode,
                PlanName = TestPlan.PlanName,
                PlanManId = TestPlan.PlanManId,
                PlanDate = TestPlan.PlanDate,
                TestStartTime = TestPlan.TestStartTime,
                Duration = TestPlan.Duration,
                TotalScore = TestPlan.TotalScore,
                QuestionCount = TestPlan.QuestionCount,
                TestPalce = TestPlan.TestPalce,
                UnitIds = TestPlan.UnitIds,
                UnitNames = TestPlan.UnitNames,
                DepartIds = TestPlan.DepartIds,
                DepartNames = TestPlan.DepartNames,               
                InstallationIds = TestPlan.InstallationIds,
                InstallationNames = TestPlan.InstallationNames,
                WorkPostIds = TestPlan.WorkPostIds,
                WorkPostNames = TestPlan.WorkPostNames,
                States = TestPlan.States
            };
            db.Training_TestPlan.InsertOnSubmit(newTestPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训计划
        /// </summary>
        /// <param name="TestPlan"></param>
        public static void UpdateTestPlan(Model.Training_TestPlan TestPlan)
        {
            Model.Training_TestPlan newTestPlan = db.Training_TestPlan.FirstOrDefault(e => e.TestPlanId == TestPlan.TestPlanId);
            if (newTestPlan != null)
            {
                newTestPlan.PlanCode = TestPlan.PlanCode;
                newTestPlan.PlanName = TestPlan.PlanName;
                newTestPlan.PlanManId = TestPlan.PlanManId;
                newTestPlan.PlanDate = TestPlan.PlanDate;
                newTestPlan.TestStartTime = TestPlan.TestStartTime;
                newTestPlan.Duration = TestPlan.Duration;
                newTestPlan.TotalScore = TestPlan.TotalScore;
                newTestPlan.QuestionCount = TestPlan.QuestionCount;
                newTestPlan.TestPalce = TestPlan.TestPalce;
                newTestPlan.UnitIds = TestPlan.UnitIds;
                newTestPlan.UnitNames = TestPlan.UnitNames;
                newTestPlan.DepartIds = TestPlan.DepartIds;
                newTestPlan.DepartNames = TestPlan.DepartNames;
                newTestPlan.InstallationIds = TestPlan.InstallationIds;
                newTestPlan.InstallationNames = TestPlan.InstallationNames;
                newTestPlan.WorkPostIds = TestPlan.WorkPostIds;
                newTestPlan.WorkPostNames = TestPlan.WorkPostNames;
                newTestPlan.States = TestPlan.States;
                //newTestPlan.States = TestPlan.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训计划信息
        /// </summary>
        /// <param name="TestPlanId"></param>
        public static void DeleteTestPlanById(string TestPlanId)
        {
            Model.Training_TestPlan TestPlan = db.Training_TestPlan.FirstOrDefault(e => e.TestPlanId == TestPlanId);
            if (TestPlan != null)
            {               
                var testPlanTrainings = from x in db.Training_TestPlanTraining where x.TestPlanId == TestPlanId select x;
                if (testPlanTrainings.Count() > 0)
                {
                    db.Training_TestPlanTraining.DeleteAllOnSubmit(testPlanTrainings);
                }
                CommonService.DeleteSysPushRecordByDataId(TestPlanId);
                db.Training_TestPlan.DeleteOnSubmit(TestPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取培训计划列
        /// </summary>
        /// <returns></returns>
        public static List<Model.Training_TestPlan> GetTestPlanList() 
        {
            return (from x in db.Training_TestPlan orderby x.PlanCode select x).ToList();
        }
    }
}

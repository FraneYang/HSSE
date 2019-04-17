using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class SCLItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Hazard_SCLItem GetSCLItemById(string SCLItemId)
        {
            return Funs.DB.Hazard_SCLItem.FirstOrDefault(e => e.SCLItemId == SCLItemId);
        }

        /// <summary>
        /// 添加SCLItem信息
        /// </summary>
        /// <param name="SCLItem"></param>
        public static void AddSCLItem(Model.Hazard_SCLItem SCLItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_SCLItem newSCLItem = new Model.Hazard_SCLItem();
            newSCLItem.SCLItemId=SQLHelper.GetNewID(typeof(Model.Hazard_SCLItem));
            newSCLItem.SCLId = SCLItem.SCLId;
            newSCLItem.SortIndex = SCLItem.SortIndex;
            newSCLItem.CheckItem = SCLItem.CheckItem;
            newSCLItem.Standard = SCLItem.Standard;
            newSCLItem.Consequence = SCLItem.Consequence;
            newSCLItem.NowControlMeasures = SCLItem.NowControlMeasures;
            newSCLItem.HazardJudge_L = SCLItem.HazardJudge_L;
            newSCLItem.HazardJudge_S = SCLItem.HazardJudge_S;
            newSCLItem.HazardJudge_R = SCLItem.HazardJudge_R;
            newSCLItem.RiskLevel = SCLItem.RiskLevel;
            newSCLItem.ControlMeasures = SCLItem.ControlMeasures;
            db.Hazard_SCLItem.InsertOnSubmit(newSCLItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改SCLItem信息
        /// </summary>
        /// <param name="SCLItem"></param>
        public static void UpdateSCLItem(Model.Hazard_SCLItem SCLItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_SCLItem newSCLItem = db.Hazard_SCLItem.FirstOrDefault(e => e.SCLItemId == SCLItem.SCLItemId);
            if (newSCLItem != null)
            {
                newSCLItem.SortIndex = SCLItem.SortIndex;
                newSCLItem.CheckItem = SCLItem.CheckItem;
                newSCLItem.Standard = SCLItem.Standard;
                newSCLItem.Consequence = SCLItem.Consequence;
                newSCLItem.NowControlMeasures = SCLItem.NowControlMeasures;
                newSCLItem.HazardJudge_L = SCLItem.HazardJudge_L;
                newSCLItem.HazardJudge_S = SCLItem.HazardJudge_S;
                newSCLItem.HazardJudge_R = SCLItem.HazardJudge_R;
                newSCLItem.RiskLevel = SCLItem.RiskLevel;
                newSCLItem.ControlMeasures = SCLItem.ControlMeasures;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="SCLItemId"></param>
        public static void DeleteSCLItemById(string SCLItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_SCLItem SCLItem = db.Hazard_SCLItem.FirstOrDefault(e => e.SCLItemId == SCLItemId);
            if (SCLItem != null)
            {
                db.Hazard_SCLItem.DeleteOnSubmit(SCLItem);
                db.SubmitChanges();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class LECItemService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Hazard_LECItem GetLECItemById(string LECItemId)
        {
            return Funs.DB.Hazard_LECItem.FirstOrDefault(e => e.LECItemId == LECItemId);
        }

        /// <summary>
        /// 添加LECItem信息
        /// </summary>
        /// <param name="LECItem"></param>
        public static void AddLECItem(Model.Hazard_LECItem LECItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LECItem newLECItem = new Model.Hazard_LECItem();
            newLECItem.LECItemId=SQLHelper.GetNewID(typeof(Model.Hazard_LECItem));
            newLECItem.LECId = LECItem.LECId;
            newLECItem.RiskPlace = LECItem.RiskPlace;
            newLECItem.SortIndex = LECItem.SortIndex;
            newLECItem.HazardDescription = LECItem.HazardDescription;
            newLECItem.PossibleAccidents = LECItem.PossibleAccidents;
            newLECItem.HazardJudge_L = LECItem.HazardJudge_L;
            newLECItem.HazardJudge_E = LECItem.HazardJudge_E;
            newLECItem.HazardJudge_C = LECItem.HazardJudge_C;
            newLECItem.HazardJudge_D = LECItem.HazardJudge_D;
            newLECItem.RiskLevel = LECItem.RiskLevel;
            newLECItem.ControlMeasures = LECItem.ControlMeasures;
            newLECItem.Remark = LECItem.Remark;
            db.Hazard_LECItem.InsertOnSubmit(newLECItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改LECItem信息
        /// </summary>
        /// <param name="LECItem"></param>
        public static void UpdateLECItem(Model.Hazard_LECItem LECItem)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LECItem newLECItem = db.Hazard_LECItem.FirstOrDefault(e => e.LECItemId == LECItem.LECItemId);
            if (newLECItem != null)
            {
                newLECItem.RiskPlace = LECItem.RiskPlace;
                newLECItem.SortIndex = LECItem.SortIndex;
                newLECItem.HazardDescription = LECItem.HazardDescription;
                newLECItem.PossibleAccidents = LECItem.PossibleAccidents;
                newLECItem.HazardJudge_L = LECItem.HazardJudge_L;
                newLECItem.HazardJudge_E = LECItem.HazardJudge_E;
                newLECItem.HazardJudge_C = LECItem.HazardJudge_C;
                newLECItem.HazardJudge_D = LECItem.HazardJudge_D;
                newLECItem.RiskLevel = LECItem.RiskLevel;
                newLECItem.ControlMeasures = LECItem.ControlMeasures;
                newLECItem.Remark = LECItem.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="LECItemId"></param>
        public static void DeleteLECItemById(string LECItemId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_LECItem LECItem = db.Hazard_LECItem.FirstOrDefault(e => e.LECItemId == LECItemId);
            if (LECItem != null)
            {
                db.Hazard_LECItem.DeleteOnSubmit(LECItem);
                db.SubmitChanges();
            }
        }
    }
}
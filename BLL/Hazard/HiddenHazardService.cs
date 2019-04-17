using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class HiddenHazardService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Hazard_HiddenHazard GetHiddenHazardById(string HiddenHazardId)
        {
            return Funs.DB.Hazard_HiddenHazard.FirstOrDefault(e => e.HiddenHazardId == HiddenHazardId);
        }

        /// <summary>
        /// 根据主键获取隐患视图信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.View_Hazard_HiddenHazard GetHiddenHazardViewById(string HiddenHazardId)
        {
            return Funs.DB.View_Hazard_HiddenHazard.FirstOrDefault(e => e.HiddenHazardId == HiddenHazardId);
        }

        /// <summary>
        /// 修改隐患信息
        /// </summary>
        /// <param name="hiddenHazard"></param>
        public static void UpdateHiddenHazard(Model.Hazard_HiddenHazard hiddenHazard)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_HiddenHazard newHiddenHazard = db.Hazard_HiddenHazard.FirstOrDefault(e => e.HiddenHazardId == hiddenHazard.HiddenHazardId);
            if (newHiddenHazard != null)
            {
                newHiddenHazard.HiddenHazardCode = hiddenHazard.HiddenHazardCode;
                newHiddenHazard.HiddenHazardName = hiddenHazard.HiddenHazardName;
                newHiddenHazard.InstallationId = hiddenHazard.InstallationId;
                newHiddenHazard.DepartId = hiddenHazard.DepartId;
                newHiddenHazard.FindManId = hiddenHazard.FindManId;
                newHiddenHazard.FindTime = hiddenHazard.FindTime;
                newHiddenHazard.Description = hiddenHazard.Description;
                newHiddenHazard.HiddenHazardPlace = hiddenHazard.HiddenHazardPlace;
                newHiddenHazard.BePohotoUrl = hiddenHazard.BePohotoUrl;
                newHiddenHazard.UnitId = hiddenHazard.UnitId;
                newHiddenHazard.CorrectManId = hiddenHazard.CorrectManId;
                newHiddenHazard.IsMajor = hiddenHazard.IsMajor;
                newHiddenHazard.LimitTime = hiddenHazard.LimitTime;
                newHiddenHazard.HiddenHazardTypeId = hiddenHazard.HiddenHazardTypeId;
                newHiddenHazard.CorrectMeasures = hiddenHazard.CorrectMeasures;
                newHiddenHazard.ControlMeasures = hiddenHazard.ControlMeasures;
                newHiddenHazard.CorrectMoney = hiddenHazard.CorrectMoney;
                newHiddenHazard.CorrectScheme = hiddenHazard.CorrectScheme;
                newHiddenHazard.AuditManId = hiddenHazard.AuditManId;
                newHiddenHazard.AuditTime = hiddenHazard.AuditTime;
                newHiddenHazard.QHSEAuditManId = hiddenHazard.QHSEAuditManId;
                newHiddenHazard.QHSEAuditTime = hiddenHazard.QHSEAuditTime;
                newHiddenHazard.AfPohotoUrl = hiddenHazard.AfPohotoUrl;
                newHiddenHazard.CorrectTime = hiddenHazard.CorrectTime;
                newHiddenHazard.AcceptanceManId = hiddenHazard.AcceptanceManId;
                newHiddenHazard.AcceptanceTime = hiddenHazard.AcceptanceTime;
                newHiddenHazard.States = hiddenHazard.States;
                newHiddenHazard.IsFiled = hiddenHazard.IsFiled;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="hiddenHazardId"></param>
        public static void DeleteHiddenHazardById(string hiddenHazardId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_HiddenHazard HiddenHazard = db.Hazard_HiddenHazard.FirstOrDefault(e => e.HiddenHazardId == hiddenHazardId);
            if (HiddenHazard != null)
            {
                CommonService.DeleteSysPushRecordByDataId(hiddenHazardId);
                BLL.HiddenHazardDelayService.DeleteHiddenHazardDelayByHiddenHazardId(hiddenHazardId);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, HiddenHazard.BePohotoUrl);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, HiddenHazard.AfPohotoUrl);
                db.Hazard_HiddenHazard.DeleteOnSubmit(HiddenHazard);
                db.SubmitChanges();
            }
        }
    }
}
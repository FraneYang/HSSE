using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class HiddenHazardDelayService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Hazard_HiddenHazardDelay GetHiddenHazardDelayById(string delayId)
        {
            return Funs.DB.Hazard_HiddenHazardDelay.FirstOrDefault(e => e.DelayId == delayId);
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="HiddenHazardDelayId"></param>
        public static void DeleteHiddenHazardDelayById(string delayId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_HiddenHazardDelay hiddenHazardDelay = db.Hazard_HiddenHazardDelay.FirstOrDefault(e => e.DelayId == delayId);
            if (hiddenHazardDelay != null)
            {
                var hiddenHazard = BLL.HiddenHazardService.GetHiddenHazardById(hiddenHazardDelay.HiddenHazardId);
                if (hiddenHazard != null)
                {
                    hiddenHazard.LimitTime = hiddenHazardDelay.OldLimitTime;
                    BLL.HiddenHazardService.UpdateHiddenHazard(hiddenHazard);
                }

                CommonService.DeleteSysPushRecordByDataId(delayId);
                db.Hazard_HiddenHazardDelay.DeleteOnSubmit(hiddenHazardDelay);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="HiddenHazardDelayId"></param>
        public static void DeleteHiddenHazardDelayByHiddenHazardId(string HiddenHazardId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var HiddenHazardDelays = from x in db.Hazard_HiddenHazardDelay where x.HiddenHazardId == HiddenHazardId select x;
            if (HiddenHazardDelays.Count() > 0)
            {
                foreach (var item in HiddenHazardDelays)
                {
                    DeleteHiddenHazardDelayById(item.DelayId);
                }
            }
        }
    }
}
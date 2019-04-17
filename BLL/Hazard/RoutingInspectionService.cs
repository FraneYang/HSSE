using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class RoutingInspectionService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;
        
        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="RoutingInspectionId"></param>
        /// <returns></returns>
        public static Model.Hazard_RoutingInspection GetRoutingInspectionById(string routingInspectionId)
        {
            return Funs.DB.Hazard_RoutingInspection.FirstOrDefault(e => e.RoutingInspectionId == routingInspectionId);
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="RoutingInspectionId"></param>
        /// <returns></returns>
        public static Model.View_Hazard_RoutingInspection GetViewRoutingInspectionById(string routingInspectionId)
        {
            return Funs.DB.View_Hazard_RoutingInspection.FirstOrDefault(e => e.RoutingInspectionId == routingInspectionId);
        }


        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="RoutingInspectionId"></param>
        public static void DeleteRoutingInspectionById(string routingInspectionId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_RoutingInspection RoutingInspection = db.Hazard_RoutingInspection.FirstOrDefault(e => e.RoutingInspectionId == routingInspectionId);
            if (RoutingInspection != null)
            {               
                db.Hazard_RoutingInspection.DeleteOnSubmit(RoutingInspection);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改RoutingInspection信息
        /// </summary>
        /// <param name="RoutingInspection"></param>
        public static void UpdateRoutingInspection(string routingInspectionId, bool isFiled)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Hazard_RoutingInspection newRoutingInspection = db.Hazard_RoutingInspection.FirstOrDefault(e => e.RoutingInspectionId == routingInspectionId);
            if (newRoutingInspection != null)
            {
                newRoutingInspection.IsFiled = isFiled;
                db.SubmitChanges();
            }
        }
    }
}
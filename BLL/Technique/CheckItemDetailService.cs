using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static  class CheckItemDetailService
    {
       public static Model.HSSEDB_ENN db = Funs.DB;

       /// <summary>
       /// 根据主键获取检查项明细
       /// </summary>
       /// <param name="checkItemDetailId"></param>
       /// <returns></returns>
       public static Model.Technique_CheckItemDetail GetCheckItemDetailById(string checkItemDetailId)
       {
           return Funs.DB.Technique_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetailId);
       }

       /// <summary>
       /// 添加检查项明细
       /// </summary>
       /// <param name="checkItemDetail"></param>
       public static void AddCheckItemDetail(Model.Technique_CheckItemDetail checkItemDetail)
       {
            Model.Technique_CheckItemDetail newCheckItemDetail = new Model.Technique_CheckItemDetail
            {
                CheckItemDetailId = checkItemDetail.CheckItemDetailId,
                CheckItemSetId = checkItemDetail.CheckItemSetId,
                CheckContent = checkItemDetail.CheckContent,
                SortIndex = checkItemDetail.SortIndex
            };
            db.Technique_CheckItemDetail.InsertOnSubmit(newCheckItemDetail);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改检查项明细
       /// </summary>
       /// <param name="checkItemDetail"></param>
       public static void UpdateCheckItemDetail(Model.Technique_CheckItemDetail checkItemDetail)
       {
           Model.Technique_CheckItemDetail newCheckItemDetail = db.Technique_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetail.CheckItemDetailId);
           if (newCheckItemDetail != null)
           {
               newCheckItemDetail.CheckContent = checkItemDetail.CheckContent;
               newCheckItemDetail.SortIndex = checkItemDetail.SortIndex;
               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 根据主键删除检查项明细
       /// </summary>
       /// <param name="checkItemDetailId"></param>
       public static void DeleteCheckItemDetail(string checkItemDetailId)
       {
           Model.Technique_CheckItemDetail checkItemDetail = db.Technique_CheckItemDetail.FirstOrDefault(e => e.CheckItemDetailId == checkItemDetailId);
           if (checkItemDetail != null)
           {
               db.Technique_CheckItemDetail.DeleteOnSubmit(checkItemDetail);
               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 根据检查项主键删除所有相关明细信息
       /// </summary>
       /// <param name="rectifyId"></param>
       public static void DeleteCheckItemDetailByCheckItemSetId(string checkItemSetId)
       {
           var q = (from x in db.Technique_CheckItemDetail where x.CheckItemSetId == checkItemSetId select x).ToList();
           if (q.Count() > 0)
           {
               db.Technique_CheckItemDetail.DeleteAllOnSubmit(q);
               db.SubmitChanges();
           }
       }
    }
}
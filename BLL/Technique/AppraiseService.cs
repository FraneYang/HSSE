using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全评价
    /// </summary>
   public static  class AppraiseService
    {
       public static Model.HSSEDB_ENN db = Funs.DB;
       /// <summary>
       /// 根据主键获取安全评价信息
       /// </summary>
       /// <param name="appraise"></param>
       /// <returns></returns>
       public static Model.Technique_Appraise GetAppraiseById(string appraiseId)
       {
           return Funs.DB.Technique_Appraise.FirstOrDefault(e => e.AppraiseId == appraiseId);
       }

       /// <summary>
       /// 添加安全评价信息
       /// </summary>
       /// <param name="appraise"></param>
       public static void AddAppraise(Model.Technique_Appraise appraise)
       {
            Model.Technique_Appraise newAppraise = new Model.Technique_Appraise
            {
                AppraiseId = appraise.AppraiseId,
                AppraiseCode = appraise.AppraiseCode,
                AppraiseTitle = appraise.AppraiseTitle,
                Summary = appraise.Summary,
                AppraiseDate = appraise.AppraiseDate,
                ArrangementPerson = appraise.ArrangementPerson,
                AttachUrl = appraise.AttachUrl,
                ArrangementDate = appraise.ArrangementDate
            };

            db.Technique_Appraise.InsertOnSubmit(newAppraise);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改安全评价信息
       /// </summary>
       /// <param name="appraise"></param>
       public static void UpdateAppraise(Model.Technique_Appraise appraise)
       {
           Model.Technique_Appraise newAppraise = db.Technique_Appraise.FirstOrDefault(e => e.AppraiseId == appraise.AppraiseId);
           if (newAppraise != null)
           {
               newAppraise.AppraiseCode = appraise.AppraiseCode;
               newAppraise.AppraiseTitle = appraise.AppraiseTitle;
               newAppraise.Summary = appraise.Summary;
               newAppraise.AppraiseDate = appraise.AppraiseDate;
               newAppraise.ArrangementPerson = appraise.ArrangementPerson;
               newAppraise.AttachUrl = appraise.AttachUrl;
               newAppraise.ArrangementDate = appraise.ArrangementDate;

               db.SubmitChanges();
           }
       }

       /// <summary>
       ///根据主键删除安全评价
       /// </summary>
       /// <param name="appraise"></param>
       public static void DeleteAppraiseById(string appraiseId)
       {
           Model.Technique_Appraise appraise = db.Technique_Appraise.FirstOrDefault(e => e.AppraiseId == appraiseId);
           if (appraise != null)
           {
               if (!string.IsNullOrEmpty(appraise.AttachUrl))
               {
                   BLL.UploadFileService.DeleteFile(Funs.RootPath, appraise.AttachUrl);
               }
               ////删除附件表
               BLL.CommonService.DeleteAttachFileById(appraise.AppraiseId);
               db.Technique_Appraise.DeleteOnSubmit(appraise);
               db.SubmitChanges();
           }
       }
    }
}

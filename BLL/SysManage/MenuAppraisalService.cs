using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class MenuAppraisalService
   {        
       /// <summary>
       ///  根据测评分值主键获取测评分值信息
       /// </summary>
       /// <param name="appraisalId"></param>
       /// <returns></returns>
       public static Model.Sys_MenuAppraisal GetMenuAppraisalByAppraisalId(string appraisalId)
       {
           return  Funs.DB.Sys_MenuAppraisal.FirstOrDefault(x=> x.AppraisalId == appraisalId);
       }
        
       /// <summary>
       /// 添加测评分值信息
       /// </summary>
       /// <param name="menuAppraisal"></param>
       public static void AddMenuAppraisal(Model.Sys_MenuAppraisal menuAppraisal)
      {           
           Model.Sys_MenuAppraisal newMenuAppraisal = new Model.Sys_MenuAppraisal();
           newMenuAppraisal.AppraisalId = SQLHelper.GetNewID(typeof(Model.Sys_MenuAppraisal));
           newMenuAppraisal.MenuId = menuAppraisal.MenuId;
           newMenuAppraisal.MenuOperation = menuAppraisal.MenuOperation;
           newMenuAppraisal.Score = menuAppraisal.Score;
           Funs.DB.Sys_MenuAppraisal.InsertOnSubmit(newMenuAppraisal);
           Funs.DB.SubmitChanges();
       }

       /// <summary>
       /// 修改测评分值信息
       /// </summary>
       /// <param name="menuAppraisal"></param>
       public static void UpdateMenuAppraisal(Model.Sys_MenuAppraisal menuAppraisal)
       {           
           Model.Sys_MenuAppraisal newMenuAppraisal = Funs.DB.Sys_MenuAppraisal.FirstOrDefault(e => e.AppraisalId == menuAppraisal.AppraisalId);
           if (newMenuAppraisal != null)
           {
               newMenuAppraisal.MenuId = menuAppraisal.MenuId;
               newMenuAppraisal.MenuOperation = menuAppraisal.MenuOperation;
               newMenuAppraisal.Score = menuAppraisal.Score;
               Funs.DB.SubmitChanges();
           }
       }

       /// <summary>
       /// 删除测评分值信息
       /// </summary>
       /// <param name="appraisalId"></param>
       public static void DeleteMenuAppraisal(string appraisalId)
       {
           Model.Sys_MenuAppraisal flow = Funs.DB.Sys_MenuAppraisal.FirstOrDefault(e => e.AppraisalId == appraisalId);
           if (flow != null)
           {
               Funs.DB.Sys_MenuAppraisal.DeleteOnSubmit(flow);
               Funs.DB.SubmitChanges();
           }
       }
    }
}

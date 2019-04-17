using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 测评记录
    /// </summary>
    public static class PersonAppraisalService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="PersonAppraisalId"></param>
        public static void DeletePersonAppraisalById(string PersonAppraisalId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Appraisal_PersonAppraisal PersonAppraisal = db.Appraisal_PersonAppraisal.FirstOrDefault(e => e.PersonAppraisalId == PersonAppraisalId);
            if (PersonAppraisal != null)
            {
                CommonService.DeleteSysPushRecordByDataId(PersonAppraisalId);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, PersonAppraisal.PohotoUrl);
                db.Appraisal_PersonAppraisal.DeleteOnSubmit(PersonAppraisal);
                db.SubmitChanges();
            }
        }
    }
}

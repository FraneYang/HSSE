using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 人员资质
    /// </summary>
    public static class PersonQualityService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取人员资质
        /// </summary>
        /// <param name="personQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_PersonQuality GetPersonQualityById(string personQualityId)
        {
            return db.QualityAudit_PersonQuality.FirstOrDefault(e => e.PersonQualityId == personQualityId);
        }

        /// <summary>
        /// 根据人员ID获取人员资质
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_PersonQuality GetPersonQualityByPersonId(string personId)
        {
            return db.QualityAudit_PersonQuality.FirstOrDefault(e => e.PersonId == personId);
        }

        /// <summary>
        /// 添加人员资质
        /// </summary>
        /// <param name="personQuality"></param>
        public static void AddPersonQuality(Model.QualityAudit_PersonQuality personQuality)
        {
            Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
            {
                PersonQualityId = personQuality.PersonQualityId,
                PersonId = personQuality.PersonId,
                CertificateId = personQuality.CertificateId,
                CertificateNo = personQuality.CertificateNo,
                SendUnit = personQuality.SendUnit,
                SendDate = personQuality.SendDate,
                LimitDate = personQuality.LimitDate,
                LateCheckDate = personQuality.LateCheckDate,
                AuditDate = personQuality.AuditDate,
                Remark = personQuality.Remark,
                CompileMan = personQuality.CompileMan,
                CompileDate = personQuality.CompileDate,
                URL = personQuality.URL,
                ProspectiveIds = personQuality.ProspectiveIds,
                ProspectiveNames = personQuality.ProspectiveNames,
            };
            db.QualityAudit_PersonQuality.InsertOnSubmit(newPersonQuality);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员资质
        /// </summary>
        /// <param name="personQuality"></param>
        public static void UpdatePersonQuality(Model.QualityAudit_PersonQuality personQuality)
        {
            Model.QualityAudit_PersonQuality newPersonQuality = db.QualityAudit_PersonQuality.FirstOrDefault(e => e.PersonQualityId == personQuality.PersonQualityId);
            if (newPersonQuality != null)
            {
                newPersonQuality.PersonId = personQuality.PersonId;
                newPersonQuality.CertificateId = personQuality.CertificateId;
                newPersonQuality.CertificateNo = personQuality.CertificateNo;
                newPersonQuality.SendUnit = personQuality.SendUnit;
                newPersonQuality.SendDate = personQuality.SendDate;
                newPersonQuality.LimitDate = personQuality.LimitDate;
                newPersonQuality.LateCheckDate = personQuality.LateCheckDate;
                newPersonQuality.AuditDate = personQuality.AuditDate;
                newPersonQuality.Remark = personQuality.Remark;
                newPersonQuality.CompileMan = personQuality.CompileMan;
                newPersonQuality.CompileDate = personQuality.CompileDate;
                newPersonQuality.URL = personQuality.URL;
                newPersonQuality.ProspectiveIds = personQuality.ProspectiveIds;
                newPersonQuality.ProspectiveNames = personQuality.ProspectiveNames;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除人员资质
        /// </summary>
        /// <param name="personQualityId"></param>
        public static void DeletePersonQualityById(string personQualityId)
        {
            var personQuality = db.QualityAudit_PersonQuality.FirstOrDefault(e => e.PersonQualityId == personQualityId);
            if (personQuality != null)
            {
                var otem = from x in db.QualityAudit_PersonQualityItem where x.PersonQualityId == personQualityId select x;
                if (otem.Count() > 0)
                {
                    db.QualityAudit_PersonQualityItem.DeleteAllOnSubmit(otem);
                    db.SubmitChanges();
                }

                BLL.CommonService.DeleteAttachFileById(personQualityId);////删除附件表
                db.QualityAudit_PersonQuality.DeleteOnSubmit(personQuality);
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///  保存存资质复查时间
        /// </summary>
        /// <param name="personQualityId"></param>
        /// <param name="lateCheckDate"></param>
        public static void SavePersonQualityItem(string personQualityId, DateTime? lateCheckDate)
        {
            if (lateCheckDate.HasValue)
            {
                var personQualityItem = Funs.DB.QualityAudit_PersonQualityItem.FirstOrDefault(x => x.CheckDate == lateCheckDate);
                if (personQualityItem == null)
                {
                    Model.QualityAudit_PersonQualityItem newItem = new Model.QualityAudit_PersonQualityItem
                    {
                        PersonQualityItemId = BLL.SQLHelper.GetNewID(typeof(Model.QualityAudit_PersonQualityItem)),
                        CheckDate = lateCheckDate,
                        PersonQualityId = personQualityId,
                    };
                    Funs.DB.QualityAudit_PersonQualityItem.InsertOnSubmit(newItem);
                    Funs.DB.SubmitChanges();
                }
            }
        }
    }
}
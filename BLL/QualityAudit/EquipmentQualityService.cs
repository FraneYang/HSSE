using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备资质
    /// </summary>
    public static class EquipmentQualityService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取特种设备资质
        /// </summary>
        /// <param name="equipmentQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_EquipmentQuality GetEquipmentQualityById(string equipmentQualityId)
        {
            return db.QualityAudit_EquipmentQuality.FirstOrDefault(e => e.EquipmentQualityId == equipmentQualityId);
        }

        /// <summary>
        /// 根据设备Id获取特种设备资质
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_EquipmentQuality GetEquipmentQualityByEquipmentId(string equipmentId)
        {
            return db.QualityAudit_EquipmentQuality.FirstOrDefault(e => e.EuipmentId == equipmentId);
        }

        /// <summary>
        /// 添加特种设备资质
        /// </summary>
        /// <param name="equipmentQuality"></param>
        public static void AddEquipmentQuality(Model.QualityAudit_EquipmentQuality equipmentQuality)
        {
            Model.QualityAudit_EquipmentQuality newEquipmentQuality = new Model.QualityAudit_EquipmentQuality
            {
                EquipmentQualityId = equipmentQuality.EquipmentQualityId,
                InstallationId = equipmentQuality.InstallationId,
                UnitId = equipmentQuality.UnitId,
                EuipmentId = equipmentQuality.EuipmentId,
                EquipmentQualityCode = equipmentQuality.EquipmentQualityCode,
                EquipmentQualityName = equipmentQuality.EquipmentQualityName,
                SizeModel = equipmentQuality.SizeModel,
                FactoryCode = equipmentQuality.FactoryCode,
                CertificateCode = equipmentQuality.CertificateCode,
                CheckDate = equipmentQuality.CheckDate,
                LimitDate = equipmentQuality.LimitDate,
                InDate = equipmentQuality.InDate,
                OutDate = equipmentQuality.OutDate,
                ApprovalPerson = equipmentQuality.ApprovalPerson,
                CarNum = equipmentQuality.CarNum,
                Remark = equipmentQuality.Remark,
                CompileMan = equipmentQuality.CompileMan,
                CompileDate = equipmentQuality.CompileDate
            };
            db.QualityAudit_EquipmentQuality.InsertOnSubmit(newEquipmentQuality);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改特种设备资质
        /// </summary>
        /// <param name="equipmentQuality"></param>
        public static void UpdateEquipmentQuality(Model.QualityAudit_EquipmentQuality equipmentQuality)
        {
            Model.QualityAudit_EquipmentQuality newEquipmentQuality = db.QualityAudit_EquipmentQuality.FirstOrDefault(e => e.EquipmentQualityId == equipmentQuality.EquipmentQualityId);
            if (newEquipmentQuality != null)
            {
                newEquipmentQuality.InstallationId = equipmentQuality.InstallationId;
                newEquipmentQuality.UnitId = equipmentQuality.UnitId;
                newEquipmentQuality.EuipmentId = equipmentQuality.EuipmentId;
                newEquipmentQuality.EquipmentQualityCode = equipmentQuality.EquipmentQualityCode;
                newEquipmentQuality.EquipmentQualityName = equipmentQuality.EquipmentQualityName;
                newEquipmentQuality.SizeModel = equipmentQuality.SizeModel;
                newEquipmentQuality.FactoryCode = equipmentQuality.FactoryCode;
                newEquipmentQuality.CertificateCode = equipmentQuality.CertificateCode;
                newEquipmentQuality.CheckDate = equipmentQuality.CheckDate;
                newEquipmentQuality.LimitDate = equipmentQuality.LimitDate;
                newEquipmentQuality.InDate = equipmentQuality.InDate;
                newEquipmentQuality.OutDate = equipmentQuality.OutDate;
                newEquipmentQuality.ApprovalPerson = equipmentQuality.ApprovalPerson;
                newEquipmentQuality.CarNum = equipmentQuality.CarNum;
                newEquipmentQuality.Remark = equipmentQuality.Remark;
                newEquipmentQuality.CompileMan = equipmentQuality.CompileMan;
                newEquipmentQuality.CompileDate = equipmentQuality.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特种设备资质
        /// </summary>
        /// <param name="equipmentQualityId"></param>
        public static void DeleteEquipmentQualityById(string equipmentQualityId)
        {
            Model.QualityAudit_EquipmentQuality equipmentQuality = db.QualityAudit_EquipmentQuality.FirstOrDefault(e => e.EquipmentQualityId == equipmentQualityId);
            if (equipmentQuality != null)
            {
                db.QualityAudit_EquipmentQuality.DeleteOnSubmit(equipmentQuality);
                db.SubmitChanges();
            }
        }
    }
}
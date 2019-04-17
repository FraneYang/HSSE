using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 设备设施
    /// </summary>
    public static class EuipmentService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取设备设施
        /// </summary>
        /// <param name="EuipmentId"></param>
        /// <returns></returns>
        public static Model.Base_Euipment GetEuipmentByEuipmentId(string EuipmentId)
        {
            return Funs.DB.Base_Euipment.FirstOrDefault(e => e.EuipmentId == EuipmentId);
        }

        /// <summary>
        /// 获取名称是否存在
        /// </summary>
        /// <param name="EuipmentId"></param>
        /// <param name="EuipmentName"></param>
        /// <returns></returns>
        public static bool IsExistEuipmentName(string InstallationId, string WorkAreaId, string EuipmentId, string EuipmentName)
        {
            bool isExist = false;
            Model.Base_Euipment Euipment = new Model.Base_Euipment();
            if (!string.IsNullOrEmpty(EuipmentId))
            {
                Euipment = Funs.DB.Base_Euipment.FirstOrDefault(x => x.InstallationId == InstallationId && x.WorkAreaId == WorkAreaId && x.EuipmentName == EuipmentName && x.EuipmentId != EuipmentId);
            }
            else
            {
                Euipment = Funs.DB.Base_Euipment.FirstOrDefault(x => x.InstallationId == InstallationId && x.WorkAreaId == WorkAreaId && x.EuipmentName == EuipmentName);
            }
            if (Euipment != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// 添加设备设施
        /// </summary>
        /// <param name="euipment"></param>
        public static void AddEuipment(Model.Base_Euipment euipment)
        {
            Model.Base_Euipment newEuipment = new Model.Base_Euipment
            {
                InstallationId = euipment.InstallationId,
                WorkAreaId = euipment.WorkAreaId,
                EuipmentId = euipment.EuipmentId,
                EuipmentCode = euipment.EuipmentCode,
                EuipmentName = euipment.EuipmentName,
                EuipmentTypeId = euipment.EuipmentTypeId,
                EuipmentNo = euipment.EuipmentNo,
                Remark = euipment.Remark,
                Identification = euipment.Identification,
                RiskLevel = euipment.RiskLevel,
                QRCodeUrl = euipment.QRCodeUrl,
            };
            db.Base_Euipment.InsertOnSubmit(newEuipment);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改设备设施
        /// </summary>
        /// <param name="euipment"></param>
        public static void UpdateEuipment(Model.Base_Euipment euipment)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Euipment newEuipment = db.Base_Euipment.FirstOrDefault(e => e.EuipmentId == euipment.EuipmentId);
            if (newEuipment != null)
            {
                newEuipment.InstallationId = euipment.InstallationId;
                newEuipment.WorkAreaId = euipment.WorkAreaId;
                newEuipment.EuipmentCode = euipment.EuipmentCode;
                newEuipment.EuipmentName = euipment.EuipmentName;
                newEuipment.Remark = euipment.Remark;
                newEuipment.EuipmentTypeId = euipment.EuipmentTypeId;
                newEuipment.EuipmentNo = euipment.EuipmentNo;
                newEuipment.Identification = euipment.Identification;
                newEuipment.RiskLevel = euipment.RiskLevel;
                newEuipment.QRCodeUrl = euipment.QRCodeUrl;
                db.SubmitChanges();

                var risks = from x in Funs.DB.Hazard_RiskList where x.EuipmentId == euipment.EuipmentId select x;
                if (risks != null)
                {
                    foreach (var item in risks)
                    {
                        item.InstallationId = euipment.InstallationId;
                        item.TaskActivity = euipment.EuipmentName;
                        BLL.RiskListService.UpdateRiskList(item);
                    }
                }
            }
        }

        /// <summary>
        /// 根据主键删除设备设施
        /// </summary>
        /// <param name="EuipmentId"></param>
        public static void DeleteEuipmentById(string EuipmentId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Euipment Euipment = db.Base_Euipment.FirstOrDefault(e => e.EuipmentId == EuipmentId);
            if (Euipment != null)
            {
                var lecItems =BLL.LECItemService.GetLECItemListByDataId(EuipmentId);
                if (lecItems.Count() > 0)
                {
                    foreach (var item in lecItems)
                    {
                        var lecItemRecords  = from x in db.Hazard_LECItemRecord where x.LECItemId == item.LECItemId select x;
                        if (lecItemRecords.Count() > 0)
                        {
                            db.Hazard_LECItemRecord.DeleteAllOnSubmit(lecItemRecords);
                            db.SubmitChanges();
                        }

                        var riskList = BLL.RiskListService.GetRiskListByLECItemId(item.LECItemId);
                        if (riskList != null)
                        {
                            db.Hazard_RiskList.DeleteOnSubmit(riskList);
                            db.SubmitChanges();
                        }                        
                    }

                    db.Hazard_LECItem.DeleteAllOnSubmit(lecItems);
                    db.SubmitChanges();
                }

                var sclItems = BLL.SCLItemService.GetSCLItemListByEuipmentId(EuipmentId);
                if (sclItems.Count() > 0)
                {
                    foreach (var item in sclItems)
                    {
                        var sclItemRecords = from x in db.Hazard_SCLItemRecord where x.SCLItemId == item.SCLItemId select x;
                        if (sclItemRecords.Count() > 0)
                        {
                            db.Hazard_SCLItemRecord.DeleteAllOnSubmit(sclItemRecords);
                            db.SubmitChanges();
                        }

                        var riskList = BLL.RiskListService.GetRiskListBySCLItemId(item.SCLItemId);
                        if (riskList != null)
                        {
                            db.Hazard_RiskList.DeleteOnSubmit(riskList);
                            db.SubmitChanges();
                        }
                    }

                    db.Hazard_SCLItem.DeleteAllOnSubmit(sclItems);
                    db.SubmitChanges();
                }
                db.Base_Euipment.DeleteOnSubmit(Euipment);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据装置Id获取设备设施下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_Euipment> GetEuipmentList(string installationId, string workAreaId)
        {
            var Euipment = (from x in Funs.DB.Base_Euipment orderby x.EuipmentCode select x).ToList();
            if (!string.IsNullOrEmpty(installationId))
            {
                Euipment = Euipment.Where(x => x.InstallationId == installationId).ToList();
            }
            if (!string.IsNullOrEmpty(workAreaId))
            {
                Euipment = Euipment.Where(x => x.WorkAreaId == workAreaId).ToList();
            }
            return Euipment;
        }

        #region 设备设施表下拉框
        /// <summary>
        ///  设备设施表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitEuipmentDropDownList(FineUIPro.DropDownList dropName, string installationId, string workAreaId, bool isShowPlease)
        {
            dropName.DataValueField = "EuipmentId";
            dropName.DataTextField = "EuipmentName";
            dropName.DataSource = BLL.EuipmentService.GetEuipmentList(installationId, workAreaId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        /// <summary>
        /// 获取设备设施下拉表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Euipment> GetEquipmentList()
        {
            return (from x in db.Base_Euipment orderby x.EuipmentCode select x).ToList();
        }
    }
}

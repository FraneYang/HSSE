using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 培训类别
    /// </summary>
    public static class TrainTypeService
    {
        public static  Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取培训类型
        /// </summary>
        /// <param name="trainTypeId"></param>
        /// <returns></returns>
        public static Model.Base_TrainType GetTrainTypeById(string trainTypeId)
        {
            return db.Base_TrainType.FirstOrDefault(e => e.TrainTypeId == trainTypeId);
        }

        /// <summary>
        /// 添加培训类型
        /// </summary>
        /// <param name="trainType"></param>
        public static void AddTrainType(Model.Base_TrainType trainType)
        {
            Model.Base_TrainType newTrainType = new Model.Base_TrainType
            {
                TrainTypeId = trainType.TrainTypeId,
                TrainTypeCode = trainType.TrainTypeCode,
                TrainTypeName = trainType.TrainTypeName,
                Remark = trainType.Remark
            };
            db.Base_TrainType.InsertOnSubmit(newTrainType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改培训类型
        /// </summary>
        /// <param name="trainType"></param>
        public static void UpdateTrainType(Model.Base_TrainType trainType)
        {
            Model.Base_TrainType newTrainType = db.Base_TrainType.FirstOrDefault(e => e.TrainTypeId == trainType.TrainTypeId);
            if (newTrainType != null)
            {
                newTrainType.TrainTypeCode = trainType.TrainTypeCode;
                newTrainType.TrainTypeName = trainType.TrainTypeName;
                newTrainType.Remark = trainType.Remark;;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除培训类型
        /// </summary>
        /// <param name="trainTypeId"></param>
        public static void DeleteTrainTypeById(string trainTypeId)
        {
            Model.Base_TrainType trainType = db.Base_TrainType.FirstOrDefault(e => e.TrainTypeId == trainTypeId);
            if (trainType != null)
            {
                db.Base_TrainType.DeleteOnSubmit(trainType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取培训类型列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_TrainType> GetTrainTypeList()
        {
            return (from x in Funs.DB.Base_TrainType orderby x.TrainTypeCode select x).ToList();
        }
    }
}

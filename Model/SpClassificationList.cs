using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 风险分级管控
    /// </summary>
    public class SpClassificationList
    {
        /// <summary>
        /// NewId
        /// </summary>
        public string NewId
        {
            get;
            set;
        }
        /// <summary>
        /// 风险ID
        /// </summary>
        public string RiskListId
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName
        {
            get;
            set;
        }
        /// <summary>
        /// 装置科室名称
        /// </summary>
        public string InstallationName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }        
        /// <summary>
        /// 风险所属单位
        /// </summary>
        public string HazardInstallationName
        {
            get;
            set;
        }
        /// <summary>
        /// 作业活动名称
        /// </summary>
        public string TaskActivity
        {
            get;
            set;
        }
        /// <summary>
        /// 风险点
        /// </summary>
        public string HazardDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 风险等级
        /// </summary>
        public string RiskLevelName
        {
            get;
            set;
        }
        /// <summary>
        /// 巡检频率
        /// </summary>
        public int? Frequency
        {
            get;
            set;
        }
        /// <summary>
        /// 最近巡检时间
        /// </summary>
        public DateTime? PatrolTime
        {
            get;
            set;
        }
        /// <summary>
        /// 下次巡检时间
        /// </summary>
        public DateTime? NextPatrolTime
        {
            get;
            set;
        }
    }
}

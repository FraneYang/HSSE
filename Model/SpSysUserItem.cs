﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 用户项
    /// </summary>
    public class SpSysUserItem
    {
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartId
        {
            get;
            set;
        }
        /// <summary>
        /// 装置ID
        /// </summary>
        public string InstallationId
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
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
    }
}

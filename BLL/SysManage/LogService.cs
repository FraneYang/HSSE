namespace BLL
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Net;

    public static class LogService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="opLog"></param>
        /// <param name="dataId"></param>
        public static void AddLog(string userId, string opLog)
        {
            var user = BLL.UserService.GetUserByUserId(userId);
            if (user != null)
            {
                Model.HSSEDB_ENN db = Funs.DB;
                Model.Sys_Log syslog = new Model.Sys_Log
                {
                    LogId = SQLHelper.GetNewID(typeof(Model.Sys_Log)),
                    HostName = Dns.GetHostName()
                };
                IPAddress[] ips = Dns.GetHostAddresses(syslog.HostName);
                if (ips.Length > 0)
                {
                    foreach (IPAddress ip in ips)
                    {
                        if (ip.ToString().IndexOf('.') != -1)
                        {
                            syslog.Ip = ip.ToString();
                        }
                    }
                }
                syslog.OperationTime = DateTime.Now;
                syslog.OperationLog = opLog;
                syslog.UserId = userId;
                syslog.LogSource = 1;
                db.Sys_Log.InsertOnSubmit(syslog);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目Id删除所有相关日志信息
        /// </summary>
        /// <param name="projectId"></param>
        public static void DeleteLog()
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var q = (from x in db.Sys_Log select x).ToList();
            if (q!=null)
            {
                db.Sys_Log.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
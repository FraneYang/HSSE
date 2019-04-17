using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 数据消息推送及响应
    /// </summary>
    public static class SysPushRecord
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        public static Model.Sys_PushRecord GetPushRecordByDataIdFlowStep(string dataId, int step)
        {
            return (from x in Funs.DB.Sys_PushRecord
                    where x.DataId == dataId && x.FlowStep == step && x.IsClosed == true
                    orderby x.SigningTime descending 
                    select x).FirstOrDefault();
        }
    }
}

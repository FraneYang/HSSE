using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 公文公告
    /// </summary>
    public static class NoticesService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取公文公告
        /// </summary>
        /// <param name="NoticesId"></param>
        /// <returns></returns>
        public static Model.Resource_Notices GetNoticesById(string NoticesId)
        {
            return Funs.DB.Resource_Notices.FirstOrDefault(e => e.NoticesId == NoticesId);
        }
        
        /// <summary>
        /// 添加公文公告
        /// </summary>
        /// <param name="Notices"></param>
        public static void AddNotices(Model.Resource_Notices Notices)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Resource_Notices newNotices = new Model.Resource_Notices
            {
                NoticesId = Notices.NoticesId,
                Title = Notices.Title,
                ReleaseTime = Notices.ReleaseTime,
                Original = Notices.Original,
                Url = Notices.Url,
                Summary = Notices.Summary,
                ReleaseUnit = Notices.ReleaseUnit
            };
            db.Resource_Notices.InsertOnSubmit(newNotices);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改公文公告
        /// </summary>
        /// <param name="Notices"></param>
        public static void UpdateNotices(Model.Resource_Notices Notices)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Resource_Notices newNotices = db.Resource_Notices.FirstOrDefault(e => e.NoticesId == Notices.NoticesId);
            if (newNotices != null)
            {
                newNotices.Title = Notices.Title;
                newNotices.ReleaseTime = Notices.ReleaseTime;
                newNotices.Original = Notices.Original;
                newNotices.Url = Notices.Url;
                newNotices.Summary = Notices.Summary;
                newNotices.ReleaseUnit = Notices.ReleaseUnit;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除公文公告
        /// </summary>
        /// <param name="NoticesId"></param>
        public static void DeleteNoticesById(string NoticesId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Resource_Notices Notices = db.Resource_Notices.FirstOrDefault(e => e.NoticesId == NoticesId);
            if (Notices != null)
            {
                if (!string.IsNullOrEmpty(Notices.Url))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, Notices.Url);
                }

                db.Resource_Notices.DeleteOnSubmit(Notices);
                db.SubmitChanges();
            }
        }
    }
}

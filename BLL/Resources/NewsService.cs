using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 新闻动态
    /// </summary>
    public static class NewsService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取新闻动态
        /// </summary>
        /// <param name="NewsId"></param>
        /// <returns></returns>
        public static Model.Resource_News GetNewsById(string NewsId)
        {
            return Funs.DB.Resource_News.FirstOrDefault(e => e.NewsId == NewsId);
        }
        
        /// <summary>
        /// 添加新闻动态
        /// </summary>
        /// <param name="News"></param>
        public static void AddNews(Model.Resource_News News)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Resource_News newNews = new Model.Resource_News
            {
                NewsId = News.NewsId,
                Title = News.Title,
                ReleaseTime = News.ReleaseTime,
                Original = News.Original,
                Url = News.Url,
                Summary = News.Summary
            };
            db.Resource_News.InsertOnSubmit(newNews);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改新闻动态
        /// </summary>
        /// <param name="News"></param>
        public static void UpdateNews(Model.Resource_News News)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Resource_News newNews = db.Resource_News.FirstOrDefault(e => e.NewsId == News.NewsId);
            if (newNews != null)
            {
                newNews.Title = News.Title;
                newNews.ReleaseTime = News.ReleaseTime;
                newNews.Original = News.Original;
                newNews.Url = News.Url;
                newNews.Summary = News.Summary;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除新闻动态
        /// </summary>
        /// <param name="NewsId"></param>
        public static void DeleteNewsById(string NewsId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Resource_News News = db.Resource_News.FirstOrDefault(e => e.NewsId == NewsId);
            if (News != null)
            {
                if (!string.IsNullOrEmpty(News.Url))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, News.Url);
                }

                db.Resource_News.DeleteOnSubmit(News);
                db.SubmitChanges();
            }
        }
    }
}

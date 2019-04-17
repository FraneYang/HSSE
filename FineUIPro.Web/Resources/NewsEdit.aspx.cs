using System;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Resources
{
    public partial class NewsEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string NewsId
        {
            get
            {
                return (string)ViewState["NewsId"];
            }
            set
            {
                ViewState["NewsId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();//设置权限
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();


                this.NewsId = Request.Params["NewsId"];
                if (!string.IsNullOrEmpty(this.NewsId))
                {
                    var News = BLL.NewsService.GetNewsById(this.NewsId);
                    if (News != null)
                    {
                        this.txtTitle.Text = News.Title;
                        if (News.ReleaseTime != null)
                        {
                            this.dpkReleaseTime.Text = string.Format("{0:yyyy-MM-dd hh:mm:ss}", News.ReleaseTime);
                        }
                        this.txtSummary.Text = News.Summary;
                        this.txtOriginal.Text = HttpUtility.HtmlDecode(News.Original);
                    }
                }
                else
                {
                    this.dpkReleaseTime.Text = string.Format("{0:yyyy-MM-dd hh:mm:ss}", System.DateTime.Now);
                }
            }
        }
        
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData( bool isColose)
        {
            Model.Resource_News newNews = new Model.Resource_News
            {
                Title = this.txtTitle.Text.Trim(),
                ReleaseTime = Funs.GetNewDateTime(this.dpkReleaseTime.Text),
                Summary = this.txtSummary.Text.Trim()
            };
            var att = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == this.NewsId);
            if(att != null)
            {
                newNews.Url = att.AttachUrl;
            }
            
            newNews.Original = HttpUtility.HtmlEncode(this.txtOriginal.Text);
            if (string.IsNullOrEmpty(this.NewsId))
            {
                this.NewsId = SQLHelper.GetNewID(typeof(Model.Resource_News));
                newNews.NewsId = this.NewsId;
                BLL.NewsService.AddNews(newNews);
                BLL.LogService.AddLog(this.CurrUser.UserId, "增加安全新闻动态");
            }
            else
            {
                newNews.NewsId = this.NewsId;
                BLL.NewsService.UpdateNews(newNews);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改安全新闻动态");
            }
            if (isColose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion               

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.NewsId))
            {
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/News&menuId={1}", NewsId, BLL.Const.NewsMenuId)));
        }
        #endregion

        #region 设置权限
        /// <summary>
        /// 设置权限
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.NewsMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion        
    }
}
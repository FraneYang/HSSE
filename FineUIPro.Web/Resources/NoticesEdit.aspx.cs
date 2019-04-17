using System;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Resources
{
    public partial class NoticesEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string NoticesId
        {
            get
            {
                return (string)ViewState["NoticesId"];
            }
            set
            {
                ViewState["NoticesId"] = value;
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


                this.NoticesId = Request.Params["NoticesId"];
                if (!string.IsNullOrEmpty(this.NoticesId))
                {
                    var getNotices = BLL.NoticesService.GetNoticesById(this.NoticesId);
                    if (getNotices != null)
                    {
                        this.txtTitle.Text = getNotices.Title;
                        if (getNotices.ReleaseTime != null)
                        {
                            this.dpkReleaseTime.Text = string.Format("{0:yyyy-MM-dd hh:mm:ss}", getNotices.ReleaseTime);
                        }
                        this.txtSummary.Text = getNotices.Summary;
                        this.txtOriginal.Text = HttpUtility.HtmlDecode(getNotices.Original);
                        this.txtReleaseUnit.Text = getNotices.ReleaseUnit;
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
            Model.Resource_Notices newNotices = new Model.Resource_Notices
            {
                Title = this.txtTitle.Text.Trim(),
                ReleaseTime = Funs.GetNewDateTime(this.dpkReleaseTime.Text),
                Summary = this.txtSummary.Text.Trim(),
                ReleaseUnit = this.txtReleaseUnit.Text.Trim(),
            };
            var att = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == this.NoticesId);
            if(att != null)
            {
                newNotices.Url = att.AttachUrl;
            }
            
            newNotices.Original = HttpUtility.HtmlEncode(this.txtOriginal.Text);
            if (string.IsNullOrEmpty(this.NoticesId))
            {
                this.NoticesId = SQLHelper.GetNewID(typeof(Model.Resource_Notices));
                newNotices.NoticesId = this.NoticesId;
                BLL.NoticesService.AddNotices(newNotices);
                BLL.LogService.AddLog(this.CurrUser.UserId, "增加安全公文公告");
            }
            else
            {
                newNotices.NoticesId = this.NoticesId;
                BLL.NoticesService.UpdateNotices(newNotices);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改安全公文公告");
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
            if (string.IsNullOrEmpty(this.NoticesId))
            {
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Notices&menuId={1}", NoticesId, BLL.Const.NoticesMenuId)));
        }
        #endregion

        #region 设置权限
        /// <summary>
        /// 设置权限
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.NoticesMenuId);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Model;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainingEduItemSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string TrainingEduItemId
        {
            get
            {
                return (string)ViewState["TrainingEduItemId"];
            }
            set
            {
                ViewState["TrainingEduItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
        /// </summary>
        public string TrainingEduId
        {
            get
            {
                return (string)ViewState["TrainingEduId"];
            }
            set
            {
                ViewState["TrainingEduId"] = value;
            }
        }

        /// <summary>
        /// 图片
        /// </summary>
        private string PictureUrl
        {
            get
            {
                return (string)ViewState["PictureUrl"];
            }
            set
            {
                ViewState["PictureUrl"] = value;
            }
        }
        /// <summary>
        /// 附件
        /// </summary>
        private string AttachUrl
        {
            get
            {
                return (string)ViewState["AttachUrl"];
            }
            set
            {
                ViewState["AttachUrl"] = value;
            }
        }
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BindEnumrableToDropDownList();
                this.TrainingEduItemId = Request.QueryString["TrainingEduItemId"];
                this.TrainingEduId = Request.QueryString["TrainingEduId"];
                if (!String.IsNullOrEmpty(this.TrainingEduItemId))
                {
                    var q = BLL.TrainingEduItemService.GetTrainingEduItemById(this.TrainingEduItemId);
                    if (q != null)
                    {
                        this.txtTrainingEduItemCode.Text = q.TrainingEduItemCode;
                        this.txtTrainingEduItemName.Text = q.TrainingEduItemName;
                        this.txtSummary.Text = HttpUtility.HtmlDecode(q.Summary);
                        if (!string.IsNullOrEmpty(q.InstallationIds))
                        {
                            string[] ids1 = q.InstallationIds.Split(',');
                            DropDownBox1.Values = ids1;
                        }
                       
                        this.PictureUrl = q.PictureUrl;
                        if (!string.IsNullOrEmpty(this.PictureUrl))
                        {
                            this.trImageUrl.Visible = true;
                            this.divPictureUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.PictureUrl);
                            this.divBeImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../", this.PictureUrl);
                        }
                        this.AttachUrl = q.AttachUrl;
                        if (!string.IsNullOrEmpty(this.AttachUrl))
                        {                           
                            this.divAttachUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);
                        }
                    }
                }
            }
        }
        
        private void BindEnumrableToDropDownList()
        {
            List<Model.Base_Installation> myList = BLL.InstallationService.GetInstallationByDepartIdList(string.Empty);
            RadioButtonList1.DataTextField = "InstallationName";
            RadioButtonList1.DataValueField = "InstallationId";
            RadioButtonList1.DataSource = myList;
            RadioButtonList1.DataBind();
        }
        #endregion

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPictureUrlDelete_Click(object sender, EventArgs e)
        {
            this.trImageUrl.Visible = false;
            this.PictureUrl = string.Empty;
            this.divPictureUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.PictureUrl);
            this.divBeImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../", this.PictureUrl);
        }
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrlDelete_Click(object sender, EventArgs e)
        {            
            this.AttachUrl = string.Empty;
            this.divAttachUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);
        }

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(bool isClosed)
        {
            Model.Training_TrainingEduItem TrainingEduItem = new Training_TrainingEduItem
            {
                TrainingEduItemCode = txtTrainingEduItemCode.Text.Trim(),
                TrainingEduItemName = txtTrainingEduItemName.Text.Trim(),
                Summary = txtSummary.Text.Trim(),
                PictureUrl = this.PictureUrl,
                AttachUrl=this.AttachUrl,
            };
            if (!string.IsNullOrEmpty(DropDownBox1.Text))
            {
                TrainingEduItem.InstallationIds = String.Join(",", DropDownBox1.Values);
                TrainingEduItem.InstallationNames = String.Join(",", DropDownBox1.Texts);
            }

            if (String.IsNullOrEmpty(TrainingEduItemId))
            {
                TrainingEduItem.TrainingEduItemId = SQLHelper.GetNewID(typeof(Model.Training_TrainingEduItem));
                TrainingEduItem.TrainingEduId = this.TrainingEduId;
                this.TrainingEduItemId = TrainingEduItem.TrainingEduItemId;
                BLL.TrainingEduItemService.AddTrainingEduItem(TrainingEduItem);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加培训教材库");
            }
            else
            {
                Model.Training_TrainingEduItem t = BLL.TrainingEduItemService.GetTrainingEduItemById(TrainingEduItemId);
                TrainingEduItem.TrainingEduItemId = TrainingEduItemId;
                if (t != null)
                {
                    TrainingEduItem.TrainingEduId = t.TrainingEduId;
                }
                BLL.TrainingEduItemService.UpdateTrainingEduItem(TrainingEduItem);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改培训教材库");
            }
            if (isClosed)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPictureUrl_Click(object sender, EventArgs e)
        {
            if (btnPictureUrl.HasFile)
            {
                this.PictureUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnPictureUrl, this.PictureUrl, UploadFileService.TrainingEduFilePath);
                if (!string.IsNullOrEmpty(this.PictureUrl))
                {
                    this.trImageUrl.Visible = true;
                    this.divPictureUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.PictureUrl);
                    this.divBeImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../", this.PictureUrl);
                }
            }
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (btnAttachUrl.HasFile)
            {
                this.AttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnAttachUrl, this.AttachUrl, UploadFileService.TrainingEduFilePath);
                if (!string.IsNullOrEmpty(this.AttachUrl))
                {                    
                    this.divAttachUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);                    
                }
            }
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.TrainingEduMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证教材名称是否存在
        /// <summary>
        /// 验证教材名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Training_TrainingEduItem.FirstOrDefault(x => x.TrainingEduId == this.TrainingEduId && x.TrainingEduItemName == this.txtTrainingEduItemName.Text.Trim() && (x.TrainingEduItemId != this.TrainingEduItemId || (this.TrainingEduItemId == null && x.TrainingEduItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的教材名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrlC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TrainingEduItemId))
            {
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainingEduItemAttachUrl&menuId={1}", this.TrainingEduItemId, BLL.Const.TrainingEduMenuId)));
        }
        #endregion
    }
}
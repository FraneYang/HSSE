using System;
using System.Linq;
using Model;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class AccidentCaseItemEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主表Id
        /// </summary>
        public string AccidentCaseId
        {
            get
            {
                return (string)ViewState["AccidentCaseId"];
            }
            set
            {
                ViewState["AccidentCaseId"] = value;
            }
        }

        /// <summary>
        /// 明细表Id
        /// </summary>
        public string AccidentCaseItemId
        {
            get
            {
                return (string)ViewState["AccidentCaseItemId"];
            }
            set
            {
                ViewState["AccidentCaseItemId"] = value;
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
                this.GetButtonPower();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.AccidentCaseId = Request.QueryString["AccidentCaseId"];
                if (!string.IsNullOrEmpty(this.AccidentCaseId))
                {
                    var accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(this.AccidentCaseId);
                    if (accidentCase != null)
                    {
                        this.txtAccidentCaseName.Text = accidentCase.AccidentCaseName;
                    }
                }
                this.AccidentCaseItemId = Request.QueryString["AccidentCaseItemId"];
                if (!String.IsNullOrEmpty(this.AccidentCaseItemId))
                {
                    var q = BLL.AccidentCaseItemService.GetAccidentCaseItemById(this.AccidentCaseItemId);
                    if (q != null)
                    {
                        this.txtActivities.Text = q.Activities;
                        this.txtAccidentName.Text = q.AccidentName;
                        this.txtAccidentProfiles.Text = q.AccidentProfiles;
                        this.txtAccidentReview.Text = q.AccidentReview;
                        if (!string.IsNullOrEmpty(q.AccidentCaseId))
                        {
                            var accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(q.AccidentCaseId);
                            if (accidentCase != null)
                            {
                                this.txtAccidentCaseName.Text = accidentCase.AccidentCaseName;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(bool isClosed)
        {
            Model.EduTrain_AccidentCaseItem accidentCaseItem = new EduTrain_AccidentCaseItem
            {
                Activities = this.txtActivities.Text.Trim(),
                AccidentName = this.txtAccidentName.Text.Trim(),
                AccidentProfiles = this.txtAccidentProfiles.Text.Trim(),
                AccidentReview = this.txtAccidentReview.Text.Trim()
            };
            if (string.IsNullOrEmpty(this.AccidentCaseItemId))
            {
                accidentCaseItem.CompileMan = this.CurrUser.UserName;
                accidentCaseItem.CompileDate = DateTime.Now;
                accidentCaseItem.AccidentCaseItemId = SQLHelper.GetNewID(typeof(Model.EduTrain_AccidentCaseItem));
                AccidentCaseItemId = accidentCaseItem.AccidentCaseItemId;
                accidentCaseItem.AccidentCaseId = this.AccidentCaseId;
                BLL.AccidentCaseItemService.AddAccidentCaseItem(accidentCaseItem);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加事故案例库");
            }
            else
            {
                Model.EduTrain_AccidentCaseItem t = BLL.AccidentCaseItemService.GetAccidentCaseItemById(this.AccidentCaseItemId);
                accidentCaseItem.AccidentCaseItemId = this.AccidentCaseItemId;
                if (t != null)
                {
                    accidentCaseItem.AccidentCaseId = t.AccidentCaseId;
                }
                BLL.AccidentCaseItemService.UpdateAccidentCaseItem(accidentCaseItem);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改事故案例库");
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

        #region 验证事故名称是否存在
        /// <summary>
        /// 验证事故名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.EduTrain_AccidentCaseItem.FirstOrDefault(x => x.AccidentCaseId == this.AccidentCaseId && x.AccidentName == this.txtAccidentName.Text.Trim() && (x.AccidentCaseItemId != this.AccidentCaseItemId || (this.AccidentCaseItemId == null && x.AccidentCaseItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的事故名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.AccidentCaseMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
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
            if (string.IsNullOrEmpty(this.AccidentCaseItemId))
            {
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/AccidentCaseItemAttachUrl&menuId={1}", this.AccidentCaseItemId, BLL.Const.AccidentCaseMenuId)));
        }
        #endregion
    }
}
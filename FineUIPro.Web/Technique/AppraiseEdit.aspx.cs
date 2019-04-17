using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class AppraiseEdit :PageBase
    {
        #region 定义变量
        /// <summary>
        /// 安全评价
        /// </summary>
        public string AppraiseId
        {
            get
            {
                return (string)ViewState["AppraiseId"];
            }
            set
            {
                ViewState["AppraiseId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ////权限按钮方法
                this.GetButtonPower();
                //整理人下拉选项
                this.ddlArrangementPerson.DataTextField = "UserName";
                ddlArrangementPerson.DataValueField = "UserId";
                ddlArrangementPerson.DataSource = BLL.UserService.GetUserList();
                ddlArrangementPerson.DataBind();
                Funs.FineUIPleaseSelect(this.ddlArrangementPerson);

                //加载默认整理人、整理日期
                this.ddlArrangementPerson.SelectedValue = this.CurrUser.UserId;
                this.dpkArrangementDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                this.AppraiseId = Request.Params["AppraiseId"];
                if (!string.IsNullOrEmpty(this.AppraiseId))
                {
                    var Appraise = BLL.AppraiseService.GetAppraiseById(this.AppraiseId);
                    if (Appraise != null)
                    {
                        this.txtAppraiseCode.Text = Appraise.AppraiseCode;
                        this.txtAppraiseTitle.Text = Appraise.AppraiseTitle;
                        if (Appraise.AppraiseDate != null)
                        {
                            this.dpkAppraiseDate.Text = string.Format("{0:yyyy-MM-dd}", Appraise.AppraiseDate);
                        }
                        if (Appraise.ArrangementDate != null)
                        {
                            this.dpkArrangementDate.Text = string.Format("{0:yyyy-MM-dd}", Appraise.ArrangementDate);
                        }
                        this.txtAbstract.Text = Appraise.Summary;
                        if (!string.IsNullOrEmpty(Appraise.ArrangementPerson))
                        {
                            this.ddlArrangementPerson.SelectedItem.Text = Appraise.ArrangementPerson;
                        }
                    }
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
            SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            Model.Technique_Appraise appraise = new Model.Technique_Appraise
            {
                AppraiseCode = this.txtAppraiseCode.Text.Trim(),
                AppraiseTitle = this.txtAppraiseTitle.Text.Trim(),
                Summary = this.txtAbstract.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkAppraiseDate.Text.Trim()))
            {
                appraise.AppraiseDate = Convert.ToDateTime(this.dpkAppraiseDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.dpkArrangementDate.Text.Trim()))
            {
                appraise.ArrangementDate = Convert.ToDateTime(this.dpkArrangementDate.Text.Trim());
            }
            if (this.ddlArrangementPerson.SelectedValue != "null")
            {
                appraise.ArrangementPerson = this.ddlArrangementPerson.SelectedItem.Text;
            }
            if (string.IsNullOrEmpty(this.AppraiseId))
            {
                appraise.CompileMan = this.CurrUser.UserName;
                appraise.CompileDate = DateTime.Now;
                this.AppraiseId = appraise.AppraiseId = SQLHelper.GetNewID(typeof(Model.Technique_Appraise));
                BLL.AppraiseService.AddAppraise(appraise);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加安全评价");
            }
            else
            {
                appraise.AppraiseId = this.AppraiseId;
                BLL.AppraiseService.UpdateAppraise(appraise);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改安全评价");
            }
        }
        #endregion        

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.AppraiseId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Appraise&menuId=0ADD01FB-8088-4595-BB40-6A73F332A229", this.AppraiseId)));
        }
        #endregion

        #region 验证编号是否存在
        /// <summary>
        /// 验证编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_Appraise.FirstOrDefault(x => x.AppraiseCode == this.txtAppraiseCode.Text.Trim() && (x.AppraiseId != this.AppraiseId || (this.AppraiseId == null && x.AppraiseId != null)));
            if (q != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Technique_Appraise.FirstOrDefault(x => x.AppraiseTitle == this.txtAppraiseTitle.Text.Trim() && (x.AppraiseId != this.AppraiseId || (this.AppraiseId == null && x.AppraiseId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的标题已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.AppraiseMenuId);
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
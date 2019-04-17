using System;
using System.Linq;
using BLL;
using Model;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainingEduSave : PageBase
    {
        #region 自定义
        /// <summary>
        /// 主键
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
        /// 主键
        /// </summary>
        public string SupTrainingEduId
        {
            get
            {
                return (string)ViewState["SupTrainingEduId"];
            }
            set
            {
                ViewState["SupTrainingEduId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SupTrainingEduId = Request.QueryString["SupTrainingEduId"];
                this.TrainingEduId = Request.QueryString["TrainingEduId"];
                if (!String.IsNullOrEmpty(TrainingEduId))
                {
                    var q = BLL.TrainingEduService.GetTrainingEduById(this.TrainingEduId);
                    if (q != null)
                    {
                        this.SupTrainingEduId = q.SupTrainingEduId;
                        txtTrainingCode.Text = q.TrainingEduCode;
                        txtTrainingName.Text = q.TrainingEduName;
                    }
                }

                var supq = BLL.TrainingEduService.GetTrainingEduById(this.SupTrainingEduId);
                if (supq != null)
                {
                    this.txtSupTrainingEdu.Text = supq.TrainingEduName;
                }
                else
                {
                    this.txtSupTrainingEdu.Text = "培训教材库";
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Training_TrainingEdu training = new Training_TrainingEdu
            {
                TrainingEduCode = txtTrainingCode.Text.Trim(),
                TrainingEduName = txtTrainingName.Text.Trim(),
                SupTrainingEduId =this.SupTrainingEduId,
            };

            if (String.IsNullOrEmpty(TrainingEduId))
            {
                TrainingEduId = SQLHelper.GetNewID(typeof(Model.Training_TrainingEdu));
                training.TrainingEduId = TrainingEduId;
                BLL.TrainingEduService.AddTrainingEdu(training);
            }
            else
            {
                training.TrainingEduId = TrainingEduId;
                BLL.TrainingEduService.UpdateTrainingEdu(training);
            }
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(TrainingEduId) + ActiveWindow.GetHideReference());

        }

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

        #region 验证培训教材库名称是否存在
        /// <summary>
        /// 验证培训教材库名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Training_TrainingEdu.FirstOrDefault(x => x.TrainingEduName == this.txtTrainingName.Text.Trim() && (x.TrainingEduId != this.TrainingEduId || (this.TrainingEduId == null && x.TrainingEduId != null)) && x.SupTrainingEduId == this.SupTrainingEduId);
            if (standard != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
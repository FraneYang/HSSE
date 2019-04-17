using System;
using System.Linq;
using Model;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class AccidentCaseEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
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
        /// 上级ID
        /// </summary>
        public string SupAccidentCaseId
        {
            get
            {
                return (string)ViewState["SupAccidentCaseId"];
            }
            set
            {
                ViewState["SupAccidentCaseId"] = value;
            }
        }
        #endregion

        #region 加载
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.drpIsEndLever.DataTextField = "ConstText";
                this.drpIsEndLever.DataValueField = "ConstValue";
                this.drpIsEndLever.DataSource = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_Y_N);
                this.drpIsEndLever.DataBind();

                this.AccidentCaseId = Request.QueryString["AccidentCaseId"];
                this.SupAccidentCaseId = Request.QueryString["SupAccidentCaseId"];
                if (!string.IsNullOrEmpty(this.AccidentCaseId))
                {
                    var q = BLL.AccidentCaseService.GetAccidentCaseById(this.AccidentCaseId);
                    if (q != null)
                    {
                        this.SupAccidentCaseId = q.SupAccidentCaseId;
                        txtAccidentCaseCode.Text = q.AccidentCaseCode;
                        txtAccidentCaseName.Text = q.AccidentCaseName;
                        if (q.IsEndLever == true)
                        {
                            this.drpIsEndLever.SelectedValue = "true";
                        }
                        else
                        {
                            this.drpIsEndLever.SelectedValue = "false";
                        }
                    }
                }

                var supq = BLL.AccidentCaseService.GetAccidentCaseById(this.SupAccidentCaseId);
                if (supq != null)
                {
                    this.txtSupAccidentCase.Text = supq.AccidentCaseName;
                }
                else
                {
                    this.txtSupAccidentCase.Text = "事故案例库";
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
            Model.EduTrain_AccidentCase accidentCase = new EduTrain_AccidentCase
            {
                AccidentCaseCode = txtAccidentCaseCode.Text.Trim(),
                AccidentCaseName = txtAccidentCaseName.Text.Trim(),
                IsEndLever = Convert.ToBoolean(drpIsEndLever.SelectedValue)
            };
            if (String.IsNullOrEmpty(this.AccidentCaseId))
            {
                accidentCase.AccidentCaseId = SQLHelper.GetNewID(typeof(Model.EduTrain_AccidentCase));
                this.AccidentCaseId = accidentCase.AccidentCaseId;
                accidentCase.SupAccidentCaseId = this.SupAccidentCaseId;
                BLL.AccidentCaseService.AddAccidentCase(accidentCase);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加事故案例类型");
            }
            else
            {
                Model.EduTrain_AccidentCase t = BLL.AccidentCaseService.GetAccidentCaseById(this.AccidentCaseId);
                accidentCase.AccidentCaseId = this.AccidentCaseId;
                if (t != null)
                {
                    accidentCase.SupAccidentCaseId = t.SupAccidentCaseId;
                }
                BLL.AccidentCaseService.UpdateAccidentCase(accidentCase);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改事故案例类型");
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 验证名称是否存在
        /// <summary>
        /// 验证名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.EduTrain_AccidentCase.FirstOrDefault(x => x.SupAccidentCaseId == this.SupAccidentCaseId && x.AccidentCaseName == this.txtAccidentCaseName.Text.Trim() && (x.AccidentCaseId != this.AccidentCaseId || (this.AccidentCaseId == null && x.AccidentCaseId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
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
    }
}
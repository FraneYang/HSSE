using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class HAZOPEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string HAZOPId
        {
            get
            {
                return (string)ViewState["HAZOPId"];
            }
            set
            {
                ViewState["HAZOPId"] = value;
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
                var unit = BLL.CommonService.GetIsThisUnit();
                if (unit != null)
                {
                    this.hdUnitId.Text = unit.UnitId;
                    this.txtUnitName.Text = unit.UnitName;
                }

                this.HAZOPId = Request.Params["HAZOPId"];
                if (!string.IsNullOrEmpty(this.HAZOPId))
                {
                    var hazop = BLL.HAZOPService.GetHAZOPById(this.HAZOPId);
                    if (hazop != null)
                    {
                        if (!string.IsNullOrEmpty(hazop.UnitId))
                        {
                            var u = BLL.UnitService.GetUnitByUnitId(hazop.UnitId);
                            if (u != null)
                            {
                                this.hdUnitId.Text = u.UnitId;
                                this.txtUnitName.Text = u.UnitName;
                            }
                        }
                        this.txtTitle.Text = hazop.HAZOPTitle;
                        this.txtAbstract.Text = hazop.Summary;
                        if (hazop.HAZOPDate != null)
                        {
                            this.dpkHAZOPDate.Text = string.Format("{0:yyyy-MM-dd}", hazop.HAZOPDate);
                        }
                    }
                }
                else
                {
                    this.dpkHAZOPDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
        private void SaveData(bool isClose)
        {
            Model.Technique_HAZOP hazop = new Model.Technique_HAZOP
            {
                UnitId = this.hdUnitId.Text,
                HAZOPTitle = this.txtTitle.Text.Trim(),
                Summary = this.txtAbstract.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkHAZOPDate.Text.Trim()))
            {
                hazop.HAZOPDate = Convert.ToDateTime(this.dpkHAZOPDate.Text.Trim());
            }
            if (string.IsNullOrEmpty(HAZOPId))
            {
                hazop.CompileMan = this.CurrUser.UserName;
                hazop.CompileDate = DateTime.Now;
                this.HAZOPId = hazop.HAZOPId = SQLHelper.GetNewID(typeof(Model.Technique_HAZOP));
                BLL.HAZOPService.AddHAZOP(hazop);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加HAZOP管理");
            }
            else
            {
                hazop.HAZOPId = this.HAZOPId;
                BLL.HAZOPService.UpdateHAZOP(hazop);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改HAZOP管理");
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion              

        #region 验证标题是否存在
        /// <summary>
        /// 验证标题是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_HAZOP.FirstOrDefault(x => x.HAZOPTitle == this.txtTitle.Text.Trim() && (x.HAZOPId != this.HAZOPId || (this.HAZOPId == null && x.HAZOPId != null)));
            if (q != null)
            {
                ShowNotify("输入的标题已存在！", MessageBoxIcon.Warning);
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
            if (string.IsNullOrEmpty(this.HAZOPId))
            {
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HAZOP&menuId=41C22E63-36B7-4C44-89EC-F765BFBB7C55", HAZOPId)));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.HAZOPMenuId);
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
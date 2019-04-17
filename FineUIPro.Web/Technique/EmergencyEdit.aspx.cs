using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class EmergencyEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 安全评价
        /// </summary>
        public string EmergencyId
        {
            get
            {
                return (string)ViewState["EmergencyId"];
            }
            set
            {
                ViewState["EmergencyId"] = value;
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
                //应急类型下拉选项
                BLL.EmergencyTypeService.InitEmergencyTypeDropDownList(this.ddlEmergencyType, true);

                this.EmergencyId = Request.Params["EmergencyId"];
                if (!string.IsNullOrEmpty(this.EmergencyId))
                {
                    var Emergency = BLL.EmergencyService.GetEmergencyListById(this.EmergencyId);
                    if (Emergency != null)
                    {
                        this.txtEmergencyCode.Text = Emergency.EmergencyCode;
                        this.txtEmergencyName.Text = Emergency.EmergencyName;
                        this.txtSummary.Text = Emergency.Summary;
                        if (!string.IsNullOrEmpty(Emergency.EmergencyTypeId))
                        {
                            this.ddlEmergencyType.SelectedValue = Emergency.EmergencyTypeId;
                        }
                        this.txtRemark.Text = Emergency.Remark;                      
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
            Model.Technique_Emergency emergency = new Model.Technique_Emergency
            {
                EmergencyCode = this.txtEmergencyCode.Text.Trim(),
                EmergencyName = this.txtEmergencyName.Text.Trim(),
                Summary = this.txtSummary.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            ////应急救援库类型下拉框
            if (this.ddlEmergencyType.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.ddlEmergencyType.SelectedValue))
            {
                emergency.EmergencyTypeId = this.ddlEmergencyType.SelectedValue;                
            }

            if (string.IsNullOrEmpty(this.EmergencyId))
            {
                emergency.CompileMan = this.CurrUser.UserName;
                emergency.CompileDate = DateTime.Now;
                this.EmergencyId = emergency.EmergencyId = SQLHelper.GetNewID(typeof(Model.Technique_Emergency));
                BLL.EmergencyService.AddEmergencyList(emergency);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加应急救援库");
            }
            else
            {
                emergency.EmergencyId = this.EmergencyId;
                BLL.EmergencyService.UpdateEmergencyList(emergency);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改应急救援库");
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
            if (string.IsNullOrEmpty(this.EmergencyId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Emergency&menuId=575C5154-A135-4737-8682-A129EA717660", this.EmergencyId)));
        }
        #endregion
        
        #region 验证编号、名称否存在
        /// <summary>
        /// 验证编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_Emergency.FirstOrDefault(x => x.EmergencyCode == this.txtEmergencyCode.Text.Trim() && (x.EmergencyId != this.EmergencyId || (this.EmergencyId == null && x.EmergencyId != null)));
            if (q != null)
            {
                ShowNotify("输入的应急救援库编号已存在！", MessageBoxIcon.Warning);
            }
            var q2 = Funs.DB.Technique_Emergency.FirstOrDefault(x => x.EmergencyName == this.txtEmergencyName.Text.Trim() && (x.EmergencyId != this.EmergencyId || (this.EmergencyId == null && x.EmergencyId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的应急救援库名称已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.EmergencyMenuId);
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
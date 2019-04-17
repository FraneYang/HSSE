using System;
using System.IO;
using System.Web.UI;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class HiddenHazardView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HiddenHazardId
        {
            get
            {
                return (string)ViewState["HiddenHazardId"];
            }
            set
            {
                ViewState["HiddenHazardId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.HiddenHazardId = Request.Params["HiddenHazardId"];
                if (!string.IsNullOrEmpty(this.HiddenHazardId))
                {
                    var getHiddenHazard = BLL.HiddenHazardService.GetHiddenHazardViewById(this.HiddenHazardId);
                    if (getHiddenHazard != null)
                    {
                        this.txtHiddenHazardCode.Text = getHiddenHazard.HiddenHazardCode;
                        this.txtHiddenHazardName.Text = getHiddenHazard.HiddenHazardName;
                        if (getHiddenHazard.IsMajor == true)
                        {
                            this.txtIsMajor.Text = "重大";
                        }
                        else
                        {
                            this.txtIsMajor.Text = "一般";
                        }
                        this.txtLimitTime.Text = string.Format("{0:yyyy-MM-dd hh:mm:ss}", getHiddenHazard.LimitTime);
                        if (!string.IsNullOrEmpty(getHiddenHazard.InstallationName))
                        {
                            this.txtHiddenHazardPlace.Text = getHiddenHazard.InstallationName + "：" + getHiddenHazard.HiddenHazardPlace;
                        }
                        else
                        {
                            this.txtHiddenHazardPlace.Text = getHiddenHazard.HiddenHazardPlace;
                        }
                        this.txtHiddenHazardTypeName.Text = getHiddenHazard.HiddenHazardTypeName;
                        this.txtDescription.Text = getHiddenHazard.Description;
                        this.txtControlMeasures.Text = getHiddenHazard.ControlMeasures;
                        this.txtCorrectMeasures.Text = getHiddenHazard.CorrectMeasures;
                        this.txtCorrectMoney.Text = getHiddenHazard.CorrectMoney.ToString();
                        this.txtCorrectScheme.Text = getHiddenHazard.CorrectScheme;
                        this.txtFindManName.Text = getHiddenHazard.FindManName;
                        this.txtOperateManNames.Text = getHiddenHazard.OperateManNames;
                        this.txtFindTime.Text= string.Format("{0:yyyy-MM-dd hh:mm:ss}", getHiddenHazard.FindTime); 
                        this.divBeImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../", getHiddenHazard.BePohotoUrl);
                        this.divAfImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../", getHiddenHazard.AfPohotoUrl);
                        this.divCorrectSchemeUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", getHiddenHazard.CorrectSchemeUrl);
                    }
                }
            }
        }
        #endregion        
    }
}
using System;

namespace FineUIPro.Web.ProjectData
{
    public partial class WorkAreaView : PageBase
    {
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
                string workAreaId = Request.Params["WorkAreaId"];
                if (!string.IsNullOrEmpty(workAreaId))
                {
                    Model.Base_WorkArea workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(workAreaId);
                    if (workArea != null)
                    {
                        this.txtWorkAreaCode.Text = workArea.WorkAreaCode;
                        this.txtWorkAreaName.Text = workArea.WorkAreaName;
                        var install = BLL.InstallationService.GetInstallationByInstallationId(workArea.InstallationId);
                        if (install != null)
                        {
                            this.drpInstallationId.Text = install.InstallationName;
                        }
                        this.txtRemark.Text = workArea.Remark;
                    }
                }
            }
        }
        #endregion
    }
}
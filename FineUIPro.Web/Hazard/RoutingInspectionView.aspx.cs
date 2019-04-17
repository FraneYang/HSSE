using System;
using System.IO;
using System.Web.UI;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class RoutingInspectionView : PageBase
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
                var getRoutingInspection = BLL.RoutingInspectionService.GetViewRoutingInspectionById(Request.Params["RoutingInspectionId"]);
                if (getRoutingInspection != null)
                {
                    this.txtPatrolTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRoutingInspection.PatrolTime);
                    this.txtPatrolManName.Text = getRoutingInspection.PatrolManName;
                    this.txtInstallationName.Text = getRoutingInspection.InstallationName;
                    this.txtTaskActivity.Text = getRoutingInspection.TaskActivity;
                    this.txtHazardDescription.Text = getRoutingInspection.HazardDescription;
                    this.txtOldRiskLevel.Text = getRoutingInspection.OldRiskLevel;
                    this.txtNowRiskLevel.Text = getRoutingInspection.NowRiskLevel;
                    this.txtPatrolResultName.Text = getRoutingInspection.PatrolResultName;
                }
            }
        }
        #endregion        
    }
}
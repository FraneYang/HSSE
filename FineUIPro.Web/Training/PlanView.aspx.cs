using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Training
{
    public partial class PlanView : PageBase
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
                string planId = Request.Params["PlanId"];
                var plan = BLL.PlanService.GetPlanById(planId);
                if (plan != null)
                {
                    this.txtPlanCode.Text = plan.PlanCode;
                    this.txtPlanName.Text = plan.PlanName;
                    this.txtDesignerName.Text =BLL.UserService.GetUserNameByUserId(plan.DesignerId);
                    this.txtDesignerDate.Text = string.Format("{0:yyyy-MM-dd}", plan.DesignerDate);
                    this.txtInstallationNames.Text = plan.InstallationNames;
                    var testPlanTraining = from x in Funs.DB.Training_PlanItem
                                           join y in Funs.DB.Training_TrainingEdu on x.TrainingEduId equals y.TrainingEduId
                                           where x.PlanId == planId
                                           orderby y.TrainingEduCode
                                           select y.TrainingEduName;
                    foreach (var item in testPlanTraining)
                    {
                        this.txtTrainingEdu.Text += item + "；";
                    }
                }
            }
        }
        #endregion
    }
}
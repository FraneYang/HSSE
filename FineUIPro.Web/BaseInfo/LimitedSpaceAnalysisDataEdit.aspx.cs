using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class LimitedSpaceAnalysisDataEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 动火分析点数据主键
        /// </summary>
        public string AnalysisDataId
        {
            get
            {
                return (string)ViewState["AnalysisDataId"];
            }
            set
            {
                ViewState["AnalysisDataId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 动火分析点数据编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///权限
                this.GetButtonPower();
                this.AnalysisDataId = Request.Params["AnalysisDataId"];          
                if (!string.IsNullOrEmpty(this.AnalysisDataId))
                {
                    var LimitedSpaceAnalysisData = BLL.LimitedSpaceAnalysisDataService.GetLimitedSpaceAnalysisDataById(this.AnalysisDataId);
                    if (LimitedSpaceAnalysisData != null)
                    {
                        this.txtSortIndex.Text = LimitedSpaceAnalysisData.SortIndex.ToString();
                        this.txtAnalysisPoint.Text = LimitedSpaceAnalysisData.AnalysisPoint;
                        this.txtMinData.Text = LimitedSpaceAnalysisData.MinData.ToString();
                        this.txtMaxData.Text = LimitedSpaceAnalysisData.MaxData.ToString();
                        this.txtMeasure.Text = LimitedSpaceAnalysisData.Measure;
                        if (LimitedSpaceAnalysisData.Category == "1")
                        {
                            this.txtCategory.Text = "有毒有害介质";
                        }
                        else if (LimitedSpaceAnalysisData.Category == "2")
                        {
                            this.txtCategory.Text = "可燃气";
                        }
                        else if (LimitedSpaceAnalysisData.Category == "3")
                        {
                            this.txtCategory.Text = "氧含量";
                        }
                    }
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
            if (BLL.LimitedSpaceAnalysisDataService.IsExistDataValue(this.AnalysisDataId, this.txtAnalysisPoint.Text.Trim()))
            {
                Alert.ShowInParent("动火分析点数据名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_LimitedSpaceAnalysisData newLimitedSpaceAnalysisData = new Model.Base_LimitedSpaceAnalysisData
            {
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text.Trim()),
                AnalysisPoint = this.txtAnalysisPoint.Text.Trim(),
                MinData = Funs.GetNewDecimalOrZero(this.txtMinData.Text),
                MaxData = Funs.GetNewDecimalOrZero(this.txtMaxData.Text),
                Measure = this.txtMeasure.Text.Trim(),
            };
            if (string.IsNullOrEmpty(this.AnalysisDataId))
            {
                newLimitedSpaceAnalysisData.AnalysisDataId = SQLHelper.GetNewID(typeof(Model.Base_LimitedSpaceAnalysisData));
                BLL.LimitedSpaceAnalysisDataService.AddLimitedSpaceAnalysisData(newLimitedSpaceAnalysisData);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加动火分析点数据");
            }
            else
            {
                newLimitedSpaceAnalysisData.AnalysisDataId = this.AnalysisDataId;
                BLL.LimitedSpaceAnalysisDataService.UpdateLimitedSpaceAnalysisData(newLimitedSpaceAnalysisData);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改动火分析点数据");
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.LimitedSpaceAnalysisDataMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证动火分析点数据编号、名称是否存在
        /// <summary>
        /// 验证动火分析点数据编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_LimitedSpaceAnalysisData.FirstOrDefault(x => x.AnalysisPoint == this.txtAnalysisPoint.Text.Trim() && (x.AnalysisDataId != this.AnalysisDataId || (this.AnalysisDataId == null && x.AnalysisDataId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }            

        }
        #endregion

    }
}
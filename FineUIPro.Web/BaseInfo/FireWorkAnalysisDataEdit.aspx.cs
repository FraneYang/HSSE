using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class FireWorkAnalysisDataEdit : PageBase
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
                    var FireWorkAnalysisData = BLL.FireWorkAnalysisDataService.GetFireWorkAnalysisDataById(this.AnalysisDataId);
                    if (FireWorkAnalysisData != null)
                    {
                        this.txtSortIndex.Text = FireWorkAnalysisData.SortIndex.ToString();
                        this.txtAnalysisPoint.Text = FireWorkAnalysisData.AnalysisPoint;
                        this.txtMinData.Text = FireWorkAnalysisData.MinData.ToString();
                        this.txtMaxData.Text = FireWorkAnalysisData.MaxData.ToString();
                        this.txtMeasure.Text = FireWorkAnalysisData.Measure;
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
            if (BLL.FireWorkAnalysisDataService.IsExistDataValue(this.AnalysisDataId, this.txtAnalysisPoint.Text.Trim()))
            {
                Alert.ShowInParent("动火分析点数据名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_FireWorkAnalysisData newFireWorkAnalysisData = new Model.Base_FireWorkAnalysisData
            {
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text.Trim()),
                AnalysisPoint = this.txtAnalysisPoint.Text.Trim(),
                MinData = Funs.GetNewDecimalOrZero(this.txtMinData.Text),
                MaxData = Funs.GetNewDecimalOrZero(this.txtMaxData.Text),
                Measure = this.txtMeasure.Text.Trim(),
            };
            if (string.IsNullOrEmpty(this.AnalysisDataId))
            {
                newFireWorkAnalysisData.AnalysisDataId = SQLHelper.GetNewID(typeof(Model.Base_FireWorkAnalysisData));
                BLL.FireWorkAnalysisDataService.AddFireWorkAnalysisData(newFireWorkAnalysisData);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加动火分析点数据");
            }
            else
            {
                newFireWorkAnalysisData.AnalysisDataId = this.AnalysisDataId;
                BLL.FireWorkAnalysisDataService.UpdateFireWorkAnalysisData(newFireWorkAnalysisData);
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.FireWorkAnalysisDataMenuId);
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
            var q = Funs.DB.Base_FireWorkAnalysisData.FirstOrDefault(x => x.AnalysisPoint == this.txtAnalysisPoint.Text.Trim() && (x.AnalysisDataId != this.AnalysisDataId || (this.AnalysisDataId == null && x.AnalysisDataId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            } 
        }
        #endregion

    }
}
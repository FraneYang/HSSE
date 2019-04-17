﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class WorkAreaEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string WorkAreaId
        {
            get
            {
                return (string)ViewState["WorkAreaId"];
            }
            set
            {
                ViewState["WorkAreaId"] = value;
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
                this.txtWorkAreaCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                //this.drpUnitId.DataTextField = "UnitName";
                //this.drpUnitId.DataValueField = "UnitId";
                //this.drpUnitId.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.CurrUser.LoginProjectId);
                //this.drpUnitId.DataBind();
                //Funs.FineUIPleaseSelect(this.drpUnitId);

                this.WorkAreaId = Request.Params["WorkAreaId"];
                if (!string.IsNullOrEmpty(this.WorkAreaId))
                {
                    Model.Base_WorkArea workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(this.WorkAreaId);
                    if (workArea != null)
                    {
                        this.txtWorkAreaCode.Text = workArea.WorkAreaCode;
                        this.txtWorkAreaName.Text = workArea.WorkAreaName;
                        //if (!string.IsNullOrEmpty(workArea.UnitId))
                        //{
                        //    this.drpUnitId.SelectedValue = workArea.UnitId;
                        //}
                        this.txtRemark.Text = workArea.Remark;
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
            Model.Base_WorkArea workArea = new Model.Base_WorkArea();
            workArea.WorkAreaCode = this.txtWorkAreaCode.Text.Trim();
            workArea.WorkAreaName = this.txtWorkAreaName.Text.Trim();
            //if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            //{
            //    workArea.UnitId = this.drpUnitId.SelectedValue;
            //}
            workArea.Remark = this.txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.WorkAreaId))
            {
                workArea.WorkAreaId = this.WorkAreaId;
                BLL.WorkAreaService.UpdateWorkArea(workArea);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改作业区域");
            }
            else
            {
                this.WorkAreaId = SQLHelper.GetNewID(typeof(Model.Base_WorkArea));
                workArea.WorkAreaId = this.WorkAreaId;
                BLL.WorkAreaService.AddWorkArea(workArea);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加作业区域");
            }
            ShowNotify("保存数据成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 验证作业区域名称、项目编号是否存在
        /// <summary>
        /// 验证作业区域名称、项目编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_WorkArea.FirstOrDefault(x => x.WorkAreaCode == this.txtWorkAreaCode.Text.Trim()  && (x.WorkAreaId != this.WorkAreaId || (this.WorkAreaId == null && x.WorkAreaId != null)));
            if (q != null)
            {
                ShowNotify("输入的区域编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_WorkArea.FirstOrDefault(x => x.WorkAreaName == this.txtWorkAreaName.Text.Trim()  && (x.WorkAreaId != this.WorkAreaId || (this.WorkAreaId == null && x.WorkAreaId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的区域名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
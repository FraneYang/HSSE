﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class CheckItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 上级检查项
        /// </summary>
        public string SupCheckItem
        {
            get
            {
                return (string)ViewState["SupCheckItem"];
            }
            set
            {
                ViewState["SupCheckItem"] = value;
            }
        }

        /// <summary>
        /// 检查项
        /// </summary>
        public string CheckItemSetId
        {
            get
            {
                return (string)ViewState["CheckItemSetId"];
            }
            set
            {
                ViewState["CheckItemSetId"] = value;
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
                this.CheckItemSetId = Request.Params["checkItemSetId"];
                this.SupCheckItem = Request.Params["supCheckItem"];

                if (!string.IsNullOrEmpty(this.CheckItemSetId))
                {
                    var checkItemSet = BLL.CheckItemSetService.GetCheckItemSetById(this.CheckItemSetId);
                    if (checkItemSet != null)
                    {
                        this.txtCheckItemName.Text = checkItemSet.CheckItemName;
                        if (checkItemSet.IsEndLever == true)
                        {
                            this.chkIsEndLevel.Checked = true;
                        }
                        else
                        {
                            chkIsEndLevel.Checked = false;
                        }
                        this.txtMapCode.Text = checkItemSet.MapCode;
                        this.txtSortIndex.Text = checkItemSet.SortIndex.ToString();
                    }
                    // 是末级存在明细 或者 不是末级存在下级 不修改是否末级菜单
                    this.chkIsEndLevel.Enabled = BLL.CheckItemSetService.IsDeleteCheckItemSet(this.CheckItemSetId);
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
            string staName = this.txtCheckItemName.Text.Trim();
            if (!string.IsNullOrEmpty(staName))
            {
                if (!BLL.CheckItemSetService.IsExistCheckItemName(this.CheckItemSetId, this.SupCheckItem, staName))
                {
                    Model.Technique_CheckItemSet checkItemSet = new Model.Technique_CheckItemSet
                    {
                        CheckItemName = staName,
                        SupCheckItem = this.SupCheckItem,
                        MapCode = this.txtMapCode.Text.Trim(),
                        SortIndex = Funs.GetNewIntOrZero(this.txtSortIndex.Text.Trim()),
                        IsEndLever = Convert.ToBoolean(this.chkIsEndLevel.Checked),
                        CheckType = Request.Params["checkType"]
                    };
                    if (string.IsNullOrEmpty(this.CheckItemSetId))
                    {
                        checkItemSet.CheckItemSetId = SQLHelper.GetNewID(typeof(Model.Technique_CheckItemSet));
                        BLL.CheckItemSetService.AddCheckItemSet(checkItemSet);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "增加检查项目设置信息");
                    }
                    else
                    {
                        checkItemSet.CheckItemSetId = this.CheckItemSetId;
                        BLL.CheckItemSetService.UpdateCheckItemSet(checkItemSet);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改检查项目设置信息");
                    }
                }
                else
                {
                    Alert.ShowInParent("检查项目名称已存在！");
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("检查项目名称不能为空！");
            }
        }
        #endregion
    }
}
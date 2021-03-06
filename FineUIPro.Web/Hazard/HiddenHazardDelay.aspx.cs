﻿namespace FineUIPro.Web.SysManage
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class HiddenHazardDelay : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT HiddenHazardDelay.DelayId,HiddenHazard.HiddenHazardCode,HiddenHazard.HiddenHazardName,Users.UserName AS ApplicantMan,HiddenHazardDelay.DelayReasons,HiddenHazardDelay.OldLimitTime,HiddenHazardDelay.LimitTime,CorrectMeasures,ControlMeasures"
                            + @" ,(CASE WHEN HiddenHazardDelay.States =1 THEN '审核中' WHEN  HiddenHazardDelay.States =2 THEN '已完成' WHEN  HiddenHazardDelay.States =-1 THEN '已作废' ELSE '待审核' END )  AS StatesName,CorrectMoney "
                            + @" FROM Hazard_HiddenHazardDelay AS HiddenHazardDelay"
                            + @" LEFT JOIN Hazard_HiddenHazard AS HiddenHazard ON HiddenHazardDelay.HiddenHazardId =HiddenHazard.HiddenHazardId"
                            + @" LEFT JOIN Sys_User AS Users ON HiddenHazardDelay.ApplicantManId =Users.UserId"                            
                            + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtHiddenHazardCode.Text.Trim()))
            {
                strSql += " AND HiddenHazardCode LIKE @HiddenHazardCode";
                listStr.Add(new SqlParameter("@HiddenHazardCode", "%" + this.txtHiddenHazardCode.Text.Trim() + "%"));
            }           
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        
        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.HiddenHazardService.DeleteHiddenHazardById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除隐患巡检");
                    //var HiddenHazards = BLL.HiddenHazardService.GetHiddenHazardById(rowID);
                    //if (HiddenHazards != null)
                    //{                        
                    //}
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {            
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
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
            //var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.HiddenHazardMenuId);
            //if (buttonList.Count() > 0)
            //{
            //    if (buttonList.Contains(BLL.Const.BtnDelete))
            //    {
            //        this.btnMenuDelete.Hidden = false;
            //    }
            //}
            if (this.CurrUser.UserId == BLL.Const.sysglyId)
            {
                this.btnMenuDelete.Hidden = false;
            }
        }
        #endregion

        /// <summary>
        /// 获取整改前图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrlByImage(object ImageUrl)
        {
            string url = string.Empty;
            if (ImageUrl != null)
            {
                url = BLL.UploadAttachmentService.ShowImage("../", ImageUrl.ToString());
            }
            return url;
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion 
    }
}
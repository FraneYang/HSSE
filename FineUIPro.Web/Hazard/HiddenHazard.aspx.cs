namespace FineUIPro.Web.Hazard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class HiddenHazard : PageBase
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
            string strSql = @"SELECT HiddenHazardId,HiddenHazardCode,HiddenHazardName,FindTime,Description,LimitTime, IsMajor,FindManUser.UserName AS FindManName,CorrectManUser.UserName AS CorrectManName,CorrectMeasures,HiddenHazardType.HiddenHazardTypeName,ReasonAnalysis"
                            + @" ,(CASE WHEN Installation.InstallationName IS NULL THEN HiddenHazardPlace ELSE Installation.InstallationName+':' + HiddenHazardPlace END) AS HiddenHazardPalce,BePohotoUrl,AfPohotoUrl,HiddenHazard.OperateManNames "
                            + @" ,(CASE WHEN States =1 THEN '审核中' WHEN  States =2 THEN '待整改' WHEN  States =3 THEN '待复查验收' WHEN  States =4 THEN '已完成' WHEN  States =-1 THEN '已作废' ELSE '待提交' END )  AS StatesName "
                            + @" FROM Hazard_HiddenHazard AS HiddenHazard "
                            + @" LEFT JOIN Base_Installation AS Installation ON HiddenHazard.InstallationId =Installation.InstallationId "
                            + @" LEFT JOIN Base_HiddenHazardType AS HiddenHazardType ON HiddenHazard.HiddenHazardTypeId=HiddenHazardType.HiddenHazardTypeId   "
                            + @" LEFT JOIN Sys_User AS FindManUser ON HiddenHazard.FindManId =FindManUser.UserId  "
                            + @" LEFT JOIN Sys_User AS CorrectManUser ON HiddenHazard.CorrectManId =CorrectManUser.UserId "                                                        
                            + @" WHERE  (isFiled IS NULL OR isFiled = 0)";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (HiddenHazardName LIKE @Name OR HiddenHazardCode LIKE @Name OR CorrectMeasures LIKE @Name OR HiddenHazardType.HiddenHazardTypeName LIKE @Name OR Description LIKE @Name OR ReasonAnalysis LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.HiddenHazardMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnFile.Hidden = false;
                }
            }
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

        #region 查看详细信息
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.ViewData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            this.ViewData();
        }

        private void ViewData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenHazardView.aspx?HiddenHazardId={0}", Id, "查看 - ")));
        }
        #endregion

        #region 归档         
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuFile_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string values = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    values += rowID + "|";
                }

                if (!string.IsNullOrEmpty(values) && values.Length <= 1850)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../InformationProject/FileCabinetAChange.aspx?values={0}&menuId={1}", values, BLL.Const.HiddenHazardMenuId , "查看 - "), "归档", 600, 540));
                }
                else
                {
                    Alert.ShowInTop("请一次至少一条，最多50条记录归档！", MessageBoxIcon.Warning);
                }

                BindGrid();
            }
        }
        #endregion
    }
}
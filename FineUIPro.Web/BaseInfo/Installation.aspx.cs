namespace FineUIPro.Web.BaseInfo
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class Installation : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("InstallationEdit.aspx") + "return false;";               
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
            string strSql = @"SELECT Installation.InstallationId,Installation.DepartId,Installation.InstallationCode,Installation.InstallationName,Installation.IsUsed,Installation.Def,Depart.DepartCode,Depart.DepartName,Installation.ManagerNames"
                          + @",(CASE WHEN InstallType='1' THEN '科室' WHEN InstallType='3' THEN '检修装置' ELSE '装置' END ) AS InstallTypeName"
                          + @" FROM  Base_Installation AS Installation"
                          + @" LEFT JOIN Base_Depart AS Depart ON Installation.DepartId =Depart.DepartId"                                                                          
                          + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();         
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (Installation.InstallationName LIKE @Name OR Installation.InstallationCode LIKE @Name OR Installation.Def LIKE @Name OR Depart.DepartName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }          
           
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();           
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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.InstallationMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
        
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
                string strShowNotify = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var installation = BLL.InstallationService.GetInstallationByInstallationId(rowID);
                    if (installation != null)
                    {
                        string delMess = judgementDelete(rowID);
                        if (string.IsNullOrEmpty(delMess))
                        {
                            BLL.InstallationService.DeleteInstallationById(rowID);
                            BLL.LogService.AddLog(this.CurrUser.UserId, "删除装置/科室");
                        }
                        else
                        {
                            strShowNotify += "装置：" + installation.InstallationName + delMess;
                        }
                    }
                }

                BindGrid();
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
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

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("InstallationEdit.aspx?InstallationId={0}", Id, "编辑 - ")));
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Hazard_RiskList.FirstOrDefault(x => x.InstallationId == id) != null)
            {
                content += "已在【风险信息库】中使用，不能删除！";
            }
            if (Funs.DB.Base_WorkArea.FirstOrDefault(x => x.InstallationId == id) != null)
            {
                content += "已在【区域信息】中使用，不能删除！";
            }
            if (Funs.DB.Sys_User.FirstOrDefault(x => x.InstallationId.Contains(id)) != null)
            {
                content += "已在【人员信息】中使用，不能删除！";
            }
            if (Funs.DB.Base_Euipment.FirstOrDefault(x => x.InstallationId == id) != null)
            {
                content += "已在【设备设施】中使用，不能删除！";
            }
            if (Funs.DB.Training_Plan.FirstOrDefault(x => x.InstallationIds.Contains(id)) != null)
            {
                content += "已在【培训计划】中使用，不能删除！";
            }           
            if (Funs.DB.Training_TestPlan.FirstOrDefault(x => x.InstallationIds.Contains(id)) != null)
            {
                content += "已在【考试计划】中使用，不能删除！";
            }
            if (Funs.DB.Training_TrainingEduItem.FirstOrDefault(x => x.InstallationIds.Contains(id)) != null)
            {
                content += "已在【考试试题库】中使用，不能删除！";
            }        
            if (Funs.DB.Training_TrainingItem.FirstOrDefault(x => x.InstallationIds.Contains(id)) != null)
            {
                content += "已在【培训教材库】中使用，不能删除！";
            }
            if (Funs.DB.Emergency_RescueInfo.FirstOrDefault(x => x.InstallationIds.Contains(id)) != null)
            {
                content += "已在【应急预警】中使用，不能删除！";
            }
            if (Funs.DB.Emergency_Warning.FirstOrDefault(x => x.InstallationIds.Contains(id)) != null)
            {
                content += "已在【应急信息】中使用，不能删除！";
            }

            return content;
        }
        #endregion
    }
}
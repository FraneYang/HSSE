namespace FineUIPro.Web.SysManage
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class UserList : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("UserListEdit.aspx") + "return false;";               
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.ConstValue.InitConstValueDropDownList(this.drpIsPost, ConstValue.Group_Y_N, true);               
                // 绑定表格
                this.BindGrid();
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnRefresh1.Hidden = false;
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Users.UserId,Users.Account,Users.UserCode,Users.Password,Users.UserName,Users.UnitId,Unit.UnitCode,Unit.UnitName,Users.DepartId,Depart.DepartName,Users.InstallationId,Users.InstallationName,Users.EntryTime"
                          + @" ,Roles.RoleName,Users.RoleId,Users.IsPost,CASE WHEN Users.IsPost=1 THEN '是' ELSE '否' END AS IsPostName,Users.IdentityCard,Users.Telephone,Users.WorkPostName,Users.WorkPostId,SortIndex,IsEmergency,IsTemp "
                          + @" From dbo.Sys_User AS Users"
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=Users.UnitId"
                          + @" LEFT JOIN Base_Depart AS Depart ON Depart.DepartId=Users.DepartId"
                          //+ @" LEFT JOIN Base_Installation AS Installation ON Installation.InstallationId=Users.InstallationId"
                          //+ @" LEFT JOIN Base_WorkPost AS WorkPost ON WorkPost.WorkPostId=Users.WorkPostId"  
                          + @" LEFT JOIN Sys_Role AS Roles ON Roles.RoleId=Users.RoleId"                                                   
                          + @" WHERE Users.UserId !=@UserId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@UserId", Const.sysglyId));

            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null)
            {
                if (this.rbUsrType.SelectedValue == "0")
                {
                    strSql += " AND Users.UnitId = '" + unit.UnitId + "'";
                }
                else
                {
                    strSql += " AND (Users.UnitId != '" + unit.UnitId + "' OR Users.UnitId IS NULL)";
                }
            }
            
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (Users.Account LIKE @Name OR Users.UserCode LIKE @Name OR Users.UserName LIKE @Name OR Unit.UnitName LIKE @Name OR Depart.DepartName LIKE @Name OR Users.InstallationName LIKE @Name OR Users.WorkPostName LIKE @Name OR Roles.RoleName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是本单位或者管理员
            {
                strSql += " AND Users.UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }

            if (this.drpIsPost.SelectedValue != BLL.Const._Null)
            {
                if (this.drpIsPost.SelectedValue == "True")
                {
                    strSql += " AND Users.IsPost = 1";
                }
                else
                {
                    strSql += " AND (Users.IsPost = 0 OR Users.IsPost IS NULL)";
                }
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();           
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbUsrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.UserMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnImport.Hidden = false;
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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (judgementDelete(rowID, false))
                    {
                        BLL.UserService.DeleteUser(rowID);
                        BLL.LogService.AddLog( this.CurrUser.UserId, "删除用户信息");
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!");
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("UserListEdit.aspx?userId={0}", Id, "编辑 - ")));
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            if (Funs.DB.Hazard_JHAItemRecord.FirstOrDefault(x => x.EvaluatorId == id) != null)
            {
                content = "该用户已在【JHA评估】中使用，不能删除！";
            }
            if (Funs.DB.Hazard_SCLItemRecord.FirstOrDefault(x => x.EvaluatorId == id) != null)
            {
                content = "该用户已在【SCL评估】中使用，不能删除！";
            }
            if (Funs.DB.Hazard_LECItemRecord.FirstOrDefault(x => x.EvaluatorId == id) != null)
            {
                content = "该用户已在【LEC评估】中使用，不能删除！";
            }
            if (Funs.DB.Sys_PushRecord.FirstOrDefault(x => x.ReceiveManId == id && x.IsResponse== true && !x.IsAgree.HasValue) != null)
            {
                content = "该用户存在待办事项，不能删除！";
            }
            if (Funs.DB.Hazard_HiddenHazard.FirstOrDefault(x => x.FindManId == id) != null)
            {
                content = "该用户已在【隐患排查】中使用，不能删除！";
            }
            if (Funs.DB.License_Overhaul.FirstOrDefault(x => x.CompileManId == id) != null)
            {
                content = "该用户已在【检修作业票】中使用，不能删除！";
            }
            if (Funs.DB.License_FireWork.FirstOrDefault(x => x.ApplyManId == id) != null)
            {
                content = "该用户已在【动火业票】中使用，不能删除！";
            }

            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {         
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("UserIn.aspx", "导入 - ")));
        }
        #endregion

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        #region 导出按钮
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("用户信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion


        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Refresh1_Click(object sender, EventArgs e)
        {
            var userItems = from x in Funs.DB.Sys_User select x;
            if (userItems.Count() > 0)
            {
                foreach (var userItem in userItems)
                {
                    if (!string.IsNullOrEmpty(userItem.InstallationName))
                    {
                        string getInstallationId = string.Empty;
                        var installList = Funs.GetStrListByStr(userItem.InstallationName, ',');
                        if (installList.Count() > 0)
                        {
                            foreach (var installItem in installList)
                            {
                                var install = Funs.DB.Base_Installation.FirstOrDefault(x => x.InstallationName == installItem);
                                if (install != null && !string.IsNullOrEmpty(install.InstallationId))
                                {
                                    userItem.DepartId = install.DepartId;
                                    getInstallationId += install.InstallationId + ",";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(getInstallationId))
                        {
                            userItem.InstallationId = getInstallationId.Substring(0, getInstallationId.LastIndexOf(","));
                        }

                        string getWorkPostId = string.Empty;
                        var workPostList = Funs.GetStrListByStr(userItem.WorkPostName, ',');
                        if (workPostList.Count() > 0)
                        {
                            foreach (var workPostItem in workPostList)
                            {
                                var workPost = Funs.DB.Base_WorkPost.FirstOrDefault(x => x.WorkPostName == workPostItem);
                                if (workPost != null && !string.IsNullOrEmpty(workPost.WorkPostId))
                                {
                                    getWorkPostId += workPost.WorkPostId + ",";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(getWorkPostId))
                        {
                            userItem.WorkPostId = getWorkPostId.Substring(0, getWorkPostId.LastIndexOf(","));
                        }
                    }

                    BLL.UserService.UpdateUser(userItem);
                }
            }

            Alert.ShowInTop("操作完成！", MessageBoxIcon.Success);
        }
    }
}
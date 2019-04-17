namespace FineUIPro.Web.Lock
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class SmartLockView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string SmartLockId
        {
            get
            {
                return (string)ViewState["SmartLockId"];
            }
            set
            {
                ViewState["SmartLockId"] = value;
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
                this.SmartLockId = Request.Params["SmartLockId"];
                this.GetButtonPower();
                this.BindGrid();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Item.SmartLockItemId,Item.SmartLockId,Item.Place,Item.DeviceName,Item.StartTime,Item.EndTime,Item.ApplicantManId,Item.ApplicantTime,Item.AuditManId,Item.AuditTime,Item.States"
                          + @",ApplicantMan.UserName AS ApplicantManName,AuditMan.UserName AS AuditManName,(CASE WHEN Item.States='1' THEN '已审核' WHEN Item.States='-1' THEN '已作废' ELSE '待审核' END) AS StatesName "
                          + @" FROM dbo.Lock_SmartLockItem AS Item"
                          + @" LEFT JOIN Sys_User AS ApplicantMan  ON Item.ApplicantManId=ApplicantMan.UserId"
                          + @" LEFT JOIN Sys_User AS AuditMan  ON Item.AuditManId=AuditMan.UserId WHERE Item.SmartLockId=@SmartLockId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@SmartLockId", this.SmartLockId));
            //if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            //{
            //    strSql += " AND (Plans.PlanName LIKE @name OR Users.UserName LIKE @name)";
            //    listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
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
                    BLL.SmartLockItemService.DeleteSmartLockItemById(rowID); 
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除智能锁记录信息！");
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.SmartLockMenuId);
            if (buttonList.Count() > 0)
            {                
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion       
    }
}
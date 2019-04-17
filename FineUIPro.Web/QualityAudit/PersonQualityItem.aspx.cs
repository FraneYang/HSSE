using System;
using System.Collections.Generic;
using BLL;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.QualityAudit
{
    public partial class PersonQualityItem : PageBase
    {
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
                this.BindGrid();
            }
        }
        #endregion

        #region BindGrid
        /// <summary>
        ///  gv 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT PersonQualityItemId,PersonQualityId,CheckDate FROM QualityAudit_PersonQualityItem"
                         + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND PersonQualityId=@PersonQualityId";
            listStr.Add(new SqlParameter("@PersonQualityId", Request.Params["PersonQualityId"]));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

        }
        #endregion
    }
}
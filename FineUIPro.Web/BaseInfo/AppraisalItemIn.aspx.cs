namespace FineUIPro.Web.BaseInfo
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;

    public partial class AppraisalItemIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 导入集合
        /// </summary>
        public static List<Model.Base_AppraisalItem> viewAppraisalItems = new List<Model.Base_AppraisalItem>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static string errorInfos = string.Empty;
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
                this.hdFileName.Text = string.Empty;
                this.hdCheckResult.Text = string.Empty;
                if (viewAppraisalItems != null)
                {
                    viewAppraisalItems.Clear();
                }
                errorInfos = string.Empty;
            }
        }
        #endregion

        #region 数据导入
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fuAttachUrl.HasFile == false)
                {
                    ShowNotify("请您选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                string IsXls = Path.GetExtension(this.fuAttachUrl.FileName).ToString().Trim().ToLower();
                if (IsXls != ".xls")
                {
                    ShowNotify("只可以选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                if (viewAppraisalItems != null)
                {
                    viewAppraisalItems.Clear();
                }
                if (!string.IsNullOrEmpty(errorInfos))
                {
                    errorInfos = string.Empty;
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                if (!Directory.Exists(initFullPath))
                {
                    Directory.CreateDirectory(initFullPath);
                }

                this.hdFileName.Text = BLL.Funs.GetNewFileName() + IsXls;
                string filePath = initFullPath + this.hdFileName.Text;
                this.fuAttachUrl.PostedFile.SaveAs(filePath);                
                ImportXlsToData(rootPath + initPath + this.hdFileName.Text);
            }
            catch (Exception ex)
            {
                ShowNotify("'" + ex.Message + "'", MessageBoxIcon.Warning);
            }
        }

        #region 读Excel提取数据
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData(string fileName)
        {
            try
            {
                viewAppraisalItems.Clear();
                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                DataTable m_tableName = new DataTable();
                DataSet ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();
                m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (m_tableName != null && m_tableName.Rows.Count > 0)
                {
                    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();
                }
                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();

                AddDatasetToSQL(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将Dataset的数据导入数据库
        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集行数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL(DataTable pds)
        {
            string result = string.Empty;
            int ic, ir;
            ic = pds.Columns.Count;
            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {                         
                for (int i = 0; i < ir; i++)
                {
                    ////试题类型
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    if (string.IsNullOrEmpty(col1))
                    {
                        if (!string.IsNullOrEmpty(pds.Rows[i][0].ToString().Trim()) || !string.IsNullOrEmpty(pds.Rows[i][2].ToString().Trim()))
                        {
                            result += "第" + (i + 2).ToString() + "行," + "导入考核内容" + "," + "此项为必填项！" + "|";
                        }
                    }
                    else
                    {
                        Model.Base_AppraisalItem newViewTrainingItem = new Model.Base_AppraisalItem
                        {
                            AppraisalItemId = SQLHelper.GetNewID(typeof(Model.Base_AppraisalItem)),
                            Code = pds.Rows[i][0].ToString().Trim(),
                            CheckItem = pds.Rows[i][1].ToString().Trim(),
                            Score = Funs.GetNewDecimal(pds.Rows[i][2].ToString().Trim()),
                            Remark = pds.Rows[i][3].ToString().Trim(),
                        };

                        var addItem = Funs.DB.Base_AppraisalItem.FirstOrDefault(x => x.CheckItem == newViewTrainingItem.CheckItem);
                        if (addItem == null)
                        {
                            BLL.AppraisalItemService.AddAppraisalItem(newViewTrainingItem);
                            ///加入
                            viewAppraisalItems.Add(newViewTrainingItem);
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "导入考核项重复" + "|";
                        }
                    }
                }
                if (viewAppraisalItems.Count > 0)
                {
                    viewAppraisalItems = viewAppraisalItems.Distinct().ToList();
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = viewAppraisalItems;
                    this.Grid1.DataBind();
                }

                if (!string.IsNullOrEmpty(result))
                {
                    viewAppraisalItems.Clear();
                    result = "数据导入完成，未成功数据：" + result.Substring(0, result.LastIndexOf("|"));
                    errorInfos = result;
                    Alert alert = new Alert
                    {
                        Message = result,
                        Target = Target.Self
                    };
                    alert.Show();
                }
                else
                {
                    errorInfos = string.Empty;
                    ShowNotify("导入成功！", MessageBoxIcon.Success);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
            else
            {
                Alert.ShowInTop("导入数据为空！", MessageBoxIcon.Warning);
            }
            return true;
        }
        #endregion
        #endregion       
        
        #region 关闭弹出窗口
        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["appraisalItem"] != null)
            {
                viewAppraisalItems = Session["appraisalItem"] as List<Model.Base_AppraisalItem>;
            }
            if (viewAppraisalItems.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = viewAppraisalItems;
                this.Grid1.DataBind();
            }
        }
        
        #endregion

        #region 下载模板
        /// <summary>
        /// 下载模板按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("确定下载导入模板吗？", String.Empty, MessageBoxIcon.Question, PageManager1.GetCustomEventReference(false, "Confirm_OK"), PageManager1.GetCustomEventReference("Confirm_Cancel")));
        }

        /// <summary>
        /// 下载导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")
            {
                string rootPath = Server.MapPath("~/");
                string filePath = Const.AppraisalItemTemplateUrl;
                string uploadfilepath = rootPath + filePath;               
                string fileName = Path.GetFileName(filePath);
                FileInfo info = new FileInfo(uploadfilepath);
                long fileSize = info.Length;
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.ContentType = "excel/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Length", fileSize.ToString().Trim());
                Response.TransmitFile(uploadfilepath, 0, fileSize);
                Response.End();
            }
        }
        #endregion
    }
}
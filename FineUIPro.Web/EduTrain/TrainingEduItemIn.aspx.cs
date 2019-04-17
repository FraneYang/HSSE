namespace FineUIPro.Web.EduTrain
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;

    public partial class TrainingEduItemIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 导入集合
        /// </summary>
        public static List<Model.View_Training_TrainingEduItem> viewTrainingEduItems = new List<Model.View_Training_TrainingEduItem>();

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
                if (viewTrainingEduItems != null)
                {
                    viewTrainingEduItems.Clear();
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
                if (viewTrainingEduItems != null)
                {
                    viewTrainingEduItems.Clear();
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
                viewTrainingEduItems.Clear();
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
                ///教材类型
                var trainingEdus = from x in Funs.DB.Training_TrainingEdu select x;
                ///岗位（装置/科室）
                var Installations = from x in Funs.DB.Base_Installation select x;             
                for (int i = 0; i < ir; i++)
                {
                    Model.View_Training_TrainingEduItem newViewTrainingEduItem = new Model.View_Training_TrainingEduItem
                    {
                        TrainingEduCode = pds.Rows[i][0].ToString().Trim(),
                        TrainingEduItemCode = pds.Rows[i][2].ToString().Trim(),
                        TrainingEduItemName = pds.Rows[i][3].ToString().Trim(),
                        InstallationNames = pds.Rows[i][4].ToString().Trim(),
                        Summary = pds.Rows[i][5].ToString().Trim(),                                                              
                    };

                    ////教材类型
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    if (string.IsNullOrEmpty(col1))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入教材类型" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var standard = trainingEdus.FirstOrDefault(x => x.TrainingEduName == col1);
                        if (standard != null)
                        {
                            newViewTrainingEduItem.TrainingEduId = standard.TrainingEduId;
                            newViewTrainingEduItem.TrainingEduCode = standard.TrainingEduCode;
                            newViewTrainingEduItem.TrainingEduName = standard.TrainingEduName;
                        }
                        else
                        {
                            Model.Training_TrainingEdu newTraining = new Model.Training_TrainingEdu();
                            newViewTrainingEduItem.TrainingEduId = newTraining.TrainingEduId = SQLHelper.GetNewID(typeof(Model.Training_TrainingEdu));
                            newViewTrainingEduItem.TrainingEduName = newTraining.TrainingEduName = col1;
                            newTraining.TrainingEduCode = newViewTrainingEduItem.TrainingEduCode;
                            BLL.TrainingEduService.AddTrainingEdu(newTraining);
                        }
                    }
                    ////适合岗位
                    string col4 = pds.Rows[i][4].ToString().Trim();
                    if (!string.IsNullOrEmpty(col4))
                    {

                        List<string> InstallationSels = Funs.GetStrListByStr(col4, ',');
                        if (InstallationSels.Count() > 0)
                        {
                            string ids = string.Empty;
                            foreach (var item in InstallationSels)
                            {
                                var wp = Installations.FirstOrDefault(x => x.InstallationName == item);
                                if (wp != null)
                                {
                                    ids += wp.InstallationId + ",";
                                }
                                else
                                {
                                    result += "第" + (i + 2).ToString() + "行," + "导入适合岗位(装置/科室)" + item + "," + "此项基础表不存在！" + "|";
                                }
                            }
                            if (!string.IsNullOrEmpty(ids))
                            {
                                newViewTrainingEduItem.InstallationNames = col4;
                                newViewTrainingEduItem.InstallationIds = ids.Substring(0, ids.LastIndexOf(","));
                            }
                        }
                    }
                    
                    ////教材内容
                    if (string.IsNullOrEmpty(newViewTrainingEduItem.TrainingEduItemName))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入教材名称" + "," + "为必填项！" + "|";
                    }
                    else
                    {
                        var addTrainingEduItem = Funs.DB.Training_TrainingEduItem.FirstOrDefault(x => x.TrainingEduItemName == newViewTrainingEduItem.TrainingEduItemName && x.TrainingEduId == newViewTrainingEduItem.TrainingEduId);
                        if (addTrainingEduItem == null)
                        {
                            Model.Training_TrainingEduItem newTrainingEduItem = new Model.Training_TrainingEduItem
                            {
                                TrainingEduItemId = newViewTrainingEduItem.TrainingEduItemId = SQLHelper.GetNewID(typeof(Model.Standard_HSSEStandard)),
                                TrainingEduId = newViewTrainingEduItem.TrainingEduId,
                                TrainingEduItemCode = newViewTrainingEduItem.TrainingEduItemCode,
                                TrainingEduItemName = newViewTrainingEduItem.TrainingEduItemName,
                                Summary = newViewTrainingEduItem.Summary,
                                AttachUrl = newViewTrainingEduItem.AttachUrl,
                                PictureUrl = newViewTrainingEduItem.PictureUrl,
                                InstallationIds = newViewTrainingEduItem.InstallationIds,
                                InstallationNames = newViewTrainingEduItem.InstallationNames,
                            };
                            BLL.TrainingEduItemService.AddTrainingEduItem(newTrainingEduItem);
                            ///加入培训教材库
                            viewTrainingEduItems.Add(newViewTrainingEduItem);
                        }
                    }
                }
                if (viewTrainingEduItems.Count > 0)
                {
                    viewTrainingEduItems = viewTrainingEduItems.Distinct().ToList();
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = viewTrainingEduItems;
                    this.Grid1.DataBind();
                }

                if (!string.IsNullOrEmpty(result))
                {
                    viewTrainingEduItems.Clear();
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
            if (Session["TrainingEduItem"] != null)
            {
                viewTrainingEduItems = Session["TrainingEduItem"] as List<Model.View_Training_TrainingEduItem>;
            }
            if (viewTrainingEduItems.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = viewTrainingEduItems;
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
                string filePath = Const.TrainingEduTemplateUrl;
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
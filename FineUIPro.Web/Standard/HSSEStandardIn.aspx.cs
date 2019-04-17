namespace FineUIPro.Web.Standard
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;

    public partial class HSSEStandardIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 导入集合
        /// </summary>
        public static List<Model.View_Standard_HSSEStandard> viewHSSEStandards = new List<Model.View_Standard_HSSEStandard>();

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
                BLL.SpecialtyService.InitSpecialtyRadioButtonList(this.ckSpecialty);
                if (viewHSSEStandards != null)
                {
                    viewHSSEStandards.Clear();
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
                if (viewHSSEStandards != null)
                {
                    viewHSSEStandards.Clear();
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
                viewHSSEStandards.Clear();
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
                ///标准
                var standards = from x in Funs.DB.Base_Standard select x;
                ///管理对象
                var managedObjects = from x in Funs.DB.Standard_ManagedObject select x;
                ///管理项目
                var managedItems = from x in Funs.DB.Standard_ManagedItem select x;
                for (int i = 0; i < ir; i++)
                {
                    Model.View_Standard_HSSEStandard newViewHSSEStandard = new Model.View_Standard_HSSEStandard
                    {
                        SpecialtyId = this.ckSpecialty.SelectedValue,
                        StandardCode = pds.Rows[i][0].ToString().Trim(),
                        ManagedObjectCode = pds.Rows[i][2].ToString().Trim(),
                        ManagedItemCode = pds.Rows[i][4].ToString().Trim(),
                        HSSEStandardCode = pds.Rows[i][6].ToString().Trim()
                    };
                    ////标准
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    if (string.IsNullOrEmpty(col1))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入标准名称" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var standard = standards.FirstOrDefault(x => x.StandardName == col1 && x.SpecialtyId == newViewHSSEStandard.SpecialtyId);
                        if (standard != null)
                        {
                            newViewHSSEStandard.StandardId = standard.StandardId;
                            newViewHSSEStandard.StandardCode = standard.StandardCode;
                            newViewHSSEStandard.StandardName = standard.StandardName;
                        }
                        else
                        {
                            Model.Base_Standard newStandard = new Model.Base_Standard();
                            newViewHSSEStandard.StandardId = newStandard.StandardId = SQLHelper.GetNewID(typeof(Model.Base_Standard));
                            newViewHSSEStandard.StandardName = newStandard.StandardName = col1;
                            newStandard.StandardCode = newViewHSSEStandard.StandardCode;
                            newStandard.SpecialtyId = newViewHSSEStandard.SpecialtyId;
                            newStandard.Remark = "系统导入";
                            BLL.StandardService.AddStandard(newStandard);
                        }
                    }
                    ///管理对象
                    string col3 = pds.Rows[i][3].ToString().Trim();
                    if (string.IsNullOrEmpty(col3))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入管理对象" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var managedObject = managedObjects.FirstOrDefault(x => x.ManagedObjectName == col3 && x.StandardId == newViewHSSEStandard.StandardId);
                        if (managedObject != null)
                        {
                            newViewHSSEStandard.ManagedObjectId = managedObject.ManagedObjectId;
                            newViewHSSEStandard.ManagedObjectCode = managedObject.ManagedObjectCode;
                            newViewHSSEStandard.ManagedObjectName = managedObject.ManagedObjectName;
                        }
                        else
                        {
                            Model.Standard_ManagedObject newManagedObject = new Model.Standard_ManagedObject();
                            newViewHSSEStandard.ManagedObjectId = newManagedObject.ManagedObjectId = SQLHelper.GetNewID(typeof(Model.Standard_ManagedObject));
                            newViewHSSEStandard.ManagedObjectName = newManagedObject.ManagedObjectName = col3;
                            newManagedObject.ManagedObjectCode = newViewHSSEStandard.ManagedObjectCode;
                            newManagedObject.StandardId = newViewHSSEStandard.StandardId;
                            newManagedObject.Remark = "系统导入";
                            BLL.ManagedObjectService.AddManagedObject(newManagedObject);
                        }
                    }
                    ////管理项目
                    string col5 = pds.Rows[i][5].ToString().Trim();
                    if (string.IsNullOrEmpty(col5))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入管理对象" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var managedItem = managedItems.FirstOrDefault(x => x.ManagedItemName == col5 && x.ManagedObjectId == newViewHSSEStandard.ManagedObjectId);
                        if (managedItem != null)
                        {
                            newViewHSSEStandard.ManagedItemId = managedItem.ManagedItemId;
                            newViewHSSEStandard.ManagedItemCode = managedItem.ManagedItemCode;
                            newViewHSSEStandard.ManagedItemName = managedItem.ManagedItemName;
                        }
                        else
                        {
                            Model.Standard_ManagedItem newManagedItem = new Model.Standard_ManagedItem();
                            newViewHSSEStandard.ManagedItemId = newManagedItem.ManagedItemId = SQLHelper.GetNewID(typeof(Model.Standard_ManagedItem));
                            newViewHSSEStandard.ManagedItemName = newManagedItem.ManagedItemName = col5;
                            newManagedItem.ManagedItemCode = newViewHSSEStandard.ManagedItemCode;
                            newManagedItem.ManagedObjectId = newViewHSSEStandard.ManagedObjectId;
                            newManagedItem.Remark = "系统导入";
                            BLL.ManagedItemService.AddManagedItem(newManagedItem);
                        }
                    }

                    ////具体要求
                    string col7 = pds.Rows[i][7].ToString().Trim();
                    if (string.IsNullOrEmpty(col7))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "导入具体要求" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        var hsseStandard = Funs.DB.Standard_HSSEStandard.FirstOrDefault(x => x.ManagedItemId == newViewHSSEStandard.ManagedItemId && x.HSSEStandardName == col7);
                        if (hsseStandard == null)
                        {
                            Model.Standard_HSSEStandard newHSSEStandard = new Model.Standard_HSSEStandard
                            {
                                HSSEStandardId = newViewHSSEStandard.HSSEStandardId = SQLHelper.GetNewID(typeof(Model.Standard_HSSEStandard)),
                                HSSEStandardName = col7,
                                HSSEStandardCode = newViewHSSEStandard.HSSEStandardCode,
                                ManagedItemId = newViewHSSEStandard.ManagedItemId
                            };
                            BLL.HSSEStandardService.AddHSSEStandard(newHSSEStandard);
                            ///加入专家辅助试图
                            viewHSSEStandards.Add(newViewHSSEStandard);
                        }
                    }
                }
                if (viewHSSEStandards.Count > 0)
                {
                    viewHSSEStandards = viewHSSEStandards.Distinct().ToList();
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = viewHSSEStandards;
                    this.Grid1.DataBind();
                }

                if (!string.IsNullOrEmpty(result))
                {
                    viewHSSEStandards.Clear();
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
            if (Session["hsseStandard"] != null)
            {
                viewHSSEStandards = Session["hsseStandard"] as List<Model.View_Standard_HSSEStandard>;
            }
            if (viewHSSEStandards.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = viewHSSEStandards;
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
                string filePath = Const.HSSEStandardTemplateUrl;
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
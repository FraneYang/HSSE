namespace FineUIPro.Web.QualityAudit
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;

    public partial class PersonQualityIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 导入集合
        /// </summary>
        public static List<Model.View_QualityAudit_PersonQuality> viewPersonQualitys = new List<Model.View_QualityAudit_PersonQuality>();

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
                if (viewPersonQualitys != null)
                {
                    viewPersonQualitys.Clear();
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
                if (viewPersonQualitys != null)
                {
                    viewPersonQualitys.Clear();
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
                viewPersonQualitys.Clear();
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
            string results = string.Empty;
            int ic, ir;
            ic = pds.Columns.Count;
            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var units = from x in Funs.DB.Base_Unit select x;
                var users = from x in Funs.DB.Sys_User select x;
                var certificates = from x in Funs.DB.Base_Certificate select x;
                var prospectives = from x in Funs.DB.Base_Prospective select x;
                for (int i = 0; i < ir; i++)
                {
                    string col0 = pds.Rows[i][0].ToString().Trim();
                    string col1 = pds.Rows[i][1].ToString().Trim();
                    string col2 = pds.Rows[i][2].ToString().Trim();
                    string col3 = pds.Rows[i][3].ToString().Trim();
                    string col4 = pds.Rows[i][4].ToString().Trim();
                    string col5 = pds.Rows[i][5].ToString().Trim();
                    string col6 = pds.Rows[i][6].ToString().Trim();
                    string col7 = pds.Rows[i][7].ToString().Trim();
                    string col8 = pds.Rows[i][8].ToString().Trim();
                    string col9 = pds.Rows[i][9].ToString().Trim();
                    string col10 = pds.Rows[i][10].ToString().Trim();
                    string col11 = pds.Rows[i][11].ToString().Trim();
                    string col12 = pds.Rows[i][12].ToString().Trim();
                    string col13 = pds.Rows[i][13].ToString().Trim();
                    string result = string.Empty;
                    if (string.IsNullOrEmpty(col1))
                    {
                        results += "第" + (i + 2).ToString() + "行," + "导入姓名" + "," + "此项为必填项！" + "|";
                    }
                    else
                    {
                        Model.View_QualityAudit_PersonQuality newPersonQualityView = new Model.View_QualityAudit_PersonQuality
                        {
                            UnitName = col0,
                            UserName = col1,
                            CertificateNo = col2,
                            CertificateName = col3,
                            ProspectiveNames = col4,
                            SendUnitName = col5,
                            CompileManName = col11,
                            Remark = col12
                        };

                        var unit0 = units.FirstOrDefault(x => x.UnitName == newPersonQualityView.UnitName);
                        if (unit0 != null)
                        {
                            newPersonQualityView.UnitId = unit0.UnitId;
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "导入所属单位" + "," + "此项为必填且在单位表中存在！" + "|";
                        }

                        var person = users.FirstOrDefault(x => x.UserName == newPersonQualityView.UserName && x.UnitId == newPersonQualityView.UnitId);
                        if (person != null && !string.IsNullOrEmpty(person.UserId))
                        {
                            newPersonQualityView.PersonId = person.UserId;
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "导入人员" + "," + "此项为必填且在用户表中存在！" + "|";
                        }

                        var certificate = certificates.FirstOrDefault(x => x.CertificateName == newPersonQualityView.CertificateName);
                        if (certificate != null)
                        {
                            newPersonQualityView.CertificateId = certificate.CertificateId;
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "导入证书" + "," + "此项为必填且在证书表中存在！" + "|";
                        }
                        var sendUnit = units.FirstOrDefault(x => x.UnitName == newPersonQualityView.SendUnitName);
                        if (sendUnit != null)
                        {
                            newPersonQualityView.SendUnit = sendUnit.UnitId;
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "导入发证单位" + "," + "在单位表中不存在！" + "|";
                        }
                        var compileMan = users.FirstOrDefault(x => x.UserName == newPersonQualityView.CompileManName);
                        if (compileMan != null)
                        {
                            newPersonQualityView.CompileMan = compileMan.UserId;
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "导入编制人" + "," + "在用户表中不存在！" + "|";
                        }

                        ////准操项目                        
                        if (!string.IsNullOrEmpty(col4))
                        {
                            List<string> ProspectiveNames = Funs.GetStrListByStr(col4, ',');
                            if (ProspectiveNames.Count() > 0)
                            {
                                string ids = string.Empty;
                                foreach (var item in ProspectiveNames)
                                {
                                    if (!string.IsNullOrEmpty(item))
                                    {
                                        var wp = prospectives.FirstOrDefault(x => x.ProspectiveName == item.Trim());
                                        if (wp != null)
                                        {
                                            ids += wp.ProspectiveId + ",";
                                        }
                                        else
                                        {
                                            result += "第" + (i + 2).ToString() + "行," + "导入准操项目" + item + "," + "此项基础表不存在！" + "|";
                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(ids))
                                {
                                    newPersonQualityView.ProspectiveIds = ids.Substring(0, ids.LastIndexOf(","));
                                }
                            }
                        }
                        if (Funs.GetNewDateTime(col6).HasValue)
                        {
                            newPersonQualityView.SendDate = Funs.GetNewDateTime(col6);
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "发证时间不能为空且必须为日期格式！" + "|";
                        }
                        if (Funs.GetNewDateTime(col7).HasValue)
                        {
                            newPersonQualityView.LimitDate = Funs.GetNewDateTime(col7);
                        }
                        else
                        {
                            result += "第" + (i + 2).ToString() + "行," + "有效期不能为空且必须为日期格式！" + "|";
                        }
                        if (!string.IsNullOrEmpty(col8) && !Funs.GetNewDateTime(col8).HasValue)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "复查日期必须为日期格式！" + "|";
                        }
                        else
                        {
                            newPersonQualityView.LateCheckDate = Funs.GetNewDateTime(col8);
                        }
                        if (!string.IsNullOrEmpty(col9) && !Funs.GetNewDateTime(col9).HasValue)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "审核时间必须为日期格式！" + "|";
                        }
                        else
                        {
                            newPersonQualityView.AuditDate = Funs.GetNewDateTime(col9);
                        }

                        if (!string.IsNullOrEmpty(col10) && !Funs.GetNewDateTime(col10).HasValue)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "编制时间必须为日期格式！" + "|";
                        }
                        else
                        {
                            newPersonQualityView.CompileDate = Funs.GetNewDateTime(col10);
                        }

                        if (string.IsNullOrEmpty(result))
                        {
                            Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
                            {
                                PersonId = newPersonQualityView.PersonId,
                                CertificateId = newPersonQualityView.CertificateId,
                                CertificateNo = newPersonQualityView.CertificateNo,
                                SendUnit = newPersonQualityView.SendUnit,
                                SendDate = newPersonQualityView.SendDate,
                                LimitDate = newPersonQualityView.LimitDate,
                                LateCheckDate = newPersonQualityView.LateCheckDate,
                                AuditDate = newPersonQualityView.AuditDate,
                                Remark = newPersonQualityView.Remark,
                                CompileMan = newPersonQualityView.CompileMan,
                                CompileDate = newPersonQualityView.CompileDate,
                                ProspectiveIds = newPersonQualityView.ProspectiveIds,
                                ProspectiveNames = newPersonQualityView.ProspectiveNames,
                            };

                            var addItem = Funs.DB.QualityAudit_PersonQuality.FirstOrDefault(x => x.PersonId == newPersonQualityView.PersonId && x.CertificateNo == newPersonQualityView.CertificateNo);
                            if (addItem == null)
                            {
                                newPersonQualityView.PersonQualityId = newPersonQuality.PersonQualityId = SQLHelper.GetNewID(typeof(Model.View_QualityAudit_PersonQuality));
                                BLL.PersonQualityService.AddPersonQuality(newPersonQuality);
                            }
                            else
                            {
                                newPersonQualityView.PersonQualityId = newPersonQuality.PersonQualityId = addItem.PersonQualityId;
                                BLL.PersonQualityService.UpdatePersonQuality(newPersonQuality);
                            }

                            BLL.PersonQualityService.SavePersonQualityItem(newPersonQuality.PersonQualityId, newPersonQuality.LateCheckDate);
                            if (!string.IsNullOrEmpty(col13))
                            {
                                string url = "FileUpload\\PersonQualityAttachUrl\\" + col13;
                                var att = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == newPersonQuality.PersonQualityId);
                                if (att == null)
                                {
                                    Model.AttachFile newAttachFile = new Model.AttachFile();
                                    newAttachFile.AttachFileId = SQLHelper.GetNewID(typeof(Model.AttachFile));
                                    newAttachFile.ToKeyId = newPersonQuality.PersonQualityId;
                                    newAttachFile.AttachSource = BLL.UploadFileService.GetSourceByAttachUrl(url, BLL.UploadFileService.getFileSize(url), string.Empty);
                                    newAttachFile.AttachUrl = url;
                                    newAttachFile.MenuId = BLL.Const.PersonQualityMenuId;
                                    Funs.DB.AttachFile.InsertOnSubmit(newAttachFile);
                                    Funs.DB.SubmitChanges();
                                }
                            }

                            ///加入
                            viewPersonQualitys.Add(newPersonQualityView);
                        }
                        else
                        {
                            results += result;
                        }
                    }
                }

                if (viewPersonQualitys.Count > 0)
                {
                    viewPersonQualitys = viewPersonQualitys.Distinct().ToList();
                    this.Grid1.Hidden = false;
                    this.Grid1.DataSource = viewPersonQualitys;
                    this.Grid1.DataBind();
                }

                if (!string.IsNullOrEmpty(results))
                {
                    viewPersonQualitys.Clear();
                    results = "数据导入完成，未成功数据：" + results.Substring(0, results.LastIndexOf("|"));
                    errorInfos = results;
                    Alert alert = new Alert
                    {
                        Message = results,
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
            if (Session["personQuality"] != null)
            {
                viewPersonQualitys = Session["personQuality"] as List<Model.View_QualityAudit_PersonQuality>;
            }
            if (viewPersonQualitys.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = viewPersonQualitys;
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
                string filePath = Const.PersonQualityTemplateUrl;
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
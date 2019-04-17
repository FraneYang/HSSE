using System.IO;
using System.Web;

namespace BLL
{
    /// <summary>
    /// 上传附件相关
    /// </summary>
    public class UploadFileService
    {
        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="fileUpload">上传控件</param>
        /// <param name="fileUrl">上传路径</param>
        /// <param name="constUrl">定义路径</param>
        /// <returns></returns>
        public static string UploadAttachment(string rootPath, FineUIPro.FileUpload fileUpload, string fileUrl, string constUrl)
        {
            if (!string.IsNullOrEmpty(fileUrl))  ////是否存在附件 存在则删除
            {
                string urlFullPath = rootPath + fileUrl;
                if (File.Exists(urlFullPath))
                {
                    File.Delete(urlFullPath);
                }
            }

            string initFullPath = rootPath + constUrl;
            if (!Directory.Exists(initFullPath))
            {
                Directory.CreateDirectory(initFullPath);
            }

            string filePath = fileUpload.PostedFile.FileName;
            string fileName = Funs.GetNewFileName() + "~" + Path.GetFileName(filePath);
            int count = fileUpload.PostedFile.ContentLength;
            string savePath = constUrl + fileName;
            string fullPath = initFullPath + fileName;
            if (!File.Exists(fullPath))
            {
                byte[] buffer = new byte[count];
                Stream stream = fileUpload.PostedFile.InputStream;

                stream.Read(buffer, 0, count);
                MemoryStream memoryStream = new MemoryStream(buffer);
                FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                memoryStream.WriteTo(fs);
                memoryStream.Flush();
                memoryStream.Close();
                fs.Flush();
                fs.Close();
                memoryStream = null;
                fs = null;
                //if (!string.IsNullOrEmpty(fileUrl))
                //{
                //    fileUrl += "," + savePath;
                //}
                //else
                //{
                //    fileUrl += savePath;
                //}
                fileUrl = savePath;
            }
            else
            {
                fileUrl = string.Empty;
            }

            return fileUrl;
        }
        #endregion

        #region 附件资源删除
        /// <summary>
        /// 附件资源删除
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="fileUrl"></param>
        public static void DeleteFile(string rootPath, string fileUrl)
        {
            if (!string.IsNullOrEmpty(fileUrl))
            {
                string[] strs = fileUrl.Trim().Split(',');
                foreach (var item in strs)
                {
                    string urlFullPath = rootPath + item;
                    if (File.Exists(urlFullPath))
                    {
                        File.Delete(urlFullPath);
                    }
                }
            }
        }
        #endregion

        #region 上传文件路径
        /// <summary>
        /// 培训教材库-附件路径
        /// </summary>
        public static string TrainingEduFilePath = "FileUpload\\TrainingEdu\\";
        /// <summary>
        /// 考试试题库-附件路径
        /// </summary>
        public static string TrainingFilePath = "FileUpload\\Training\\";
        /// <summary>
        /// 培训任务试题记录图片
        /// </summary>
        public const string TaskRecordFilePath = "FileUpload\\TaskRecord\\";
        /// <summary>
        /// 个人图片
        /// </summary>
        public const string PersonalFilePath = "FileUpload\\Personal\\";
        /// <summary>
        /// 设备设施二维码
        /// </summary>
        public const string EuipmentFilePath = "FileUpload\\Euipment\\";

        #region 分包商资质
        /// <summary>
        /// 营业执照扫描件-附件路径
        /// </summary>
        public const string BL_ScanUrlFilePath = "FileUpload\\BL_ScanUrl\\";
        /// <summary>
        /// 机构代码扫描件-附件路径
        /// </summary>
        public const string O_ScanUrlFilePath = "FileUpload\\O_ScanUrl\\";
        /// <summary>
        /// 资质证书扫描件-附件路径
        /// </summary>
        public const string C_ScanUrlFilePath = "FileUpload\\C_ScanUrl\\";
        /// <summary>
        /// 质量体系认证证书扫描件-附件路径
        /// </summary>
        public const string QL_ScanUrlFilePath = "FileUpload\\QL_ScanUrl\\";
        /// <summary>
        /// HSE体系认证证书扫描件-附件路径
        /// </summary>
        public const string H_ScanUrlFilePath = "FileUpload\\H_ScanUrl\\";
        /// <summary>
        /// 安全生产许可证扫描件-附件路径
        /// </summary>
        public const string SL_ScanUrlFilePath = "FileUpload\\SL_ScanUrl\\";
        #endregion

        #endregion

        /// <summary>
        /// 通过附件路径得到Source
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetSourceByAttachUrl(string attachUrl, long size, string oldSrouce)
        {
            string attachSource = string.Empty;
            var allUrl = Funs.GetStrListByStr(attachUrl, ',');
            foreach (var item in allUrl)
            {
                int strInt = item.LastIndexOf("~");
                if (strInt < 0)
                {
                    strInt = item.LastIndexOf("\\");
                }
                string name = item.Substring(strInt + 1);
                string type = item.Substring(item.LastIndexOf(".") + 1);
                string savedName = item.Substring(item.LastIndexOf("\\") + 1);

                string id = SQLHelper.GetNewID(typeof(Model.AttachFile));
                attachSource += "{    \"name\": \"" + name + "\",    \"type\": \"" + type + "\",    \"savedName\": \"" + savedName
                    + "\",    \"size\": " + size + ",    \"id\": \"" + SQLHelper.GetNewID(typeof(Model.AttachFile)) + "\"  }@";
            }
            attachSource = attachSource.Substring(0, attachSource.LastIndexOf("@")).Replace("@", ",");

            if (!string.IsNullOrEmpty(oldSrouce))
            {
                attachSource = oldSrouce.Replace("]", ",") + attachSource + "]";
            }
            else
            {
                attachSource = "[" + attachSource + "]";
            }
            return attachSource;
        }

        /// <summary>
        /// 获取附件大小
        /// </summary>
        /// <param name="fileaddress"></param>
        /// <returns></returns>
        public static long getFileSize(string fileaddress)
        {
            long size =0;
            string filename = Funs.RootPath + fileaddress;
            FileInfo file = new FileInfo(filename);
            if (file.Exists)
            {
                size = file.Length;
            }

            return size;
        }
    }
}
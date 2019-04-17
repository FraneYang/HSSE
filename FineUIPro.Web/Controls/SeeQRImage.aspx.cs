using BLL;
using System;
using System.IO;
using System.Text;
using System.Web.UI;
using ThoughtWorks.QRCode.Codec;

namespace FineUIPro.Web.Controls
{
    public partial class SeeQRImage : Page
    {
        #region 自定义
        /// <summary>
        /// 二维码路径id
        /// </summary>
        public string AttachUrl
        {
            get
            {
                return (string)ViewState["AttachUrl"];
            }
            set
            {
                ViewState["AttachUrl"] = value;
            }
        }
        /// <summary>
        /// 菜单id
        /// </summary>
        public string MenuId
        {
            get
            {
                return (string)ViewState["MenuId"];
            }
            set
            {
                ViewState["MenuId"] = value;
            }
        }
        /// <summary>
        /// 数据id
        /// </summary>
        public string DataId
        {
            get
            {
                return (string)ViewState["DataId"];
            }
            set
            {
                ViewState["DataId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MenuId = Request.Params["menuId"];
                this.DataId = Request.Params["dataId"];
                this.AttachUrl = Request.Params["QRUrl"];
                if (string.IsNullOrEmpty(this.AttachUrl))
                {
                    if (!string.IsNullOrEmpty(this.DataId))
                    {
                        if (MenuId == BLL.Const.EuipmentMenuId)
                        {
                            var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(this.DataId);
                            if (euipment != null)
                            {
                                string name = "设备设施：";
                                name += BLL.InstallationService.GetInstallationNameByInstallationId(euipment.InstallationId);
                                var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(euipment.WorkAreaId);
                                if (workArea != null)
                                {
                                    name += "," + workArea.WorkAreaName; //作业区域
                                }
                                //// 设备设施：作业区域，设备名称，设备位号
                                this.txtName.InnerText = name + "," + euipment.EuipmentName + "," + euipment.EuipmentNo;

                                if (!string.IsNullOrEmpty(euipment.QRCodeUrl) && File.Exists(euipment.QRCodeUrl))
                                {
                                    this.AttachUrl = euipment.QRCodeUrl;
                                }
                                else
                                {
                                    this.CreateCode_Simple("euipment$" + this.DataId);
                                }
                            }
                        }
                        else if (MenuId == BLL.Const.JobEnvironmentMenuId)
                        {
                            var jobEnvironment = BLL.JobEnvironmentService.GetJobEnvironmentByJobEnvironmentId(this.DataId);
                            if (jobEnvironment != null)
                            {
                                string name = "作业环境：";
                                name += BLL.InstallationService.GetInstallationNameByInstallationId(jobEnvironment.InstallationId);
                                var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(jobEnvironment.WorkAreaId);
                                if (workArea != null)
                                {
                                    name += "," + workArea.WorkAreaName;
                                }

                                this.txtName.InnerText = name + "," + jobEnvironment.JobEnvironmentName;
                                if (!string.IsNullOrEmpty(jobEnvironment.QRCodeUrl) && File.Exists(jobEnvironment.QRCodeUrl))
                                {
                                    this.AttachUrl = jobEnvironment.QRCodeUrl;
                                }
                                else
                                {
                                    this.CreateCode_Simple("jobEnvironment$" + this.DataId);
                                }
                            }
                        }
                    }

                    this.divBeImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../", this.AttachUrl, 260, 260);
                }
            }
        }

        //生成二维码方法一
        private void CreateCode_Simple(string nr)
        {
            BLL.UploadFileService.DeleteFile(Funs.RootPath, this.AttachUrl);//删除二维码
            string imageUrl = string.Empty;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeScale = nr.Length,
                QRCodeVersion = 0,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
            };
            System.Drawing.Image image = qrCodeEncoder.Encode(nr, Encoding.UTF8);
            string filepath = Funs.RootPath + BLL.UploadFileService.EuipmentFilePath;
            //如果文件夹不存在，则创建
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
            imageUrl = filepath + filename;

            System.IO.FileStream fs = new System.IO.FileStream(imageUrl, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            image.Dispose();
            this.AttachUrl = BLL.UploadFileService.EuipmentFilePath + filename;
            if (MenuId == BLL.Const.EuipmentMenuId)
            {
                var euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(this.DataId);
                if (euipment != null)
                {
                    euipment.QRCodeUrl = this.AttachUrl;
                    BLL.EuipmentService.UpdateEuipment(euipment);

                    string name = "设备设施：";
                    name += BLL.InstallationService.GetInstallationNameByInstallationId(euipment.InstallationId);
                    var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(euipment.WorkAreaId);
                    if (workArea != null)
                    {
                        name += "," + workArea.WorkAreaName;
                    }

                    this.txtName.InnerText = name + "," + euipment.EuipmentName + "," + euipment.EuipmentNo;
                }
            }
            else if (MenuId == BLL.Const.JobEnvironmentMenuId)
            {
                var jobEnvironment = BLL.JobEnvironmentService.GetJobEnvironmentByJobEnvironmentId(this.DataId);
                if (jobEnvironment != null)
                {
                    jobEnvironment.QRCodeUrl = this.AttachUrl;
                    BLL.JobEnvironmentService.UpdateJobEnvironment(jobEnvironment);

                    string name = "作业环境：";
                    name += BLL.InstallationService.GetInstallationNameByInstallationId(jobEnvironment.InstallationId);
                    var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(jobEnvironment.WorkAreaId);
                    if (workArea != null)
                    {
                        name += "," + workArea.WorkAreaName;
                    }

                    this.txtName.InnerText = name + "," + jobEnvironment.JobEnvironmentName;
                }
            }
        }
    }
}

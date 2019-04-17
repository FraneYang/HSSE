namespace FineUIPro.Web
{
    using System;
    using BLL;
    using System.Configuration;
    using System.Web;

    public partial class Login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.tbxUserName.Focus();
                var unit = BLL.CommonService.GetIsThisUnit();
                if (unit != null && !string.IsNullOrEmpty(unit.UnitName))
                {
                    this.lbSubName.Text = unit.UnitName;
                }
                this.LoadData();
                if (Request.Cookies["UserInfo"] != null)
                {
                    if (Request.Cookies["UserInfo"]["username"] != null)
                    {
                        this.tbxUserName.Text = HttpUtility.UrlDecode(Request.Cookies["UserInfo"]["username"].ToString());
                    }
                    if (Request.Cookies["UserInfo"]["password"] != null)
                    {
                        this.tbxPassword.Text = Request.Cookies["UserInfo"]["password"].ToString();
                    }

                    this.ckRememberMe.Checked = true;
                }

                this.lbVevion.Text = "请使用IE10以上版本浏览器 系统版本:" + ConfigurationManager.AppSettings["SystemVersion"];
                this.Image1.ImageUrl = "~/" + BLL.Const.APPImageUrl;                
                this.HyperLink3.NavigateUrl = Funs.APPUrl;
            }
        }

        /// <summary>
        /// 生成图片
        /// </summary>
        private void LoadData()
        {
            this.InitCaptchaCode();
        }

        /// <summary>
        /// 初始化验证码
        /// </summary>
        private void InitCaptchaCode()
        {
            // 创建一个 6 位的随机数并保存在 Session 对象中
            Session["CaptchaImageText"] = GenerateRandomCode();
            imgCaptcha.Text = String.Format("<img src=\"{0}\" />", ResolveUrl("~/Captcha/captcha.ashx?w=100&h=26&t=" + DateTime.Now.Ticks));
        }

        /// <summary>
        /// 创建一个 6 位的随机数
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomCode()
        {
            string s = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                s += random.Next(10).ToString();
            }
            return s;
        }

        /// <summary>
        ///  重置图片验证码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgCaptcha_Click(object sender, EventArgs e)
        {
            InitCaptchaCode();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbxCaptcha.Text != Session["CaptchaImageText"].ToString())
            {
                ShowNotify("验证码错误！", MessageBoxIcon.Error);
                return;
            }
            if (BLL.LoginService.UserLogOn(tbxUserName.Text, this.tbxPassword.Text, this.ckRememberMe.Checked, this.Page))
            {                
                BLL.LogService.AddLog(this.CurrUser.UserId, "登录成功！");
                PageContext.Redirect("~/default.aspx");                
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 厂区图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbWelderRead_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Hazard/RiskMap.aspx");
        }
    }
}

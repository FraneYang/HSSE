<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="FineUIPro.Web.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/Captcha/captcha.ashx;~/Captcha/CaptchaImage.cs" />
    <style type="text/css">
        .imgcaptcha .f-field-label {
            margin: 0;
        }

        .login-image {
            border-width: 0 1px 0 0;
            width: 116px;
            height: 116px;
        }

            .login-image .ui-icon {
                font-size: 96px;
       }
       
        .mybgpanel.bg1 > .f-panel-bodyct > .f-panel-body  {
            background-color: #B1EEEF;
            background-image: url(Images/LoginHSSE.jpg);
        } 
         .text
        {
            font-size:14pt;
        }      
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server"/> 
         <f:Window ID="Window1" runat="server" Title="登录表单" IsModal="false" EnableClose="false" IconFont="SignIn" ShowHeader="false"
                    WindowPosition="Center" Layout="VBox" CssClass="mybgpanel bg1" Width="978px" Height="560px">
              <Items>
                <f:Panel ID="Panel4" BoxFlex="1" ShowBorder="false" ShowHeader="false" BoxConfigAlign="Stretch"
                    runat="server" Layout="HBox">
                    <Items>
                        <f:Panel ID="Panel70" BoxFlex="30" ShowBorder="false" ShowHeader="false" BoxConfigAlign="Stretch"
                            runat="server" Layout="VBox" Margin="290px 0px 0 0px">
                            <Items>
                                <f:TextBox ID="tbxUserName" Label="用户名" Required="true" ShowRedStar="true" runat="server"
                                    Margin="0px 330px 0 330px" NextFocusControl="tbxPassword" LabelWidth="70px" LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="tbxPassword" Label="密码" TextMode="Password" Required="true" LabelWidth="70px" LabelAlign="Right"
                                    ShowRedStar="true" runat="server" Margin="5px 330px 0 330px" NextFocusControl="tbxCaptcha">
                                </f:TextBox>
                                <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="3px 330px 0 330px">
                                    <Items>
                                        <f:TextBox ID="tbxCaptcha" BoxFlex="1" Margin="3px 5px 0 0" Label="验证码" Required="true" 
                                            runat="server" LabelWidth="70px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:LinkButton ID="imgCaptcha" CssClass="imgcaptcha" Width="100px" EncodeText="false" Margin="5px 5px 0 0"
                                            runat="server" OnClick="imgCaptcha_Click">
                                        </f:LinkButton>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel2" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="3px 330px 0 320px">
                                    <Items>
                                        <f:Button ID="btnLogin" Text="登录" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Top"
                                            runat="server" OnClick="btnLogin_Click" Margin="5px 10px 0 90px" >
                                        </f:Button>
                                        <f:Button ID="btnReset" Text="重置" Type="Reset" 
                                            runat="server" Margin="5px 0 0 0">
                                        </f:Button>
                                        <f:CheckBox runat="server" Label="记住" ID="ckRememberMe" Margin="5px 0 0 10px" LabelAlign="Right" LabelWidth="50px"></f:CheckBox>
                                    </Items>
                                </f:Panel> 
                            </Items>
                        </f:Panel>
                    </Items>
                    <Items>
                        <f:Panel ID="Panel5"  Title="面板1" BoxFlex="1" runat="server"  ShowBorder="false" 
                            ShowHeader="false" Layout="VBox"   MarginTop="260px" MarginLeft="-200px" >
                            <Items>
                                <f:Image ID="Image1" CssClass="userphoto" ImageUrl="~/res/images/blank_180.png" runat="server"
                                    BoxFlex="1" Width="120px" Height="120px">
                                </f:Image>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Panel>
            </Items>
            <Items>
                 <f:Panel ID="Panel6" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="0px 0px 0 410px">
                    <Items>   
                        <f:Label ID="lbSubName" runat="server" CssClass="text"></f:Label>                                
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel3" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch"
                    runat="server" MarginLeft="210px">
                    <Items>
                        <f:Label ID="lbVevion" runat="server" MarginLeft="50px" ToolTip="当前软件运行环境要求及当前系统版本。"></f:Label>                                 
                            <f:HyperLink ID="HyperLink3" Text="APP下载" Target="_blank"  runat="server" MarginLeft="5px">
                            </f:HyperLink>
                            <f:LinkButton  ID="lbWelderRead" Text="厂区图" OnClick="lbWelderRead_Click" runat="server" MarginLeft="10px" Hidden="true">
                        </f:LinkButton>
                    </Items>
                </f:Panel>
            </Items>                   
        </f:Window>
    </form>
</body>
</html>
     
        

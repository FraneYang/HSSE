<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationCollectionEdit.aspx.cs"
    Inherits="FineUIPro.Web.QualityAudit.InformationCollectionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑信息采集</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnit" runat="server" Label="单位" EnableEdit="true" ForceSelection="false"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUserName" runat="server" Label="姓名" LabelAlign="Right" Required="true"
                        ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:RadioButtonList ID="rblSex" runat="server" Label="性别" LabelAlign="Right">
                        <f:RadioItem Text="男" Value="1" Selected="true"/>
                        <f:RadioItem Text="女" Value="2" />
                    </f:RadioButtonList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtBirthDay" runat="server" Label="出生日期" LabelAlign="Right" EnableEdit="false">
                    </f:DatePicker>
                      <f:DatePicker ID="txtEntryTime" runat="server" Label="登记时间" LabelAlign="Right" EnableEdit="false">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号码" LabelAlign="Right" Required="true"
                        ShowRedStar="true" MaxLength="18">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTelephone" runat="server" Label="联系方式" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" ToolTip="保存" runat="server" ValidateForms="SimpleForm1"
                        Hidden="true" OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonnelDocumentEdit.aspx.cs"
    Inherits="FineUIPro.Web.QualityAudit.PersonnelDocumentEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑人员建档</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSortIndex" runat="server" Label="排列序号" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtUserName" runat="server" Label="姓名" Required="true" ShowRedStar="true"
                        MaxLength="20" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUserCode" runat="server" Label="用户编号" MaxLength="20" AutoPostBack="true"
                        OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <f:TextBox ID="txtAccount" runat="server" Label="登录账号" Required="true" ShowRedStar="true"
                        MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnit" runat="server" Label="单位" EnableEdit="true" ForceSelection="false"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                    <f:TextBox ID="txtTelephone" runat="server" Label="手机号码" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpDepart" runat="server" Label="部门(中心)" EnableEdit="true" AutoPostBack="true"
                        OnSelectedIndexChanged="drpDepart_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="drpInstallation" runat="server" Label="装置/科室" EnableEdit="true"
                        EnableMultiSelect="true" ForceSelection="false" MaxLength="500" EnableCheckBoxSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpWorkPost" runat="server" Label="所属岗位" EnableEdit="true" EnableMultiSelect="true"
                        ForceSelection="false" MaxLength="500" EnableCheckBoxSelect="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpRole" runat="server" Label="所属角色" EnableEdit="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号" MaxLength="50" AutoPostBack="true"
                        OnTextChanged="TextBox_TextChanged">
                        <%--RegexPattern="IDENTITY_CARD"--%>
                    </f:TextBox>
                     </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtEntryTime" runat="server" Label="登记时间" LabelAlign="Right" EnableEdit="false">
                    </f:DatePicker>
                    <f:DropDownList ID="drpIsPost" runat="server" Label="在岗" EnableEdit="false" Required="true"
                        ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
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

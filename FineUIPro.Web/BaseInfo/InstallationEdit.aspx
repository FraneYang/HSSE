<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstallationEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.InstallationEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑装置/科室</title>
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
                     <f:TextBox ID="txtInstallationCode" runat="server" Label="编号"  MaxLength="20" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <f:TextBox ID="txtInstallationName" runat="server" Label="名称" Required="true" ShowRedStar="true"  MaxLength="20"
                        FocusOnPageLoad="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>            
                   
             <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpDepart" runat="server" Label="部门(中心)" EnableEdit="true" >
                    </f:DropDownList>
                    <f:DropDownList ID="drpIsUsed" runat="server" Label="启用" EnableEdit="false" 
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>           
        <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpManagerIds" runat="server" Label="负责人" EnableEdit="true" EnableMultiSelect="true"
                        ForceSelection="false" MaxLength="500" EnableCheckBoxSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDef" runat="server" Label="备注" MaxLength="500">
                   </f:TextBox>
                </Items>
            </f:FormRow>          
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
                        OnClick="btnSave_Click">
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

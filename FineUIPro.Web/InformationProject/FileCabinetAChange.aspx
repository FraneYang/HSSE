<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileCabinetAChange.aspx.cs" Inherits="FineUIPro.Web.InformationProject.FileCabinetAChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox" Title="文件柜目录">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Tree ID="tvFileCabinetA" KeepCurrentSelection="true" Height="420px" Title="文件柜" Width="570px"
                ShowHeader="false" runat="server"  ShowBorder="false" EnableSingleClickExpand="true"> 
            </f:Tree>
           </Items>
        </f:Panel>
    </form>
</body>
</html>

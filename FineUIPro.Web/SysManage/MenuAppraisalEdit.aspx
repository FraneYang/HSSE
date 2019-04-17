<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuAppraisalEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.MenuAppraisalEdit" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="20px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
             <f:FormRow>
               <Items>
                   <f:DropDownList ID="drpMenuOperation" runat="server" Label="页面操作" EnableEdit="true" 
                        Required="true" ShowRedStar="true" LabelWidth="120px" LabelAlign="Right">
                    </f:DropDownList>
                </Items>  
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtScore" runat="server" Label="分值" NoDecimal="true" 
                        LabelWidth="120px" LabelAlign="Right" ShowRedStar="true" Required="true" ></f:NumberBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
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

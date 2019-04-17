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
                    <f:NumberBox ID="txtMenuOperation" runat="server" Label="动作序号" FocusOnPageLoad="true"
                        LabelWidth="120px" LabelAlign="Right" ShowRedStar="true" Required="true" NoDecimal="true" NoNegative="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
               <Items>
                    <f:TextArea ID="txtMenuOperationName" runat="server" Label="动作名称" 
                        LabelWidth="120px" LabelAlign="Right" ShowRedStar="true" Required="true" >
                    </f:TextArea>
                </Items>  
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtScore" runat="server" Label="分值" 
                        LabelWidth="120px" LabelAlign="Right" ShowRedStar="true" Required="true" ></f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtOutTime" runat="server" Label="超时时长(分钟)" 
                        LabelWidth="120px" LabelAlign="Right" NoDecimal="true" NoNegative="true"></f:NumberBox>
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

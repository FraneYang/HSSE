<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardEdit.aspx.cs" 
    Inherits="FineUIPro.Web.Standard.HSSEStandardEdit" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
              <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHSSEStandardCode" runat="server" Label="编号" FocusOnPageLoad="true">
                    </f:TextBox>                    
                </Items>
            </f:FormRow>  
             <f:FormRow>
                <Items>
                    <f:TextArea ID="txtHSSEStandardName" runat="server" Label="具体要求" Required="true" ShowRedStar="true"
                        Height="120px"></f:TextArea>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="内容" ID="txtFileContent" ShowLabel="false" 
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="300" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                                       
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
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

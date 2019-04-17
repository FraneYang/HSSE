<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppraisalItemEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.AppraisalItemEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑测评考核项</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">        
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtCode" runat="server" Label="编码" FocusOnPageLoad="true" MaxLength="50">
                    </f:TextBox>
                    
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtCheckItem" runat="server" Label="考核项" Required="true" ShowRedStar="true"  MaxLength="200"
                         AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Height="100px">
                    </f:TextArea>
                </Items>                
            </f:FormRow>        
             <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtScore" runat="server" Label="分值" Required="true" ShowRedStar="true" DecimalPrecision="1">
                    </f:NumberBox>
                </Items>                
            </f:FormRow>              
             <f:FormRow>
                <Items>                   
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="500" Height="60px">
                   </f:TextArea>
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

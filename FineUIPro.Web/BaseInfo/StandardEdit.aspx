<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StandardEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.StandardEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑标准名称</title>
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
                   <f:TextBox ID="txtStandardCode" runat="server" Label="编号" MaxLength="50" FocusOnPageLoad="true">
                   </f:TextBox>                    
                </Items>
            </f:FormRow>     
             <f:FormRow>
                <Items>    
                  <f:TextBox ID="txtStandardName" runat="server" Label="标准名称" MaxLength="100"
                        Required="true" ShowRedStar="true" AutoPostBack="true" OnTextChanged="txtStandardName_TextChanged" >
                   </f:TextBox>
                </Items>
            </f:FormRow>   
            <f:FormRow>
                <Items>    
                  <f:DropDownList ID="drpSpecialtyId" runat="server" Label="专业" EnableEdit="true" ForceSelection="false" >
                    </f:DropDownList>
                </Items>
            </f:FormRow>   
             <f:FormRow>
                <Items>  
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="200" Height="60">
                   </f:TextArea>
                </Items>
            </f:FormRow>          
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrl_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

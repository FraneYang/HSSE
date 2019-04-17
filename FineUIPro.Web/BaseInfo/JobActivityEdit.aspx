<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobActivityEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.JobActivityEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑作业活动</title>
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
                   <f:TextBox ID="txtJobActivityCode" runat="server" Label="编号"
                       Required="true" ShowRedStar="true" AutoPostBack="true" FocusOnPageLoad="true">
                   </f:TextBox>  
                     <f:TextBox ID="txtJobActivityName" runat="server" Label="作业活动" MaxLength="100"
                        Required="true" ShowRedStar="true" AutoPostBack="true" OnTextChanged="txtJobActivityName_TextChanged">
                   </f:TextBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>    
                  <f:DropDownList ID="drpInstallationId" runat="server" Label="所属单位</br>(装置)" EnableEdit="true" ForceSelection="false"  
                      Required="true" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="drpInstallationId_SelectedIndexChanged" >
                  </f:DropDownList>
                     <f:DropDownList ID="drpWorkAreaId" runat="server" Label="作业活动地点</br>(区域单元)" EnableEdit="true" ForceSelection="false" 
                      Required="true" ShowRedStar="true">
                  </f:DropDownList>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>  
                     <f:DropDownList ID="drpIdentification" runat="server" Label="评价方法" ForceSelection="false">                    
                       <f:ListItem Value="JHA" Text="JHA" Selected="true"/>
                     </f:DropDownList>
                     <f:Label runat="server" ID="xx"></f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="描述" MaxLength="200">
                   </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                   
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
    </form>
</body>
</html>

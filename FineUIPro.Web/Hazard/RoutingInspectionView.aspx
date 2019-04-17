<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutingInspectionView.aspx.cs"
    Inherits="FineUIPro.Web.Hazard.RoutingInspectionView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>隐患详细</title>
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
                     <f:TextBox ID="txtPatrolManName" runat="server" Label="巡检人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtPatrolTime" runat="server" Label="巡检时间" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtInstallationName" runat="server" Label="所属单位" Readonly="true">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTaskActivity" runat="server" Label="设备设施</br>作业活动" Readonly="true" Height="64px">
                    </f:TextArea>                       
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtHazardDescription" runat="server" Label="风险点" Readonly="true" Height="90px">
                    </f:TextArea>
                </Items>
            </f:FormRow>             
              <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOldRiskLevel" runat="server" Label="原等级" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtNowRiskLevel" runat="server" Label="现等级" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtPatrolResultName" runat="server" Label="巡检结果" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow> 
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                                      
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>        
    </form>
</body>
</html>

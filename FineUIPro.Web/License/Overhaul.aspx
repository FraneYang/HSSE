﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overhaul.aspx.cs" Inherits="FineUIPro.Web.License.Overhaul" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>检修工作票</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="检修工作票" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="OverhaulId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="OverhaulId" AllowSorting="true" SortField="LicenseCode"
                SortDirection="DESC" OnSort="Grid1_Sort"   AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtLicenseCode" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                             </f:TextBox>                            
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>                   
                    <f:RenderField Width="100px" ColumnID="LicenseCode" DataField="LicenseCode" EnableFilter="true"
                        SortField="LicenseCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>      
                     <f:RenderField  ColumnID="SendTicketTime" DataField="SendTicketTime" SortField="SendTicketTime" Width="145px"
                        HeaderText="送票时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                    <f:RenderField Width="80px" ColumnID="RiskGrade" DataField="RiskGrade" 
                        SortField="RiskGrade" FieldType="String" HeaderText="作业风险" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>  
                    <f:RenderField Width="150px" ColumnID="InstallationName" DataField="InstallationName" 
                        SortField="InstallationName" FieldType="String" HeaderText="设备所在车间" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>
                     <f:RenderField Width="80px" ColumnID="DevicePositionNum" DataField="DevicePositionNum" 
                        SortField="DevicePositionNum" FieldType="String" HeaderText="设备位号" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>    
                     <f:RenderField Width="120px" ColumnID="DeviceName" DataField="DeviceName" 
                        SortField="DeviceName" FieldType="String" HeaderText="设备名称" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                    <f:RenderField  ColumnID="UnitName" DataField="UnitName" SortField="UnitName" FieldType="String"
                         Width="220px" HeaderText="检修单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField  ColumnID="OverhaulCategory" DataField="OverhaulCategory" SortField="OverhaulCategory" FieldType="String" Width="90px"
                        HeaderText="检修类别" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                       <f:RenderField Width="80px" ColumnID="CompileManName" DataField="CompileManName" 
                        SortField="CompileManName" FieldType="String" HeaderText="编制人" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>    
                     <f:RenderField  ColumnID="StatesName" DataField="StatesName" SortField="StatesName" FieldType="String" Width="100px"
                        HeaderText="状态" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>
                <PageItems>
                    <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                    </f:ToolbarSeparator>
                    <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                    </f:ToolbarText>
                    <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <f:ListItem Text="10" Value="10" />
                        <f:ListItem Text="15" Value="15" />
                        <f:ListItem Text="20" Value="20" />
                        <f:ListItem Text="25" Value="25" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
       
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"  Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除" Hidden ="true">
        </f:MenuButton>        
    </f:Menu>
    </form>
    <script type="text/jscript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
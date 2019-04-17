<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiddenHazardDelay.aspx.cs" Inherits="FineUIPro.Web.SysManage.HiddenHazardDelay" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>延期申请</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="延期申请" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="DelayId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="DelayId" AllowSorting="true" SortField="HiddenHazardCode"
                SortDirection="DESC" OnSort="Grid1_Sort"   AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtHiddenHazardCode" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="60px">
                             </f:TextBox>                            
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                    
                    <f:RenderField Width="100px" ColumnID="HiddenHazardCode" DataField="HiddenHazardCode" EnableFilter="true"
                        SortField="HiddenHazardCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>                        
                    <f:RenderField Width="150px" ColumnID="HiddenHazardName" DataField="HiddenHazardName" 
                        SortField="HiddenHazardName" FieldType="String" HeaderText="隐患名称" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                    <f:RenderField Width="150px" ColumnID="CorrectMeasures" DataField="CorrectMeasures" ExpandUnusedSpace="true"
                        SortField="CorrectMeasures" FieldType="String" HeaderText="整改措施" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>  
                     <f:RenderField Width="80px" ColumnID="ApplicantMan" DataField="ApplicantMan" ExpandUnusedSpace="true"
                        SortField="CorrectMeasures" FieldType="String" HeaderText="申请人" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>  
                      <f:RenderField  ColumnID="DelayReasons" DataField="DelayReasons" SortField="DelayReasons" FieldType="String" Width="200px"
                        HeaderText="到期未完成原因" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField  ColumnID="OldLimitTime" DataField="OldLimitTime" SortField="OldLimitTime" Width="145px"
                        HeaderText="原期限" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField  ColumnID="LimitTime" DataField="LimitTime" SortField="LimitTime" Width="145px"
                        HeaderText="延期至" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>   
                    <f:RenderField Width="150px" ColumnID="ControlMeasures" DataField="ControlMeasures" ExpandUnusedSpace="true"
                        SortField="ControlMeasures" FieldType="String" HeaderText="控制措施" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>  
                    <f:RenderField  ColumnID="StatesName" DataField="StatesName" SortField="StatesName" FieldType="String" Width="80px"
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

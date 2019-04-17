<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RescueInfoPlan.aspx.cs" Inherits="FineUIPro.Web.Emergency.RescueInfoPlan" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>应急事件</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="应急事件" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RescueInfoPlanId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="RescueInfoPlanId" AllowSorting="true" SortField="ReceiveTime"
                SortDirection="DESC" OnSort="Grid1_Sort"   AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True"  EnableRowDoubleClickEvent="false" OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" >
                        <Items>                                                  
                            <f:TextBox runat="server" Label="查询" ID="txtContent" EmptyText="输入查询条件" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="300px" LabelWidth="60px">
                             </f:TextBox>                            
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>                          
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" 
                            HeaderTextAlign="Center" TextAlign="Center"/>                                                                      
                    <f:RenderField  ColumnID="ReceiveTime" DataField="ReceiveTime" SortField="ReceiveTime" Width="145px"
                        HeaderText="接收日期" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                    <f:RenderField Width="220px" ColumnID="Treatment" DataField="Treatment" 
                        SortField="Treatment" FieldType="String" HeaderText="处理措施" HeaderTextAlign="Center"
                        TextAlign="Left" ExpandUnusedSpace="true">                       
                    </f:RenderField> 
                     <f:RenderField Width="120px" ColumnID="Department" DataField="Department" 
                        SortField="Department" FieldType="String" HeaderText="应急部门" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                     <f:RenderField Width="120px" ColumnID="Obligation" DataField="Obligation"
                        SortField="Obligation" FieldType="String" HeaderText="职责" HeaderTextAlign="Center"
                        TextAlign="Left">                       
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
     <f:Window ID="Window1" Title="推送信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="1100px"
        Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
       <%-- <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" runat="server" Text="查看" Icon="Eye">
        </f:MenuButton>--%>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PushRecordMessage.aspx.cs" Inherits="FineUIPro.Web.ShowDialog.PushRecordMessage" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>推送信息</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="推送信息" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="PushRecordId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="PushRecordId" AllowSorting="true" SortField="PushDateTime"
                SortDirection="DESC" OnSort="Grid1_Sort"   AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" >
                        <Items>                                                  
                            <f:TextBox runat="server" Label="查询" ID="txtContent" EmptyText="输入查询条件" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="300px" LabelWidth="60px">
                             </f:TextBox>       
                             <f:CheckBoxList ID="ckMenuType"  Label="响应" runat="server" AutoColumnWidth="true" Width="70px"
                                 LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                <Items>      
                                    <f:CheckItem Text="是" Value="1" />                             
                                    <f:CheckItem Text="否" Value="0" />                                   
                                </Items>
                                <Listeners>
                                    <f:Listener Event="change" Handler="onCheckBoxListChange" />
                                </Listeners>
                            </f:CheckBoxList>                     
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>                          
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>                   
                     <f:RenderField  ColumnID="PushDateTime" DataField="PushDateTime" SortField="PushDateTime" Width="145px"
                        HeaderText="推送时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                                                      
                    <f:RenderField Width="80px" ColumnID="ReceiveManName" DataField="ReceiveManName" 
                        SortField="ReceiveManName" FieldType="String" HeaderText="接收人" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                     <f:RenderField Width="160px" ColumnID="PushContent" DataField="PushContent" ExpandUnusedSpace="true"
                        SortField="PushContent" FieldType="String" HeaderText="推送内容" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                    <f:CheckBoxField Width="50px" SortField="IsResponse" RenderAsStaticField="true" DataField="IsResponse"
                        HeaderText="响应" HeaderTextAlign="Center" TextAlign="Center">
                    </f:CheckBoxField>
                     <f:RenderField  ColumnID="ResponseTime" DataField="ResponseTime" SortField="ResponseTime" Width="145px"
                        HeaderText="响应时间" HeaderTextAlign="Center" TextAlign="Left">
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
    </form>
    <script type="text/jscript">     
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
          // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

        // 同时只能选中一项
        function onCheckBoxListChange(event, checkbox, isChecked) {
            var me = this;
            // 当前操作是：选中
            if (isChecked) {
                // 仅选中这一项
                me.setValue(checkbox.getInputValue());
            }
            // __doPostBack('', 'CheckBoxList1Change');
        }
    </script>
</body>
</html>

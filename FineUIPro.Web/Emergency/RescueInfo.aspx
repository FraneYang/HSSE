<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RescueInfo.aspx.cs" Inherits="FineUIPro.Web.Emergency.RescueInfo" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>接警</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="接警" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RescueInfoId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="RescueInfoId" AllowSorting="true" SortField="ReceiveTime"
                SortDirection="DESC" OnSort="Grid1_Sort"   AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True"  EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
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
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>                   
                     <f:RenderField  ColumnID="ReceiveTime" DataField="ReceiveTime" SortField="ReceiveTime" Width="145px"
                        HeaderText="接收日期" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                                                      
                    <f:RenderField Width="160px" ColumnID="InstallationNames" DataField="InstallationNames" 
                        SortField="InstallationNames" FieldType="String" HeaderText="装置" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                     <f:RenderField Width="90px" ColumnID="AccidentType" DataField="AccidentType" 
                        SortField="AccidentType" FieldType="String" HeaderText="事故类型" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                     <f:RenderField Width="150px" ColumnID="AccidentPlace" DataField="AccidentPlace" 
                        SortField="AccidentPlace" FieldType="String" HeaderText="事故地点" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>  
                    <f:RenderField Width="150px" ColumnID="AccidentOverview" DataField="AccidentOverview" 
                        SortField="AccidentOverview" FieldType="String" HeaderText="事故概述" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>  
                     <f:RenderField Width="75px" ColumnID="DeathsNumber" DataField="DeathsNumber" 
                        SortField="DeathsNumber" FieldType="String" HeaderText="死亡人数" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>  
                    <f:RenderField Width="75px" ColumnID="InjuredNumber" DataField="InjuredNumber" 
                        SortField="InjuredNumber" FieldType="String" HeaderText="受伤人数" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>                     
                     <f:RenderField Width="80px" ColumnID="AccidentPerson" DataField="AccidentPerson"
                        SortField="AccidentPerson" FieldType="String" HeaderText="负责人" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                     <f:RenderField Width="80px" ColumnID="PoliceTelephone" DataField="PoliceTelephone"
                        SortField="PoliceTelephone" FieldType="String" HeaderText="联系电话" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>    
                     <f:RenderField Width="80px" ColumnID="PoliceType" DataField="PoliceType"
                        SortField="PoliceType" FieldType="String" HeaderText="报警类型" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>     
                      <f:RenderField Width="80px" ColumnID="PoliceLevel" DataField="PoliceLevel"
                        SortField="PoliceLevel" FieldType="String" HeaderText="报警等级" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                     <f:RenderField Width="75px" ColumnID="PoliceMan" DataField="PoliceMan" 
                        SortField="PoliceMan" FieldType="String" HeaderText="报警人" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>                
                     <f:RenderField  ColumnID="PoliceTime" DataField="PoliceTime" SortField="PoliceTime" Width="145px"
                        HeaderText="报警时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>              
                     <f:RenderField  ColumnID="PushStatesName" DataField="PushStatesName" SortField="PushStatesName" 
                        FieldType="String" Width="75px" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Left">
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
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" runat="server" Text="查看" Icon="Eye">
        </f:MenuButton>
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

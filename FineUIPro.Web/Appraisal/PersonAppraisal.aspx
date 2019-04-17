<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonAppraisal.aspx.cs" Inherits="FineUIPro.Web.Appraisal.PersonAppraisal" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>测评记录</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="测评记录" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="PersonAppraisalId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="PersonAppraisalId" AllowSorting="true" SortField="FindTime"
                SortDirection="DESC" OnSort="Grid1_Sort"   AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
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
                     <f:RenderField ColumnID="FindTime" DataField="FindTime" SortField="FindTime" Width="145px"
                        HeaderText="违规时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="75px" ColumnID="ProblemManName" DataField="ProblemManName" 
                        SortField="ProblemManName" FieldType="String" HeaderText="违规人" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>                                                    
                    <f:RenderField Width="110px" ColumnID="InstallationName" DataField="InstallationName" 
                        SortField="InstallationName" FieldType="String" HeaderText="装置" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                     <f:RenderField Width="120px" ColumnID="Place" DataField="Place" 
                        SortField="Place" FieldType="String" HeaderText="地点" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>
                     <f:RenderField Width="160px" ColumnID="CheckItem" DataField="CheckItem" ExpandUnusedSpace="true"
                        SortField="CheckItem" FieldType="String" HeaderText="违规项" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>   
                     <f:RenderField Width="75px" ColumnID="Score" DataField="Score"
                        SortField="Score" FieldType="Double" HeaderText="扣分值" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>     
                      <f:RenderField Width="75px" ColumnID="FindManName" DataField="FindManName"
                        SortField="FindManName" FieldType="String" HeaderText="发现人" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                    <f:RenderField Width="75px" ColumnID="GetScore" DataField="GetScore"
                        SortField="GetScore" FieldType="Double" HeaderText="加分值" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                     <f:RenderField Width="75px" ColumnID="AuditManName" DataField="AuditManName" 
                        SortField="AuditManName" FieldType="String" HeaderText="审核人" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>                
                     <f:RenderField  ColumnID="AuditTime" DataField="AuditTime" SortField="AuditTime" Width="145px"
                        HeaderText="审核时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>              
                     <f:RenderField  ColumnID="StatesName" DataField="StatesName" SortField="StatesName" 
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardView.aspx.cs" Inherits="FineUIPro.Web.Standard.HSSEStandardView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专家辅助</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="专家辅助" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HSSEStandardId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="HSSEStandardId" AllowSorting="true" SortField="SpecialtyCode,StandardCode,ManagedObjectCode,ManagedItemCode,HSSEStandardCode"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"  Width="980px" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                         <Items>
                              <f:TextBox runat="server" Label="查询" ID="txtName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>         
                             <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>  
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                    
                    <f:RenderField Width="70px" ColumnID="HSSEStandardCode" DataField="HSSEStandardCode" FieldType="String"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="SpecialtyName" DataField="SpecialtyName" FieldType="String"
                        HeaderText="专业" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="StandardName" DataField="StandardName" FieldType="String"
                        HeaderText="标准" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ManagedObjectName" DataField="ManagedObjectName" FieldType="String"
                        HeaderText="管理对象" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                    
                     <f:RenderField Width="120px" ColumnID="ManagedItemName" DataField="ManagedItemName" FieldType="String"
                        HeaderText="管理项目" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="HSSEStandardName" DataField="HSSEStandardName" FieldType="String"
                        HeaderText="具体要求" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>                    
                </Columns>
                <Listeners>
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Window ID="Window1" Title="文件内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server"  IsModal="true"
        Width="800px" Height="540px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnAttachUrl" EnablePostBack="true" runat="server"  Text="原文下载" Icon="TableCell"
            OnClick="btnAttachUrl_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnView" EnablePostBack="true" runat="server"  Text="查看" Icon="FolderMagnify"
            OnClick="btnView_Click">
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
        function onGridDataLoad(event) {
            this.mergeColumns(['SpecialtyName'], { depends: true });
            this.mergeColumns(['StandardName'], { depends: true });
            this.mergeColumns(['ManagedObjectName'], { depends: true });
            this.mergeColumns(['ManagedItemName'], { depends: true });
        }
    </script>
</body>
</html>

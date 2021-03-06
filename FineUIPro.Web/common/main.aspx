﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="FineUIPro.Web.common.main" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首页</title>
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
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel2" />
    <f:Panel ID="Panel2" runat="server" ShowBorder="True" Layout="VBox" ShowHeader="false"
        BodyPadding="2">
        <Items>
             <f:Panel ID="Panel1" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false"
                Layout="HBox" BoxConfigChildMargin="0 2 2 0" MarginBottom="2">
                <Items >       
                    <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Right" RegionSplit="true"
                        EnableCollapse="false" ShowBorder="true" Title="左侧显示区" Layout="VBox" 
                        ShowHeader="false" AutoScroll="true" BodyPadding="2px" IconFont="ArrowCircleLeft">
                        <Items>                            
                            <f:Form BodyPadding="1px" ID="formNotice" EnableCollapse="false" Layout="VBox" runat="server" TitleToolTip="近三个月通知通告"
                                Title="通知通告" IconFont="Tag" ShowHeader="true" ShowBorder="false" AutoScroll="true">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:Grid ID="GridNotice" ShowBorder="false" ShowHeader="false" Title="通知通告" EnableCollapse="true"
                                                ShowGridHeader="false" runat="server" BoxFlex="1" DataKeyNames="NoticesId" AllowCellEditing="true"
                                                EnableColumnLines="true" ClicksToEdit="2" DataIDField="NoticesId" EnableRowDoubleClickEvent="true"
                                                OnRowDoubleClick="GridNotice_RowDoubleClick" SortField="ReleaseTime" SortDirection="DESC">
                                                <Columns>
                                                   <%-- <f:RenderField MinWidth="95px" ColumnID="Type" DataField="Type" FieldType="String"
                                                        HeaderText="类别" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>--%>
                                                    <f:RenderField MinWidth="300px"  ColumnID="Title" DataField="Title" FieldType="String"
                                                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                    </f:RenderField>
                                                    <f:RenderField MinWidth="90px"  ColumnID="ReleaseTime" DataField="ReleaseTime" FieldType="Date" Renderer="Date"
                                                        RendererArgument="yyyy-MM-dd" HeaderText="日期" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField Width="1px" ColumnID="Url" DataField="Url" FieldType="String" HeaderText="路径"
                                                        Hidden="true" HeaderTextAlign="Center">                                        
                                                    </f:RenderField>
                                                </Columns>
                                                <Listeners>
                                                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu2" />
                                                </Listeners>
                                            </f:Grid>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                            <f:Form BodyPadding="1px" ID="formPicture" EnableCollapse="false" Layout="VBox" runat="server"
                                Title="新闻动态" IconFont="Tag" ShowHeader="false"  ShowBorder="false">
                                <Rows>
                                    <f:FormRow Margin="5 0 0 0" Height="330px">
                                        <Items>
                                            <f:ContentPanel ID="picContent" runat="server" ShowHeader="false" Margin="5 0 0 0" AutoScroll="true" Height="320px">
                                               <script type="text/javascript">
                                                   var focus_width = 500    
                                                   var focus_height = 285
                                                   var text_height = 15
                                                   var swf_height = focus_height + text_height+10
                                                   var texts = "<%=texts%>"
                                                   var pics = "<%=pics%> "
                                                   var links = "<%=links%>"
                                                   document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="' + focus_width + '" height="' + swf_height + '">');
                                                   document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="../Images/focus.swf"><param name=wmode value=transparent><param name="quality" value="high">');
                                                   document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
                                                   document.write('<param name="FlashVars" value="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '">');
                                                   document.write('<embed src="../Images/focus.swf" wmode="opaque" FlashVars="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '" menu="false" bgcolor="#DADADA" quality="high" width="' + focus_width + '" height="' + swf_height + '" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />');
                                                   document.write('</object>');
                                               </script>
                                             </f:ContentPanel> 
                                        </Items>
                                     </f:FormRow>                          
                                   </Rows>
                               </f:Form>
                        </Items>
                      </f:Panel>             
                    <f:Panel runat="server" ID="panelRightRegion" RegionPosition="Left" RegionSplit="true" Layout="VBox"   
                        EnableCollapse="false" Title="右边显示区" ShowBorder="true" ShowHeader="false"
                        AutoScroll="true" BodyPadding="2px" IconFont="ArrowCircleRight">
                        <Items>
                         <%--<f:Form BodyPadding="1px" ID="formToDoMatter" EnableCollapse="false" Layout="VBox" runat="server"
                                Title="待办事项" IconFont="Tag" ShowHeader="true" ShowBorder="false" AutoScroll="true">
                                <Rows>
                                    <f:FormRow Margin="2 2 2 2">
                                        <Items>
                                          <f:Grid ID="GridToDoMatter" ShowBorder="false" ShowHeader="false" Title="待办事项" EnableCollapse="true"
                                                ShowGridHeader="false" runat="server" BoxFlex="1" DataKeyNames="Id" AllowCellEditing="true"
                                                EnableColumnLines="true" ClicksToEdit="2" DataIDField="Id" EnableRowDoubleClickEvent="true"
                                                OnRowDoubleClick="GridToDoMatter_RowDoubleClick" SortField="Date" SortDirection="DESC">
                                                <Columns>
                                                    <f:RenderField MinWidth="130px" ColumnID="Type" DataField="Type" FieldType="String"
                                                        HeaderText="类别" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField MinWidth="300px" ColumnID="Name" DataField="Name" FieldType="String"
                                                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                    </f:RenderField>
                                                    <f:RenderField MinWidth="90px" ColumnID="Date" DataField="Date" FieldType="Date" Renderer="Date"
                                                        RendererArgument="yyyy-MM-dd" HeaderText="日期" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField Width="1px" ColumnID="Url" DataField="Url" FieldType="String" HeaderText="路径"
                                                        Hidden="true" HeaderTextAlign="Center">                                        
                                                    </f:RenderField>
                                                </Columns>
                                                <Listeners>
                                                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu3" />
                                                </Listeners>
                                            </f:Grid>
                                       </Items>
                                     </f:FormRow>                          
                                 </Rows>
                          </f:Form> --%>
                         <f:Form BodyPadding="1px" ID="formHSSEStandard"  Layout="VBox" runat="server" 
                                Title="专家辅助" IconFont="Tag" ShowHeader="true" ShowBorder="false" AutoScroll="true"  EnableTableStyle="true">
                            <Rows>
                                <f:FormRow>                    
                                    <Items>
                                        <f:Grid ID="GridHSSEStandard" ShowBorder="false" ShowHeader="false" Title="专家辅助" EnableCollapse="true"
                                            ShowGridHeader="false" runat="server" BoxFlex="1" DataKeyNames="ManagedItemId" AllowCellEditing="false"
                                            EnableColumnLines="true" ClicksToEdit="2" DataIDField="ManagedItemId" EnableRowDoubleClickEvent="true"
                                            OnRowDoubleClick="GridHSSEStandard_RowDoubleClick" 
                                            SortField="SpecialtyCode,StandardCode,ManagedObjectCode,ManagedItemCode" SortDirection="DESC">
                                            <Toolbars>
                                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                                                <Items>
                                                    <f:TextBox ID="txtStandard" runat="server" EmptyText="输入查询信息"
                                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged"  LabelAlign="Right" Width="500">
                                                    </f:TextBox>   
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                            <Columns>
                                               <f:RenderField MinWidth="110px" ColumnID="SpecialtyName" DataField="SpecialtyName" FieldType="String"
                                                    HeaderText="专业" HeaderTextAlign="Center" TextAlign="Left">
                                                </f:RenderField>
                                                <f:RenderField MinWidth="140px" ColumnID="StandardName" DataField="StandardName" FieldType="String"
                                                    HeaderText="标准" HeaderTextAlign="Center" TextAlign="Left" >
                                                </f:RenderField>
                                                 <f:RenderField MinWidth="140px" ColumnID="ManagedObjectName" DataField="ManagedObjectName" FieldType="String"
                                                    HeaderText="管理对象" HeaderTextAlign="Center" TextAlign="Left">
                                                </f:RenderField>
                                                 <f:RenderField MinWidth="130px" ColumnID="ManagedItemName" DataField="ManagedItemName" FieldType="String"
                                                    HeaderText="管理项目" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                </f:RenderField>
                                            </Columns>
                                            <Listeners>
                                                <f:Listener Event="dataload" Handler="onGridDataLoadHSSEStandard" />
                                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu1" />
                                            </Listeners>
                                        </f:Grid>                                            
                                    </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                    </f:Panel>                                     
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="详细页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        Width="1300px" Height="640px"  CloseAction="HidePostBack">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuOpen1" OnClick="btnMenuOpen1_Click" EnablePostBack="true"
            runat="server" Text="展开" Icon="ControlAddBlue">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuClose1" OnClick="btnMenuClose1_Click" EnablePostBack="true"
            runat="server" Text="收起" Icon="ControlRemoveBlue">
        </f:MenuButton>
    </f:Menu>
   <f:Menu ID="Menu2" runat="server">
        <f:MenuButton ID="btnMenuOpen2" OnClick="btnMenuOpen2_Click" EnablePostBack="true"
            runat="server" Text="展开" Icon="ControlAddBlue">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuClose2" OnClick="btnMenuClose2_Click" EnablePostBack="true"
            runat="server" Text="收起" Icon="ControlRemoveBlue">
        </f:MenuButton>
    </f:Menu>
    <f:Menu ID="Menu3" runat="server">
        <f:MenuButton ID="btnMenuOpen3" OnClick="btnMenuOpen3_Click" EnablePostBack="true"
            runat="server" Text="展开" Icon="ControlAddBlue">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuClose3" OnClick="btnMenuClose3_Click" EnablePostBack="true"
            runat="server" Text="收起" Icon="ControlRemoveBlue">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
        var menuID1 = '<%= Menu1.ClientID %>';
        var menuID2 = '<%= Menu2.ClientID %>';
        var menuID3 = '<%= Menu3.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu1(event, rowId) {
            F(menuID1).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function onRowContextMenu2(event, rowId) {
            F(menuID2).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function onRowContextMenu3(event, rowId) {
            F(menuID3).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGridHSSEStandard() {
            __doPostBack(null, 'reloadGridHSSEStandard');
        }
        function reloadGridNotice() {
            __doPostBack(null, 'reloadGridNotice');
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
        function onGridDataLoadHSSEStandard(event) {
            this.mergeColumns(['SpecialtyName'], { depends: true });
            this.mergeColumns(['StandardName'], { depends: true });
            this.mergeColumns(['ManagedObjectName'], { depends: true });
        }
    </script>
    <script type="text/javascript">
        var panelLeftRegionClientID = '<%= panelLeftRegion.ClientID %>';
        var panelRightRegionClientID = '<%= panelRightRegion.ClientID %>';
        
        var formNoticeClientID = '<%= formNotice.ClientID %>';
        var formPictureClientID = '<%= formPicture.ClientID %>'; 
      // <%-- var formToDoMatterClientID = '<%= formToDoMatter.ClientID %>';--%>
        var formHSSEStandardClientID = '<%= formHSSEStandard.ClientID %>';
        F.ready(function () {

            var panelLeftRegion = F(panelLeftRegionClientID);
            var panelRightRegion = F(panelRightRegionClientID);

            var formNotice = F(formNoticeClientID);
            var formPicture = F(formPictureClientID);
           // var formToDoMatter = F(formToDoMatterClientID);
            var formHSSEStandard = F(formHSSEStandardClientID);

            function resetpanelLeftRegionHeight() {
                var bodyWidth = this.form1.clientWidth;
                // 改变高度
                panelLeftRegion.setWidth(bodyWidth / 2 -50);
                panelRightRegion.setWidth(bodyWidth / 2 + 50);

                var height = $(window).height();
                formNotice.setHeight(height - 300 - 60);
                // formPicture.setHeight(height);
              //  formToDoMatter.setHeight((height - 100) / 2);
                formHSSEStandard.setHeight((height - 25));
            }

            // 页面加载完毕后，首先对窗体进行高度设置
            resetpanelLeftRegionHeight();
            // 页面大小改变事件
            F.windowResize(function () {
                resetpanelLeftRegionHeight();
            });
        });

    </script>  
</body>
</html>

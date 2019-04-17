<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JHAEdit.aspx.cs" Inherits="FineUIPro.Web.Hazard.JHAEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑JHA评价</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
         .f-grid-row.Blue {
             background-color: blue;
         }
         .f-grid-row.Yellow {
             background-color: yellow;
         }
         .f-grid-row.Orange {
             background-color: orange;
         }
         .f-grid-row.Red {
             background-color: red;
         } 
            .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }  
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Layout="VBox" 
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>           
            <f:FormRow runat="server" ID="fRow0">
                <Items>                   
                   <f:TextBox Label="作业活动" runat="server" ID="txtJobActivityName" Readonly="true" LabelWidth="120px">
                     </f:TextBox>
                     <f:TextBox Label="编号" runat="server" ID="txtJobActivityCode" Readonly="true" LabelWidth="120px">
                     </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox Label="装置" runat="server" ID="txtInstallation" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox Label="区域单元" runat="server" ID="txtWorkArea" Readonly="true" LabelWidth="120px">
                    </f:TextBox>                         
                </Items>
            </f:FormRow>                         
            <f:FormRow>
                <Items>
                      <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="JHA评价" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="JHAItemId" AllowCellEditing="true" EnableColumnLines="true"
                        ClicksToEdit="2" DataIDField="JHAItemId" AllowSorting="true" SortField="SortIndex"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="false" IsDatabasePaging="true"
                        PageSize="1000" EnableRowDoubleClickEvent="true" Height="400"
                        OnRowDoubleClick="Grid1_RowDoubleClick" Width="900px" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>                                      
                                    <f:Button ID="btnAdd" ToolTip="新增" Icon="Add" ValidateForms="SimpleForm1" runat="server" OnClick="btnAdd_Click" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnMenuADD" ToolTip="多步增加" Icon="ControlAddBlue" ValidateForms="SimpleForm1" runat="server" 
                                        OnClick="btnMenuADD_Click" Hidden="true">
                                    </f:Button>
                                     <f:Button ID="btnSubmit" Icon="SystemSaveNew" ToolTip="提交" runat="server" ValidateForms="SimpleForm1"
                                        Hidden="true" OnClick="btnSubmit_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>      
                        <f:RenderField Width="50px" ColumnID="SortIndex" DataField="SortIndex" FieldType="Int"
                            HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="JobStep" DataField="JobStep" FieldType="String"
                            HeaderText="工作步骤" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>                        
                        <f:RenderField Width="220px" ColumnID="PossibleAccidents" DataField="PossibleAccidents" FieldType="String"
                            HeaderText="危害或潜在事件" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                       <f:RenderField Width="220px" ColumnID="NowControlMeasures" DataField="NowControlMeasures" FieldType="String"
                            HeaderText="现有控制措施" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="HazardJudge_L" DataField="HazardJudge_L" FieldType="String"
                            HeaderText="L" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="HazardJudge_S" DataField="HazardJudge_S" FieldType="String"
                            HeaderText="S" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="HazardJudge_R" DataField="HazardJudge_R" FieldType="String"
                            HeaderText="R" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="RiskLevelName" DataField="RiskLevelName" FieldType="String"
                            HeaderText="风险等级" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                         <f:RenderField Width="200px" ColumnID="ControlMeasures" DataField="ControlMeasures" FieldType="String"
                            HeaderText="工程技术措施" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                       <f:RenderField Width="200px" ColumnID="ManagementMeasures" DataField="ManagementMeasures" FieldType="String"
                            HeaderText="管理措施" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="ProtectiveMeasures" DataField="ProtectiveMeasures" FieldType="String"
                            HeaderText="防护措施" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="OtherMeasures" DataField="OtherMeasures" FieldType="String"
                            HeaderText="其他措施" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>                       
                    </Columns> 
                    <Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>
                  </f:Grid>
                </Items>
            </f:FormRow>          
        </Rows>       
    </f:Form>
    <f:Window ID="Window1" Title="信息维护页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        CloseAction="HidePostBack" Width="1000px" Height="540px" ShowHeader="false">
    </f:Window>
    <f:Window ID="Window2" Title="信息维护页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        CloseAction="HidePostBack" Width="800px" Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除"
            Icon="Delete">
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
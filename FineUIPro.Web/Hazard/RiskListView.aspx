<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RiskListView.aspx.cs" Inherits="FineUIPro.Web.Hazard.RiskListView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>风险信息库</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />      
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%">
            <Items>                
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="540px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="2px" EnableTabCloseMenu="false" runat="server" MarginRight="5px"
                    ActiveTabIndex="0">              
                    <Tabs>     
                        <f:Tab ID="Tab2" Title="风险信息" BodyPadding="5px" Layout="HBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Layout="VBox" 
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
                                    <Rows>                      
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox Label="所属单位" runat="server" ID="txtInstallationName" Readonly="true" LabelWidth="140px">
                                                </f:TextBox>
                                                <f:TextBox Label="设备设施作业活动" runat="server" ID="txtTaskActivity" Readonly="true" LabelWidth="160px">
                                                </f:TextBox>  
                                            </Items>
                                        </f:FormRow>              
                                        <f:FormRow>
                                            <Items>   
                                               <f:TextBox Label="风险点" ID="txtHazardDescription" runat="server" Readonly="true" LabelWidth="140px">
                                                </f:TextBox>   
                                            </Items>
                                        </f:FormRow>
                                         <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtPossibleAccidents" runat="server" Label="危害或潜在事件" Readonly="true" LabelWidth="140px">
                                                </f:TextBox> 
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                 <f:TextBox ID="txtRiskLevelName" runat="server" Label="评价级别" Readonly="true" LabelWidth="140px">
                                                </f:TextBox>
                                                 <f:TextBox ID="txtEvaluationMethod" runat="server" Label="评价方法" Readonly="true" LabelWidth="140px">
                                                </f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                         <f:FormRow>
                                            <Items>                   
                                                <f:TextArea ID="txtControlMeasures" runat="server" Label="工程技术措施" Readonly="true" LabelWidth="140px" Height="60px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                         <f:FormRow>
                                            <Items>                   
                                                <f:TextArea ID="txtManagementMeasures" runat="server" Label="管理措施" Readonly="true" LabelWidth="140px" Height="60px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                         <f:FormRow>
                                            <Items>                   
                                                <f:TextArea ID="txtProtectiveMeasures" runat="server" Label="防护措施" Readonly="true" LabelWidth="140px" Height="60px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                         <f:FormRow>
                                            <Items>                   
                                                <f:TextArea ID="txtOtherMeasures" runat="server" Label="其他措施" Readonly="true" LabelWidth="140px" Height="60px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                         <f:FormRow>
                                            <Items>   
                                                 <f:DropDownList ID="drpRiskOwnerIds" runat="server" Label="风险责任人" LabelWidth="140px" EnableMultiSelect="true"
                                                     EnableCheckBoxSelect="true" EnableEdit="true" MaxLength="4000" Hidden="true">
                                                </f:DropDownList>
                                                <f:CheckBox runat="server" ID="ckIsUsed" Label="启用" LabelWidth="140px"></f:CheckBox>
                                                 <f:DatePicker runat="server" Label="启用时间" ID="dpkStartDate" 
                                                    LabelWidth="140px" DateFormatString="yyyy-MM-dd hh:mm:ss">
                                                </f:DatePicker>
                                                <f:TextBox ID="txtQRCodePosition"  runat="server" Label="二维码位置"  LabelWidth="140px" MaxLength="200">
                                                </f:TextBox>
                                            </Items>
                                        </f:FormRow>        
                                    </Rows>
                                    <Toolbars>                       
                                        <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>                                                    
                                                <f:ToolbarFill runat="server"></f:ToolbarFill>    
                                                <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
                                                    OnClick="btnSave_Click" MarginRight="10px">
                                                </f:Button>             
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                </f:Form>
                            </Items>             
                        </f:Tab>  
                        <f:Tab ID="Tab1" Title="风险责任人" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                                    EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="RiskItemId" 
                                    DataIDField="RiskItemId" AllowSorting="true" SortField="RiskOwnerName" SortDirection="ASC" AllowPaging="true"  
                                    EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">   
                                     <Toolbars>                       
                                        <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>       
                                                <f:DropDownList ID="drpRiskOwner" runat="server" Label="责任人" EnableEdit="true" LabelWidth="70px"
                                                    Required="true" ShowRedStar="true">
                                                </f:DropDownList>
                                                 <f:NumberBox ID="txtFrequency" runat="server" Label="巡检频率(天/次)"  Required="true" ShowRedStar="true"
                                                     NoDecimal="true" NoNegative="true" LabelWidth="140px">
                                                 </f:NumberBox>
                                                <f:ToolbarFill runat="server"></f:ToolbarFill>    
                                                <f:Button ID="btnSure" Icon="Accept" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
                                                    OnClick="btnSure_Click" ToolTip="确认">
                                                </f:Button>     
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="120px"
                                             HeaderTextAlign="Center" TextAlign="Center"/>
                                        <f:RenderField Width="200px" ColumnID="RiskOwnerName" DataField="RiskOwnerName"
                                            FieldType="String" HeaderText="责任人"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="WorkPostName" DataField="WorkPostName" ExpandUnusedSpace="true"
                                            FieldType="String" HeaderText="岗位"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                         <f:RenderField Width="150px" ColumnID="Frequency" DataField="Frequency" 
                                            FieldType="Int" HeaderText="巡检频率"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                    </Columns>
                                      <Listeners>
                                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                     </Listeners>
                                </f:Grid>
                            </Items>             
                        </f:Tab>
                        <f:Tab ID="Tab3" Title="岗位巡检人" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                                    EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="RiskItemId" 
                                    DataIDField="RiskItemId" AllowSorting="true" SortField="RiskOwnerName" SortDirection="ASC" AllowPaging="true"  
                                    EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">                                       
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="120px"
                                             HeaderTextAlign="Center" TextAlign="Center"/>
                                        <f:RenderField Width="200px" ColumnID="RiskOwnerName" DataField="RiskOwnerName" 
                                            FieldType="String" HeaderText="责任人"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField> 
                                        <f:RenderField Width="200px" ColumnID="WorkPostName" DataField="WorkPostName" ExpandUnusedSpace="true"
                                            FieldType="String" HeaderText="岗位"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                         <f:RenderField Width="150px" ColumnID="Frequency" DataField="Frequency" 
                                            FieldType="Int" HeaderText="巡检频率"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                    </Columns>                                  
                                </f:Grid>
                            </Items>             
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
              </Items> 
        </f:Panel>
         <f:Menu ID="Menu1" runat="server">
              <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                Hidden="true" runat="server" Text="修改" Icon="TableEdit">
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

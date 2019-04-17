<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FireWorkView.aspx.cs"
    Inherits="FineUIPro.Web.License.FireWorkView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>动火安全作业证</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style>
        .formtitle .f-field-body {
            text-align: center;           
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" TitleAlign="Center"
        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>
             <f:FormRow ColumnWidths="75% 25%">
                 <Items>
                     <f:Label runat="server"></f:Label>
                    <f:Label ID="lbStandard" runat="server" Text="执行标准：GB-30871-2014">
                    </f:Label>
                 </Items>
             </f:FormRow> 
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtApplyUnit" runat="server" Label="申请单位" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                      <f:TextBox ID="txtApplyManName" runat="server" Label="申请人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtLicenseCode" runat="server" Label="作业证编号" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:CheckBoxList ID="cbFireWorkLevel" runat="server" Readonly="true" Label="动火作业级别" LabelWidth="120px">
                        <f:CheckItem Text="特级动火作业" Value="0"/>
                        <f:CheckItem Text="一级动火作业" Value="1"/>
                        <f:CheckItem Text="二级动火作业" Value="2"/>
                    </f:CheckBoxList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtFireWorkPalce" runat="server" Label="动火地点" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow> 
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtFireWorkModeName" runat="server" Label="动火方式" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtStartDate" runat="server" Label="动火时间" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtFireHeaderName" runat="server" Label="动火作业负责人" Readonly="true" LabelWidth="130px">
                    </f:TextBox>
                     <f:TextBox ID="txtFireManName" runat="server" Label="动火人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="动火分析" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="FireWorkAnalysisId" EnableColumnLines="true"
                         DataIDField="FireWorkAnalysisId" AllowSorting="true" SortField="AnalysisTime,SortIndex"
                        SortDirection="ASC" AllowPaging="false" Width="980px">                       
                        <Columns>                                             
                            <f:RenderField Width="160px" ColumnID="AnalysisTime" DataField="AnalysisTime" FieldType="String"
                                HeaderText="动火分析时间" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="AnalysisPoint" DataField="AnalysisPoint" FieldType="String"
                                HeaderText="动火点名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="AnalysisData" DataField="AnalysisData" FieldType="String"
                                HeaderText="分析数据" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="AnalysisManName" DataField="AnalysisManName" FieldType="String"
                                HeaderText="分析人" HeaderTextAlign="Center" TextAlign="Left" >
                            </f:RenderField>         
                        </Columns>
                    </f:Grid>
                </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtOtherSpecial" runat="server" Label="涉及的其他特殊作业" Readonly="true" LabelWidth="160px">
                    </f:TextBox>                   
                 </Items>
             </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:TextArea ID="txtHAZIDName" runat="server" Label="危害辨识" Readonly="true" LabelWidth="160px" Height="40px">
                    </f:TextArea>                   
                 </Items>
             </f:FormRow>
           <f:FormRow ColumnWidths="10% 70% 20%">
                 <Items>
                    <f:Label ID="序号" runat="server" Text="序号" Readonly="true">
                    </f:Label>
                    <f:Label ID="安全措施" runat="server" Text="安全措施" Readonly="true" >
                    </f:Label>
                    <f:Label ID="确认人" runat="server" Text="确认人" Readonly="true" >
                    </f:Label>                     
                 </Items>
             </f:FormRow>           
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed1" runat="server" Readonly="true" Text="1">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures1" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName1" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed2" runat="server" Readonly="true" Text="2">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures2" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName2" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed3" runat="server" Readonly="true" Text="3">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures3" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName3" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed4" runat="server" Readonly="true" Text="4">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures4" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName4" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed5" runat="server" Readonly="true" Text="5">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures5" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName5" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed6" runat="server" Readonly="true" Text="6">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures6" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName6" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed7" runat="server" Readonly="true" Text="7">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures7" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName7" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed8" runat="server" Readonly="true" Text="8">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures8" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName8" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed9" runat="server" Readonly="true" Text="9">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures9" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName9" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>                   
                    <f:CheckBox ID="txtSIsUsed10" runat="server" Readonly="true" Text="10">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures10" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName10" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtProduceUnitManName" runat="server" Label="生产单位负责人" Readonly="true" LabelWidth="130px">
                    </f:TextBox>
                    <f:TextBox ID="txtFireWatchName" runat="server" Label="监火人" Readonly="true" LabelWidth="130px">
                    </f:TextBox>
                     <f:TextBox ID="txtFireFirstManName" runat="server" Label="动火初审人" Readonly="true" LabelWidth="130px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtSafeEduManName" runat="server" Label="实施安全教育人" Readonly="true" LabelWidth="130px">
                    </f:TextBox>
                    <f:Label ID="TextBox33" runat="server"  Readonly="true" >
                    </f:Label>
                     <f:Label ID="TextBox34" runat="server" Readonly="true">
                    </f:Label>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                   <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="申请单位意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                 <f:TextArea ID="txtApplyUnitManOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="40% 30% 30%">
                            <Items>
                                  <f:Label ID="Label6" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtApplyUnitManName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtApplyUnitManTime" runat="server" Text="年月日时分">
                                  </f:Label>
                            </Items>
                        </f:FormRow>
                     </Rows>
                    </f:Form>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                   <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="安全管理部门意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:TextArea ID="txtSafeDepManOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="40% 30% 30%">
                            <Items>
                                  <f:Label ID="Label9" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtSafeDepManName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtSafeDepManTime" runat="server" Text="年月日时分">
                                  </f:Label>
                            </Items>
                        </f:FormRow>
                     </Rows>
                    </f:Form>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                   <f:Form ID="Form4" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="动火审批人意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:TextArea ID="txtFireApprovalManOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="40% 30% 30%">
                            <Items>
                                  <f:Label ID="Label12" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtFireApprovalManName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtFireApprovalManTime" runat="server" Text="年月日时分">
                                  </f:Label>
                            </Items>
                        </f:FormRow>
                     </Rows>
                    </f:Form>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:Form ID="Form5" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="动火前，岗位当班班长验票">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtInspectTicketManOpinion" runat="server"  Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label15" runat="server" >
                                    </f:Label>
                                    <f:Label ID="txtInspectTicketManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtInspectTicketManTime" runat="server" Text="年月日时分">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
              <Items>
                <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="完工验收">
                <Rows>
                    <f:FormRow>
                        <Items>
                          <f:TextArea ID="txtFireWorkManOpinion" runat="server"  Readonly="true" Height="50px">
                          </f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow ColumnWidths="40% 30% 30%">
                        <Items>
                            <f:Label ID="Label18" runat="server" >
                            </f:Label>
                            <f:Label ID="txtFireWorkManName" runat="server" Label="签字">
                            </f:Label>
                            <f:Label ID="txtFireWorkManTime" runat="server" Text="年月日时分">
                            </f:Label>
                        </Items>
                    </f:FormRow>
                    </Rows>
                </f:Form>
             </Items>
           </f:FormRow>
          <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" >
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <%--<Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>--%>
    </f:Form>
    </form>
</body>
</html>

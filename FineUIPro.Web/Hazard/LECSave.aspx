<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LECSave.aspx.cs" Inherits="FineUIPro.Web.Hazard.LECSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑LEC评估</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />      
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%">
            <Items>                
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="480px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="2px" EnableTabCloseMenu="false" runat="server" 
                    ActiveTabIndex="0">              
                    <Tabs>     
                        <f:Tab ID="Tab2" Title="LEC评价" BodyPadding="5px" Layout="HBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
                                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                         <f:FormRow>
                                            <Items>
                                                <f:NumberBox runat="server" ID="txtSortIndex" NoDecimal="true" NoNegative="true" Label="排序" LabelWidth="120px"></f:NumberBox>
                                                <f:DropDownList ID="drpRiskLevelId" runat="server" Label="风险等级" ShowRedStar="true" Required="true"
                                                    LabelWidth="120px" Readonly="true">
                                                </f:DropDownList>                    
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                 <f:TextArea ID="txtHazardDescription" runat="server" Label="危险源描述" ShowRedStar="true" Required="true"
                                                     MaxLength="500" LabelWidth="120px" Height="50px" FocusOnPageLoad="true">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow>            
                                        <f:FormRow>
                                            <Items>
                                                 <f:TextArea ID="txtPossibleAccidents" runat="server" Label="危害或潜在事件" MaxLength="200" LabelWidth="120px" Height="50px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow>     
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="drpHazardJudge_L" runat="server" Label="L" LabelWidth="120px" ShowRedStar="true" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="txtHazardJudge_TextChanged" EnableEdit="true">
                                                    <f:ListItem Text="请选择" Value="0" />
                                                    <f:ListItem Text="完全可以预料（10）" Value="10.0" />
                                                    <f:ListItem Text="相当可能（6）" Value="6.0" />
                                                    <f:ListItem Text="可能，但不经常（3）" Value="3.0" />
                                                    <f:ListItem Text="可能性小，完全意外（1）" Value="1.0" />
                                                    <f:ListItem Text="很不可能，可以设想（0.5）" Value="0.5" />
                                                    <f:ListItem Text="极不可能（0.2）" Value="0.2" />
                                                    <f:ListItem Text="实际不可能（0.1）" Value="0.1" />
                                                </f:DropDownList>   
                                                <f:DropDownList ID="drpHazardJudge_E" runat="server" Label="E" LabelWidth="120px" ShowRedStar="true" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="txtHazardJudge_TextChanged" EnableEdit="true">
                                                    <f:ListItem Text="请选择" Value="0" />
                                                    <f:ListItem Text="连续暴露（10）" Value="10.0" />
                                                    <f:ListItem Text="每天工作时间内暴露（6）" Value="6.0" />
                                                    <f:ListItem Text="每周一次暴露（3）" Value="3.0" />
                                                    <f:ListItem Text="每月一次暴露（2）" Value="2.0" />
                                                    <f:ListItem Text="每年一次暴露（1）" Value="1.0" />
                                                    <f:ListItem Text="非常罕见地暴露（0.5）" Value="0.5" />
                                                </f:DropDownList>  
                                            </Items>
                                        </f:FormRow>
                                         <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="drpHazardJudge_C" runat="server" Label="C" LabelWidth="120px" ShowRedStar="true" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="txtHazardJudge_TextChanged" EnableEdit="true">
                                                    <f:ListItem Text="请选择" Value="0" />
                                                    <f:ListItem Text="大灾难，许多人死亡（100）" Value="100.0" />
                                                    <f:ListItem Text="灾难，数人死亡（40）" Value="40.0" />
                                                    <f:ListItem Text="非常严重，一人死亡（15）" Value="15.0" />
                                                    <f:ListItem Text="重伤或较重危害（7）" Value="7.0" />
                                                    <f:ListItem Text="轻伤或一般危害（3）" Value="3.0" />
                                                    <f:ListItem Text="轻微危害或不利于基本的安全卫生要求（1）" Value="1.0" />
                                                </f:DropDownList> 
                                                <f:NumberBox runat="server" ID="txtHazardJudge_D" Label="D" NoNegative="true" 
                                                    DecimalPrecision="1" LabelWidth="120px" Readonly="true" ShowRedStar="true" Required="true"></f:NumberBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                 <f:TextArea ID="txtControlMeasures" runat="server" LabelWidth="120px" Label="工程技术措施" MaxLength="800" Height="40px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow> 
                                        <f:FormRow>
                                            <Items>
                                                 <f:TextArea ID="txtManagementMeasures" runat="server" LabelWidth="120px" Label="管理措施" MaxLength="800" Height="40px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow> 
                                         <f:FormRow>
                                            <Items>
                                                 <f:TextArea ID="txtProtectiveMeasures" runat="server" LabelWidth="120px" Label="防护措施" MaxLength="800" Height="40px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow> 
                                         <f:FormRow>
                                            <Items>
                                                 <f:TextArea ID="txtOtherMeasures" runat="server" LabelWidth="120px" Label="其他措施" MaxLength="800" Height="40px">
                                                </f:TextArea>
                                            </Items>
                                        </f:FormRow> 
                                    </Rows>
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <f:Label runat="server"  ID="lbEuiqment"></f:Label>
                                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                                <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
                                                    OnClick="btnSave_Click" >
                                                </f:Button>
                                                <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose" MarginRight="10px">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                </f:Form>
                            </Items>             
                        </f:Tab>  
                        <f:Tab ID="Tab1" Title="评价记录" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                                    EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="LECItemRecordId" 
                                    DataIDField="LECItemRecordId" AllowSorting="true" SortField="EvaluationTime" SortDirection="DESC" AllowPaging="true"  
                                    EnableTextSelection="True" >                                   
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="120px"
                                             HeaderTextAlign="Center" TextAlign="Center"/>
                                        <f:RenderField Width="200px" ColumnID="EvaluatorName" DataField="EvaluatorName" 
                                            FieldType="String" HeaderText="评价人"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                         <f:RenderField Width="200px" ColumnID="EvaluationTime" DataField="EvaluationTime" ExpandUnusedSpace="true"
                                            FieldType="String" HeaderText="评价时间"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="RiskLevelName" DataField="RiskLevelName" 
                                            FieldType="String" HeaderText="风险等级"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>             
                        </f:Tab>                    
                    </Tabs>
                </f:TabStrip>
              </Items> 
        </f:Panel>    
    </form>
</body>
</html>

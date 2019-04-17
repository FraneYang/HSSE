<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SCLSave.aspx.cs" Inherits="FineUIPro.Web.Hazard.SCLSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑SCL评估</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />      
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%">
            <Items>                
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="520px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="2px" EnableTabCloseMenu="false" runat="server" 
                    ActiveTabIndex="0">              
                    <Tabs>     
                        <f:Tab ID="Tab2" Title="SCL评价" BodyPadding="5px" Layout="HBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                     <f:FormRow>
                                        <Items>
                                            <f:NumberBox runat="server" ID="txtSortIndex" NoDecimal="true" NoNegative="true" Label="排序" LabelWidth="120px"></f:NumberBox>
                                              <f:TextBox ID="txtCheckItem" runat="server" Label="检查项目" ShowRedStar="true" Required="true"
                                                 MaxLength="50" LabelWidth="120px"  FocusOnPageLoad="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                             <f:TextArea ID="txtStandard" runat="server" Label="标准" MaxLength="500" LabelWidth="120px" Height="40px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>          
                                    <f:FormRow>
                                        <Items>
                                             <f:TextArea ID="txtConsequence" runat="server" Label="未达标危害</br>或潜在事件" MaxLength="500" LabelWidth="120px" Height="40px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>   
                                     <f:FormRow>
                                        <Items>
                                             <f:TextArea ID="txtNowControlMeasures" runat="server" Label="现有控制措施" MaxLength="800" LabelWidth="120px"  Height="40px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>   
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpHazardJudge_L" runat="server" Label="L" LabelWidth="120px" ShowRedStar="true" 
                                                AutoPostBack="true" OnSelectedIndexChanged="txtHazardJudge_TextChanged" EnableEdit="true">
                                                <f:ListItem Text="请选择" Value="0" />
                                                <f:ListItem Text="(5)在现场没有采取防范、监测、保护、控制措施，或危害的发生不可能被发现（没有监测系统），或在正常情况下经常发生此类事故或事件。" Value="5.0" />
                                                <f:ListItem Text="(4)危害的发生不容易被发现，现场没有检测系统，也未进行过任何检测，或在现场有控制措施，但未有效执行或控制措施不当，或危害常发生或在预期情况下发生。" Value="4.0" />
                                                <f:ListItem Text="(3)没有保护措施（如没有保护装置、没有个人防护用品等），或未严格按操作程序执行，或危害的发生容易被发现（现场有监测系统），或曾经做过监测，或过去曾经发生类似事故或事件，或在异常情况下会发生类似事故或事件。" Value="3.0" />
                                                <f:ListItem Text="(2)危害一旦发生能及时发现，并定期进行监测，或现场有防范控制措施，并能有效执行，或过去偶尔发生事故或事件。" Value="2.0" />
                                                <f:ListItem Text="(1)有充分、有效的防范、控制、监测、保护措施，或员工安全卫生意识相当高，严格执行操作规程。极不可能发生事故或事件。" Value="1.0" />
                                            </f:DropDownList>   
                                          
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                             <f:TriggerBox ID="tbxHazardJudge_S" Label="S" LabelWidth="120px" ShowRedStar="true"  ShowLabel="true" Readonly="false" TriggerIcon="ArrowDown"
                                                OnTriggerClick="tbxHazardJudge_S_TriggerClick" EmptyText="请选择" runat="server" >
                                              </f:TriggerBox>                                                                                               
                                            <f:NumberBox runat="server" ID="txtHazardJudge_R" Label="R" NoNegative="true" 
                                                DecimalPrecision="1" LabelWidth="120px" Readonly="true" ShowRedStar="true" Required="true"></f:NumberBox>
                                             <f:DropDownList ID="drpRiskLevelId" runat="server" Label="风险等级" ShowRedStar="true" Required="true"
                                                LabelWidth="120px" Readonly="true">
                                            </f:DropDownList>  
                                        </Items>
                                    </f:FormRow>
                                     <f:FormRow>
                                        <Items>
                                             <f:TextArea ID="txtControlMeasures" runat="server" LabelWidth="120px" Label="工程技术措施" MaxLength="800"  Height="40px">
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
                                                OnClick="btnSave_Click">
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
                                    EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="SCLItemRecordId" 
                                    DataIDField="SCLItemRecordId" AllowSorting="true" SortField="EvaluationTime" SortDirection="DESC" AllowPaging="true"  
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
        <f:Window ID="WindowS" Title="危害后果严重性S的取值" BodyPadding="10px" IsModal="true" Hidden="true"
        Target="Top" Width="560px" Height="360px"
        runat="server">
        <Items>
             <f:Form ID="Form2" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableTableStyle="false"
                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                <Rows>
                    <f:FormRow>
                        <Items>
                              <f:DropDownList ID="drpHazardJudge_S1" runat="server" Label="法律法规及其他要求" EnableEdit="true" >              
                                <f:ListItem Text="违反法律、法规和标准(5)" Value="5.0" />
                                <f:ListItem Text="潜在违反法规和标准(4)" Value="4.0" />
                                <f:ListItem Text="不符合上级公司或行业的安全方针、制度、规定等(3)" Value="3.0" />
                                <f:ListItem Text="不符合公司的安全操作规程、规定(2)" Value="2.0" />
                                <f:ListItem Text="完全符合(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>  </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_S2" runat="server" Label="人员" EnableEdit="true">                
                                <f:ListItem Text="死亡(5)" Value="5.0" />
                                <f:ListItem Text="丧失劳动能力(4)" Value="4.0" />
                                <f:ListItem Text="截肢、骨折、听力丧失、慢性病(3)" Value="3.0" />
                                <f:ListItem Text="轻微受伤、间歇不舒服(2)" Value="2.0" />
                                <f:ListItem Text="无伤亡(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_S3" runat="server" Label="财产损失/万元" EnableEdit="true">
                                <f:ListItem Text="＞50(5)" Value="5.0" />
                                <f:ListItem Text="＞25(4)" Value="4.0" />
                                <f:ListItem Text="＞10(3)" Value="3.0" />
                                <f:ListItem Text="＜10(2)" Value="2.0" />
                                <f:ListItem Text="无损失(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>  </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_S4" runat="server" Label="停工" EnableEdit="true">
                               <f:ListItem Text="部分装置（＞2套）或设备停工(5)" Value="5.0" />
                                <f:ListItem Text="2套装置停工或设备停工(4)" Value="4.0" />
                                <f:ListItem Text="1套装置停工或设备(3)" Value="3.0" />
                                <f:ListItem Text="受影响不大，几乎不停工(2)" Value="2.0" />
                                <f:ListItem Text="没有停工(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_S5" runat="server" Label="公司形象" EnableEdit="true">
                               <f:ListItem Text="重大国际国内影响(5)" Value="5.0" />
                                <f:ListItem Text="行业内、省内影响(4)" Value="4.0" />
                                <f:ListItem Text="地区影响(3)" Value="3.0" />
                                <f:ListItem Text="公司及周边范围(2)" Value="2.0" />
                                <f:ListItem Text="形象没有受损(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                </Rows>
                 <Toolbars>
                     <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                        <Items>                           
                            <f:Button ID="btnCloseWindow" Text="确定" OnClick="btnCloseWindowS_Click" runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                 </Toolbars>
            </f:Form>
        </Items>
    </f:Window>
    </form>
</body>
</html>

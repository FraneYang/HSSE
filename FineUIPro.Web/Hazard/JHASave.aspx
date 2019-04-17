<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JHASave.aspx.cs" Inherits="FineUIPro.Web.Hazard.JHASave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑JHA评估</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />      
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%">
            <Items>                
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="500px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="2px" EnableTabCloseMenu="false" runat="server" 
                    ActiveTabIndex="0">              
                    <Tabs>     
                        <f:Tab ID="Tab2" Title="JHA评价" BodyPadding="5px" Layout="HBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                     <f:FormRow>
                                        <Items>
                                            <f:NumberBox runat="server" ID="txtSortIndex" NoDecimal="true" NoNegative="true" Label="排序" LabelWidth="90px"></f:NumberBox>
                                              <f:TextBox ID="txtJobStep" runat="server" Label="工作步骤" ShowRedStar="true" Required="true"
                                                 MaxLength="50" LabelWidth="90px"  FocusOnPageLoad="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>                                          
                                    <f:FormRow>
                                        <Items>
                                             <f:TextArea ID="txtPossibleAccidents" runat="server" Label="危害或潜在事件" MaxLength="500" LabelWidth="90px" Height="50px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>   
                                     <f:FormRow>
                                        <Items>
                                             <f:TextArea ID="txtNowControlMeasures" runat="server" Label="现有控制措施" MaxLength="800" LabelWidth="90px"  Height="50px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>   
                                    <f:FormRow>
                                        <Items>
                                            <f:TriggerBox ID="tbxHazardJudge_L" Label="L" LabelWidth="90px" ShowRedStar="true"  ShowLabel="true" Readonly="false" TriggerIcon="ArrowDown"
                                                OnTriggerClick="tbxHazardJudge_L_TriggerClick" EmptyText="请选择" runat="server" >
                                              </f:TriggerBox>
                                             <f:TriggerBox ID="tbxHazardJudge_S" Label="S" LabelWidth="90px" ShowRedStar="true"  ShowLabel="true" Readonly="false" TriggerIcon="ArrowDown"
                                                OnTriggerClick="tbxHazardJudge_S_TriggerClick" EmptyText="请选择" runat="server" >
                                              </f:TriggerBox>                                           
                                        </Items>
                                    </f:FormRow>
                                     <f:FormRow>
                                        <Items>                    
                                            <f:NumberBox runat="server" ID="txtHazardJudge_R" Label="R" NoNegative="true" 
                                                DecimalPrecision="1" LabelWidth="90px" Readonly="true" ShowRedStar="true" Required="true"></f:NumberBox>
                                             <f:DropDownList ID="drpRiskLevelId" runat="server" Label="风险等级" ShowRedStar="true" Required="true"
                                                LabelWidth="90px" Readonly="true">
                                            </f:DropDownList>  
                                        </Items>
                                    </f:FormRow>
                                     <f:FormRow>
                                        <Items>
                                             <f:TextArea ID="txtControlMeasures" runat="server" LabelWidth="90px" Label="工程技术措施" MaxLength="800"  Height="40px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow> 
                                    <f:FormRow>
                                        <Items>
                                                <f:TextArea ID="txtManagementMeasures" runat="server" LabelWidth="90px" Label="管理措施" MaxLength="800" Height="40px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow> 
                                        <f:FormRow>
                                        <Items>
                                                <f:TextArea ID="txtProtectiveMeasures" runat="server" LabelWidth="90px" Label="防护措施" MaxLength="800" Height="40px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow> 
                                        <f:FormRow>
                                        <Items>
                                                <f:TextArea ID="txtOtherMeasures" runat="server" LabelWidth="90px" Label="其他措施" MaxLength="800" Height="40px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow> 
                                </Rows>
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                        <Items>
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
                                    EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="JHAItemRecordId" 
                                    DataIDField="JHAItemRecordId" AllowSorting="true" SortField="EvaluationTime" SortDirection="DESC" AllowPaging="true"  
                                    EnableTextSelection="True" >                                   
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="90px"
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
        <f:Window ID="WindowL" Title="危害导致事故发生的可能性（L）的取值" BodyPadding="10px" IsModal="true" Hidden="true"
        Target="Top" Width="700px" Height="360px"
        runat="server">
        <Items>
             <f:Form ID="Form2" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableTableStyle="false"
                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                <Rows>
                    <f:FormRow>
                        <Items>
                              <f:DropDownList ID="drpHazardJudge_L1" runat="server" Label="偏差发生频率" EnableEdit="true" LabelWidth="150px">              
                                <f:ListItem Text="在正常情况下经常发生此类事故或事件(5)" Value="5.0" />
                                <f:ListItem Text="危害常发生或在预期情况下发生(4)" Value="4.0" />
                                <f:ListItem Text="过去曾经发生或在异常情况下发生类似事故事件(3)" Value="3.0" />
                                <f:ListItem Text="过去偶尔发生危险事故事件(2)" Value="2.0" />
                                <f:ListItem Text="及不可能发生事故或事件(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                              </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_L2" runat="server" Label="管理措施" EnableEdit="true" LabelWidth="150px">                
                                <f:ListItem Text="从来没有检查，没有操作规程(5)" Value="5.0" />
                                <f:ListItem Text="偶尔检查或大检查，有操作规程，但只是偶尔执行（或操作规程内容不完善）(4)" Value="4.0" />
                                <f:ListItem Text="月检，有操作规程，只是部分执行(3)" Value="3.0" />
                                <f:ListItem Text="周检，有操作规程，但偶尔不执行(2)" Value="2.0" />
                                <f:ListItem Text="日检，有操作规程，且严格执行(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_L3" runat="server" Label="员工胜任程度（意识、技能、经验）" EnableEdit="true"  LabelWidth="150px">
                                <f:ListItem Text="不胜任（无任何培训、无经验、无上岗资格）(5)" Value="5.0" />
                                <f:ListItem Text="不够胜任（有上岗资格，但没有接受有效培训）(4)" Value="4.0" />
                                <f:ListItem Text="一般胜任（有培训、有上岗资格、但经验不足，多次出现差错）(3)" Value="3.0" />
                                <f:ListItem Text="胜任（但偶尔出现差错）(2)" Value="2.0" />
                                <f:ListItem Text="高度胜任（培训充分，经验丰富，意识强）(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                              </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_L4" runat="server" Label="监测、控制、报警、联锁、补救措施" EnableEdit="true" LabelWidth="150px">
                                <f:ListItem Text="无(5)" Value="5.0" />
                                <f:ListItem Text="不完善(4)" Value="4.0" />
                                <f:ListItem Text="有，但没有完全使用(3)" Value="3.0" />
                                <f:ListItem Text="有，偶尔失去作用或出差错(2)" Value="2.0" />
                                <f:ListItem Text="完全有效(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>                   
                </Rows>
                 <Toolbars>
                     <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:Button ID="btnCloseWindowL" Text="确定" OnClick="btnCloseWindowL_Click" runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                 </Toolbars>
            </f:Form>
        </Items>
    </f:Window>
        <f:Window ID="WindowS" Title="危害及影响后果的严重性（S）的取值" BodyPadding="10px" IsModal="true" Hidden="true"
        Target="Top" Width="520px" Height="360px"
        runat="server">
        <Items>
             <f:Form ID="Form3" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableTableStyle="false"
                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                <Rows>
                    <f:FormRow>
                        <Items>
                              <f:DropDownList ID="drpHazardJudge_S1" runat="server" Label="人员伤亡程度" EnableEdit="true" >              
                                <f:ListItem Text="●死亡●终身残废●丧失劳动能力(5)" Value="5.0" />
                                <f:ListItem Text="●部分丧失劳动能力●职业病●慢性病●住院治疗(4)" Value="4.0" />
                                <f:ListItem Text="需要去医院治疗，但不需住院(3)" Value="3.0" />
                                <f:ListItem Text="●皮外伤●短时间身体不适(2)" Value="2.0" />
                                <f:ListItem Text="没有受伤(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                      </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_S2" runat="server" Label="财产损失/万元" EnableEdit="true">                
                                <f:ListItem Text="≥50(5)" Value="5.0" />
                                <f:ListItem Text="≥25(4)" Value="4.0" />
                                <f:ListItem Text="＞10(3)" Value="3.0" />
                                <f:ListItem Text="＜10(2)" Value="2.0" />
                                <f:ListItem Text="无损失(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_S3" runat="server" Label="停工时间" EnableEdit="true">
                                <f:ListItem Text="三套以上装置停工(5)" Value="5.0" />
                                <f:ListItem Text="二套装置停工(4)" Value="4.0" />
                                <f:ListItem Text="一套装置停工(3)" Value="3.0" />
                                <f:ListItem Text="影响不大，装置局部停工(2)" Value="2.0" />
                                <f:ListItem Text="没有停工(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_S4" runat="server" Label="法规及规章制度符合状况" EnableEdit="true">
                                <f:ListItem Text="违反法律、法规和标准(5)" Value="5.0" />
                                <f:ListItem Text="潜在违反法律、法规和标准(4)" Value="4.0" />
                                <f:ListItem Text="不符合集团公司规章制度标准(3)" Value="3.0" />
                                <f:ListItem Text="不符合公司规章制度(2)" Value="2.0" />
                                <f:ListItem Text="完全符合(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpHazardJudge_S5" runat="server" Label="形象受损程度" EnableEdit="true">
                                <f:ListItem Text="全国性影响(5)" Value="5.0" />
                                <f:ListItem Text="地区性、行业内影响(4)" Value="4.0" />
                                <f:ListItem Text="集团公司范围内影响(3)" Value="3.0" />
                                <f:ListItem Text="公司内影响(2)" Value="2.0" />
                                <f:ListItem Text="无影响(1)" Value="1.0" Selected="true" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                </Rows>
                 <Toolbars>
                     <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
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

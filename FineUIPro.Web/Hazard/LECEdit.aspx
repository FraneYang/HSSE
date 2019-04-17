<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LECEdit.aspx.cs" Inherits="FineUIPro.Web.Hazard.LECEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑LEC评价</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpInstallationId" runat="server" Label="装置" EnableEdit="true" >
                    </f:DropDownList>
                    <f:TextBox ID="txtEvaluatorName" runat="server" Label="评价人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtEvaluationTime" runat="server" Label="评价时间" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>           
            <f:FormRow>
                <Items>
                  <f:Grid ID="Grid1" ShowBorder="false" ShowHeader="false" Title="LEC评价" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="LECItemId" AllowCellEditing="true"
                    EnableColumnLines="true" DataIDField="LECItemId" AllowSorting="true" SortField="SortIndex" ClicksToEdit="1">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>                               
                                <f:Button ID="btnAdd" Text="新增" Icon="Add" runat="server" OnClick="btnAdd_Click" Hidden="true">
                                </f:Button>
                                <f:Button ID="btnDelete" Text="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click" Hidden="true"
                                    runat="server" AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true" AjaxLoadingMaskText="正在删除数据，请稍候">
                                </f:Button>                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField />
                        <f:RenderField Width="120px" ColumnID="RiskPlace" DataField="RiskPlace"
                            FieldType="String" HeaderText="区域位置" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:TextBox ID="txtRiskPlace" Required="true" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SortIndex" DataField="SortIndex"
                            FieldType="String" HeaderText="序号NO." HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                 <f:NumberBox ID="nbSortIndex" NoDecimal="true" NoNegative="true" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="HazardDescription" DataField="HazardDescription" FieldType="String"
                             HeaderText="危险源描述" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:TextBox ID="txtHazardDescription" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                         <f:RenderField Width="120px" ColumnID="PossibleAccidents" DataField="PossibleAccidents" FieldType="String"
                             HeaderText="可能导致的事故" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:TextBox ID="txtPossibleAccidents" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="HazardJudge_L" DataField="HazardJudge_L"
                            FieldType="String" HeaderText="L" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                 <f:NumberBox ID="nbHazardJudge_L" NoDecimal="true" NoNegative="true" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="HazardJudge_E" DataField="HazardJudge_E"
                            FieldType="String" HeaderText="E" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:NumberBox ID="nbHazardJudge_E" NoDecimal="true" NoNegative="true" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="HazardJudge_C" DataField="HazardJudge_C"
                            FieldType="String" HeaderText="C" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:NumberBox ID="nbHazardJudge_C" NoDecimal="true" NoNegative="true" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="HazardJudge_D" DataField="HazardJudge_D"
                            FieldType="String" HeaderText="D" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:NumberBox ID="nbHazardJudge_D" NoDecimal="true" NoNegative="true" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="RiskLevel" DataField="RiskLevel" FieldType="String"
                             HeaderText="风险评价级别" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:DropDownList ID="drpRiskLevel" runat="server">
                                    <f:ListItem Value="1" Text="一级"/>
                                    <f:ListItem Value="2" Text="二级"/>
                                    <f:ListItem Value="3" Text="三级"/>
                                    <f:ListItem Value="4" Text="四级"/>
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                         <f:RenderField Width="120px" ColumnID="ControlMeasures" DataField="ControlMeasures" FieldType="String"
                             HeaderText="控制措施" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:TextBox ID="txtControlMeasures" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                         <f:RenderField Width="120px" ColumnID="Remark" DataField="Remark" FieldType="String"
                             HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:TextBox ID="txtRemark" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                    </Columns>                    
                  </f:Grid>
                </Items>
            </f:FormRow>          
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" Hidden="true"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="信息维护页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        CloseAction="HidePostBack" Width="1000px" Height="550px">
    </f:Window>
    </form>
</body>
</html>

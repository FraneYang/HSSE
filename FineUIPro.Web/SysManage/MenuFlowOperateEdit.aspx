<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuFlowOperateEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.MenuFlowOperateEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="20px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
             <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtFlowStep" runat="server" Label="步骤" NoDecimal="true" NoNegative="true" 
                        LabelWidth="80px" LabelAlign="Right"  ShowRedStar="true"  Required="true"></f:NumberBox>
                     <f:TextBox ID="txtAuditFlowName" runat="server" Label="步骤名称"  LabelWidth="80px" LabelAlign="Right"
                        Required="true" ShowRedStar="true" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:DropDownBox runat="server" ID="drpWorkPosts" DataControlID="rbWorkPosts"  LabelWidth="80px" LabelAlign="Right"
                        EnableMultiSelect="true" ShowLabel="true" Label="审批岗位" EnableEdit="true" MaxLength="4000">
                        <PopPanel>
                            <f:SimpleForm ID="SimpleForm2" BodyPadding="10px" runat="server" AutoScroll="true"
                                ShowBorder="True" ShowHeader="false" Hidden="true">
                                <Items>
                                    <f:Label ID="Label1" runat="server" Text="请选择审批岗位：">
                                    </f:Label>
                                    <f:CheckBoxList ID="rbWorkPosts" ColumnNumber="5" runat="server">                                       
                                    </f:CheckBoxList>
                                </Items>
                            </f:SimpleForm>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
               <Items>
                   <f:DropDownList ID="drpDepartIds" runat="server" Label="部门/中心" EnableEdit="true" EnableMultiSelect="true"
                        ShowRedStar="true" LabelWidth="80px" LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="drpDepartIds_SelectedIndexChanged">  
                   </f:DropDownList> 
                   <f:DropDownList ID="drpInstallationIds" runat="server" Label="科室/装置" EnableEdit="true" EnableMultiSelect="true"
                        ShowRedStar="true" LabelWidth="80px" LabelAlign="Right">  
                    </f:DropDownList> 
                </Items> 
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtPushGroup" runat="server" Label="组别" NoDecimal="true" NoNegative="true" CompareMessage="【组别】值不能小于1"
                        LabelWidth="80px" LabelAlign="Right" ShowRedStar="true" Required="true" CompareOperator="GreaterThanEqual" CompareValue="1">
                    </f:NumberBox>
                    <f:DropDownList ID="drpIsNeed" runat="server" Label="填写意见" EnableEdit="false" 
                        Required="true" ShowRedStar="true" LabelWidth="80px" LabelAlign="Right">
                    </f:DropDownList>
                     <f:CheckBox ID="IsFlowEnd" MarginLeft="40px" runat="server" Text="流程结束" >
                    </f:CheckBox>
                </Items>
            </f:FormRow> 
             <%--<f:FormRow>
               <Items>
                   <f:DropDownList ID="drpMatchesValue" runat="server" Label="过滤条件" EnableEdit="false" 
                        ShowRedStar="true" LabelWidth="80px" LabelAlign="Right">                       
                       <f:ListItem Value="3" Text="装置"/>
                       <f:ListItem Value="2" Text="部门"/>
                       <f:ListItem Value="1" Text="单位"/>
                       <f:ListItem Value="0" Text="无"  Selected="true"/>
                    </f:DropDownList>   
                </Items> 
            </f:FormRow>--%>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

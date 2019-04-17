<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SCLInfoEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.SCLInfoEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑SCL评价</title>
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
                    <f:DropDownList ID="drpEuipmentTypeId" runat="server" Label="设备设施类型" LabelWidth="120px" FocusOnPageLoad="true"
                        EnableEdit="true" ForceSelection="false" AutoPostBack="true" OnSelectedIndexChanged="drpEuipmentTypeId_OnSelectedIndexChanged">
                  </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:NumberBox ID="txtSortIndex" runat="server" Label="序号" NoDecimal="true" NoNegative="true" LabelWidth="120px">
                    </f:NumberBox>
                    <f:TextBox ID="txtCheckItem" runat="server" Label="检查项目" Required="true" ShowRedStar="true"  MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtStandard" runat="server" Label="标准" MaxLength="500" LabelWidth="120px">
                   </f:TextBox>
                      </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtConsequence" runat="server" Label="未达标准的</br>主要后果" MaxLength="500" LabelWidth="120px">
                   </f:TextBox>
                </Items>
            </f:FormRow>  
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtNowControlMeasures" runat="server" Label="现有控制措施" MaxLength="800" LabelWidth="120px">
                   </f:TextBox>
                      </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpHazardJudge_L" runat="server" Label="L" ShowRedStar="true" LabelWidth="120px"
                        AutoPostBack="true" OnSelectedIndexChanged="txtHazardJudge_TextChanged" EnableEdit="true">
                        <f:ListItem Text="请选择" Value="0" />
                        <f:ListItem Text="5" Value="5.0" />
                        <f:ListItem Text="4" Value="4.0" />
                        <f:ListItem Text="3" Value="3.0" />
                        <f:ListItem Text="2" Value="2.0" />
                        <f:ListItem Text="1" Value="1.0" />
                    </f:DropDownList>   
                    <f:DropDownList ID="drpHazardJudge_S" runat="server" Label="S" ShowRedStar="true" LabelWidth="120px"
                        AutoPostBack="true" OnSelectedIndexChanged="txtHazardJudge_TextChanged" EnableEdit="true">
                        <f:ListItem Text="请选择" Value="0" />
                        <f:ListItem Text="5" Value="5.0" />
                        <f:ListItem Text="4" Value="4.0" />
                        <f:ListItem Text="3" Value="3.0" />
                        <f:ListItem Text="2" Value="2.0" />
                        <f:ListItem Text="1" Value="1.0" />
                    </f:DropDownList>  
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>                    
                    <f:NumberBox runat="server" ID="txtHazardJudge_R" Label="R" NoNegative="true" LabelWidth="120px"
                        DecimalPrecision="1"  Readonly="true" ShowRedStar="true" Required="true"></f:NumberBox>
                     <f:DropDownList ID="drpRiskLevelId" runat="server" Label="风险等级" LabelWidth="120px"
                         ShowRedStar="true" Required="true" Readonly="true">
                    </f:DropDownList>  
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                     <f:TextBox ID="txtControlMeasures" runat="server" Label="建议改正</br>控制措施" MaxLength="800" LabelWidth="120px">
                   </f:TextBox>
                </Items>
            </f:FormRow>        
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
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

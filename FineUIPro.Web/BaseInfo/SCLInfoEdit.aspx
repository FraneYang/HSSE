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
                     <f:NumberBox ID="txtSortIndex" runat="server" Label="序号" NoDecimal="true" NoNegative="true">
                    </f:NumberBox>
                    <f:TextBox ID="txtCheckItem" runat="server" Label="检查项目" Required="true" ShowRedStar="true"  MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtStandard" runat="server" Label="标准" MaxLength="500">
                   </f:TextBox>
                      </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtConsequence" runat="server" Label="未达标准的</br>主要后果" MaxLength="500">
                   </f:TextBox>
                </Items>
            </f:FormRow>  
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtNowControlMeasures" runat="server" Label="现有控制措施" MaxLength="800">
                   </f:TextBox>
                      </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtHazardJudge_L" runat="server" Label="风险评价L" NoNegative="true" NoDecimal="true">
                   </f:NumberBox>
                   <f:NumberBox ID="txtHazardJudge_S" runat="server" Label="风险评价S" NoNegative="true" NoDecimal="true">
                   </f:NumberBox>
                </Items>
            </f:FormRow>   
             <f:FormRow>
                <Items>                   
                   <f:NumberBox ID="txtHazardJudge_R" runat="server" Label="风险评价R" NoNegative="true" NoDecimal="true">
                   </f:NumberBox>
                   <f:DropDownList ID="drpRiskLevel" runat="server" Label="风险等级" EnableEdit="true" >
                    </f:DropDownList>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                     <f:TextBox ID="txtControlMeasures" runat="server" Label="建议改正</br>控制措施" MaxLength="800">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LimitedSpaceAnalysisDataEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.LimitedSpaceAnalysisDataEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑受限分析点数据</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Label ID="txtSortIndex" runat="server" Label="排列序号">
                    </f:Label>
                    <f:Label ID="txtAnalysisPoint" runat="server" Label="测量对象">
                    </f:Label>
                    <f:Label ID="txtCategory" runat="server" Label="类型">
                    </f:Label>
               </Items>
            </f:FormRow>  
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtMinData" runat="server" Label="最小值" NoNegative="true" DecimalPrecision="1"
                         LabelAlign="Right"></f:NumberBox>
                    <f:NumberBox ID="txtMaxData" runat="server" Label="最大值" NoNegative="true" DecimalPrecision="1"
                         LabelAlign="Right"></f:NumberBox> 
                    <f:TextBox ID="txtMeasure" runat="server" Label="单位" MaxLength="10">
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
                    <f:Button ID="btnClose" EnablePostBack="false"  runat="server" Icon="SystemClose" MarginRight="10px">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

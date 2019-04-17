<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardIn.aspx.cs" Inherits="FineUIPro.Web.Standard.HSSEStandardIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入专家辅助</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:RadioButtonList ID="ckSpecialty" runat="server" ColumnNumber="15" AutoColumnWidth="true">                               
                    </f:RadioButtonList>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="数据导入" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>  
                    <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                        LabelWidth="150px">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="HSSEStandardId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="HSSEStandardId" AllowSorting="true" SortField="StandardCode,ManagedObjectCode,ManagedItemCode,HSSEStandardCode"
                        PageSize="50" Height="360px">
                        <Columns>
                            <f:TemplateField Width="50px" HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>                              
                            <f:RenderField Width="80px" ColumnID="StandardCode" DataField="StandardCode" FieldType="String"
                                HeaderText="标准编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="300px" ColumnID="StandardName" DataField="StandardName" FieldType="String"
                                HeaderText="标准" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="80px" ColumnID="ManagedObjectCode" DataField="ManagedObjectCode" FieldType="String"
                                HeaderText="对象编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="ManagedObjectName" DataField="ManagedObjectName" FieldType="String"
                                HeaderText="管理对象" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField> 
                             <f:RenderField Width="80px" ColumnID="ManagedItemCode" DataField="ManagedItemCode" FieldType="String"
                                HeaderText="项目编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="120px" ColumnID="ManagedItemName" DataField="ManagedItemName" FieldType="String"
                                HeaderText="管理项目" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="80px" ColumnID="HSSEStandardCode" DataField="HSSEStandardCode" FieldType="String"
                                HeaderText="排列编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="400px" ColumnID="HSSEStandardName" DataField="HSSEStandardName" FieldType="String"
                                HeaderText="具体要求" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>    
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdFileName" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdCheckResult" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" Text="说明：1 专家辅助信息导入模板为.xls后缀的EXCEL文件，黑体字为必填项。2 对于导入信息中重复记录自动过滤插入一条记录。3 如需修改已有专家辅助信息，请到系统中修改。4 数据导入完成，成功后自动返回，如果有不成功数据页面弹出提示框，列表显示导入成功数据。">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="导入信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPerson.aspx.cs" Inherits="FineUIPro.Web.QualityAudit.ShowPerson" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>显示培训人员</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="true" ShowHeader="false" Layout="HBox"
        Margin="5px" BodyPadding="5px">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人员" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="UserId" EnableCheckBoxSelect="true"
                EnableColumnLines="true" DataIDField="UserId" AllowSorting="true" PageSize="10000"
                SortField="UnitName,UserName" SortDirection="ASC" OnSort="Grid1_Sort" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DropDownList ID="drpUnit" runat="server" Label="单位" AutoPostBack="true" Width="300px"
                                LabelWidth="50px" LabelAlign="right" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:Label ID="lb112" runat="server" Width="20px">
                            </f:Label>
                            <f:TextBox runat="server" EmptyText="输入姓名" AutoPostBack="True" Label="姓名" LabelWidth="45px"
                                Width="200px" ID="txtPersonName" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" ToolTip="确认" Icon="Accept" runat="server" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                        HeaderText="单位名称" HeaderTextAlign="Center" TextAlign="Left" SortField="UnitName">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" FieldType="String"
                        HeaderText="人员姓名" HeaderTextAlign="Center" TextAlign="Left" SortField="UserName">
                    </f:RenderField>
                    <f:RenderField HeaderText="性别" ColumnID="Sex" DataField="Sex" SortField="Sex" FieldType="String"
                        HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                    </f:RenderField>
                    <f:RenderField HeaderText="岗位名称" ColumnID="WorkPostName" DataField="WorkPostName"
                        SortField="WorkPostName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                        Width="120px">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfI" HeaderText="身份证号" Width="160px" HeaderTextAlign="Center"
                        SortField="IdentityCard" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbI" runat="server" Text=' <%# Bind("IdentityCard") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField HeaderText="装置/科室" ColumnID="InstallationName" DataField="InstallationName"
                        SortField="InstallationName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                        Width="150px">
                    </f:RenderField>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

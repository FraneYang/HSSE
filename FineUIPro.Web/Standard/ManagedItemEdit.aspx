<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagedItemEdit.aspx.cs" Inherits="FineUIPro.Web.Standard.ManagedItemEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑管理项目</title>
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
                   <f:TextBox ID="txtManagedItemCode" runat="server" Label="编号" MaxLength="50" FocusOnPageLoad="true">
                   </f:TextBox>                    
                </Items>
            </f:FormRow>     
             <f:FormRow>
                <Items>    
                  <f:TextBox ID="txtManagedItemName" runat="server" Label="管理项目" MaxLength="100"
                        Required="true" ShowRedStar="true" AutoPostBack="true" OnTextChanged="txtManagedItemName_TextChanged">
                   </f:TextBox>
                </Items>
            </f:FormRow>   
            <f:FormRow>
                <Items>    
                  <f:DropDownBox runat="server" ID="drpManagedObject" Label="管理对象" EmptyText="请从下拉表格中选择" MatchFieldWidth="false" LabelAlign="Right"
                         EnableMultiSelect="false">
                        <PopPanel>
                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="ManagedObjectId" DataTextField="ManagedObjectName"
                                DataKeyNames="ManagedObjectId"  AllowSorting="true" SortField="ManagedObjectCode" SortDirection="ASC" EnableColumnLines="true"
                                Hidden="true" Width="800px" Height="400px" EnableMultiSelect="false" PageSize="300">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                        <Items>
                                            <f:TextBox runat="server" Label="查询" ID="txtName" EmptyText="输入查询条件" 
                                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                                            </f:TextBox>                                                                                                                         
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>             
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center"
                                        TextAlign="Center" />                         
                                    <f:RenderField Width="200px" ColumnID="SpecialtyName" DataField="SpecialtyName" FieldType="String"
                                        HeaderText="专业" HeaderTextAlign="Center" TextAlign="Left">
                                    </f:RenderField>
                                    <f:RenderField Width="360px" ColumnID="StandardName" DataField="StandardName" FieldType="String"
                                        HeaderText="标准" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="ManagedObjectCode" DataField="ManagedObjectCode" FieldType="String"
                                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                                    </f:RenderField>
                                     <f:RenderField Width="150px" ColumnID="ManagedObjectName" DataField="ManagedObjectName" FieldType="String"
                                        HeaderText="管理对象" HeaderTextAlign="Center" TextAlign="Left">
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>               
             <f:FormRow>
                <Items>  
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="200" Height="60">
                   </f:TextArea>
                </Items>
            </f:FormRow>          
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                   
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
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

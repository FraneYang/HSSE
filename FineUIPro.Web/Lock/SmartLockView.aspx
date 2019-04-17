<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmartLockView.aspx.cs" Inherits="FineUIPro.Web.Lock.SmartLockView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>智能锁使用记录</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1"
                        AllowCellEditing="true" DataIDField="SmartLockItemId" DataKeyNames="SmartLockItemId"
                        EnableMultiSelect="false" ShowGridHeader="true" EnableColumnLines="true" Height="500px">                       
                        <Columns>
                            <f:RowNumberField ColumnID="rbNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" /> 
                            <f:RenderField Width="120px" ColumnID="Place" DataField="Place"
                                FieldType="String" HeaderText="所在位置" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="120px" ColumnID="DeviceName" DataField="DeviceName"
                                FieldType="String" HeaderText="禁动设备" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="StartTime" DataField="StartTime"
                                FieldType="String" HeaderText="开始时间" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="EndTime" DataField="EndTime"
                                FieldType="String" HeaderText="结束时间" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="ApplicantManName" DataField="ApplicantManName"
                                FieldType="String" HeaderText="申请人" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="ApplicantTime" DataField="ApplicantTime"
                                FieldType="String" HeaderText="申请时间" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="AuditManName" DataField="AuditManName"
                                FieldType="String" HeaderText="审核人" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="AuditTime" DataField="AuditTime"
                                FieldType="String" HeaderText="审核时间" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>                            
                            <f:RenderField Width="80px" ColumnID="StatesName" DataField="StatesName" FieldType="String"
                                HeaderText="状态" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>                           
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>       
    </f:Form>   
    <f:Menu ID="Menu1" runat="server">    
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"  Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除" Hidden="true">
        </f:MenuButton>     
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

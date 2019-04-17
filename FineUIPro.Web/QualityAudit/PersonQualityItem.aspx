<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonQualityItem.aspx.cs"
    Inherits="FineUIPro.Web.QualityAudit.PersonQualityItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>审核时间记录</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                BoxFlex="1" DataKeyNames="PersonQualityItemId" EnableColumnLines="true" DataIDField="PersonQualityItemId"
                SortField="CheckDate" SortDirection="DESC" AllowPaging="false" IsDatabasePaging="true" PageSize="10000">              
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="60px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                    <f:RenderField Width="150px" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate" Renderer="Date"
                        FieldType="Date" HeaderText="复查日期" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                </Columns>
                <Listeners>                 
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>               
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
    <script type="text/jscript">      
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {           
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

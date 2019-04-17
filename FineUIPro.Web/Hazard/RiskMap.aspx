<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RiskMap.aspx.cs" Inherits="FineUIPro.Web.Hazard.RiskMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IMGnd - Demo 厂区地图演示</title>
    <script src="../Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <link href="../Styles/sunny/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" for="maps" event="OnMapEvent(id,param1,param2)">
        //ShowTips(id+','+param1+','+param2);
        //1124 是地图加载完成事件，通过param1判断是否加载成功 0 == 成功，非0 错误码
        if ( id == 1124 )
        {
            OnMapLoadEvent(param1,param2);
        }
    </script>
    <script type="text/javascript">
        function ZoomOut() {
            document.maps.ZoomOut();
        }
        function ZoomIn() {
            document.maps.ZoomIn();
        }
        function ZoomAll() {
            //将地图缩放到全貌显示
            document.maps.ZoomAll();
        }
        function SetZoom(fZoom) {
            /*自定义缩放量，0.1 ~ 8 之间的数字*/
            document.maps.SetZoom(fZoom);
        }
        function ShowTips(info) {
            $('.map .tips').html(info);
        }
        function ShowInfo(info) {
            $('.map .tips').html(info);
        }

        var g_RandomTimeID = 0;
        var g_DevJson = null;
        function RandomDevStatus(en) {

            try { clearInterval(g_RandomTimeID); } catch (LER) { }
            if (en == 1) {
                //自动请求状态
                g_RandomTimeID = setTimeout('RandomDevStatusV()', 3000);
            }
        }
        function RandomDevStatusV() {
            if (!g_DevJson) return;
            var devid = '';
            for (var dev in g_DevJson.LIST) {
                if (devid != '') { devid += ','; }
                devid += g_DevJson.LIST[dev].ID;
            }
            $.post('devstatus.ashx', { devid: devid }, function (data) {
                document.maps.SetDeviceStatusJson(data);
                //alert(data);
                //再次请求
                g_RandomTimeID = setTimeout('RandomDevStatusV()', 3000);
            }, "text");
            /*
            for (var i = 0; i < g_DevJson.LIST.length; i++) {
            g_DevJson.LIST[i].STATUS = Math.floor(Math.random() * 4);
            }
            var json = JSON.stringify(g_DevJson);
            document.maps.SetDeviceStatusJson(json);
            */
        }
        function RefreshStatus() {
            if (!g_DevJson) return;
            var devid = '';
            for (var dev in g_DevJson.LIST) {
                if (devid != '') { devid += ','; }
                devid += g_DevJson.LIST[dev].ID;
            }
            $.post('devstatus.ashx', { devid: devid }, function (data) {
                document.maps.SetDeviceStatusJson(data);
            }, "text");
        }

        /*
        param1 0 ~ 7
        E_RET_OK,       //0   
        E_RET_BUSING,   //1
        E_RET_PATHERR,  //2
        E_RET_PARSEFILE,	//文件解析错误
        E_RET_VER,			//版本不兼容	
        E_RET_TMPFILE,      
        E_RET_REPLACE,
        E_RET_DOWNLOAD
        param2 0 本地加载， 1 服务器下载并加载
        */
        function OnMapLoadEvent(param1, param2) {
            if (param1 != 0) {
                //if (param2 == 1) 
                {  //param2 == 1, 升级错误 , param2 == 0,加载本地文件错误

                    var err = ["OK", "繁忙", "路径错误", "解析文件错误", "版本不匹配!", "临时文件无法操作", "更新本地包文件失败", "服务器下载错误！"];
                    var des = "Unknow";
                    if (param1 < err.length)
                        des = err[param1];
                    ShowTips((param2 == 1 ? "升级" : "加载") + "地图失败，错误码:" + param1 + "," + des);
                }
                //ShowTips("加载地图失败，错误码:" + param1);
            } else {
                try {
                    if (param2 == 1) { ShowInfo("已成功下载地图!"); }
                    var jstr = document.maps.GetAllDeviceStatus();
                    g_DevJson = eval('(' + jstr + ')');
                    //开始模拟修改设备状态
                    if ('1' == $('input[name=rnds]').filter(':checked').val()) {
                        RandomDevStatus(1);
                    } else {
                        RandomDevStatus(0);
                    }
                    LoadMapStatus();
                    LoadDevList();
                } catch (LR) { alert(LR); }
            }
        }
        /*按设备ID定位*/
        function GoDev(id) {
            document.maps.MoveToDevById(id, 1);  //1自动缩放，0不更改缩放级别
        }
        function LoadDevList() {
            var html = '';
            if (g_DevJson) {
                html += '<table align="center" width="500"><tr><td>编号</td><td>名称</td><td>级别</td><td>颜色</td><td>定位</td></tr>';
                for (var i = 0; i < g_DevJson.LIST.length; i++) {

                    var level = '一级';
                    var color = '蓝色';
                    if (g_DevJson.LIST[i].STATUS + 1 == 2) {
                        level = '二级';
                        color = '黄色';
                    } else if (g_DevJson.LIST[i].STATUS + 1 == 3) {
                        level = '三级';
                        color = '橙色';
                    }
                    else if (g_DevJson.LIST[i].STATUS + 1 == 4) {
                        level = '四级';
                        color = '红色';
                    }

                    //g_DevJson.LIST[i].STATUS = Math.floor(Math.random() * 4);
                    html += '<tr onmouseover="this.style.backgroundColor=\'#444444\'" onmouseout="this.style.backgroundColor=\'\'" ><td>' + g_DevJson.LIST[i].ID + '</td><td>' + g_DevJson.LIST[i].NAME + '</td><td>' + level + '</td><td>' + color + '</td><td><a href="javascrip' + 't:GoDev(\'' + g_DevJson.LIST[i].ID + '\')">定位</a></td></tr>';
                }
                html += '</table>';
            }
            $('#devlist').html(html);
        }
        function LoadMapStatus() {
            /*enum	E_MS_TYPE
            {
            MS_BACKGNDCOLOR, 0
            MS_TEXTCOLOR, 1
            MS_MINIMAP, 2
            MS_DEVNAME, 3
            MS_SCALEICON,4
            MS_USECACHE,5
            MS_EDITMODE,6
            };
            */
            //从地图状态获取更新UI
            var id = [
                { v: 2, id: ['minihide', 'minishow'] },
                { v: 3, id: ['devnameOff', 'devnameOn'] },
                { v: 4, id: ['scaleOff', 'scaleOn'] },
                { v: 5, id: ['cacheOff', 'cacheOn'] },
                { v: 6, id: ['editOff', 'editOn'] }
            ];
            for (var i in id) {
                var v = document.maps.GetMapStatus(id[i].v);
                $('#' + id[i].id[v]).prop('checked', true).trigger('change');
            }
            ;
            var v = document.maps.GetMapStatus(0);
            $('input[name=bgdColor]').val('0x' + v.toString(16));

            v = document.maps.GetMapStatus(1);
            $('input[name=txtColor]').val('0x' + v.toString(16));
        }
        function DownloadMaps() {           
            function GetServerUrl() {
                var pos = window.location.href.lastIndexOf('/Hazard/');             
                var url = window.location.href.substr(0, pos) + "/mapdata/lastversion.imgnd";
                return url;
            }
            var ret = document.maps.LoadMapPacketFromHttp(GetServerUrl(), 1 /* 0-不更新本地，只临时加载显示地图,1-下载并更新本地地图包*/);
            if (ret == 0) {
                ShowInfo('已开始下载……');
            } else {
                ShowInfo('下载异常，错误码：' + ret);
            }
        }

        function LoadDefMap() {
            document.maps.LoadMapPacket('', 100);   //为空表示加载控件默认地图，于控件目录下mapdata/default.imgnd
        }
        function LoadDiyPath() {
            document.maps.LoadMapPacket('', 0); //第二参数为0，第一参数为空表示弹对选择文件对话框，否则按第一参数路径加载
        }
        function onDiyStatus() {
            var id = $('input[name=itid]').val();
            var status = $('input[name=itval]').val();
            if (id != '' && status != '') {
                id = (id);
                status = parseInt(status);
                document.maps.SetDeviceStatus(id, status);
            }
        }
        function SaveMap(p) {
            var r;
            if (p == 0) {
                r = document.maps.SaveCurPacket('', 0);     //弹出保存对话框
            } else {
                r = document.maps.SaveCurPacket('', 100);   //保存到默认路径于控件目录下mapdata/default.imgnd             
            }
        }
        function SetBackGnd() {
            var v = parseInt($('input[name=bgdColor]').val());
            document.maps.SetBackGroundColor(v);
        }
        function SetTxtColor() {
            var v = parseInt($('input[name=txtColor]').val());
            document.maps.SetTextColor(v);
        }
        function ChangeMiniMapSize() {
            var v = $('select[name=minisize]').val();
            switch (v) {
                case '0':
                    document.maps.SetMiniMapSize(100, 100);
                    break;
                case '1':
                    document.maps.SetMiniMapSize(150, 150);
                    break;
                case '2':
                    document.maps.SetMiniMapSize(200, 200);
                    break;
            }
        }
        $(function () {
            $('#minishow').click(function () { document.maps.ShowMiniMap(1); });
            $('#minihide').click(function () { document.maps.ShowMiniMap(0); });
            $('#scaleOn').click(function () { document.maps.SetScaleDevIcon(1); });
            $('#scaleOff').click(function () { document.maps.SetScaleDevIcon(0); });
            $('#devnameOn').click(function () { document.maps.ShowDevName(1); });
            $('#devnameOff').click(function () { document.maps.ShowDevName(0); });

            $('#editOff').click(function () { document.maps.SetEditMode(0); });
            $('#editOn').click(function () { document.maps.SetEditMode(1); });

            $('#cacheOff').click(function () { document.maps.SetUseCache(0); });
            $('#cacheOn').click(function () { document.maps.SetUseCache(1); });

            $('#rndsOn').click(function () { RandomDevStatus(1); });
            $('#rndsOff').click(function () { RandomDevStatus(0); });


            $("#ass,#subass").accordion({ collapsible: true, heightStyle: "content" });
            //$('#mini,#scale,#showname,#editmode,#cache,#rndstatus').buttonset();
            $('#mini,#editmode,#cache,#rndstatus').buttonset();
            $('button,input[type=button]').button();
            $('select').selectable();
            LoadDefMap();
        });
    </script>
</head>
<body>
    <div id="wapper">
        <div class="header" runat="server" visible="false">
            <div class="title">
                厂区地图演示 v1.0</div>
        </div>
        <div class="main">
            <div id="ass">
                <h3>地图</h3>
                <div>
                    <div class="map">
                        <div class="tips">
                        </div>
                        <object classid="CLSID:A7DBE17A-D028-45F3-8FC5-59A0185730DF" width="100%" height="500px"
                            id="maps">
                            需在IE浏览器下正常运行!</object>
                    </div>
                    <div id="subass">
                        <h3>
                            选项</h3>
                        <div>
                            <div id="mini">
                                <input type="radio" checked="checked" value="1" id="minishow" name="miniv"/><label for="minishow">显示小地图</label>
                                <input type="radio" value="0" id="minihide"  name="miniv"/><label for="minihide">隐藏小地图</label>                        
                                <input type="radio" checked="checked" value="1" id="scaleOn" name="scalev" /><label for="scaleOn">缩放图标</label>
                                <input type="radio"  value="0" id="scaleOff"  name="scalev"/><label for="scaleOff">不缩放图标</label>                       
                                <input type="radio" checked="checked" value="1" id="devnameOn" name="sdev" /><label for="devnameOn">显示设备名</label>
                                <input type="radio"  value="0" id="devnameOff"  name="sdev"/><label for="devnameOff">隐藏设备名</label>
                                <input type="radio" value="1" id="rndsOn" name="rnds" checked="checked" style="width:1px"/><label for="rndsOn" runat="server" visible="false">刷新状态</label>
                            </div>
                            <div id="editmode" runat="server" visible="false">
                                <input type="radio"  value="1" id="editOn" name="edit" /><label for="editOn">编辑模式</label>
                                <input type="radio"  value="0" id="editOff" checked="checked" name="edit"/><label for="editOff">关闭编辑</label>
                            </div>
                            <div id="cache" runat="server" visible="false">
                                <input type="radio" value="1" id="cacheOn" name="ecache" /><label for="cacheOn">启用绘制缓存</label>
                                <input type="radio" value="0" id="cacheOff" checked="checked" name="ecache" /><label
                                    for="cacheOff">禁用绘制缓存</label>
                            </div>
                            <div id="rndstatus" runat="server" visible="false">
                                
                                <input type="radio" value="0" id="rndsOff" name="rnds" /><label
                                    for="rndsOff">不刷新</label>
                            </div>
                            小地图大小:<select name="minisize">
                            <option value="0">100 X 100</option>
                            <option value="1" selected="selected">150 X 150</option>
                            <option value="2">200 X 200</option>
                            </select><input type="button" onclick="ChangeMiniMapSize()" value="修改"/>
                            <button onclick="ZoomOut()">
                                放大</button>
                            <button onclick="ZoomIn()">
                                缩小</button>
                            <button onclick="ZoomAll()">
                                全显</button>
                            <button onclick="RefreshStatus()" runat="server" >更新状态</button>
                            <button onclick="LoadDiyPath()">加载自定义地图</button>
                            <button onclick="LoadDefMap()" runat="server">加载默认地图</button>                           
                            <button onclick="SaveMap(0)" runat="server" >保存地图</button>
                            <button onclick="SaveMap(1)" >保存到默认地图</button>                            
                          
                        </div>
                        <h3 runat="server" visible="false">
                            升级地图</h3>
                        <div runat="server">
                            <span class="tips">可从服务器下载地图到本地缓存</span><br />
                            <input type="button" value="下载并更新地图" onclick="DownloadMaps()" />
                        </div>
                        <h3 runat="server" visible="false">测试接口</h3>
                        <div runat="server" visible="false">                                   
                             设备编号:<input type="text" name="itid" />                           
                             状态:<input type="text" name="itval" />
                            <input type="button" onclick="onDiyStatus()" value="设置" /><br />
                            设置背景:
                            RGB:(HEX:0xBBGGRR)<input type="text" name="bgdColor" value="0x336699" /><input type="button" onclick="SetBackGnd()" value="设置" /><br />
                            设置设备名颜色:
                            RGB:(HEX:0xBBGGRR)<input type="text" name="txtColor" value="0xFF0000" /><input type="button" onclick="SetTxtColor()"  value="设置"/>
                        </div>
                        <h3>设备列表</h3>
                        <div>
                            <div id="devlist"></div>
                        </div>
                    </div>
                </div>
            </div>
          </div>
      </div>
</body>
</html>

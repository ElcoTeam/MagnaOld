<!--/* ***********************************************************/
/* *************************  公共工具  JS     ******************/
/* *************************  需要jqeury 支持  ******************/
/* *************************  DOM  操作        ******************/
/* ***********************************************************/

//弹出浏览器窗口

//    channelmode=yes|no|1|0	是否使用剧院模式显示窗口。默认为 no。

//    directories=yes|no|1|0	是否添加目录按钮。默认为 yes。

//    fullscreen=yes|no|1|0	是否使用全屏模式显示浏览器。默认是 no。处于全屏模式的窗口必须同时处于剧院模式。

//    height=pixels	窗口文档显示区的高度。以像素计。

//    left=pixels	窗口的 x 坐标。以像素计。

//    location=yes|no|1|0	是否显示地址字段。默认是 yes。

//    menubar=yes|no|1|0	是否显示菜单栏。默认是 yes。

//    resizable=yes|no|1|0	窗口是否可调节尺寸。默认是 yes。

//    scrollbars=yes|no|1|0	是否显示滚动条。默认是 yes。

//    status=yes|no|1|0	是否添加状态栏。默认是 yes。

//    titlebar=yes|no|1|0	是否显示标题栏。默认是 yes。

//    toolbar=yes|no|1|0	是否显示浏览器的工具栏。默认是 yes。

//    top=pixels	窗口的 y 坐标。

//    width=pixels	窗口的文档显示区的宽度。以像素计。

function open_win(url, name, width, height, top, left) {
    //    var w = parseFloat(screen.width) / 2.0;
    //    var h = parseFloat(screen.height) / 2.0;
    //    var left = w - (parseFloat(width) / 2.0);
    //    var top_temp = (h - (parseFloat(height) / 2.0));

    //    alert(screen.height + ' ' + h + ' ' + height + ' ' + top);
    var feature = 'left=' + left + ',top=' + top + ',width=' + width + ',height=' + height + ',resizable=yes,location=no,titlebar=no,scrollbars=yes';
    myWindow = window.open(url, name, feature);
    myWindow.focus();

}

//数组内容 弹出警告
function print_array(arr) {
    var t = 'array(\n';
    for (var key in arr) {
        if (typeof (arr[key]) == 'array' || typeof (arr[key]) == 'object') {
            var t_tmp = key + ' = ' + print_array(arr[key]);
            t += '\t' + t_tmp + '\n';
        } else {
            var t_tmp = key + ' = ' + arr[key];
            t += '\t' + t_tmp + '\n';
        }
    }

    t = t + ')';
    return t; ;
}

//js 获取url参数
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

//Cookie  操作
var Cookies = {};
/**//**  
 * 设置Cookies  
 */
Cookies.set = function(c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + value + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
};
/**//**  
 * 读取Cookies  
 */
Cookies.get = function(name) {
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;
    var j = 0;
    while (i < clen) {
        j = i + alen;
        if (document.cookie.substring(i, j) == arg)
            return Cookies.getCookieVal(j);
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0)
            break;
    }
    return null;
};

function chkcookies(name) {

    var c = document.cookie.indexOf(name + "=");
    if (c != -1) {
        return true;
    }
    else {
        return false;
    }
    return false; 
   }
/**//**  
 * 清除Cookies  
 */
Cookies.clear = function(name) {
    if (Cookies.get(name)) {
        var expdate = new Date();
        expdate.setTime(expdate.getTime() - (86400 * 1000 * 1));
        Cookies.set(name, "", expdate);
    }
//    if (chkcookies(name)) {
//        var expdate = new Date();
//        expdate.setTime(expdate.getTime() - (86400 * 1000 * 1));
//        Cookies.set(name, "", expdate);
//    }

};

Cookies.getCookieVal = function(offset) {
    var endstr = document.cookie.indexOf(";", offset);
    if (endstr == -1) {
        endstr = document.cookie.length;
    }
    return unescape(document.cookie.substring(offset, endstr));
};

//容器滚动置底
function scrollToEnd(id) {
    var div = document.getElementById(id);
    div.scrollTop = (div.scrollHeight + 200);
}

//按钮悬停 改变样式
function addBtnClass(id) {
    $('#' + id).live('mouseover mouseout mousedown', function(event) {
        if (event.type == 'mouseover') {
            $(this).removeAttr("class");
            $(this).addClass('bgImgBtn_over');
        } else if (event.type == 'mouseout') {
            $(this).removeAttr("class");
            $(this).addClass('bgImgBtn');
        } else if (event.type == 'mousedown') {
            $(this).removeAttr("class");
            $(this).addClass('bgImgBtn_down');
        }
    });
}

//判断服务器端返回JSON 是否有效
function isUsableJson(data) {
    if (data != null && data != "null" && data != "")
        return true;
    else return false;
}

/* *************************  小工具     ******************/

/*
* 弹出层处理提示

*/
var prompt;
function openPrompt(text) {
    if ($('#promptDiv') != null) {
        $('#promptDiv').remove();
    }
    var div = document.createElement('div');
    $(div).attr('id', 'promptDiv');
    $(div).css('z-index', '9999999999');
    $(div).css('left', '40%');
    $(div).css('top', '30%');
    $(div).css('position', 'absolute');
    $(div).css('text-align', 'center');
    $(div).css('background-color', '#459e00');
    $(div).css('color', 'White');
    $(div).css('padding', '3px 15px');
    $(div).css('letter-spacing', '1px');
    $(div).css('text-align', 'center');
    $(div).css('font-weight', 'bold');
    $(div).css('display', 'none');
    $(div).text(text);
    $(div).appendTo($('body'));
    if ($('#promptDiv') != null)
        $('#promptDiv').show();
    prompt = setTimeout('closePrompt()', 600);
}
function closePrompt() {
    $('#promptDiv').remove();
    clearTimeout(prompt);
}

//日期字符串

function showtime() {
    var ary = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    time = new Date();
    syear = time.getFullYear();
    smonth = time.getMonth() + 1;
    sday = time.getDate();
    sweek = time.getDay();
    h = time.getHours();
    m = time.getMinutes();
    s = time.getSeconds();
    var sminute;
    var hou;
    var sec;
    if (h < 10)
        hou = "0" + h;
    else
        hou = h;
    if (m < 10)
        sminute = "0" + m;
    else
        sminute = m;
    if (s < 10)
        sec = "0" + s;
    else
        sec = s;
    document.getElementById("tip").innerHTML = " " + syear + "年" + smonth + "月" + sday + "日 " + ary[sweek] + " " + hou + ":" + sminute + ":" + sec;
    setTimeout("showtime()", 1000);
}
//日期格式'/'  -->  '-'
function formateDate(str) {
    return str.replace('/', '-').replace('/', '-');
}

//字符串去除两端 字符
function LTrim(str, chr) {
    var i;
    for (i = 0; i < str.length; i++) {
        if (str.charAt(i) != chr && str.charAt(i) != chr) break;
    }
    str = str.substring(i, str.length);
    return str;
}
function RTrim(str, chr) {
    var i;
    for (i = str.length - 1; i >= 0; i--) {
        if (str.charAt(i) != chr && str.charAt(i) != chr) break;
    }
    str = str.substring(0, i + 1);
    return str;
}
function Trim(str, chr) {
    return LTrim(RTrim(str));
}
//字符串替换

function replace(text, ostr, rstr) {
    re = new RegExp(ostr, "g");
    var newstart = text.replace(re, rstr);
    return newstart;
}
///判断文本框是否为空

function checkTextEmpty(id, msg) {
    //    alert($('#' + id).val());
    if ($('#' + id).val() == '') {
        alert(msg);
        return false;
    }
    return true;
}
//留言模板--需要留言样式表message/message_1  css文件支持
function msgStrTemplate() {
    var strDiv = "<div  runat='server' style='clear: both' class='$class$'>" +
                                "<div class='wrap' style='clear: both'>" +
                                    "<table cellpadding='0' cellspacing='0' border='0' class='wrap_table'>" +
                                    "<tr style='background-color: #eeeeee'>" +
                                        "<th>" +
                                            "<div class='wrap_table_index_left'>" +
                                                "&nbsp;</div>" +
                                        "</th>" +
                                        "<th>" +
                                            "<input type='hidden' value='$JobNum$'>" +
                                        "</th>" +
                                        "<th>" +
                                            "&nbsp;" +
                                        "</th>" +
                                        "<th style='padding: 0px 10px;'>" +
                                           "$People$" +
                                        "</th>" +
                                       " <th>" +
                                            "&nbsp;" +
                                        "</th>" +
                                        "<th>" +
                                            "&nbsp;" +
                                        "</th>" +
                                        "<th style='padding: 0px 10px;'>" +
                                            "$MsgDate$" +
                                        "</th>" +
                                        "<th>" +
                                            "<div class='wrap_table_index_right'>" +
                                                "&nbsp;</div>" +
                                        "</th>" +
                                   " </tr>" +
                                    "<tr>" +
                                       " <td colspan='8'>" +
                                            "$Remark$" +
                                       "</td>" +
                                   "</tr>" +
                                "</table>" +
                            "</div>" +
                        "</div>";
    return strDiv;
}

//窗口高度

function initHeight() {
    var wh = $(window).height();

    var tabH = $('a[href*="#tab"]').first().height();
    h = wh - tabH;
    $("div[id*='tab']").height(h - 40);

}

//日期范围 --需 jqeury - ui 支持
function setDateRangeHTMLCtl(startCtlId, endCtlId) {
    var dates = $("#" + startCtlId + ",#" + endCtlId + "").datepicker({
        changeMonth: true,
        changeYear: true,
        showAnim: "",
        dateFormat: 'yy-mm-dd',
        onSelect: function(selectedDate) {
            var option = this.id == "" + startCtlId + "" ? "minDate" : "maxDate",
					        instance = $(this).data("datepicker"),
					        date = $.datepicker.parseDate(
						        instance.settings.dateFormat ||
						        $.datepicker._defaults.dateFormat,
						        selectedDate, instance.settings);
            dates.not(this).datepicker("option", option, date);
        }
    });
}

//日期范围(不自动弹出日期控件) --需 jqeury - ui 支持
function setDateHideHTMLCtl(startCtlId, endCtlId, isHide) {
    if (isHide == "true") {
        $("#" + startCtlId + "").datepicker("hide");
    }
    else {
        var dates = $("#" + startCtlId + ",#" + endCtlId + "").datepicker({
            changeMonth: true,
            changeYear: true,
            showAnim: "",
            dateFormat: 'yy-mm-dd',
            onSelect: function(selectedDate) {
                var option = this.id == "" + startCtlId + "" ? "minDate" : "maxDate",
					        instance = $(this).data("datepicker"),
					        date = $.datepicker.parseDate(
						        instance.settings.dateFormat ||
						        $.datepicker._defaults.dateFormat,
						        selectedDate, instance.settings);
                dates.not(this).datepicker("option", option, date);
            }
        });
    }


}

//日期控件初始化   精确到 日

function initDate(jq_id) {
    $(jq_id).datepicker({
        dateFormat: 'yy-mm-dd',
        showAnim: "",
        changeMonth: true
    });
}

//日期控件初始化--最早、最晚范围约束  精确到 日

function initRangeDate(jq_id, startDate, endDate) {
    $(jq_id).datepicker({
        maxDate: "+" + endDate + "d",
        minDate: "-" + startDate + "d",
        dateFormat: 'yy-mm-dd',
        showAnim: ""
    });
}

//日期控件初始化--最早范围约束  精确到 日(王健)
function initRangeMinDate(jq_id, startDate) {
    $(jq_id).datepicker({
        minDate: "-" + startDate + "d",
        dateFormat: 'yy-mm-dd',
        showAnim: ""
    });
}
//日期控件初始化--最早范围约束 在当天之后  精确到 日(王健)
function initRangeMinNumDate(jq_id, startDate) {
    $(jq_id).datepicker({
        minDate: "+" + startDate + "d",
        dateFormat: 'yy-mm-dd',
        showAnim: ""
    });
}

//日期控件初始化--最早范围约束 在当天之后  精确到 日(王健)
function initRangeMinNumDateendDate(jq_id, startDate,endDate) {
    $(jq_id).datepicker({
        minDate: "+" + startDate + "d",
        maxDate: "+" + endDate + "d",
        dateFormat: 'yy-mm-dd',
        showAnim: ""
    });
}
//页面绑定回车按钮触发控件点击事件（王健）
function PageBindPressKey(id, e) {
    var key = e.which;
   
    if (key == 13) {

        $("#" + id + "").click();
        return false;
    }
    else {
        return false;
    }
}
//页面绑定回车按钮触发点击
function PageBindEnterKeyFun(id) {
    $(document).keydown(function(e) {
        var ev = document.all ? window.event : e;
        PageBindPressKey(id,ev);
    });
}



//日期控件不可写并可以退格删除调用方式（王健））））））
function DateIsNotInput() {
    $("input[day]").keydown(function(e) {

        if ($(this).attr("day") == "true") {
            return DateKeyDown($(this).attr("id"), e);
        }
    });
}
//日期控件初始化（不限定日期范围）王健
function InitDateInput() {
    $("input[day]").each(function() {
        if ($(this).attr("day") == "true") {
            
            initDate("#"+$(this).attr("id"));
            
        }
    });
}


//提交页面前，进行非空验证(王健)
function IsMustInput() {
    var flag = true;
    if ($("[com-autoValidNull]").length > 0) {
        $("[com-autoValidNull]").each(function() {

            if ($(this).attr("com-autoValidNull") != "") {

                if ($(this).val() == "") {
                    alert($(this).attr("com-autoValidNull") + "不能为空");
                    $(this).focus();
                    flag = false;
                    return false;
                }
            }
        });
    }
    if (flag == true) {
       
            $("[com-autoValidImgNull]").each(function() {

                if ($(this).attr("com-autoValidImgNull") != "") {

                    if ($(this).attr("src").indexOf("nopic.jpg") >= 0) {
                        alert($(this).attr("com-autoValidImgNull") + "不能为空");
                        $(this).focus();
                        flag = false;
                        return false;
                    }
                }
            });

        }
        if (flag == true) {

            $("[com-autoValidddlNull]").each(function() {

            if ($(this).val() == "0") {

                   
                        alert($(this).attr("com-autoValidddlNull") + "不能为空");
                        $(this).focus();
                        flag = false;
                        return false;
                   
                }
            });

        }
    return flag;
}



//退格清空文本框值（王健）  常用于  日期
function DateKeyDown(id, e) {
    var key = e.which;
    if (key == 8) {
        $("#" + id + "").val("");
        return false;
    }
    else {
        return false;
    }
}
//获取某日期的后一天日期(王健)  dt为标志日期  days 后几天

function GetBehindDate(dt, days) {
    var result="";
    dt = dt.replace('-', '/'); //js不认2000-1-31,只认2000/1/31
    var t1 = new Date(new Date(dt).valueOf() + days * 24 * 60 * 60 * 1000); // 日期加上指定的天数

    if (t1.getMonth() + 1 < 10) {
        result = t1.getFullYear() + "-0" + (t1.getMonth() + 1) + "-" + t1.getDate();
    }
    else {
        result = t1.getFullYear() + "-" + ( t1.getMonth() + 1) + "-" + t1.getDate();
    }
    return result;
}
//表行删除
function delTR(obj) {
    $(obj).parent().parent().remove();
}
//表格删除
function delTable(obj) {
    $(obj).parent().parent().parent().parent().remove();
}
//判断Input 内是否为‘undefined’如果为空返回‘’否则返回结果

function isUndefinedVal(val) {

    if (val == undefined) {
        return "";
    }
    else if (val == "undefined") {
        return "";
    }
    else if (val == "") {
        return "";
    }
    else {
        return val;
    }
}

//判断 回车键按下 执行传入控件名点击事件

function EnterKeyDown(id) {
    document.onkeydown = function(e) {
        var ev = document.all ? window.event : e;
        if (ev.keyCode == 13) {
            $(id).click();
            return false;
        }
    }
}
//控制DIV编辑框中空格问题（刘毅）
function DivKeyDown(id) {
    $("#" + id + "").keydown(function(e) {
        if (e.which == 13) {
            e.preventDefault(); //取消回车键原有事件。

            var sel = document.selection.createRange();
            sel.text = "\r\n";
            sel.select();
        }
    });
}


//******************easyUI合并DataGrid单元格扩展-----合并相同数据列（穆海东）*****************//

//eg_field 参照列，按照此列进行合并
//fields  将数组内的其他列参照eg_field进行合并
/*
$('#tt').datagrid({
    onLoadSuccess: function() {
        //MergeCells($('#tt'),'GHSMC', ['KH', 'PP']); //指定'KH', 'PP'按照'GHSMC'合并的位置进行合并

        MergeCells($('#tt'),'GHSMC', '');  //只合并'GHSMC'列

    }
});
*/
function MergeCells(target,eg_field, fields) {

    var rows = target.datagrid("getRows");

    var i = 0, j = 0, temp = {};

    for (i; i < rows.length; i++) {
        var row = rows[i];
        j = 0;

        var field = eg_field;
        var tf = temp[field];
        if (!tf) {
            tf = temp[field] = {};
            tf[row[field]] = [i];
        } else {
            var tfv = tf[row[field]];
            if (tfv) {
                tfv.push(i);
            } else {
                tfv = tf[row[field]] = [i];
            }
        }
    }
    $.each(temp, function(field, colunm) {
        $.each(colunm, function(m, n) {

            if (m != "") {
                var group = colunm[m];

                if (group.length > 1) {
                    var before,
						after,
						megerIndex = group[0];
                    for (var i = 0; i < group.length; i++) {
                        before = group[i];
                        after = group[i + 1];
                        if (after && (after - before) == 1) {
                            continue;
                        }
                        var rowspan = before - megerIndex + 1;
                        if (rowspan > 1) {
                            target.datagrid('mergeCells', {
                                index: megerIndex,
                                field: field,
                                rowspan: rowspan
                            });
                            for (var j = 0; j < fields.length; j++) {

                                target.datagrid('mergeCells', {
                                    index: megerIndex,
                                    field: fields[j],
                                    rowspan: rowspan
                                });
                            }
                        }
                        if (after && (after - before) != 1) {
                            megerIndex = after;
                        }
                    }
                }
            }
        });
    });
}

/* *************************  系统业务 操作     ******************/

//***************************************************/
//********************* 主键名称为 "ID" 加解锁*************/
//***************************************************/
/* **************************************************
///***          单据 加/解锁               ***
///*  id:   表ID  
///*  isLock：加/解 锁 
///*  tableName: 表名
///*  dbTag:数据库标示，DAL层中指定
//***************************************************/
//function lockOrder(id, isLock, tableName, dbTag) {
//    var url = "/HttpHandler/CommonHandler/LockDoc.ashx";
//    $.ajax({
//        type: "GET",
//        async: false,
//        url: encodeURI(url),
//        dataType: "html",
//        data: encodeURI("id=" + id + "&isLock=" + isLock + "&tableName=" + tableName + "&dbTag=" + dbTag),
//        success: function(msg) {
//            if (msg == "false") {
//                alert("单据加/解锁失败");
//            }
//        },
//        error: function() {
//            window.close();
//        }
//    });
//}

///* **************************************************
///***          检测单据加/解锁状态            ***
///*  id:   表ID  
///*  isLock：加/解 锁 
///*  tableName: 表名
///*  dbTag:数据库标示，DAL层中指定
//***************************************************/
//function checkLock(id, tableName, dbTag) {
//    var flag = false;
//    $.ajax({
//        type: "POST",
//        async: false,
//        url: "/HttpHandler/CommonHandler/LockDoc.ashx",
//        data: encodeURI("id=" + id + "&cl=true" + "&tableName=" + tableName + "&dbTag=" + dbTag),
//        success: function(msg) {
//            if (msg != "unlock") {
//                alert("此单据已被【" + msg + "】进行编辑");
//                flag = true;
//            }
//        },
//        error: function() {
//            alert("请求失败，请检查网络是否良好或联系ERP");
//            flag = true;
//        }
//    });
//    return flag;
//}




////***************************************************/
////*********************自定义主键名称 加解锁*************/
////***************************************************/
//function lockOrder_1(id,pk_name, isLock, tableName, dbTag) {
//    var url = "/HttpHandler/CommonHandler/LockDoc.ashx";
//    $.ajax({
//        type: "GET",
//        async: false,
//        url: encodeURI(url),
//        dataType: "html",
//        data: encodeURI("id=" + id + "&isLock=" + isLock + "&tableName=" + tableName + "&dbTag=" + dbTag + "&pk_name=" + pk_name),
//        success: function(msg) {
//            if (msg == "false") {
//                alert("单据加/解锁失败");
//            }
//        },
//        error: function() {
//            window.close();
//        }
//    });
//}

////***************************************************/
//function checkLock_1(id,pk_name, tableName, dbTag) {
//    var flag = false;
//    $.ajax({
//        type: "POST",
//        async: false,
//        url: "/HttpHandler/CommonHandler/LockDoc.ashx",
//        data: encodeURI("id=" + id + "&cl=true" + "&tableName=" + tableName + "&dbTag=" + dbTag + "&pk_name=" + pk_name),
//        success: function(msg) {
//            if (msg != "unlock") {
//                alert("此单据已被【" + msg + "】进行编辑");
//                flag = true;
//            }
//        },
//        error: function() {
//            alert("请求失败，请检查网络是否良好或联系ERP");
//            flag = true;
//        }
//    });
//    return flag;
//}



-->
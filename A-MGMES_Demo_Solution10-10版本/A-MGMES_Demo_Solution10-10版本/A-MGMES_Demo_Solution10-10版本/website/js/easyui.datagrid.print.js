// strPrintName 打印任务名  
// printDatagrid 要打印的datagrid  
function CreateFormPage(strPrintName, printDatagrid) {
    var tableString = '<table cellspacing="0" class="pb" style="border:1px solid">';
    var frozenColumns = printDatagrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象  
    var columns = printDatagrid.datagrid("options").columns;    // 得到columns对象  
    var nameList = '';

    // 载入title  
    if (typeof columns != 'undefined' && columns != '') {
        $(columns).each(function (index) {
            tableString += '\n<tr >';
            if (typeof frozenColumns != 'undefined' && typeof frozenColumns[index] != 'undefined') {
                for (var i = 0; i < frozenColumns[index].length; ++i) {
                    if (!frozenColumns[index][i].hidden) {
                        tableString += '\n<th style="border:1px solid" width="' + frozenColumns[index][i].width + '"';
                        if (typeof frozenColumns[index][i].rowspan != 'undefined' && frozenColumns[index][i].rowspan > 1) {
                            tableString += ' rowspan="' + frozenColumns[index][i].rowspan + '"';
                        }
                        if (typeof frozenColumns[index][i].colspan != 'undefined' && frozenColumns[index][i].colspan > 1) {
                            tableString += ' colspan="' + frozenColumns[index][i].colspan + '"';
                        }
                        if (typeof frozenColumns[index][i].field != 'undefined' && frozenColumns[index][i].field != '') {
                            nameList += ',{"f":"' + frozenColumns[index][i].field + '", "a":"' + frozenColumns[index][i].align + '"}';
                        }
                        tableString += '>' + frozenColumns[0][i].title + '</th>';
                    }
                }
            }
            for (var i = 0; i < columns[index].length; ++i) {
                if (!columns[index][i].hidden) {
                    tableString += '\n<th style="border:1px solid" width="' + columns[index][i].width + '"';
                    if (typeof columns[index][i].rowspan != 'undefined' && columns[index][i].rowspan > 1) {
                        tableString += ' rowspan="' + columns[index][i].rowspan + '"';
                    }
                    if (typeof columns[index][i].colspan != 'undefined' && columns[index][i].colspan > 1) {
                        tableString += ' colspan="' + columns[index][i].colspan + '"';
                    }
                    if (typeof columns[index][i].field != 'undefined' && columns[index][i].field != '') {
                        nameList += ',{"f":"' + columns[index][i].field + '", "a":"' + columns[index][i].align + '"}';
                    }
                    tableString += '>' + columns[index][i].title + '</th>';
                }
            }
            tableString += '\n</tr>';
        });
    }
    // 载入内容  
    var rows = printDatagrid.datagrid("getRows"); // 这段代码是获取当前页的所有行  
    var nl = eval('([' + nameList.substring(1) + '])');
    for (var i = 0; i < rows.length; ++i) {
        tableString += '\n<tr>';
        $(nl).each(function (j) {
            var e = nl[j].f.lastIndexOf('_0');

            tableString += '\n<td style="border:1px solid" ';
            if (nl[j].a != 'undefined' && nl[j].a != '') {
                tableString += ' style="text-align:' + nl[j].a + ';"';
            }
            tableString += '>';
            if (e + 2 == nl[j].f.length) {
                tableString += rows[i][nl[j].f.substring(0, e)];
            }
            else
                tableString += rows[i][nl[j].f];
            tableString += '</td>';
        });
        tableString += '\n</tr>';
    }
    var footerrows = printDatagrid.datagrid("getFooterRows"); // 这段代码是获取当前页的所有行  
    var nl = eval('([' + nameList.substring(1) + '])');
    for (var i = 0; i < footerrows.length; ++i) {
        tableString += '\n<tr>';
        $(nl).each(function (j) {
            var e = nl[j].f.lastIndexOf('_0');

            tableString += '\n<td style="border:1px solid" ';
            if (nl[j].a != 'undefined' && nl[j].a != '') {
                tableString += ' style="text-align:' + nl[j].a + ';"';
            }
            tableString += '>';
            if (e + 2 == nl[j].f.length) {
                tableString += footerrows[i][nl[j].f.substring(0, e)];
            }
            else
                tableString += footerrows[i][nl[j].f];
            tableString += '</td>';
        });
        tableString += '\n</tr>';
    }
    tableString += '\n</table>';
    
    var document = window.document;
    var opt = printDatagrid;
    opt = $.extend({
        debug: false,
        preview: false,     // 是否预览
        table: true,       // 是否打印table
        usePageStyle: true  // 是否使用页面中的样式
    }, opt);

    var content,
        iframe,
        win,
        links = document.getElementsByTagName("link"),
        html = '<!doctype html><html><head><meta charset="utf-8"><title>'+strPrintName+'</title>';

    // 自动添加样式
    //for (var i = 0, len = links.length; i < len; i++) {
    //    if (links[i].rel === 'stylesheet') {
    //        //if (opt.usePageStyle || links[i].href.indexOf('learun-report.css') !== -1) {
    //        //    html += links[i].outerHTML;
    //        //}
    //        if (opt.usePageStyle || links[i].href.indexOf('.css') !== -1) {
    //            html += links[i].outerHTML;
    //        }

    //    }
    //}

    content = opt.table ? '' : opt.outerHTML;
    html += '</head><body>' + content + '</body></html>';

    // 构造iframe
    var _self = opt.clone(), timer, firstCall, win, $html = $(html);
    var $iframe = $("<iframe  />");

    if (!opt.debug) { $iframe.css({ position: "absolute", width: "0px", height: "0px", left: "-600px", top: "-600px" }); }

    $iframe.appendTo("body");
    win = $iframe[0].contentWindow;
    var $tb = tableString;
    $(win.document.body).append($html).append($tb);
    //console.log($html);
    win.print();
}

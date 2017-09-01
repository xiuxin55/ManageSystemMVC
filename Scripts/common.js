/*
Author:WeiPeng
Create:2017-02-09
*/
function IEVersion() {
    var browser=navigator.appName 
    var b_version=navigator.appVersion 
    var version=b_version.split(";"); 
    var trim_Version=version[1].replace(/[ ]/g,""); 
    if(browser=="Microsoft Internet Explorer" && trim_Version=="MSIE6.0") 
    { 
        return 6;
    } 
    else if(browser=="Microsoft Internet Explorer" && trim_Version=="MSIE7.0") 
    { 
        return 7;
    } 
    else if(browser=="Microsoft Internet Explorer" && trim_Version=="MSIE8.0") 
    { 
        return 8;
    } 
    else if(browser=="Microsoft Internet Explorer" && trim_Version=="MSIE9.0") 
    { 
        return 9;
    }
    return 9;
}
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
};
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
};
String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, "");
};
String.prototype.replaceAll = function (find, newStr) {
    return this.replace(new RegExp(find, "gm"), newStr);
};
String.prototype.format = function (args) {
    var result = this.toString();
    var _dic = typeof args === "object" ? args : arguments;
    return result.replace(/\{([^{}]+)\}/g, function (str, key) {
        // return key in _dic ? _dic[key] : str;
        return _dic.hasOwnProperty(key) ? _dic[key] : str;
    });
};
function cnvtToString(v) {
    if (v != null) {
        return v;
    } else {
        return '--';
    }
}
function cnvtToString4(v) {
    if (v != null) {
        //return v;
        return (parseFloat(v)).toFixed(4);
    } else {
        return '--';
    }
}
function cnvtToStringP2(v) {
    if (v != null) {
        return (v * 100).toFixed(2) + '%';
    } else {
        return '--';
    }
}
function cnvtToStringP4(v) {
    if (v != null) {
        return (v * 100).toFixed(4) + '%';
    } else {
        return '--';
    }
}
function cnvtToStringP3(v) {
    if (v != null && v.toString() != "") {
        return (parseFloat(v)).toFixed(2) + "%";
    } else {
        return '--';
    }
}
function getClassByStr(v) {
    var ShowColor = "";
    if (v > 0) {
        ShowColor = ' class="red"';
    } 
    else if (v < 0) {
        ShowColor = ' class="green"';
    } else {
    }
    return ShowColor;
}
function getClassByStr2(v) {
    v = v.replace("%", "");
    var ShowColor = "";
    if (v > 0) {
        ShowColor = ' style=" color:#d93a39;"';
    }
    else if (v < 0) {
        ShowColor = ' style="color: #44b153;"';
    } else {
    }
    return ShowColor;
}
function cnvtToStringDate(jsondate) {
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    } else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }
    var date = new Date(parseInt(jsondate, 10));
    return date.Format("yyyy-MM-dd");
}
function cnvtToStringDate2(jsondate) {
    //将  dd/MM/yyyy hh:mm:ss转化成  yyyy-MM-dd
    var trs = jsondate.replace(/-/g, "/");//jsondate.split(" ");
   var NewDate = new Date(trs);
   return NewDate.Format("yyyy-MM-dd");
}
function cnvtToStars(v) {
    if (v != null) {
        switch (v) {
            case 1:
                return '<span class="starlight">★</span><span class="stargray">★★★★</span>';
            case 2:
                return '<span class="starlight">★★</span><span class="stargray">★★★</span>';
            case 3:
                return '<span class="starlight">★★★</span><span class="stargray">★★</span>';
            case 4:
                return '<span class="starlight">★★★★</span><span class="stargray">★</span>';
            default:
                return '<span class="starlight">★★★★★</span>';
        }
    } else {
        return '--';
    }
}
function QueryParams() {
    var rawUrl = '', urlParams = [];
    this.parse = function (url) {
        var i = url.indexOf('?');
        if (i < 0) {
            rawUrl = url;
            urlParams = [];
        }
        else if (i == 0) {
            rawUrl = '<%=Request.RawUrl%>';
            url = url.substr(1);
            urlParams = url.split('&');
        }
        else {
            rawUrl = url.substr(0, i);
            url = url.substr(i + 1);
            urlParams = url.split('&');
        }
    };
    this.indexOf = function (key) {
        if (!key) return -1;
        var i = urlParams.length, k = 0;
        key = key.toLowerCase();
        while (--i >= 0) {
            var str = urlParams[i].toLowerCase();
            k = str.indexOf('=');
            if (k < 0 && str == key || k == 0 && key.length == 0) break;
            if (k > 0 && str.substr(0, k) == key) break;
        }
        return i;
    };
    this.get = function (key) {
        var i = this.indexOf(key);
        if (i < 0) return null;
        var result = urlParams[i];
        var k = result.indexOf('=');
        if (k < 0) result = '';
        else result = result.substr(k + 1);
        return result;
    };
    this.exists = function (key) { return this.indexOf(key) >= 0; };
    this.add = function (key, value) {
        var i = this.indexOf(key);
        if (i < 0) urlParams.push(key + '=' + value);
        else urlParams[i] = value;
    };
    this.ToString = function () {
        return rawUrl + '?' + urlParams.join('&');
    };
}
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (elt /*, from*/) {
        var len = this.length >>> 0;

        var from = Number(arguments[1]) || 0;
        from = (from < 0)
             ? Math.ceil(from)
             : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++) {
            if (from in this && this[from] === elt)
                return from;
        }
        return -1;
    };
}
Array.prototype.remove = function (val) {
   
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};

// JavaScript Document 
//--------------------------------------------------- 
// 判断闰年 
//--------------------------------------------------- 
Date.prototype.isLeapYear = function() {
    return (0 == this.getYear() % 4 && ((this.getYear() % 100 != 0) || (this.getYear() % 400 == 0)));
};
//--------------------------------------------------- 
// 日期格式化 
// 格式 YYYY/yyyy/YY/yy 表示年份 
// MM/M 月份 
// W/w 星期 
// dd/DD/d/D 日期 
// hh/HH/h/H 时间 
// mm/m 分钟 
// ss/SS/s/S 秒 
//--------------------------------------------------- 
// 对Date的扩展，将 Date 转化为指定格式的String 
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，香港服务器， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function(fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};
/** 
* 对Date的扩展，将 Date 转化为指定格式的String 
* 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符 
* 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
* eg: 
* (new Date()).pattern("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
* (new Date()).pattern("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04 
* (new Date()).pattern("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04 
* (new Date()).pattern("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04 
* (new Date()).pattern("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18 
*/
Date.prototype.pattern = function(fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    var week = {
        "0": "/u65e5",
        "1": "/u4e00",
        "2": "/u4e8c",
        "3": "/u4e09",
        "4": "/u56db",
        "5": "/u4e94",
        "6": "/u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
};
//+--------------------------------------------------- 
//| 求两个时间的天数差 日期格式为 YYYY-MM-dd 
//+--------------------------------------------------- 
function daysBetween(DateOne, DateTwo) {
    var OneMonth = DateOne.substring(5, DateOne.lastIndexOf('-'));
    var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf('-') + 1);
    var OneYear = DateOne.substring(0, DateOne.indexOf('-'));

    var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf('-'));
    var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf('-') + 1);
    var TwoYear = DateTwo.substring(0, DateTwo.indexOf('-'));
    var cha = ((Date.parse(OneMonth + '/' + OneDay + '/' + OneYear) - Date.parse(TwoMonth + '/' + TwoDay + '/' + TwoYear)) / 86400000);
    return Math.abs(cha);
}
//+--------------------------------------------------- 
//| 日期计算 
//+--------------------------------------------------- 
Date.prototype.DateAdd = function(strInterval, Number) {
    var dtTmp = this;
    switch (strInterval) {
    case 's':
        return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds() + Number); //秒 
    case 'n':
        return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes() + Number, dtTmp.getSeconds()); //分 
    case 'h':
        return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()), dtTmp.getDate(), dtTmp.getHours() + Number, dtTmp.getMinutes(), dtTmp.getSeconds()); //时 
    case 'd':
        return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()), dtTmp.getDate() + Number, dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds()); //天 
    case 'w':
        return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()), dtTmp.getDate() + Number * 7, dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds()); //周 
    case 'q':
        return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds()); //季度 
    case 'm':
        return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds()); //月 
    case 'y':
        return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds()); //年 
    }
};
//+--------------------------------------------------- 
//| 比较日期差 dtEnd 格式为日期型或者 有效日期格式字符串 
//+--------------------------------------------------- 
Date.prototype.DateDiff = function(strInterval, dtEnd) {
    var dtStart = this;
    if (typeof dtEnd == 'string') //如果是字符串转换为日期型 
    {
        dtEnd = StringToDate(dtEnd);
    }
    switch (strInterval) {
    case 's':
        return parseInt((dtEnd - dtStart) / 1000);
    case 'n':
        return parseInt((dtEnd - dtStart) / 60000);
    case 'h':
        return parseInt((dtEnd - dtStart) / 3600000);
    case 'd':
        return parseInt((dtEnd - dtStart) / 86400000);
    case 'w':
        return parseInt((dtEnd - dtStart) / (86400000 * 7));
    case 'm':
        return (dtEnd.getMonth() + 1) + ((dtEnd.getFullYear() - dtStart.getFullYear()) * 12) - (dtStart.getMonth() + 1);
    case 'y':
        return dtEnd.getFullYear() - dtStart.getFullYear();
    }
};
//+--------------------------------------------------- 
//| 日期输出字符串，重载了系统的toString方法 
//+--------------------------------------------------- 
Date.prototype.toString = function(showWeek) {
    var myDate = this;
    var str = myDate.toLocaleDateString();
    if (showWeek) {
        var Week = ['日', '一', '二', '三', '四', '五', '六'];
        str += ' 星期' + Week[myDate.getDay()];
    }
    return str;
};
//+--------------------------------------------------- 
//| 日期合法性验证 
//| 格式为：YYYY-MM-DD或YYYY/MM/DD 
//+--------------------------------------------------- 
function IsValidDate(DateStr) {
    var sDate = DateStr.replace(/(^\s+|\s+$)/g, ''); //去两边空格; 
    if (sDate == '') return true;
    //如果格式满足YYYY-(/)MM-(/)DD或YYYY-(/)M-(/)DD或YYYY-(/)M-(/)D或YYYY-(/)MM-(/)D就替换为'' 
    //数据库中，免备案空间，美国服务器，合法日期可以是:YYYY-MM/DD(2003-3/21),数据库会自动转换为YYYY-MM-DD格式 
    var s = sDate.replace(/[\d]{ 4,4 }[\-/]{ 1 }[\d]{ 1,2 }[\-/]{ 1 }[\d]{ 1,2 }/g, '');
    if (s == '') //说明格式满足YYYY-MM-DD或YYYY-M-DD或YYYY-M-D或YYYY-MM-D 
    {
        var t = new Date(sDate.replace(/\-/g, '/'));
        var ar = sDate.split(/[-/:]/);
        if (ar[0] != t.getYear() || ar[1] != t.getMonth() + 1 || ar[2] != t.getDate()) {
            return false;
        }
    } else {
        return false;
    }
    return true;
}
//+--------------------------------------------------- 
//| 日期时间检查 
//| 格式为：YYYY-MM-DD HH:MM:SS 
//+--------------------------------------------------- 
function CheckDateTime(str) {
    var reg = /^(\d+)-(\d{ 1,2 })-(\d{ 1,2 }) (\d{ 1,2 }):(\d{ 1,2 }):(\d{ 1,2 })$/;
    var r = str.match(reg);
    if (r == null)return false;
    r[2] = r[2] - 1;
    var d = new Date(r[1], r[2], r[3], r[4], r[5], r[6]);
    if (d.getFullYear() != r[1])return false;
    if (d.getMonth() != r[2])return false;
    if (d.getDate() != r[3])return false;
    if (d.getHours() != r[4])return false;
    if (d.getMinutes() != r[5])return false;
    if (d.getSeconds() != r[6])return false;
    return true;
}
//+--------------------------------------------------- 
//| 把日期分割成数组 
//+--------------------------------------------------- 
Date.prototype.toArray = function() {
    var myDate = this;
    var myArray = Array();
    myArray[0] = myDate.getFullYear();
    myArray[1] = myDate.getMonth();
    myArray[2] = myDate.getDate();
    myArray[3] = myDate.getHours();
    myArray[4] = myDate.getMinutes();
    myArray[5] = myDate.getSeconds();
    return myArray;
};
//+--------------------------------------------------- 
//| 取得日期数据信息 
//| 参数 interval 表示数据类型 
//| y 年 m月 d日 w星期 ww周 h时 n分 s秒 
//+--------------------------------------------------- 
Date.prototype.DatePart = function(interval) {
    var myDate = this;
    var partStr = '';
    var Week = ['日', '一', '二', '三', '四', '五', '六'];
    switch (interval) {
    case 'y':
        partStr = myDate.getFullYear();
        break;
    case 'm':
        partStr = myDate.getMonth() + 1;
        break;
    case 'd':
        partStr = myDate.getDate();
        break;
    case 'w':
        partStr = Week[myDate.getDay()];
        break;
    case 'ww':
        partStr = myDate.WeekNumOfYear();
        break;
    case 'h':
        partStr = myDate.getHours();
        break;
    case 'n':
        partStr = myDate.getMinutes();
        break;
    case 's':
        partStr = myDate.getSeconds();
        break;
    }
    return partStr;
};
//+--------------------------------------------------- 
//| 取得当前日期所在月的最大天数 
//+--------------------------------------------------- 
Date.prototype.MaxDayOfDate = function() {
    var myDate = this;
    var ary = myDate.toArray();
    var date1 = (new Date(ary[0], ary[1] + 1, 1));
    var date2 = date1.dateAdd(1, 'm', 1);
    var result = dateDiff(date1.Format('yyyy-MM-dd'), date2.Format('yyyy-MM-dd'));
    return result;
};
//+--------------------------------------------------- 
//| 字符串转成日期类型 
//| 格式 MM/dd/YYYY MM-dd-YYYY YYYY/MM/dd YYYY-MM-dd 
//+--------------------------------------------------- 
function StringToDate(DateStr) {
    var converted = Date.parse(DateStr);
    var myDate = new Date(converted);
    if (isNaN(myDate)) {
        var arys = DateStr.split('-');
        myDate = new Date(arys[0], --arys[1], arys[2]);
    }
    return myDate;
}
function setIE8For() {
    var default_version = "8.0";
    var ua = navigator.userAgent.toLowerCase();
    var isIE = ua.indexOf("msie") > -1;
    var safariVersion;
    if (isIE) {
        safariVersion = ua.match(/msie ([\d.]+)/)[1];
    }
    if (safariVersion <= default_version) {

        $("label").click(function (e) {
            e.preventDefault();
            $("#" + $(this).attr("for")).click().change();
        });
    }
}

function CheckUrl(id) {
    var str_url = document.getElementById(id).value;
    var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
                   + "?(([0-9a-zA-Z_!~*'().&=+$%-]+: )?[0-9a-zA-Z_!~*'().&=+$%-]+@)?" //ftp的user@      
                   + "(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP形式的URL- 199.194.52.184      
                   + "|" // 允许IP和DOMAIN（域名）      
                   + "([0-9a-zA-Z_!~*'()-]+\.)*" // 域名- www.      
                   + "([0-9a-zA-Z][0-9a-zA-Z-]{0,61})?[0-9a-zA-Z]\." // 二级域名      
                   + "[a-zA-Z]{2,6})" // first level domain- .com or .museum      
                   + "(:[0-9]{1,4})?" // 端口- :80      
                   + "((/?)|"
                   + "(/[0-9a-zA-Z_!~*'().;?:@&=+$,%#-]+)+/?)$";

    var re = new RegExp(strRegex);
    return re.test(str_url);
}
function checkScript(value) {

    var pattern = new RegExp(/<script.*?>.*?<\/script>/ig);
    if (pattern.test(value)) {
        return true;
    }
    return false;
}
function checkSpecialChar(value) {

    if (checkScript(value)){
        return true;
    }
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）——|{}【】‘；：”“'。，、？%+_]");
    if (pattern.test(value)) {
        return true;
    }

    return false;
}
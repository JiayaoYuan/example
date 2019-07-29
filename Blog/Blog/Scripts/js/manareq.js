$(function () {
    //浏览器信息以及操作系统版本信息
    var BrowserMatch = {
        init: function () {
            this.browser = this.getBrowser().browser || "未知浏览器";  //获取浏览器名
            this.version = this.getBrowser().version || "未知浏览器版本号";  //获取浏览器版本
            this.OS = this.getOS() + " " + this.getDigits() || "未知操作系统"; //系统版本号 
        },
        getOS: function () {  //判断所处操作系统
            var sUserAgent = navigator.userAgent.toLowerCase();

            var isWin = (navigator.platform == "Win32") || (navigator.platform == "Win64") || (navigator.platform == "wow64");

            var isMac = (navigator.platform == "Mac68K") || (navigator.platform == "MacPPC") || (navigator.platform == "Macintosh") || (navigator.platform == "MacIntel");
            if (isMac) return "Mac";
            var isUnix = (navigator.platform == "X11") && !isWin && !isMac;
            if (isUnix) return "Unix";
            var isLinux = (String(navigator.platform).indexOf("Linux") > -1);
            var bIsAndroid = sUserAgent.toLowerCase().match(/android/i) == "android";
            if (isLinux) {
                if (bIsAndroid) return "Android";
                else return "Linux";
            }
            if (isWin) {

                var isWin2K = sUserAgent.indexOf("Windows nt 5.0") > -1 || sUserAgent.indexOf("Windows 2000") > -1;
                if (isWin2K) return "Win2000";
                var isWinXP = sUserAgent.indexOf("Windows nt 5.1") > -1 || sUserAgent.indexOf("Windows XP") > -1
                sUserAgent.indexOf("Windows XP") > -1;
                if (isWinXP) return "WinXP";
                var isWin2003 = sUserAgent.indexOf("Windows nt 5.2") > -1 || sUserAgent.indexOf("Windows 2003") > -1;
                if (isWin2003) return "Win2003";
                var isWinVista = sUserAgent.indexOf("Windows nt 6.0") > -1 || sUserAgent.indexOf("Windows Vista") > -1;
                if (isWinVista) return "WinVista";
                var isWin7 = sUserAgent.indexOf("Windows nt 6.1") > -1 || sUserAgent.indexOf("Windows 7") > -1;
                if (isWin7) return "Win7";
                var isWin8 = sUserAgent.indexOf("windows nt 6.2") > -1 || sUserAgent.indexOf("Windows 8") > -1;
                if (isWin8) return "Win8";
                var isWin10 = sUserAgent.indexOf("windows nt 10.0") > -1 || sUserAgent.indexOf("Windows 10") > -1;
                if (isWin10) return "Win10";
            }
            return "其他";
        },
        getDigits: function () { //判断当前操作系统的版本号 
            var sUserAgent = navigator.userAgent.toLowerCase();
            var is64 = sUserAgent.indexOf("win64") > -1 || sUserAgent.indexOf("wow64") > -1;
            if (is64) {
                return "64位";
            } else {
                return "32位";
            }
        },
        getBrowser: function () {  // 获取浏览器名
            var rMsie = /(msie\s|trident\/7)([\w\.]+)/;
            var rTrident = /(trident)\/([\w.]+)/;
            var rEdge = /(chrome)\/([\w.]+)/;//IE

            var rFirefox = /(firefox)\/([\w.]+)/;  //火狐
            var rOpera = /(opera).+version\/([\w.]+)/;  //旧Opera
            var rNewOpera = /(opr)\/(.+)/;  //新Opera 基于谷歌
            var rChrome = /(chrome)\/([\w.]+)/; //谷歌 
            var rUC = /(chrome)\/([\w.]+)/;//UC
            var rMaxthon = /(chrome)\/([\w.]+)/;//遨游
            var r2345 = /(chrome)\/([\w.]+)/;//2345
            var rQQ = /(chrome)\/([\w.]+)/;//QQ
            //var rMetasr =  /(metasr)\/([\w.]+)/;//搜狗
            var rSafari = /version\/([\w.]+).*(safari)/;

            var ua = navigator.userAgent.toLowerCase();


            var matchBS, matchBS2;

            //IE 低版
            matchBS = rMsie.exec(ua);
            if (matchBS != null) {
                matchBS2 = rTrident.exec(ua);
                if (matchBS2 != null) {
                    switch (matchBS2[2]) {
                        case "4.0":
                            return {
                                browser:
                                    "Microsoft IE",
                                version: "IE: 8"  //内核版本号
                            };
                            break;
                        case "5.0":
                            return {
                                browser:
                                    "Microsoft IE",
                                version: "IE: 9"
                            };
                            break;
                        case "6.0":
                            return {
                                browser:
                                    "Microsoft IE",
                                version: "IE: 10"
                            };
                            break;
                        case "7.0":
                            return {
                                browser:
                                    "Microsoft IE",
                                version: "IE: 11"
                            };
                            break;
                        default:
                            return {
                                browser:
                                    "Microsoft IE",
                                version: "Undefined"
                            };
                    }
                } else {
                    return {
                        browser: "Microsoft IE",
                        version: "IE:" + matchBS[2] || "0"
                    };
                }
            }
            //IE最新版
            matchBS = rEdge.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent))) {
                return {
                    browser: "Microsoft Edge",
                    version: "Chrome/" + matchBS[2] || "0"
                };
            }
            //UC浏览器					  
            matchBS = rUC.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent))) {
                return {
                    browser: "UC",
                    version: "Chrome/" + matchBS[2] || "0"
                };
            }
            //火狐浏览器
            matchBS = rFirefox.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent))) {
                return {
                    browser: "火狐",
                    version: "Firefox/" + matchBS[2] || "0"
                };
            }
            //Oper浏览器					 
            matchBS = rOpera.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent))) {
                return {
                    browser: "Opera",
                    version: "Chrome/" + matchBS[2] || "0"
                };
            }
            //遨游
            matchBS = rMaxthon.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent))) {
                return {
                    browser: "遨游",
                    version: "Chrome/" + matchBS[2] || "0"
                };
            }
            //2345浏览器					  
            matchBS = r2345.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent))) {
                return {
                    browser: "2345",
                    version: "Chrome/ " + matchBS[2] || "0"
                };
            }
            //QQ浏览器					  
            matchBS = rQQ.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent))) {
                return {
                    browser: "QQ",
                    version: "Chrome/" + matchBS[2] || "0"
                };
            }
            //Safari（苹果）浏览器
            matchBS = rSafari.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent)) && (!(window.chrome)) && (!(window.opera))) {
                return {
                    browser: "Safari",
                    version: "Safari/" + matchBS[1] || "0"
                };
            }
            //谷歌浏览器
            matchBS = rChrome.exec(ua);
            if ((matchBS != null) && (!(window.attachEvent))) {
                matchBS2 = rNewOpera.exec(ua);
                if (matchBS2 == null) {
                    return {
                        browser: "谷歌",
                        version: "Chrome/" + matchBS[2] || "0"
                    };
                } else {
                    return {
                        browser: "Opera",
                        version: "opr/" + matchBS2[2] || "0"
                    };
                }
            }
        }
    };
    BrowserMatch.init();
    ClickList();
    Count("2", BrowserMatch.version, BrowserMatch.OS);
    onReportClick();
});

//点击选中列表
function ClickList() {
    var IsOpen = false;    
    $(".nav-sidebar > li").attr("class", "");
    if ($(sessionStorage.getItem("active")).selector == "") {
        $(".row aside>ul:nth-child(1)>li").attr("class", "active");
    }
    for (var j = 1; j <= $(".row aside>ul").length; j++) {
        for (var i = 1; i <= $(".row aside>ul:nth-child(" + j + ")>li").length; i++) {            
            if ($(".row aside>ul:nth-child(" + j + ")>li:nth-child(" + i + ") > a").text() == $(sessionStorage.getItem("active")).selector) {
                $(".row aside>ul:nth-child(" + j + ")>li:nth-child(" + i + ")").attr("class", "active");
            }
            $(".row aside>ul:nth-child(" + j + ")>li:nth-child(" + i + ") > a").click(function () {                
                $(sessionStorage.setItem("active", $(this).text()));
                $(".nav-sidebar > li").attr("class", "");                                  
                var SessionStr = $(sessionStorage.getItem("active")).selector;                    
                if (SessionStr == "其他" || SessionStr == "用户" || SessionStr == "设置") {
                    if (!IsOpen) {
                        $(this).parent().attr("class", "active");
                        IsOpen = true;
                    } else{
                        $(this).parent().attr("class", "active open");
                        IsOpen = false;
                    }                        
                }                
            })
        }
    }    
}

function Count(index, version, os) {
    var obj = { "pd": index };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {            
            switch (obj.pd) {
                case "2": //判断登入
                    if (data.user_name == "游客") {                        
                        $(".dropdown a span").before(data.user_name);
                        $("#seeUserInfo table input[name = 'truename']").val(data.user_name);
                        $("#seeUserInfo table input[name = 'username']").val(data.user_nickname);
                        $("#seeUserInfo table input[name = 'usertel']").val(data.user_telephone_number);
                        $("#seeUserInfo table input[name = 'old_password']").val();
                        $("#seeUserInfo table input[name = 'password']").val();
                        $("#seeUserInfo table input[name = 'new_password']").val();
                    }else {
                        if (data.status != "-1") {
                            var dataobj = eval("(" + data.status + ")");
                            $.each(dataobj.root, function (i, item) {
                                $(".dropdown a span").before(item.user_name);
                                $("#main .status table tr:nth-child(1) span:nth-child(1)").text(item.user_name);
                                $("#main .status table tr:nth-child(1) span:nth-child(2)").text(item.user_login_count);
                                $("#main .status table tr:nth-child(2) span:nth-child(1)").text(item.user_login_time);
                                $("#main .status table tr:nth-child(2) span:nth-child(2)").text(item.user_ip);
                                $("#main .sysinit table tr:nth-child(2) td:nth-child(2)").text(version);
                                $("#main .sysinit table tr:nth-child(3) td:nth-child(2)").text(os);
                                $("#main .programinit table tr:nth-child(2) td span:nth-child(2)").text("PROCESSED IN "+((Date.now() - performance.timing.navigationStart)/1000)+"s");
                                $("#seeUserInfo table input[name = 'truename']").val(item.user_name);
                                $("#seeUserInfo table input[name = 'username']").val(item.user_nickname);
                                $("#seeUserInfo table input[name = 'usertel']").val(item.user_telephone_number);
                                $("#seeUserInfo table input[name = 'old_password']").val();
                                $("#seeUserInfo table input[name = 'password']").val();
                                $("#seeUserInfo table input[name = 'new_password']").val();
                            })
                        }
                    }
                    break;
                case "3":
                    $("#main > .row div:nth-child(1) > .text-muted").append(data + "条");
                    break;
                case "4":
                    $("#main > .row div:nth-child(2) > .text-muted").append(data + "条");
                    break;
                case "5":
                    $("#main > .row div:nth-child(3) > .text-muted").append(data + "条");
                    break;
                case "19":  //访问量个数
                    $("#main > .row div:nth-child(4) > .text-muted").append(data + "条");
                    break;
                case "10": //这是栏目                    
                    for (var item in data) {
                        var txt1 = "<li><label><input name='category' type='radio' value='" + data[item].sort_id + "' checked>" + data[item].sort_name + "<em class='hidden - md'>( 栏目ID: <span>" + data[item].sort_id + "</span> )</em></label></li>";
                        $("#main #thislabel").append(txt1);
                        //PointSort(data[4].sort_name);
                    }
                    $("#main #thislabel input:radio").each(function (index, item) {
                        $("#main #thislabel li:nth-child(" + (index + 1) + ") input:radio").click(function () { PointSort(data[index].sort_name); });
                    })
                    break;
                case "18": //指定文章标签和分类                    
                    //$("#article_label1 input").val(data.labelname);
                    $("#article_label2 input").val(data.labelname);
                    //$("#main #thislabel li:nth-child(2) input:radio").prop("checked", true);
                    for (var i = 1; i <= $("#main #thislabel li").length; i++) {                        
                        if ($("#main #thislabel li:nth-child(" + i + ") input").val() == data.sortname) {
                            $("#main #thislabel li:nth-child(" + i + ") input:radio").prop("checked", true);
                        }
                    }
                    break;                
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    })
}
//指定栏目文章个数
function SortCount(index) {
    var id = $("#main > .row table tbody tr:nth-child(" + index + ") > td:nth-child(4)").text();
    var obj = { "pd": "7", "ID": id };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {            
            $("#main > .row table tbody tr:nth-child(" + index + ") > td:nth-child(4)").text(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    })
}
//关键字名
function SetLabels() {    
    var obj = { "pd": "8", "Name": $("#category-keywords").val() };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {            
            $("#category-keywords").val(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    })
}
//栏目，标签值
function SetSortAndLabel(index) {
    var article_id = parseInt($("#article_id tr:nth-child(" + index + ") td:nth-child(4)").attr("class").replace("hidden-sm", ""));
    var obj = { "pd": "9", "sort": article_id, "label": article_id };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {            
            var sort = eval("(" + data.sort + ")");            
            $("#article_id tr:nth-child(" + index + ") td:nth-child(3)").text(sort.sort_name);
            var label = eval("(" + data.label + ")");
            var label_name = "";
            $.each(label, function (index, item) {
                var item = eval("(" + item + ")");
                label_name += item.label_name + " ";
            })
            $("#article_id tr:nth-child(" + index  + ") td:nth-child(4)").text(label_name);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    })
}
//点击栏目显示关键字
function PointSort(sort_name) {
    var obj = { "pd": "11", "sort_name": sort_name };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {            
            //$("#article_label1 input").val(data);
            $("#article_label2 input").val(data);
        },
        error: function (XMLHttpRequest, textStartus, errorThrown) {
            alert(errorThrown);
        }
    })
}

//分页栏
function SetPaging(PageClass) {
    var oldlinum = parseInt($(".message_footer ul >li:first-child i").text());    
    var nowlinum = 0;
    var obj = { "pd": "22", "PageClass": PageClass };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {
            if (data.status != "-1") {
                var pageCount = Math.ceil(data.status / 15);
                if (pageCount >= oldlinum && oldlinum != 0) {
                    var pageNode;
                    switch (PageClass) {
                        case "Comments":
                            pageNode = "../Comments/Index?page=";
                            break;
                        case "Articles":
                            pageNode = "../Articles/Article?page=";
                            break;
                        case "Notices":
                            pageNode = "../Main/Notice?page=";
                            break;
                        case "Flinks":
                            pageNode = "../Main/Flink?page=";
                            break;
                    }
                    for (var i = 1; i <= pageCount; i++) {
                        $(".message_footer ul >li:nth-child(" + i + ")").after("<li class=''><a href='" + pageNode + i + "'>" + i + "</a></li>");
                        $(".message_footer ul > li:nth-child(" + i + ")").after().attr("class", "");
                        $(".message_footer ul > li:nth-child(" + i + ") a").after().attr("class", "");
                    }
                    if (oldlinum == 1) {
                        $(".message_footer ul > li:nth-child(2)").attr("class", "active");
                        $(".message_footer ul > li:nth-child(2) a").attr("class", "action");
                    } else {
                        $(".message_footer ul > li:nth-child(" + oldlinum + ")").next().attr("class", "active");
                        $(".message_footer ul > li:nth-child(" + oldlinum + ") a").next().attr("class", "action");
                    }
                    $(".message_footer ul >li:first-child a").click(function () {
                        if (oldlinum > 1) {
                            nowlinum = oldlinum - 1;
                            $(this).attr("href", pageNode + nowlinum);
                        } else {
                            alert("已经是最前一页！");
                        }
                    })
                    $(".message_footer ul >li:last-child a").click(function () {
                        if (oldlinum < ($(".message_footer ul > li").length - 2)) {
                            nowlinum = oldlinum + 1;
                            $(this).attr("href", pageNode + nowlinum);
                        } else {
                            alert("已经是最后一页！");
                        }
                    })
                }
            }
        },
        error: function (XMLHttpRequest, textStartus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function onReportClick() {
    $("aside ul:nth-child(1) li a").click(function () {
        $(this).attr("href", "/YjLihouT/Main/Index?n=" + $("#bs-example-navbar-collapse-1 .dropdown > a").text());
    });
}
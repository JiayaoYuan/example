$(function () {        
    GetArticleModel1("10");
    GetArticleModel1("13");
    GetArticleModel1("16");    
    GetArticleModel1("20");
    GetArticleModel1("21");
    EveryDayOneWord();
    LocalSearch();    
})

function GetArticleModel1(index) {
    obj = { "pd": index };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {
            switch (index) {               
                case "10": //文章分类导航                    
                    //var str = new Array();
                    //var sortcount = 0;  //分类导航个数
                    var activeTag = "";
                    var active = "";
                    $.each(data, function (Index, item) {                        
                        $("#header-navbar ul").append("<li><a href='/Home/SortCategory?sort_id=" + item.sort_id + "'>" + item.sort_name + "</a></li>");
                    });
                    $(".content .title .more a").click(function () {
                        $(sessionStorage.setItem("active", $(this).text()));
                        activeTag = "";                        
                    })
                    for (var i = 1; i <= $("#header-navbar ul li").length; i++) {
                        activeTag = $(sessionStorage.getItem("activeTag")).selector;
                        active = $(sessionStorage.getItem("active")).selector;
                        var sortname = $("#header-navbar ul li:nth-child(" + i + ") a");
                        if (sortname.text() == active && active != $("#header-navbar ul li:nth-child(1) a").text()) {
                            $("#header-navbar ul li:nth-child(" + i + ")").attr("class", "hidden-index active");
                            SortToArticleCount(sortname.text());
                            //str = $("#header-navbar ul li:nth-child(" + i + ") a").attr("href").split("?");
                            //str[1] = str[1].replace("sort_id=", "");
                            //$(sessionstorage.setitem("sort_id", str[1]));
                            //$(".pagination ul .next-page a").attr("href", str[1]);
                            //alert($(".pagination ul .next-page a").attr("href"));
                            //str = null;
                        };
                        sortname.click(function () {
                            $(sessionStorage.setItem("active", $(this).text()));
                            $("#header-navbar ul li").attr("class", "hidden-index");
                            $(this).parent().attr("class", "hidden-index active");
                            //$(this).append("<span style='display:none;>" + $(this).attr("href") + "</span>");
                            //alert("not mistake");
                            //alert($(this).children("span").text());
                            //$(".excerpt header .cat").attr("href", $(this).attr("href"));
                        });
                    }
                    if ($(sessionStorage.getItem("active")).selector == "") {                        
                        $("#header-navbar ul li:nth-child(1)").attr("class", "hidden-index active");
                        $(".excerpt header .cat i").after("首页");
                        $(".excerpt header .cat").attr("href", "/Home/Index");
                        SortToArticle_IndexCount()
                    }
                    if ($(sessionStorage.getItem("active")).selector == $("#header-navbar ul li:nth-child(1) a").text()) {
                        $("#header-navbar ul li:nth-child(1)").attr("class", "hidden-index active");
                        SortToArticle_IndexCount();
                    }
                    var setInter = setInterval(function () {                        
                        $(".content .title h3").text($(sessionStorage.getItem("active")).selector);                        
                        for (var i = 1; i <= $(".excerpt").length; i++) {                            
                            //alert($(".content .excerpt_" + i + " header .cat").text());
                            if ($(".content .excerpt_" + i + " header .cat").text() == "") {
                                if ($(sessionStorage.getItem("active")).selector != "") {
                                    $(".content .excerpt_" + i + " header .cat i").after($(sessionStorage.getItem("active")).selector);
                                } else {
                                    $(".content .excerpt_" + i + " header .cat i").after($(".content .excerpt_" + (i - 1) + " header .cat").text());
                                }
                                
                            };
                        };
                        if ($(".ias_trigger a").text() == "没有更多了") {
                            clearInterval(setInter);
                        }
                    }, 1000);  
                    if (activeTag != "") {                        
                        $("#label h3").text($(sessionStorage.getItem("activeTag")).selector);
                        $("#label header .cat").text("");
                        $("#label header .cat").append("<i></i>");
                        $("#label header .cat i").after($(sessionStorage.getItem("activeTag")).selector);
                    }                    
                    break;
                case "13": //热门文章
                    if (data.status != "-1") {                        
                        var dataobj = eval("(" + data.status + ")");
                        $.each(dataobj.root, function (Index, item) {
                            $("#widget_hot_article ul").append("<li><a href = '/articles/" + (1000 + parseInt(item.article_id)) + ".html'><span class='thumbnail'><img class='thumb' data-original='images/excerpt.jpg' src='" + item.article_label_img + "' alt=''></span><span class='text'>" + item.article_description + "</span><span class='muted'><i class='glyphicon glyphicon-time'></i> " + item.article_date + " </span><span class='muted'><i class='glyphicon glyphicon-eye-open'></i> " + item.article_views + "</span></a></li >");
                        })
                    }
                    break;
                case "16": //标签云
                    if (data.status1 != "-1") {                        
                        var json1 = eval("(" + data.status1 + ")");                        
                        $.each(json1, function (Index, item) {
                            var label = eval("(" + item + ")");
                            if (label.label_name != "") {
                                $("#ptags").append("<li><a href='/Home/LabelCategory?label_id=" + label.label_id + "' title=''>" + label.label_name + " <span class='badge'>" + label.articleCount + "</span></a></li>")                                
                            }
                        })
                        for (var i = 0; i < $(".content > ul > li").length; i++) {
                            //if ($(".content > ul > li:nth-child(" + i + ") > a").text().split(" ")[0] == $(sessionstorage.getitem("active")).selector) {
                            //    alert($(sessionstorage.getitem("active")).selector);
                            //    $(".content .title h3").text($(".content > ul > li:nth-child(" + i + ") > a").text().split(" ")[0]);
                            //    $(".excerpt header .cat i").after($(".content > ul > li:nth-child(" + i + ") > a").text().split(" ")[0]);
                            //}
                            $(".content > ul > li:nth-child(" + i + ") > a").click(function () {
                                $(sessionStorage.setItem("activeTag", $(this).text().split(" ")[0]));
                            })
                        }
                    }
                    if (data.status2 != "-1") {
                        var json2 = eval("(" + data.status2 + ")");
                        $.each(json2, function (Index, item) {
                            $("#plinks").append("<li>" + item.friend_links + "</li>");
                        })
                    }
                    break;
                case "20": //公告                    
                    if (data.status != "-1") {                        
                        var json = eval("(" + data.status + ")");
                        var jsonCount = Object.getOwnPropertyNames(json);                        
                        var isjsonin5 = 0;                        
                        $.each(json, function (Index, item) {                            
                            if (jsonCount.length >= 5) {
                                for (var i = 1; i <= (jsonCount.length - 5); i++) {
                                    if (Index == jsonCount[i]) {
                                        isjsonin5++;
                                    }
                                }
                                if (isjsonin5 == jsonCount.length - 5) {
                                    var notice = eval("(" + item + ")");
                                    $("#notice ul").append("<li><time datetime = '" + notice.announce_publish_time + "'> " + notice.announce_publish_time + "</time><a data-toggle='modal' data-target='#areNotice' rel='nofollow'>" + notice.announce_title + " </a></li >");
                                }
                            } else {
                                var notice = eval("(" + item + ")");
                                $("#notice ul").append("<li><time datetime = '" + notice.announce_publish_time + "'> " + notice.announce_publish_time + "</time><a data-toggle='modal' data-target='#areNotice' rel='nofollow'>" + notice.announce_title + "<span style='display:none;'>"+ notice.announce_content +"</span></a></li >");
                            }
                            $("#notice ul li:nth-child(" + Index.replace("json", "") + ") a").click(function () {
                                NoticeContent($(this).prop('firstChild').nodeValue, $(this).children('span').text());
                            });
                        })
                    }
                    break;
                case "21": //今日推荐
                    if (data.status != "-1") {                        
                        var json = eval("(" + data.status + ")");                        
                        $.each(json.root , function (Index, item) {                            
                            $("#focusslide").siblings(".excerpt-minic").children("h2").children("a").text(item.article_title);
                            $("#focusslide").siblings(".excerpt-minic").children("h2").children("a").attr("href", "articles/" + (1000 + parseInt(item.article_id)) + ".html");
                            $("#focusslide").siblings(".excerpt-minic").children("p").text(item.article_description + "...");
                        })                        
                    }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            alert(XMLHttpRequest.status);
            alert(textStatus);
        }
    })
}

//公告内容
function NoticeContent(title, content) {
    $("#areNoticeModalLabel").text(title);
    $("#areNotice .modal-body p").text(content);
}

//指定类型的文章个数
function SortToArticleCount(sort_name) {
    var obj = { "pd": "17", "sort_name": sort_name };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {                    
            $(function () {
                jQuery.ias({
                    history: false,
                    container: '.content',
                    item: '.excerpt',
                    pagination: '.pagination',
                    next: '.next-page a',
                    trigger: '没有更多了',
                    loader: '<div class="pagination-loading"><img src="../Content/images/loading.gif"/></div>',
                    triggerPageThreshold: parseInt(Math.round(data / 4)),
                    onRenderComplete: function () {
                        $('.excerpt .thumb').lazyload({
                            placeholder: '/Content/images/occupying.png',
                            threshold: 400
                        });
                        $('.excerpt img').attr('draggable', 'false');
                        $('.excerpt a').attr('draggable', 'false');
                    }
                });
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            alert(XMLHttpRequest);
            alert(textStatus);
        }
    })
}

//指定类型的文章个数--首页
function SortToArticle_IndexCount() {
    var obj = { "pd": "3" };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {
            jQuery.ias({
                history: false,
                container: '.content',
                item: '.excerpt',
                pagination: '.pagination',
                next: '.next-page a',
                trigger: '没有更多了',
                loader: '<div class="pagination-loading"><img src="../Content/images/loading.gif"/></div>',
                triggerPageThreshold: parseInt(data / 4),
                onRenderComplete: function () {
                    $('.excerpt .thumb').lazyload({
                        placeholder: '/Content/images/occupying.png',
                        threshold: 400
                    });
                    $('.excerpt img').attr('draggable', 'false');
                    $('.excerpt a').attr('draggable', 'false');
                }
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            alert(XMLHttpRequest);
            alert(textStatus);
        }
    })
}

//每日一句
function EveryDayOneWord() {
    $.ajax({
        type: "POST",
        url: 'https://api.hibai.cn/api/index/index',
        dataType: 'json',
        data: { "TransCode": "030111", "OpenId": "123456789", "Body": "" },
        success: function (result) {
            $(".widget-sentence-content").append("<h4>" + result.Body.date + "</h4>");
            $(".widget-sentence-content").append("<p>" + result.Body.word + "</p>")
            $(".widget-sentence-content").append("<p style='text-align: right;'>——" + result.Body.word_from + "</p>")            
            return false;
        }
    });    
}

//本地搜索
function strlen(str) {
    var len = 0; for (var i = 0; i < str.length; i++) {
        var c = str.charCodeAt(i);
        //单字节加1 
        if ((c >= 0x0001 && c <= 0x007e) || (0xff60 <= c && c <= 0xff9f)) {
            len++;
        }
        else {
            len += 2;
        }
    }
    return len;
}
function LocalSearch() {    
    var last;
    var navbarform = $(".navbar-collapse .navbar-form").siblings(".live-search");
    var widgetform = $(".widget_search .navbar-form").siblings(".live-search");
    var formtxt = $(".navbar-form").attr("action");
    window.onresize = function () {
        if (window.matchMedia("(max-width: 767px)").matches) {
            navbarform.attr("style", "display:block;");
            widgetform.attr("style", "display:none;");
            if ($(".widget_search .navbar-form input").val() != "") {
                $(".navbar-collapse .navbar-form input").val($(".widget_search .navbar-form input").val());
                $(".widget_search .navbar-form input").val("");
            }            
        } else {
            navbarform.attr("style", "display:none;");
            widgetform.attr("style", "display:block;");
            if ($(".navbar-collapse .navbar-form input").val() != "") {
                $(".widget_search .navbar-form input").val($(".navbar-collapse .navbar-form input").val());
                $(".navbar-collapse .navbar-form input").val("");
            }            
        }       
    }    
    $(".navbar-form input").keyup(function (event) {
        var nowthis = $(this);
        
        var keycode = event.keyCode;
        navbarform.empty();
        navbarform.attr("style", "display:none;");
        widgetform.empty();
        widgetform.attr("style", "display:none;");
        $(".navbar-collapse .navbar-form").attr("action", "/Home/Index?s=" + nowthis.val());
        $(".widget_search .navbar-form").attr("action", "/Home/Index?s=" + nowthis.val());
        last = event.timeStamp;        
        if (keycode >= 112 && keycode <= 123 || keycode >= 16 && keycode <= 20 || keycode == 9 || keycode == 10 || keycode == 91 || keycode == 93 || keycode == 145 || keycode >= 33 && keycode <= 34 || keycode == 45 || keycode == 46 || nowthis.val() == " ") {
            null;
        } else {
            setTimeout(function () { //利用event的timeStamp来标记时间，这样每次的keyup事件都会修改last的值，注意last必需为全局变量                
                if (last - event.timeStamp == 0 && strlen(nowthis.val()) >= 3) { 
                    var obj = { "pd": "23", "SearchName": nowthis.val() };
                    $.ajax({
                        type: "post",
                        url: "/tools/Handler.ashx",
                        data: obj,
                        dataType: "json",
                        success: function (data) {
                            if (data.status != "-1") {
                                nowthis.parents(".navbar-form").siblings(".live-search").css("display", "block");
                                navbarform.append("<ul></ul>");
                                widgetform.append("<ul></ul>");
                                var json = eval("(" + data.status + ")");
                                $.each(json.root, function (Index, item) {
                                    navbarform.children("ul").append("<li id='" + (1000 + parseInt(item.article_id)) + "'><a class='clearfix' href='/articles/" + (1000 + parseInt(item.article_id)) + ".html'><div class='poster'><img src='" + item.article_label_img + "'></div><div class='title'>" + item.article_title + "<span class='release'></span></div></a></li>");
                                    widgetform.children("ul").append("<li id='" + (1000 + parseInt(item.article_id)) + "'><a class='clearfix' href='/articles/" + (1000 + parseInt(item.article_id)) + ".html'><div class='poster'><img src='" + item.article_label_img + "'></div><div class='title'>" + item.article_title + "<span class='release'></span></div></a></li>");
                                })
                                navbarform.children("ul").append("<li class='ctsx'><a class='more live_search_click' data-search='searchform'>View all results</a></li>");
                                widgetform.children("ul").append("<li class='ctsx'><a class='more live_search_click' data-search='searchform'>View all results</a></li>");
                                $(".live_search_click").click(function () {
                                    $(this).attr("href", "/Home/Index?s=" + nowthis.val());
                                });
                                nowthis.blur();
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(errorThrown);
                            alert(XMLHttpRequest);
                            alert(textStatus);
                        }
                    })
                    $(".navbar-form").attr("action", formtxt + "?s=" + nowthis.val());
                }
            }, 2000);
        }
        
    });    
    //$(".navbar-form input:nth-child(1)").bind('input propertychange', function () {
    //    var this1 = $(this);
    //    setTimeout(function () {
    //        var strlens = strlen(this1.val());
    //        thisBool++;            
    //        if (thisBool == strlens) {
    //            alert(this1.val());
    //        }
    //    }, Timeout)
    //});
}

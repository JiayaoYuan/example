$(function () {
    Count("2");
    Count("3");
    Count("4");
    Count("5");
    Count("10");

    for (var i = 1; i <= $("#main > .row table tbody tr").length; i++) {
        SortCount(i);
    }

    for (var i = 1; i <= $("#article_id tr").length; i++) {
        SetSortAndLabel(i);
    }

    $("#main #thislabel li label input:radio").click(function () {
        alert("not mistake");
    })

    if ($("#category-keywords").val() != undefined) {
        SetLabels();
    }
})


function Count(index) {
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
                case "6":
                    $("#main > .row div:nth-child(4) > .text-muted").append(data + "条");
                    break;
                case "10": //这是栏目
                    for (var item in data) {
                        var txt1 = "<li><label><input name='category' type='radio' value='" + data[item].sort_id + "' checked>" + data[item].sort_name + " <em class='hidden - md'>( 栏目ID: <span>" + data[item].sort_id + "</span> )</em></label></li>";
                        $("#main #thislabel").append(txt1);
                        PointSort(data[item].sort_name);
                    }
                    $("#main #thislabel input:radio").each(function (index, item) {
                        $("#main #thislabel li:nth-child(" + (index + 1) + ") input:radio").click(function () { PointSort(data[index].sort_name); });
                    })
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
    var obj = { "pd": "7", "ID": $("#main > .row table tbody tr:nth-child(" + index + ") > td:nth-child(4)").text() };
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
    var obj = { "pd": "9", "sort": $("#article_id tr:nth-child(" + index + ") td:nth-child(3)").text(), "label": $("#article_id tr:nth-child(" + index + ") td:nth-child(4)").text() };
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
            $("#article_label1 input").val(data);
            $("#article_label2 input").val(data);
        },
        error: function (XMLHttpRequest, textStartus, errorThrown) {
            alert(errorThrown);
        }
    })
}

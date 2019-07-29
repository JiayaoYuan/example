$(function () {
    GetArticleModel2("12");
    GetArticleModel2("14");    
})

function GetArticleModel2(index) {
    obj = { "pd": index};
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {
            switch (index) {
                case "12": //得到文章内容
                    if (data.status != "-1") {
                        var json = eval("(" + data.status + ")");
                        $.each(json.root, function (index, item) {
                            //文章关键字                            
                            $("meta[name='keywords']").attr("content", item.article_keyword);
                            //alert(item.article_title);
                            //article title                            
                            $(".article-title a").text(item.article_title);
                            $(".article-meta-time time i").after(" " + item.article_date);
                            $(".article-meta-time time").attr("data-original-title", item.article_date);
                            $(".article-meta-views i").after(" 共 " + item.article_views + "人围观");
                            $(".article-meta-source").attr("data-original-title", item.article_id);
                            $(".article-meta-source i").after(" " + item.article_id);
                            $(".article-meta-category").attr("data-original-title", item.article_id);
                            $(".article-meta-category a").append(" " + item.article_id);
                            $(".article-meta-views").attr("data-original-title", "共" + item.article_views + "人围观");
                            $(".article-meta-comment i").after(" " + item.article_comment_count + "个不明物体");
                            $(".article-meta-comment").attr("data-original-title", item.article_comment_count + "个不明物体");
                            //article content
                            $(".article-content").append(item.article_content);
                            //var contents = new Array();                    
                            //contents = item.article_content.split("</p>");
                            //alert(contents.length);                            
                            $(".article-tags").text(item.article_id);                            
                            $("#comment-submit").click(function () {                                
                                AddComment(item.article_id, item.user_id, "NewAdd");
                                $("#comment-textarea").val("");
                            });                            
                            AddComment(item.article_id, item.user_id, "");                            
                            SetSortAndLabel();
                            GetLabel();
                        })
                    };
                    break;
                case "14": //相关推荐
                    if (data.status != "-1") {
                        var json = eval("(" + data.status + ")");
                        $.each(json.root, function (index, item) {
                            $("#recommended ul").append("<li><a href='/Home/article?id=" + item.article_id + "'>" + item.article_title + "</a></li>");
                        })
                    }
                    break;           
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            alert(XMLHttpRequest.status);
            alert(textStatus);
        }
    })
}

//相关文章标签
function GetLabel() {
    obj = { "pd": 15, "label": $(".article-tags").text()};
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {
            $(".article-tags").text("标签：");
            var json = eval("("+ data.label +")");
            $.each(json, function (Index, item) {
                var label = eval("(" + item + ")");               
                $(".article-tags").append("<a href = ''>" + label.label_name + "</a>");
            })
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            alert(XMLHttpRequest.status);
            alert(textStatus);
        }
    })
}

//栏目，标签值
function SetSortAndLabel() {
    var obj = { "pd": "9", "sort": $(".article-meta-source").text(), "label": $(".article-meta-category a").text()};
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {
            var sort = eval("(" + data.sort + ")");
            $(".article-meta-category a").text("");
            $(".article-meta-category").attr("data-original-title", sort.sort_name);
            $(".article-meta-category a").append(" " + sort.sort_name);
            var label = eval("(" + data.label + ")");
            var label_name = "";
            $.each(label, function (index, item) {
                var item = eval("(" + item + ")");
                label_name += item.label_name + " ";
            })
            $(".article-meta-source").text("");
            $(".article-meta-source").append("<i class='glyphicon glyphicon-globe'></i>");
            $(".article-meta-source").attr("data-original-title", label_name);
            $(".article-meta-source i").after(" " + label_name);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    })
}

//添加评价
function AddComment(article_id, user_id, action) {
    var commentContent = $("#comment-textarea");
    var commentButton = $("#comment-submit");
    var promptBox = $('.comment-prompt');
    var promptText = $('.comment-prompt-text');
    if (action == "NewAdd") {
        promptBox.fadeIn(400);
        if (commentContent.val() === '') {
            promptText.text('请留下您的评论');
            return false;
        }
        commentButton.attr('disabled', true);
        commentButton.addClass('disabled');
        promptText.text('正在提交...');
    }
    var obj = { "pd": "6", "comment_content": $("#comment-textarea").val(), "article_id": article_id, "user_id": user_id, "action": action };    
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {            
            if (data.status != "-1") {                
                promptText.text('评论成功!');
                commentContent.val(null);
                commentButton.attr('disabled', false);
                commentButton.removeClass('disabled');                
                var json = eval("(" + data.status + ")");                
                $.each(json.root, function (Index, item) {                    
                    $("#postcomments .commentlist").prepend("<li class= 'comment-content'><i class='fa fa-thumbs-up comment-f' aria-hidden='true'></i><div class='comment-avatar'><img class='avatar' src='../Content/images/icon/icon.png' alt='' /></div><div class='comment-main'><p>" + item.user_rights + "<span class='address'>" + item.user_name + "</span><span class='time'>(" + item.comment_date + ")</span><br />" + item.comment_content + "</p></div></li >");
                })
            } else {
                alert("this is ok");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

$(function () {
    GetArticleContent("12")
})

function GetArticleContent(index) {
    obj = { "pd": index };
    $.ajax({
        type: "post",
        url: "/tools/Handler.ashx",
        data: obj,
        dataType: "json",
        success: function (data) {            
            if (data.status != "-1") {
                alert(data.status);
                var json = eval("(" + data.status + ")");
                alert("not mistake");
                $.each(json.root, function (index, item) {
                    //alert(item.article_title);
                    //article title
                    $(".article-title a").text(item.article_title);
                    $(".article-meta-time time i").after(item.article_date);
                    $(".article-meta-time time").attr("data-original-title",item.article_date);
                    $(".article-meta-views i").after("共 " + item.article_views + "人围观");
                    $(".article-meta-views").attr("data-original-title","共 " + item.article_views + "人围观");
                    $(".article-meta-comment i").after(" " + item.article_comment_count + "个不明物体");
                    $(".article-meta-comment").attr("data-original-title", " " + item.article_comment_count + "个不明物体");
                    //article content
                    alert(item.article_content);
                    $(".article-content").append(item.article_content);
                    //var contents = new Array();                    
                    //contents = item.article_content.split("</p>");
                    //alert(contents.length);
                })
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            alert(XMLHttpRequest.status);
            alert(textStatus);
        }
    })
}
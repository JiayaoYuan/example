//弹出页面方法集合 htmlSrc 为html页面路径 向父窗体
//如果调用的标签在子窗体则调用这个
function Eject(htmlSrc, title, width, height) {
    //获取浏览器高度
    var winHeight = $(window.parent).height();
    //获取浏览器宽度
    var winWidth = $(window.parent).width();
    //计算上部分高度
    var top = ((winHeight - height) / 2) + $(document).scrollTop();
    //计算左部分宽度
    var left = (winWidth - width) / 2;
    //添加遮盖层
    var CoverHtml = '<div id="coverDiv" style="background-color:rgba(0,0,0,.9); width:' + (window.parent.document.body.scrollWidth + 10) + 'px; height:' + (window.parent.document.body.scrollHeight + 10) + 'px; position:absolute; top:0px; left:0px; z-index:200;"></div>';
    //添加内容
    var ContentHtml = '<div id="tdiv" class="tdiv" style="position:absolute; top:-' + (height + top) + 'px; left:' + left + 'px; background-color:white; border:solid 1PX #C4C4C4; box-shadow:1px 1px 10px; border-radius:10px; onverflow:hidden; text-overflow:clip; overflow:hidden; width:' + width + 'px; height:' + height + 'px; z-index:200;">';
    ContentHtml += '<div style="width:100%; height:0px; border-radius:2PX 2PX 0PX 0PX; position:relative;  ">';
    ContentHtml += '<div id="CloseCover" style=" cursor:pointer; position:absolute;  width:30px; height:30px; text-align:center; line-height:30px; top:5px; right:30px; border-radius:5px 5px 0px 0px; color:rgb(200,200,200);" onclick="Cover(this)" title="点击我就关闭了！">X</div>';
    ContentHtml += '</div>';
    ContentHtml += '<iframe scrolling="no" src="' + htmlSrc + '" width="' + width + '" height="' + height + '" style="border:none;"></iframe>';
    ContentHtml += '</div>';
    //向body最后添加标签
    $(window.parent.document.body).append(CoverHtml + ContentHtml);
    //设置浏览器滚动条隐藏
    $(window.parent.document.body).css('overflow', 'hidden');
    //设置动画
    anim('#tdiv', winHeight - $(document).scrollTop(), top);
}
//弹出页面方法集合 htmlSrc 为html页面路径 本窗体
//如果调用的标签在父窗体则调用这个
function EjectIdent(htmlSrc, title, width, height) {
    //获取浏览器高度
    var winHeight = $(window).height();
    //获取浏览器宽度
    var winWidth = $(window).width();
    //计算上部分高度
    var top = ((winHeight - height) / 2) + $(document).scrollTop();
    //计算左部分宽度
    var left = (winWidth - width) / 2;
    //添加遮盖层
    var CoverHtml = '<div id="coverDiv" style="background-color:rgba(0,0,0,.9); width:' + (window.document.body.scrollWidth + 10) + 'px; height:' + (window.document.body.scrollHeight + 10) + 'px; position:absolute; top:0px; left:0px; z-index:200;"></div>';
    //添加内容
    var ContentHtml = '<div id="tdiv" class="tdiv" style="position:absolute; top:-' + (height + top) + 'px; left:' + left + 'px; background-color:white; border:solid 1PX #C4C4C4; box-shadow:1px 1px 10px; border-radius:10px; onverflow:hidden; text-overflow:clip; overflow:hidden; width:' + width + 'px; height:' + height + 'px; z-index:200;">';
    ContentHtml += '<div style="width:100%; height:0px; border-radius:2PX 2PX 0PX 0PX; position:relative;  ">';
    ContentHtml += '<div id="CloseCover" style=" cursor:pointer; position:absolute;  width:30px; height:0px; text-align:center; line-height:30px; top:5px; right:30px; border-radius:5px 5px 0px 0px; color:rgb(200,200,200);" onclick="Cover(this)" title="点击我就关闭了！">X</div>';
    ContentHtml += '</div>';
    ContentHtml += '<iframe scrolling="auto" src="' + htmlSrc + '"  width="' + width + '" height="' + height + '" style="border:none;"></iframe>';
    ContentHtml += '</div>';
    //向body最后添加标签
    $(window.document.body).append(CoverHtml + ContentHtml);
    //设置浏览器滚动条隐藏
    $(window.document.body).css('overflow', 'hidden');
    //设置动画
    animIdent('#tdiv', winHeight - $(document).scrollTop(), top);
}
//关闭窗体
function Cover(coverThis) {
    //关闭窗体
    $(coverThis).parent().parent().remove();
    //移出窗体
    $('#coverDiv').remove();
    //设置隐藏滚动条
    $(window.document.body).css('overflow', '');
}
//窗体动画
function animIdent(id, height, top) {
    //设置弹出窗体距离浏览器顶部的距离
    var defaulttop = -height;
    //每一毫秒都调用一次
    var aniva = window.setInterval(function () {
        //如果距离浏览器顶部的距离大于设定的距离
        if (defaulttop >= top) {
            //设置距顶部的距离
            $(id).css('top', top);
            //停用每秒调用一次的
            window.clearInterval(aniva);
        }
        //设置当前的距离
        $(id).css('top', defaulttop);
        //当前距离加50
        defaulttop=defaulttop + 50;
    }, 1);
}
//窗体动画
function anim(id, height, top) {
    //设置弹出窗体距离浏览器顶部的距离
    var defaulttop = -height;
    //每一毫秒都调用一次
    var aniva = window.parent.window.setInterval(function () {
        //如果距离浏览器顶部的距离大于设定的距离
        if (defaulttop >= top) {
            //设置距顶部的距离
            $(id).css('top', top);
            //停用每秒调用一次的
            window.parent.window.clearInterval(aniva);
        }
        //设置当前的距离
        $(id, parent.document).css('top', defaulttop);
        //当前距离加50
        defaulttop = defaulttop + 50;
    }, 1);
}



var box = {
    //调用提示框
    alert: function (value,error,e) { 
        var divobj = document.createElement("div");
        divobj.id = "fixedinfo";
        divobj.className = "fixed";
        divobj.style.display = "block";
        divobj.innerText = value;
        if (window.parent.document.body.scrollTop == 0) {
            divobj.style.top = window.parent.document.body.scrollTop+ "px";
        } else {
            divobj.style.top = window.parent.document.body.scrollTop - 25 + "px";
        }
        document.body.appendChild(divobj);
        if (error != null)
        {
            divobj.style.background = "rgb(234, 159, 57)";
            divobj.style.border = "1px solid rgb(234, 159, 57)";
        }
        box.Timeout("fixedinfo", "2000");
    },
    none: function (e)
    {
        document.getElementById(e).style.display = "none";
    },
    Timeout: function (e, time)
    {
        setTimeout(function () {
            document.body.removeChild(document.getElementById(e));
        },time);
    },
    getele: function (e)
    {
        document.getElementById(e);
    },
    location: function (e) //
    {
        location.href = e;
    },
    log:function(e)//js调试
    {
        console.log(e);
    },
    loading:function(e,value)//加载
    {
        var divp = document.createElement("div");
        divp.className = "position";
        //参数 : 0 提示框
        if (e == 0) {
            divp.style.zIndex = "9999";
            var divparam = document.createElement("div");
            divparam.className = "loadingalert";
            var div = document.createElement("div");
            div.className = "alertInnerHTML";
            div.innerHTML = value;
            var divbutton0 = document.createElement("div");
            divbutton0.className = "alertButton";
            divbutton0.innerHTML = "确定";
            divbutton0.setAttribute("onclick", "Divconfirm()");
            var divbutton1 = document.createElement("div");
            divbutton1.className = "alertButton";
            divbutton1.innerHTML = "取消";
            divbutton1.setAttribute("onclick", "DivNone()");
            divparam.appendChild(div);
            divparam.appendChild(divbutton1);
            divparam.appendChild(divbutton0);
        }else if (e == 1)   /*参数 : 1 alert*/
        {
            divp.style.zIndex = "9999";
            var divparam = document.createElement("div");
            divparam.className = "loadingalert";
            divparam.style.cssText = "line-height: 100px;padding: 0;";
            var div = document.createElement("div");
            div.className = "alertInnerHTML";
            div.innerHTML = value;
            divparam.appendChild(div);
        }else {
            var divparam = document.createElement("div");
            divparam.className = "loadingback";
            var div = document.createElement("div");
            div.className = "loading spin";
            divparam.appendChild(div);
        }
        //
        divp.appendChild(divparam);
        document.body.appendChild(divp);
        //document.body.innerHTML += "<div class='ball-clip-rotate'><div></div></div>";
    },
    loadinghide:function(e)
    {
        for (var i = 0 ; i < document.getElementsByClassName("position").length; i++)
        {
            document.getElementsByClassName("position").item(i).remove();
        }
    },
    loadingHTML:function(e) //首次加载时
    {
        //获取浏览器页面可见高度和宽度
        var _PageHeight = document.documentElement.clientHeight,
            _PageWidth = document.documentElement.clientWidth;
        //计算loading框距离顶部和左部的距离（loading框的宽度为215px，高度为61px）
        var _LoadingTop = _PageHeight > 61 ? (_PageHeight - 61) / 2 : 0,
            _LoadingLeft = _PageWidth > 215 ? (_PageWidth - 215) / 2 : 0;
        //在页面未加载完毕之前显示的loading Html自定义内容
        var _LoadingHtml = '<div id="loadingDiv" style="position:absolute;left:0;width:100%;height:' + _PageHeight + 'px;top:0;background:#585858;opacity:0.8;filter:alpha(opacity=80);z-index:10000;"><div class="position" style="z-index:10000;"><div class="loadingback"><div class="loading spin"></div></div></div></div>';
        //呈现loading效果
      
        document.write(_LoadingHtml);
        //监听加载状态改变
        document.onreadystatechange = completeLoading;
        //加载状态为complete时移除loading效果
        function completeLoading() {
            if (document.readyState == "complete") {
                var loadingMask = document.getElementById('loadingDiv');
                loadingMask.parentNode.removeChild(loadingMask);
            }
        }
    },
    ImageUp: function (uploadifyddd,dom, src)
    {
       
        document.getElementById('UploadTinyIfy11').click();
        alert(document.getElementById('UploadTinyIfy11').innerText);
    }

    
}


    $("#uploadtiny").uploadify({
        'uploader': '/Material/upload?filetype=pic',
        'formData': {},
        'type': "post",
        'buttonText': '上传',
        'auto': true,
        'multi': true,
        'height': 32,
        'fileSizeLimit': 2048,
        'swf': "/uploadify/uploadify.swf",
        'onUploadSuccess': function (e, response, data) {
            var stringArray = response.split("|");
            if (stringArray[0] == "1") {
                //参数dom : 元素ID,class,name 均可
                //  $(dom).attr("src", stringArray[1]);
                //$(src)
                document.all[dom].getAttribute("src", stringArray[1]);
                document.all[src].getAttribute("value", stringArray[1]);
            }

        }
    });

    window.parent.onscroll = function () {

    var fixed = document.getElementById("fixedinfo");
    if (window.parent.document.body.scrollTop == 0) {
        fixed.style.top = window.parent.document.body.scrollTop + "px";
    } else {
        fixed.style.top = window.parent.document.body.scrollTop - 25 + "px";
    }
}
     



function boxalert(value)
{
    //提示思密达
    var div = "<div id='positionfixed' style='z-index:9999; position: fixed; width: 100%;top: 0;height: 50px;background: rgb(117, 117, 117); display:none;";
    div+= "text-align:center;line-height:50px;color:#fff;font-size:14px;'>";
    div += value;
    div += "</div>";
    $("body").append(div);
    //显示
    $("#positionfixed").slideDown(1000);
  //  1500毫秒后滚粗
    setTimeout(function () {
        $("#positionfixed").slideUp(1000);
        setTimeout(function () {
            $("#positionfixed").remove();
        }, 1000);
    }, 1500);
   
}
﻿function cookieInit() {
    //cookie数据初始
    var shopcarData = getCookie(userid);
    if (shopcarData == '') {
        return false;
    }
    else {
        var _cnt = 0;
        shopcarData = new (Function("", "return" + shopcarData))();//解析json
        for (var i = 0; i < shopcarData.length; i++) {
            var span_id = "span" + shopcarData[i].gid;
            var jian_id = "jian" + shopcarData[i].gid;
            var count = shopcarData[i].count;
            _cnt += count;
            $("#" + jian_id).css("visibility", "visible");
            $("#" + span_id).css("visibility", "visible");
            $("#" + span_id).text(count);
        }
        if (_cnt > 0)
        {
            header_right_shopcarcount.style.display = "block";
            header_right_shopcarcount.innerText = _cnt;
        }
    }
}

function shop_jia(e) {

    var gid = e.getAttribute("data-id");
    var span_id = "span" + gid;
    var jian_id = "jian" + gid;
    var _cnt = header_right_shopcarcount.innerText;

    $("#" + jian_id).css("visibility", "visible");
    $("#" + span_id).css("visibility", "visible");
    header_right_shopcarcount.style.display = "block";

    var count = $("#" + span_id).text();
    if (count >= 99)
        return false;
    var c = parseInt(count) + 1;
    _cnt = parseInt(_cnt) + 1;//购物车总条数
    $("#header_right_shopcarcount").text(_cnt);

    $("#" + span_id).text(c);

    addData(gid);

}

function shop_jian(e) {
    var gid = e.getAttribute("data-id");
    var span_id = "span" + gid;
    var _cnt = header_right_shopcarcount.innerText;

    var count = $("#" + span_id).text();
    var c = parseInt(count) - 1;
    _cnt = parseInt(_cnt) - 1;
    $("#header_right_shopcarcount").text(_cnt);

    $("#" + span_id).text(c);
    if (c == 0) {
        $(e).css("visibility", "hidden");
        $("#" + span_id).css("visibility", "hidden");
    }
    if (_cnt == 0)
    {
        header_right_shopcarcount.style.display = "none";
    }
    removeData(gid);
}
function newjson(array) {
    var jsonresult = '';
    jsonresult += '[{"gid":' + array["gid"] + ',"count":' + array["count"] + '}]';

    return jsonresult;
}

function newjson1(array) {
    var jsonresult = '';
    jsonresult += '{"gid":' + array["gid"] + ',"count":' + array["count"] + '}';

    return jsonresult;
}

//添加数据到购物车cookie
function addData(gid, tag) {
    var flag = true;//存在与否标志
    var array = new Array();
    var old_data = getCookie(userid);
    var _count = 1;
    if (tag == 'd')//表示是从详情页添加的购物车 添加的数量可能不是1
    {
        var spanid = "span" + gid;
        _count = $("#" + spanid).text();
    }

    console.log("old_data:" + old_data);
    if (old_data == '')//购物车没有数据
    {
        array["gid"] = gid;
        array["count"] = _count;
        return setCookie(userid, newjson(array));
    }
    else//购物车有数据
    {
        old_data = new (Function("", "return" + old_data))();//解析json

        for (var i = 0; i < old_data.length; i++) {
            if (old_data[i].gid == gid)//存在
            {
                old_data[i].count = old_data[i].count + parseInt(_count);
                flag = false;
                break;
            }
        }

        if (flag)//新的商品添加
        {
            array["gid"] = gid;
            array["count"] = _count;
            var jsonserializer1 = newjson1(array);
            console.log("存在数据：" + jsonserializer1);
            jsonserializer1 = new (Function("", "return" + jsonserializer1))();
            old_data[old_data.length] = jsonserializer1;
        }
    }    

    return setCookie(userid, JSON.stringify(old_data));

}
//移除数据从购物车cookie
function removeData(gid) {
    var deleteresult = '';
    var removebool = false;
    var deletevalue = 0;
    var old_data = getCookie(userid);
    if (old_data == '') return false;
    old_data = new (Function("", "return" + old_data))();

    for (var i = 0; i < old_data.length; i++) {
        if (old_data[i].gid == gid) {
            if (old_data[i].count == 0) {
                return false;
            }
            old_data[i].count = old_data[i].count - 1;
            if (old_data[i].count == 0) {
                //删除为空的数据
                //   delete result[i].value[ii];
                deleteresult = JSON.stringify(old_data[i]);
                console.log(deleteresult);
                removebool = true;
                if (old_data[i].length == 1) {
                    deleteresult = JSON.stringify(old_data[i]);
                }
                //   result[i].value[ii].replace("},null","");
            }

            break;
        }
    }

    var resultstring = JSON.stringify(old_data);
    console.log(resultstring + ":数据");

    if (removebool) {
        resultstring = resultstring.replace(deleteresult, "");
        resultstring = resultstring.replace(",,", ",");
        resultstring = resultstring.replace("[,", "[");
        resultstring = resultstring.replace(",]", "]");
    }
    if (resultstring == '[]') {
        resultstring = '';
    }
    console.log("最终：" + resultstring);

    return setCookie(userid, resultstring);
}

function removeData_all(gid) {
    var deleteresult = '';
    var removebool = false;
    var deletevalue = 0;
    var old_data = getCookie(userid);
    if (old_data == '') return false;
    old_data = new (Function("", "return" + old_data))();

    for (var i = 0; i < old_data.length; i++) {
        if (old_data[i].gid == gid) {
            if (old_data[i].count == 0) {
                return false;
            }

            deleteresult = JSON.stringify(old_data[i]);
            console.log(deleteresult);
            removebool = true;
            if (old_data[i].length == 1) {
                deleteresult = JSON.stringify(old_data[i]);
            }
            break;
        }
    }

    var resultstring = JSON.stringify(old_data);
    console.log(resultstring + ":数据");

    if (removebool) {
        resultstring = resultstring.replace(deleteresult, "");
        resultstring = resultstring.replace(",,", ",");
        resultstring = resultstring.replace("[,", "[");
        resultstring = resultstring.replace(",]", "]");
    }
    if (resultstring == '[]') {
        resultstring = '';
    }
    console.log("最终：" + resultstring);

    return setCookie(userid, resultstring);
}

function setCookie(name, value, day) {
    var Days = 3;//默认保存3天
    if (day != null) {
        Days = day;
    }

    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString()+";path=/";
}

function getCookie(name) {
    console.log(name);
    if (document.cookie.length > 0) {
        start = document.cookie.indexOf(name + "=");
        console.log(start);
        if (start != -1) {
            start = start + name.length + 1;
            end = document.cookie.indexOf(";", start);//当indexOf()带2个参数时，第二个代表其实位置，参数是数字，这个数字可以加引号也可以不加（最好还是别加吧）
            if (end == -1) end = document.cookie.length;
            return unescape(document.cookie.substring(start, end));
        }
    }
    return "";
}

//清除cookie
function clearCookie(name) {
    setCookie(name, '', -1);
}

function shop_jian_details(e) {
    var gid = e.getAttribute("data-id");
    var span_id = "span" + gid;

    var count = $("#" + span_id).text();
    if (count == 1)
        return false;
    var c = parseInt(count) - 1;
    $("#" + span_id).text(c);
}

function shop_jia_details(e) {

    var gid = e.getAttribute("data-id");
    var span_id = "span" + gid;

    var count = $("#" + span_id).text();
    if (count >= 99)
        return false;
    var c = parseInt(count) + 1;

    $("#" + span_id).text(c);
}

///初始化左下角购物车
function initShopcar() {
    var shopcarData = getCookie(userid);
    if (shopcarData == '') {
        return false;
    }
    else {
        var count = 0;
        shopcarData = new (Function("", "return" + shopcarData))();//解析json
        for (var i = 0; i < shopcarData.length; i++) {
            count = parseInt(count) + parseInt(shopcarData[i].count);
        }
        span_count.innerText = count;
        if (count != 0) {
            if (count >= 100)
            {
                $("#span_count").css("width", "30px");
            }

            $("#span_count").css("display", "block");
        }
    }
}

function addShopcar() {
    $("#span_count").css("display", "block");
    addData(goodid, 'd');//添加cookie
    //修改样式
    var spanid = "span" + goodid;
    var count = $("#span_count").text();
    var c = $("#" + spanid).text();

    var new_count = parseInt(count) + parseInt(c);
    console.log(new_count);
    $("#span_count").text(new_count);

    if (new_count >= 100) {
        $("#span_count").css("width", "30px");
    }

}
//查看评价信息
function go_comment() {
    var _url = "/Mltx_MallClass/MallComment?goodid=" + goodid;
    location.href = _url;
}
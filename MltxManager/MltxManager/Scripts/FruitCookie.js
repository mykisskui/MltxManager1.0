﻿
var fruitcookieMain = {
    initData: [],
    init: [{ id: 0,title:'',price:0, count: 0, type: 0 }],
    fruit_count: document.getElementById('fruit_shopping_count'), //水果主页购物数
    fruit_count_on: 'fruit_shoppingcartCount_on',//是否显示
    fruit_cart: document.getElementById('fruitcart'),//水果库
    fruit_cart_on: 'fruit_shoppingcart_on',//库样式
    /*
        @param a cookie名称
        @param b 商品编号
        @param c 商品数量
        @param d 商品分类
        @param e 操作  0 +, 1 - 
        @param f 商品名称
        @param j 商品价格
    */
    Add: function (a, b, c, d, e, f,j) {
        console.log(arguments);
        var _getCookie = getCookie(a);
        var _data;
        var _dataEncode
        if (_getCookie != '') {
            _data = JSON.parse(getCookie(a));
            console.log(_data);
            fruitcookieMain.initData = _data;
            _data = fruitcookieMain.Data(b, c, e, fruitcookieMain.initData, d,f,j);
        } else {
            fruitcookieMain.init[0].id = 0;
            fruitcookieMain.init[0].count = 0;
            fruitcookieMain.init[0].title = '';
            fruitcookieMain.init[0].price = 0;
            fruitcookieMain.init[0].type = 0;
            _data = fruitcookieMain.Data(b, c, e, fruitcookieMain.init, d,f,j);
        }

        _data.sort();
        _dataEncode = JSON.stringify(_data);
        if (_dataEncode.indexOf('null') != -1) {
            _dataEncode = _dataEncode.replace(',null', '');
        }
        if (_dataEncode == '[null]') {
            _dataEncode = _dataEncode.replace('[null]', '');
        }
        setCookie(a, _dataEncode);
        try{
            if (_dataEncode != '') {
                _dataEncode = JSON.parse(_dataEncode);
                fruitcookieMain.fruit_count != null ? fruitcookieMain.fruit_count.innerHTML = fruitcookieMain.Count(d, _dataEncode) : null;
                fruitcookieMain.fruit_count.className.indexOf(fruitcookieMain.fruit_count_on) == -1 && (fruitcookieMain.fruit_count.className = fruitcookieMain.fruit_count.className + ' ' + fruitcookieMain.fruit_count_on + '',
                fruitcookieMain.fruit_cart.className = fruitcookieMain.fruit_cart.className + ' ' + fruitcookieMain.fruit_cart_on + '');
            } else {
                fruitcookieMain.fruit_count != null && (fruitcookieMain.fruit_count.className = fruitcookieMain.fruit_count.className.replace(fruitcookieMain.fruit_count_on, '').trim(),
                fruitcookieMain.fruit_cart.className = fruitcookieMain.fruit_cart.className.replace(fruitcookieMain.fruit_cart_on, '').trim());
            }
        } catch (e) {
            console.log(e);
        }
    },
    /*
        查询总数
        @param a 分类
        @param b 数据
    */
    Count: function (a, b) {
        var _count = 0;
        for (var i = 0; i < b.length; i++) {
            _count += b[i].count;
        }
        return _count;
    },
    /*
        查询条数
    */
    NumberCount:function(a,b){
        var _count = 0;
        for (var i = 0; i < b.length; i++) {
            _count += 1;
        }
        return _count;
    },
    /*
       查询商品数量
       @param a 分类
       @param b 数据
       @param c 商品编号
    */
    fruitCount: function (a,b,c) {
        for (var i = 0; i < b.length; i++) {

            if (b[i].type == a && b[i].id == c) {
                return b[i].count;
            }

        }
    
    },
    /*
        数据处理
        @param a 商品编号
        @param b 商品数量
        @param c 操作类型 0 + ,1 -
        @param d 数据 false
        @param e 商品分类
    */
    Data: function (a, b, c, d, e,f,j) {
        var _exist = fruitcookieMain.exist(a, e, d);

        console.log(_exist);
        _exist.id = a;
        if (c == 0) {
            _exist.count += b;
        } else if(c== 1) {
            _exist.count -= b;
        }
        _exist.type = e;
        if (_exist.title == '') {
            _exist.title = f;
            _exist.price = j;
        }
        if (_exist.count <= 0) {
            for (var i = 0; i < fruitcookieMain.initData.length; i++) {
                if (fruitcookieMain.initData[i].id == _exist.id && fruitcookieMain.initData[i].type == _exist.type) {
                    delete fruitcookieMain.initData[i];
                }
            }
        }
        return fruitcookieMain.initData;
    },
    /*
       验证是否存在,返回数据
       @param a 编号
       @param b 分类
       @param c 数据
    */
    exist: function (a,b,c) {
        for (var i = 0; i < c.length; i++) {
            if (c[i].id == a && c[i].type == b) {
                return c[i];
            }
        }

        console.log(fruitcookieMain.initData.length);
        if (fruitcookieMain.initData.length > 0) {
            fruitcookieMain.init[0].id = 0;
            fruitcookieMain.init[0].count = 0;
            fruitcookieMain.init[0].title = '';
            fruitcookieMain.init[0].price = 0;
            fruitcookieMain.init[0].type = 0;
            fruitcookieMain.initData[fruitcookieMain.initData.length] = fruitcookieMain.init[0];
            return fruitcookieMain.initData[fruitcookieMain.initData.length - 1];
        } else {
            
            fruitcookieMain.initData[0] = fruitcookieMain.init[0];
            return fruitcookieMain.initData[0];
        }
    },
    /*
        获取完整数据
        @param a 数据
        @param b 分类

    */
    HTMLData: function (a,b) {
        if (a == '') { return false; }
        var _fruit_Data = JSON.parse(a);
        var _result = '';
        for (var i = 0; i < _fruit_Data.length; i++) {
            //获取最长的title
            var title = _fruit_Data[i].title;
            _result += fruitcookieMain.ShopppingList.replace('#title',title)
            .replace('#price', _fruit_Data[i].price.toFixed(2))
            .replace('#id', _fruit_Data[i].id).replace('#count',_fruit_Data[i].count);
        }
        return _result;
    },
    /*
      输出列表数据
    */
    fruitListData: function (a,b,c) {
        if (a == '[]' || a =='') { return false; }
        var _fruit_data = JSON.parse(a);
        var _result = '';
        for (var i = 0; i < _fruit_data.length; i++) {
            _result += fruitcookieMain.DataList.replace('#href','javascript:void(0);')
            .replace('#img', _fruit_data[i].GoodsListPic)
            .replace('#title', fruitcookieMain.GetLength(_fruit_data[i].GoodsName) >= 12 ? _fruit_data[i].GoodsName.substring(0,12+Number(fruitcookieMain.ChiLength(_fruit_data[i].GoodsName))) : _fruit_data[i].GoodsName )
            .replace('#guige', _fruit_data[i].GoodsGuige)
            .replace('#price', _fruit_data[i].Rprice.toFixed(2))
            .replace('#id', _fruit_data[i].GoodsId);
            console.log(_fruit_data[i].GoodsName.length);
            fruitcookieMain.GetLength(_fruit_data[i].GoodsName);
        }
        return _result;
    
    },
    /*
        计算汉字长度
        @param a 字符串
    */
    GetLength: function (a) {
        var charcode;
        var reallength = 0;
        for (var i = 0; i < a.length; i++) {
            //查看是否为汉字
            charcode = a.charCodeAt(i);
            if (charcode >= 0 && charcode <= 128) {
                reallength += 1;
            } else {
                reallength += 2;//汉字
            }
        }
        return Number(reallength);
    },

    TitleLength: function (a) {
        var s = 0;
        for (var i = 0; i < a.length; i++) {
            if (s == 0) {
                s = Number(fruitcookieMain.GetLength(a[i].title));
            }
            if (Number(fruitcookieMain.GetLength(a[i].title)) > s) {
                s = Number(fruitcookieMain.GetLength(a[i].title));
            }
        }

        return s;
    },
    /*
        汉字数量
    */
    ChiLength: function (a) {
        var charcode;
        var reallength = 0;
        for (var i = 0; i < a.length; i++) {
            charcode = a.charCodeAt(i);
            if (charcode >= 0 && charcode <= 128) {

            } else {
                reallength += 1;
            }
        }
        return Number(reallength);
    },
    ShopppingList: [
        '<li>' +
        '<a href="#">'+
        '<div>'+
        '<span>'+
        '<span class="fruit_title">#title</span>'+
        '<span class="fruit_rmb">¥</span>'+
        '<span class="fruit_price">#price</span>'+
        '</span>                            '+
        '</div>'+
        '</a>'+
        '<div data-id="#id">'+
        '<div class="fruit_jian">-</div>'+
        '<div class="fruit_val">#count</div>'+
        '<div class="fruit_jia">+</div>'+
        '</div>'+
        '</li>'
    ].join('/n'),
    DataList: [
         '<li>' +
         '<a href="#href">'+
         '<div>'+
         '<img src="#img" />'+
         '</div>'+
         '<div>'+
         '<span class="fruit_title">#title</span>'+
         '<span class="fruit_ms">#guige</span>'+
         '<span>'+
         '<span class="fruit_rmb">¥</span>'+
         '<span class="fruit_price">#price</span>'+
         '</span>'+
         '</div>'+
         '</a>'+
         '<div data-id="#id">' +
         '<div class="fruit_jian fruit_on">-</div>'+
         '<div class="fruit_val fruit_on">0</div>'+
         '<div class="fruit_jia">+</div>'+
         '</div>'+
         '</li>'
    ].join('/n'),
    /*
        获取总商品价格
        @param a 数据
    */
    Price: function (a) {
        var result = '00.00';
        if (a != '') {
            var _count = 0, _prices = 0;
            var fruit_data = a;
            fruit_data = JSON.parse(fruit_data);
            for (var i = 0; i < fruit_data.length; i++) {
                    _count += fruit_data[i].count;
                    _prices += Number(fruit_data[i].price) * fruit_data[i].count;
            }
            result = _prices.toFixed(2);
        } else {
            result = '00.00';
        }

        return result;
    },
    /*
        获取单商品价格
        @param a 数据
        @param b 商品编号
    */
    Prices: function (a, b) {
        var result = '';
            var _count = 0, _prices = 0;
            var fruit_data = a;
            var ulList_Target = $('.fruitList > li');
            fruit_data = JSON.parse(fruit_data);
            for (var i = 0; i < fruit_data.length; i++) {
                if (fruit_data[i].id == b) {
                        var ulfinddiv = ulList_Target.find('div[data-id=' + fruit_data[i].id + ']');
                        var span_price = ulfinddiv.parent().find('a').find('div').eq(1).find('span').eq(2).find('span[class=fruit_price]');
                        result = Number(span_price.text()).toFixed(2);
                        return result;
                }
            }
    },
    /*
        获取商品名称
        @param a 数据
        @param b 商品编号
    */
    Title: function (a,b) {
        var _count = 0, _prices = 0;
        var fruit_data = a;
        var ulList_Target = $('.fruitList').eq(0).find('li');
        fruit_data = JSON.parse(fruit_data);
        for (var i = 0; i < fruit_data.length; i++) {
            if (fruit_data[i].id == b) {
                var ulfinddiv = ulList_Target.find('div[data-id=' + fruit_data[i].id + ']');
                console.log(ulfinddiv);
                var span_price = ulfinddiv.parent().find('a').find('div').eq(1).find('span[class=fruit_title]');
                
                return span_price.text();
            }
        }
    }
  
}


function setCookie(name, value) {
    var Days = 3;//保存3天

    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";
}

function getCookie(name) {
    if (document.cookie.length > 0) {
        start = document.cookie.indexOf(name + "=");

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
    var cvalue = '';
    var d = new Date();
    d.setTime(d.getTime() + (-1 * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = name + "=" + cvalue + "; " + expires+";path=/";
}
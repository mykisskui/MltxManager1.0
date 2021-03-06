﻿/*
    表单验证
*/
var authcode = {
    dom: document,
    auth: function (a, b, c, d, e) {
        var _value = authcode.dom.getElementById(a).value.trim();
        console.log(_value.match(b));
        if (!b.test(_value)) {
            box.alert(c);
            return 1;
        } else {
            switch (a) {
                case 'levelset':
                    var _value0 = _value.split('-')[0];
                    var _value1 = _value.split('-')[1];
                    if (Number(_value0) >= Number(_value1)) {
                        box.alert(c);
                        return 1;
                    }
                    break;
            }
            return 0;
        }
    },
    yanzheng: function (a, b, c) {
        var auth_self = 0;
        var _obj = listcode.arrAction(b);
        console.log(_obj);
        for (var i = 0; i < _obj.length ; i++) {
            var _id = _obj[i].id;
            var _zhengze = _obj[i].zhengze;
            var _errmsg = _obj[i].errmsg;
            auth_self = authcode.auth(_id, _zhengze, _errmsg);
            if (auth_self != 0) {
                return false;
            }
        }
        var formdata = authcode.formJson(c);
        switch (b) {
            case 'LevelAdd':
                authcode.ajax(formdata, authcode.dom.getElementById(c), 'post');
                break;
            case 'IndexEdit':
                authcode.ajax(formdata, authcode.dom.getElementById(c), 'post');
                break;
        }
        return false;



    },
    formJson: function (a) {
        var fromdata = getFormJson(authcode.dom.getElementById(a));
        return fromdata;
    },
    ajax: function (b, c, d,f) {
        var _data = [], bef = true;
        if (!bef) { return false; }
        $.ajax({
            url: c.getAttribute('data-action'),
            type: d,
            data: { data: (!f ? JSON.stringify(b) : b) },
            dataType: 'json',
            beforeSend: function () {
                bef = false;
            },
            async:false,
            success: function (data) {
                data.errcode == 0 ? box.alert(data.errmsg) : box.alert(data.errmsg, 1);
                _data = data.errcode;
                bef = true;
            }
        })
        return _data;
    }
}
var listcode = {
    arrAction: function (a) {
        switch (a) {
            case "LevelAdd":
                return eval(listcode.LevelAdd);
                break;
            case "IndexEdit":
                return eval(listcode.IndexEdit);
                break;
        }
    },
    // /^\d+$/g        
    LevelAdd: '[{"id":"levelname","zhengze":' + /^[a-zA-Z0-9\u4e00-\u9fa5]+$/ + ',"errmsg":"会员等级名称不正确"},' +
              '{"id":"levelset","zhengze":' + /[0-9]+-[0-9]+$/ + ',"errmsg":"额度设置不正确"},' +
              '{"id":"leveldiscount","zhengze":' + /(^[1-9][0-9]$)|(^[1-9]$)|(^100)$/ + ',"errmsg":"折扣率必须为[1-100]整数"}]',
    IndexEdit: '[{"id":"MemberName","zhengze":' + /^[a-zA-Z0-9\u4e00-\u9fa5]+$/ + ',"errmsg":"会员名称不正确"},' +
                '{"id":"Balance","zhengze":' + /((\+|-|)[0-9]+)|0{1}$/ + ',"errmsg":"输入余额不正确"},' +
                '{"id":"Integral","zhengze":' + /((\+|-|)[0-9]+)|0{1}$/ + ',"errmsg":"输入积分不正确"}]'
}
//Integral

function getFormJson(frm) {
    var o = {};
    var a = $(frm).serializeArray();
    console.log(a);
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });

    return o;
}
var arr = [];
$("input[type=checkbox]").click(function () {
    var a = this,b = true,c = true,d=true; 
    if (a.parentNode.className == '') {
        a.parentNode.className = 'checked';
    } else if (a.parentNode.className == 'checked') {
        a.parentNode.className = a.parentNode.className.replace('checked', '');
        c = false;d = false;
    }
    a.id == 'all_check' ? (b = true) : (b = false);
    check_find(b, c, d, a.parentNode);
    arr.length > 0 ? (authcode.dom.getElementById('checkboxdeletebutton').style.cssText = '',
    authcode.dom.getElementById('all_check').parentNode.className = 'checked') :
    (authcode.dom.getElementById('checkboxdeletebutton').style.cssText = 'display:none',
    authcode.dom.getElementById('all_check').parentNode.className = '');
    console.log(arr);
   
});
function check_find(a,b,c,d) {
    var checkClass = authcode.dom.getElementsByClassName('checkLevel');
    check_for(a, b, c, checkClass, 0, d);
    arrLen(arr, 0);
    arr.length == 0 && (d.className = '');
}
function arrLen(a, i) {
    if (a[i] == false && a.length > 0)
    {
        a.splice(i, 1);
        i--;
    }
    i++;
    return i >= a.length ? true : arrLen(a,i);
}
function check_for(a, b, c, dom, i, d) {
    if (a) {
        if (!c) {
            dom[i].parentNode.className = dom[i].parentNode.className.replace('checked', '');
            arr.length = 0;
        } else {
            dom[i].parentNode.className = 'checked';
            arr[i] = dom[i].getAttribute('data-checkbox');
        }
    } else
    {
        arr[i] = dom[i].parentNode.className == 'checked' ? dom[i].getAttribute('data-checkbox') : false;
    }
    i++;
    return i == dom.length ? true : check_for(a, b, c, dom, i);
}
$("a[data-remove]").click(function () {
    var _self = this,_arr = [];
    var RemoveData,arr_select = 0;

    switch (_self.getAttribute('data-remove')) {
        case 'true':
            var remove_id = _self.getAttribute('data-id');
            _arr[0] = remove_id;
            console.log(_arr);
            RemoveData = authcode.ajax(_arr, _self, 'post');
            arr_select = 0;
            break;
        case 'false':
            console.log(arr);
            RemoveData = authcode.ajax(arr, _self, 'post');
            arr_select = 1;
            break;
        case 'single':
            var remove_id = _self.getAttribute('data-id');
            _arr[0] = remove_id;
            RemoveData = authcode.ajax(remove_id, _self, 'post', true);
            arr_select = 0;
            break;
    }
    if (RemoveData == 0) {
        switch (arr_select) {
            case 0:
                $("#tr" + _arr[0]).remove();
                break;
            case 1:
                arr.forEach(function (item, value) {
                    $("#tr" + item).remove();
                });
                authcode.dom.getElementById('checkboxdeletebutton').style.cssText = 'display:none';
                authcode.dom.getElementById('all_check').parentNode.className = ''
                break;
        }
    }
   
    
});

/*
    会员信息搜索
*/
$("a[data-search]").click(function () {
    
    var searchKey = authcode.dom.getElementById('nav-search-input').value.trim();
    var _selfType = this.getAttribute('data-search');
    var _test;
    if (searchKey.trim() == '') {
        box.alert('请输入关键字', 1);
        return false;
    }
    switch (_selfType) {
                
        case "0":
            _test = /^1\d{10}$/;
            if (!_test.test(searchKey)) {
                box.alert('请输入正确的手机号码', 1);
                return false;
            }
            break;
        case "1":
            _test = /^[0-9]+$/;
            if (!_test.test(searchKey)) {
                box.alert('请输入正确的会员编号', 1);
                return false;
            }
            break;
    }
    if (location.href.indexOf('?') != -1) {
        var LoHref = location.href.replace(/\?search=\w*&key=\w*/, '');
        LoHref = LoHref + "?search=" + searchKey + "&key=" + _selfType + "";
        location.href = LoHref;
    } else if (location.href.indexOf('?') == -1) {
        location.href = location.href + "?search=" + searchKey + "&key=" + _selfType + "";
    } else if (location.href.indexOf('Index/') != -1) {
            
    }
});
/*
   变更会员状态
*/
$("span[data-click=status]").click(function () {
    var _self = this,_arr = [];
    var status_id = _self.getAttribute('data-id');
    var status_Eanble = status_id.indexOf('true') != -1 ? 'true' : 'false';
    status_id = status_id.replace(/true|false/, '');
    var success = authcode.ajax(status_id, _self, 'post', true);
    if (success == 0) {
        switch (status_Eanble) {
            case 'true':
                _self.setAttribute('data-id', 'false'+status_id);
                _self.style.cssText = 'color: rgb(255, 255, 255); cursor: pointer; font-weight: bold; background-color: rgb(143, 143, 143);';
                _self.innerText = '禁用';
                break;
            case 'false':
                _self.setAttribute('data-id', 'true' + status_id);
                _self.style.cssText = 'background-color: #db4444; color: #fff;cursor:pointer; font-weight: bold';
                _self.innerText = '启用';
                break;
        }
    }
});

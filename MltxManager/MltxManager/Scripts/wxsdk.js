var jsconfig = JSON.parse(sdk);
console.log(jsconfig);
wx.config({
    debug: true, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
    appId: jsconfig.appId, // 必填，公众号的唯一标识
    timestamp: jsconfig.timestamp, // 必填，生成签名的时间戳
    nonceStr: jsconfig.nonceStr, // 必填，生成签名的随机串
    signature: jsconfig.signature,// 必填，签名，见附录1
    jsApiList: ["getLocation"] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
});

wx.ready(function () {
    wx.getLocation({
        type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
        success: function (res) {
            var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
            var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
            var speed = res.speed; // 速度，以米/每秒计
            var accuracy = res.accuracy; // 位置精度
            //验证坐标范围内有没有店铺
            $.ajax({
                url: '/Mltx_Template/LatAndLong',
                type: 'get',
                data: { lat:latitude,lng:longitude },
                success: function (data) {
                    try{
                        if (data == -1) {
                            $('#index_0_menu_ul1').html('');
                            $('#index_1_module').html('');
                        } else {
                            geocoder = new qq.maps.Geocoder({
                                complete: function (result) {
                                    var address = result.detail.addressComponents;
                                    var addressValue = address.city + address.district + address.street + address.town;
                                    //获取到的地址存储到cookie中
                                    AddresssetCookie('address', addressValue);
                                    $('#indexposition').html(addressValue);
                                }
                            });
                            var latLng = new qq.maps.LatLng(latitude, longitude);
                            geocoder.getAddress(latLng);
                        }
                    } catch (e) {
                        $('#index_0_menu_ul1').html('');
                        $('#index_1_module').html('');
                    }
                }
            });
        },
        fail: function () {
            //未开启定位,使用cookie内地址
            $('#indexposition').html('未能获取您的定位信息');
            $('#index_0_menu_ul1').html('');
            $('#index_1_module').html('');
        }
    });
});


function AddresssetCookie(name, value) {
    var Days = 3;//保存3天

    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + value + ";expires=" + exp.toGMTString() + ";path=/";
}
//pc端无法获取地址信息,模拟创建
//正式使用删除
AddresssetCookie('address','青岛市市北区驼峰路');
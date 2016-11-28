
var div_str = '<div id="div_comfirm"></div>';
document.write(div_str);

var dialog = {
    //msg 提示信息  callback_OK 点击btnok执行的方法 callback_CANCEL点击btncancel执行的方法 btn_first按钮1显示内容 btn_second按钮2显示内容 
    comfirm: function (msg,callback_OK, callback_CANCEL ,btn_first, btn_second ) {
        if (btn_first == null || btn_first == '')
            btn_first = "取消";
        if (btn_second == null || btn_second == '')
            btn_second = "确定";
        var str = '<div class="weui_dialog_confirm" id="dialog1">' +
        '<div class="weui_mask"></div>' +
        '<div class="weui_dialog">' +
           '<div class="weui_dialog_hd"><strong class="weui_dialog_title">提示</strong></div>' +
            '<div class="weui_dialog_bd" >' + msg + '</div>' +
            '<div class="weui_dialog_ft">' +
                '<a href="javascript:;" class="weui_btn_dialog default" id="btncancel">' + btn_first + '</a>' +
               ' <a href="javascript:;" class="weui_btn_dialog primary" id="btnok">' + btn_second + '</a>' +
            '</div></div></div>';

        $("#div_comfirm").html(str);

        btnok.onclick = function () {
            $("#div_comfirm").html('');
            if (callback_OK && typeof callback_OK == "function")
                callback_OK(true);
            close();
        }
        btncancel.onclick = function () {
            $("#div_comfirm").html('');
            if (callback_CANCEL && typeof callback_CANCEL == "function")
                callback_CANCEL(false);
            close();
        }
    },
    alert: function (msg,callback_ok) {
        var strhtml = '<div class="weui_dialog_alert" id="dialog2">' +
          '<div class="weui_mask"></div><div class="weui_dialog" style="top:30%">' +
              '<div class="weui_dialog_hd"><strong class="weui_dialog_title">提示</strong></div>' +
              '<div class="weui_dialog_bd" >'+msg+'</div>' +
              '<div class="weui_dialog_ft">' +
                  '<a href="javascript:;" class="weui_btn_dialog primary" id="btnsure">确定</a></div></div></div>';

        $("#div_comfirm").html(strhtml);

        btnsure.onclick = function () {
            $("#div_comfirm").html('');
            if (callback_ok && typeof callback_ok == "function")
                callback_ok(true);
            close();
        }
          
      
    }
}
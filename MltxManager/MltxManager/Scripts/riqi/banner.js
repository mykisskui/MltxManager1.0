$(document).ready(function(){

	$(".main_visual").hover(function(){
	    $("#btn_prev,#btn_next").fadeIn();
	    $("#btn_prev1,#btn_next1").fadeIn();
	},function(){
	    $("#btn_prev,#btn_next").fadeOut();
	    $("#btn_prev1,#btn_next1").fadeOut();
	});
	
	$dragBln = false;
	
	$(".main_image").touchSlider({
		flexible : true,
		speed : 200,
		btn_prev : $("#btn_prev"),
		btn_next: $("#btn_next"),
		btn_prev1: $("#btn_prev1"),
		btn_next1: $("#btn_next1"),
		paging : $(".flicking_con a"),
		counter : function (e){
			$(".flicking_con a").removeClass("on").eq(e.current-1).addClass("on");
		}
	});
	
	$(".main_image").bind("mousedown", function() {
		$dragBln = false;
	});
	
	$(".main_image").bind("dragstart", function() {
		$dragBln = true;
	});
	
	$(".main_image a").click(function(){
		if($dragBln) {
			return false;
		}
	});
	
	timer = setInterval(function(){
		$("#btn_next").click();
	}, 5000);
	timer = setInterval(function () {
	    $("#btn_next1").click();
	}, 5000);
	
	$(".main_visual").hover(function(){
		clearInterval(timer);
	},function(){
		timer = setInterval(function(){
			$("#btn_next").click();
		}, 5000);
		timer = setInterval(function () {
		    $("#btn_next1").click();
		}, 5000);
	});
	
	$(".main_image").bind("touchstart",function(){
		clearInterval(timer);
	}).bind("touchend", function(){
		timer = setInterval(function(){
			$("#btn_next").click();
		}, 5000);
		timer = setInterval(function () {
		    $("#btn_next1").click();
		}, 5000);
	});
	
});
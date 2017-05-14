$(function(){
	$(".item").hover(function(){
        $(this).css("background", "rgba(0,0,0,.07)");
    }, function() {
        $(this).css("background", "rgba(0,0,0,0)");
    }).
    css({"cursor": "pointer" });
});

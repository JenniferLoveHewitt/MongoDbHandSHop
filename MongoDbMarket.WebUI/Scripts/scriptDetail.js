$(function(){
	$("body").on("click", "img", function(){
		var src = $(this).attr("src");
		$(".detailGalleryUpper img").attr("src", src); 
	});	
	
	$(".detailBottomDescription .folderData").hide();
	
	var descHtml = $(".detailBottomDescription .folderData .description").html();
	$(".detailBottomDescription .folder").empty().append(descHtml);
	
	$(".detailBottomDescription .menu p").add(":button").mouseover(function(){
		$(this).css({"cursor": "pointer"});
	});
	
	$(".detailBottomDescription .menu p:eq(0)").click(function(){
		$(".detailBottomDescription .folder").empty().append(descHtml);
	});
	
	$(".detailBottomDescription .menu p:eq(1)").click(function(){
		var delivHtml = $(".detailBottomDescription .folderData .delivery").html();
		
		$(".detailBottomDescription .folder").empty().append(delivHtml);
	});
	
	$(".detailBottomDescription .menu p:eq(2)").click(function(){
		var payHtml = $(".detailBottomDescription .folderData .payment").html();
		
		$(".detailBottomDescription .folder").empty().append(payHtml);
	});
	
	$(".detailBottomDescription .menu p:eq(3)").click(function(){
		var cashbackHtml = $(".detailBottomDescription .folderData .cashback").html();
		
		$(".detailBottomDescription .folder").empty().append(cashbackHtml);
	});
});

$(function(){
    $(".mainblock").css({"width": "100%", "float":"left", "margin": "0px", "padding": "0px"}).children().add(".leftBlock").add(".rightBlock").
    css({"width":"18%", "float":"left"});
    
    $(".centerBlock").css({"width": "63%", "float": "left"});
    
    $(".itemBlock").css({"width": "30.3%", "float":"left", 
                         "margin-left": "3%", "height": "400px", 
                         "text-align": "center"}).
    children().css({"margin": "0px", "padding": "0px"}).
    filter("p").css({"margin-top": "10px", "font-size": "15px"}).end().
    filter("h2").css({"font-size": "23px"}).end().
    filter("h3").css({"font-size": "20px"});
    
    $(".itemBlock").hover(function(){
        $(this).css("background", "rgba(0,0,0,.07)");
    }, function() {
        $(this).css("background", "rgba(0,0,0,0)");
    }).
    css({ "padding-top": "30px", "cursor": "pointer" });

    $(".mainBlockList").css("padding-bottom", "800px");

    $("a").css({ "text-decoration": "none", "color": "black" });

    $(".rightBlock").css({ "padding-left": "100px", "margin-top": "40px" });

    $(".leftBlock").css({"padding-top": "50px", "padding-left": "50px"});

    $(".leftBlock span").css({
        "padding": "15px", "border": "1px solid black",
        "color": "white", "background-color": "black",
        "font-size": "20px", "margin-left": "10px"
    });

    $(".leftBlock ul").css({ "padding-left": "15px", "margin-bottom": "15px" }).
    children().css({
        "list-style-type": "none", "padding-bottom": "15px",
        "font-size": "20px"
    }).eq(0).css({ "padding-top": "20px" });

    $(".leftBlock ul:eq(1)").children().eq(0).css({ "padding-top": "20px" });

    $(".leftBlock span:eq(0)").click(function () {
        $(".leftBlock ul:eq(0)").children().toggle("slow");
    });

    $(".leftBlock span:eq(1)").click(function () {
        $(".leftBlock ul:eq(1)").children().toggle("slow");
    });

    $(".dropdown").css({ "margin-bottom": "70px" });
});
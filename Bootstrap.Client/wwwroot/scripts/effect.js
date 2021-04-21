//以下為消除連結虛線
$(function(){
		   
	$("a").each(function() { 
                var ionFocus = $(this).attr("onFocus"); 
                if (ionFocus == null) { 
                    $(this).attr("onFocus", "this.blur();"); 
                } 
                else { 
                    $(this).attr("onFocus", ionFocus + ";this.blur();"); 
                } 
                $(this).attr("hideFocus", "hidefocus"); 
            });   
		   
})

//首頁主視覺設定
$(function(){
	
	var NN=0,//排行位置先設定為第一個
	    GOGO=0;//座標也先設定為0
	
	//第一個按鈕為紅色
	$("#VISION_BOX ul.BTN li").eq(0).css({opacity:1,backgroundColor:"#fb0000"});
	
	
	//點擊按鈕切換
	$("#VISION_BOX ul.BTN li").click(function(){
						   
		var NOW=$(this).index()*1000*-1;
		
		$(this).css({opacity:1,backgroundColor:"#fb0000"}).siblings().css({opacity:0.5,backgroundColor:"#fff"})
		$("#VISION_BOX ul.BOX").stop(true,false).animate({left:NOW},800);
	});
	
	
	//輪播計時器
	function AUTOPALY(){
		if(NN<4){
			NN+=1;
			GOGO=NN*1000*-1
			$("#VISION_BOX ul.BOX").stop(true,false).animate({left:GOGO},800);
			$("#VISION_BOX ul.BTN li").eq(NN).css({opacity:1,backgroundColor:"#fb0000"}).siblings().css({opacity:0.5,backgroundColor:"#fff"})
			
		}else{
			NN=0;	
			GOGO=NN*1000*-1
			$("#VISION_BOX ul.BOX").stop(true,false).animate({left:GOGO},800);
			$("#VISION_BOX ul.BTN li").eq(NN).css({opacity:1,backgroundColor:"#fb0000"}).siblings().css({opacity:0.5,backgroundColor:"#fff"})
		}
		
		TT = setTimeout(AUTOPALY, 6000);
    }

	TT = setTimeout(AUTOPALY, 6000); // 呼叫 啟動上面的 function
		
	// 碰到圖與按鈕，停止輪播。滑鼠離開再開始輪播
	$("#VISION_BOX ul.BTN").hover(
		function(){ 
			clearTimeout(TT);
		}, 
		function(){
			TT = setTimeout(AUTOPALY, 6000);
		}
	);	
	
});

//首頁產品翻轉效果
$(function(){
	var AAA = navigator.userAgent;
	if(AAA.match(/(IE |Firefox)/)){
		
	$(".IBOX").each(function(){
		$(this).hover(function(){
		$(this).css({"overflow":"hidden"});
		$(this).find($('.FRONT')).animate({"top":"-316px"});
		
		},function(){		
			$(this).find($('.FRONT')).animate({"top":"0"});	
		});		
	});
	
}else{
	$(".IBOX").each(function(){
		$(this).hover(function(){
			$(this).find($('.FRONT')).transition({ perspective: '500px', rotateY: '180deg', zIndex:88},1000);
			$(this).find($('.BACK')).transition({ perspective: '500px', rotateY: '0deg', zIndex:99},1000);
		
		},function(){
			$(this).find($('.FRONT')).transition({ perspective: '500px', rotateY: '0deg', zIndex:99},1000);
			$(this).find($('.BACK')).transition({ perspective: '500px', rotateY: '-180deg', zIndex:88},1000);
		
		});
	});	
}  
		   
});

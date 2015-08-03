$(document).ready(function(){
	
	$('.selectWrap select').each(function(){
		if( $('option:selected', this).val() != '' ) title = $('option:selected',this).text();
		$(this)
			.css({'z-index':10,'opacity':0})
			
		.change(function(){
			val = $('option:selected',this).text();
			$(this).parent("div").find("a").html("<span>"+val+"</span><span class='downOk'></span>");
		})
	});
	
	$(".selectWrap a").click(function(){
		return false;
	});

	$("#cntctForm").validate({
	    	required: true
	});


	$("#send").click(function(){
		
		if ($("#cntctForm").valid()) { 
			$("span.warning").hide(); 
			}
		else { 
			
			$("span.warning").show();
		}

		$(".selectWrap").each(function(){
		if($("select", this).val()=="0") {
	    	$("a", this).css({
	    		background: "#d04149",
	    		color:"#fff"
			});
			$("a span.downOk", this).css("background","url(assets/img/downOk-error.png) no-repeat center 12px")
	    	
		}else {
				$("a", this).css({
					background: "url(assets/img/selectbg.jpg) repeat-x",
					color:"#2d4553"
				});
				
			}
		});
		
	});

	
	$(".selectWrap").each(function(){
		$(this, "select").change(function(){
	     	if($("select", this).val()=="0") {
				$("a",this).css({
					background: "#d04149",
					color:"#fff"
				});
				$("a span.downOk", this).css("background","url(assets/img/downOk-error.png) no-repeat center 12px")
			}
			else {
				$("a",this).css({
					background:"url(assets/img/selectbg.jpg) repeat-x",
					color:"#2d4553"
				});

			}
		});
	});

	
	Cufon.replace('label',	{fontFamily: 'ford antenna medium'}	);
	Cufon.replace('p',	{fontFamily: 'ford antenna medium'}	);
	



}); 
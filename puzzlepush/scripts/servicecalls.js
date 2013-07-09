 
   function getplaygame(){
	var thedate = new Date();
	$.ajax({
	    type: "Post",
	    url: "play.aspx/recordstart",
	    data: { date: thedate },
	    contentType: "application/json",
	    success: function (msg) {
	        console.log(msg.d);
	    }
	});
}


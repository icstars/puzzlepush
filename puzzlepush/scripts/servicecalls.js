 
   function getplaygame(){
	var thedate = new Date();
	$.ajax({
	    type: "POST",
	    url: "play.aspx/recordstart",
	    data: { date: thedate },
	    contentType: "application/json",
	    success: function (msg) {
	        console.log(msg.d);
	    }
	});
}

function postsafetytip(){
	$.ajax({
	   type:"POST", 
       url: "play.aspx/gettrivia",
       data:"{}",
       contentType: "application/json",
       dataType: "json",
       success: function(msg){
       		var thesafety = json.parse(msg.d); 
       		console.log(thesafety.play); 
       		postsafetytip(thesafety.play); 
       	 },
       failure: function(err){
       		console.log(err);
       }
	}); 

}



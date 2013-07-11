 
   function postdate(){
	var thedate = new Date();
	$.ajax({
	    type: "POST",
	    url: "play.aspx/recordstart",
	    data: JSON.stringify({datestring: thedate }),
	    contentType: "application/json",
	    success: function (msg) {
	        console.log(msg.d);
	    }
	});
}
   /*function postdate() {
       var thedate = new Date();
       //var thedata = '{ "datestring":"' + thedate + '" }';
       //console.log(thedata);
       $.ajax({
           type: "POST",
           url: "play.aspx/datetest",
           data: JSON.stringify({datestring:thedate}),
           processData: false,
           contentType: "application/json",
           success: function (msg) {
               console.log(msg.d);
           }
       });
   }*/
   
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



/* ajax call which posts the date to the server */
function postdate() {
    var thedate = new Date();
    $.ajax({
        type: "POST",
        url: "play.aspx/recordstart",
        data: JSON.stringify({ datestring: thedate }),
        contentType: "application/json",
        dataType: "json",
        success: function (msg) {
            var s = JSON.parse(msg.d);
            //console.log(s.trivia[0]);
            triviapage((s.trivia[0].tip));
            console.log("postdate success");
        }
    });
}
function getplayerid() {
    $.ajax({
        type: "POST",
        url: "play.aspx/getpid",
        data: "{}",
        contentType: "application/json",
        dataType: "json",
        success: function (msg) {
            var s = JSON.parse(msg.d);
            return msg;
            
            console.log("getplayerid success");
        }
    });
}
/* ajax call which posts the score to the server*/
function postscore(score){
    //var thescore= ();
    pid = returnpid();
    var data = { id: "3", score: score };

    	$.ajax({
		type: "POST",
	    	url: "play.aspx/recordscore",
	    	data: JSON.stringify(data),
	    	contentType: "application/json",
	    	success: function (msg) {
	        console.log(msg.d);
	    	}
		});
}
/* ajax call which gets the trivia question from the server 
function getsafetytip() {
    $.ajax({
        type: "POST",
        url: "play.aspx/recordstart",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var thesafety = json.parse(msg.d);
            console.log(thesafety);
            triviapage(thesafety); 
        },
        failure: function (err) {
            console.log(err);
        }
    });
} */

/*postboard takes playerId and saves an array of the gameboard*/
function postboard(board) {

  
    
    console.log(board);

    

    var data = { board: board };

    $.ajax({
        type: "POST",
        url: "play.aspx/postboard",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "json",
        success: function (msg) {

            console.log(msg);
            console.log("postboard success");
        }
    });
}

function getboard(Id) {
    
    $.ajax({
        type: "POST",
        url: "play.aspx/loadboard",
        data: JSON.stringify({ id: Id }),
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            //return result;
            console.log("getboard success");
            console.log(result);
        }
    });
}

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

function postscore(){
//var thescore= ();
    	$.ajax({
			type: "POST",
	    	url: "api/score",
	    	data: JSON.stringify({scorestring: "100"}),
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
function postboard(playerId) {

    /*
    var theboard = new Array(5);
    
    console.log(theboard);
    console.log(theboard[0][1]); */

    if (!playerId)
        playerId = 1;

    var theboard = gettilearray();
    var board = { playerid: playerId , arrayboard: theboard };

    $.ajax({
        type: "POST",
        url: "api/Board",
        data: JSON.stringify(board),
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
        type: "GET",
        url: "api/Board/"+Id,
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            //return result;
            console.log("getboard success");
            console.log(result);
        }
    });
}

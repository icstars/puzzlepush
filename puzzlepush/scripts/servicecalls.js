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
            console.log("postdate"+msg.d);
        }
    });
}

/* ajax call which gets the trivia question from the server */
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
}

function postboard() {
    //= new Array()[["1", "2", "3","4", "5"], ["6", "7", "8", "9", "10"], ["11", "12", "13", "14", "15"], ["16", "17", "18", "19", "20"], ["21", "22", "23", "24", "25"]];

    var theboard = new Array(5);
    for (k = 0; k < 5; k++) {
        theboard[k] = new Array(5);
    }
    var ele = 0
    for (i = 0; i < 5; i++) {
        for (j = 0; j < 5; j++) {
            
            theboard[i][j] = ele.toString();
            ele++;
        }
    }
    console.log(theboard);
    console.log(theboard[0][1]);

    $.ajax({
        type: "POST",
        url: "api/Board",
        data: JSON.stringify({arrayboard:theboard}),
        contentType: "application/json",
        dataType: "json",
        success: function (msg) {
            //boardpage();
            Console.Log("success");
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
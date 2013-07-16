/* ajax call which posts the date to the server */
function postdate() {
    var thedate = new Date();
    $.ajax({
        type: "POST",
        url: "play.aspx/recordstart",
        data: JSON.stringify({ datestring: thedate }),
        contentType: "application/json",
        success: function (msg) {
            getsafetytip();
            console.log(msg.d);
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
    var theboard = new Array()[["1", "2", "3", "4", "5"], ["6", "7", "8", "9", "10"], ["11", "12", "13", "14", "15"], ["16", "17", "18", "19", "20"], ["21", "22", "23", "24", "25"]]
    $.ajax({
        type: "POST",
        url: "play.aspx/saveboard",
        data: JSON.stringify({ boardarray: theboard }),
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
function gethw(){

    $.ajax({
        type: "POST",
        url: "hw.aspx/getjson",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg){
       // $("#HelloWorldString").text(msg.d);
            var obj = JSON.parse(msg.d);
            console.log(obj.hw);
            console.log(obj.hw[0]);
            console.log(obj.hw[1]);
            displayhw(obj.hw[0]);
        }
    });
 
}  

function displayMessageCall(){

    $.ajax({
        type: "POST",
        url: "hw.aspx/getjson",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg){
        $("#HelloWorldString").text(msg.d);
        alert(msg);
        }
    });
 
}  

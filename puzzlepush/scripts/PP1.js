function random(){
    $.ajax({
        type:"Post",
        url: "randomarray.aspx/arrayer",
        data:'{}',
        contentType: "application/json",
        dataType: "json",
        success: function(data){
            var responseArray = Json.parse(data.d);
        }
    });

}





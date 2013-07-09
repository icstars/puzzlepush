function msg(){
    var puzzle = "HelloWorldString";
       $.ajax({
       type:"POST",
       url:"hw.aspx/getjson",
       data: "{}",
       contentType:"application/json; charset=utf-8",
       dataType:"json",
       success: function(msg){
        puzzle = msg;
        }
    });
    return puzzle; 
 
}  
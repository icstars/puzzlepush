function displayMessageCall(){

    $.ajax({
       //type:"GET",
       url:"hw.aspx/getjson",
        //url: "http://icstarstestserv.appspot.com/icstarstest",
        //data: 'HelloWorld',
        // contentType:"application/json",
         dataType:"json", 
        success: function(msg){
        $("#HelloWorldString").text(msg);
        alert(msg);
        }
    });
 
}  

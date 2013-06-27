function displayMessageCall(){

    $.ajax({
       //type:"GET",
       url:"http://ics-c28-02.cloudapp.net:8080",
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
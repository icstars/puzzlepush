<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hw.aspx.cs" Inherits="puzzlepush.hw" %>

<!DOCTYPE html>

<html>
<head>
    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js"></script>
    
    <title>Jquery calling C#</title>

</head>
<body>

    <div id="Result">Click here if you like JQuery</div>
    
    <script>
       $(document).ready(function () {
            $("#Result").click(function () {
                //ajax finds the c# method and runs it, puts it in "msg"
                $.ajax({
                    type: "POST",
                    url: "hw.aspx/getjson",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#Result").text(msg.d);
                    }
                });
            });
        });    
    </script>
    
</body>
</html>

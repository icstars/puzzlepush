<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="play.aspx.cs" Inherits="puzzlepush.play" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script>

        function getplaygame() {
            var thedate = new Date();
            console.log(thedate);
            $.ajax({
                type: "POST",
                url: "play.aspx/recordstart",
                data: { date: thedate },
                contentType: "application/json",
                success: function (msg) {
                    console.log(msg.d);
                }
            });
        }

    </script>
   <form id="form1" runat="server">
    <div>
    
       

    </div>
    </form>
</body>
</html>

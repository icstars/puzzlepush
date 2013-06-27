<!DOCTYPE html>
<html>

<head>

    <title> Hello world Test </title>

</head>

<body>

    <script src="jquery-1.5.1.js" type="text/javascript"></script>

    <script type="text/javascript">

        function displayMessageCall() {

            $.ajax({
                type: "GET",
                url: "ics-c28-02.cloud@app.net:8080",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: successMsg,
                error: errorMsg

            });
        }

        </body>

        </html>
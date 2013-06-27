<html>

<body>

<script>


    <script src="jquery-1.5.1.js" type="text/javascript"></script>

    <script type="text/javascript">


function getContent(HelloWorld)
{
		var hwString;
                $.ajax({
		 	url: ics-c28-02.cloud@app.net:8080,		 	
		 	type: 'GET',
		 	dataType:'JSON',

			success: function(hwdString){
				if (filename === hw)
				{ 
					setTimeout(function(){
						$('HelloWorld').text(hwString);
					}, 2000); 


				}
				else 
				{
					$('HelloWorld').text(data);
				}
				// get the hello world string from the data
                // helloWorldString = data.helloWorldString;}
                        error: function(errorThrown){
					alert('error')
					.log(errorThrown);
				}
			});
		}	
},            
  

  </body>

</script>

</html> 
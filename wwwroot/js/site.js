$( "#sendMessage" ).click(function() {
    var message = $("#msg_box").val();
    userModule.displayMessage("You", message);

    $.ajax({
        type:"POST",
        url:"/Chat/HandleMessage",
        data: {
            message: message
        },
        success: function(data) {
            userModule.displayMessage("Siri", data.result);
            $("#msg_box").val("");
        },
        error: function(data){
          console.log(data);
        },     
      });
  });

  var userModule = (function(){

    function displayMessage(speaker, message){
        var messageHtml = "<tr><td>" + speaker + "</td><td>" + message + "</td></tr>"
        $('#chatTable tr:last').after(messageHtml);
      }

    return{
        displayMessage: function(speaker, message){
            displayMessage(speaker, message);
        }
    }
  })();
  

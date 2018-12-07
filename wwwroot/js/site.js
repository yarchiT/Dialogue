$( "#sendMessage").click(function() {
    var message = $("#msg_box").val();
    userModule.displayMessage("You", message);
    $(".loader").css('display', 'block');
    $("body").css('background-color', 'aliceblue')

    $.ajax({
        type:"POST",
        url:"/Chat/HandleMessage",
        data: {
            message: message
        },
        success: function(data) {
            userModule.displayMessage("Siri", data.result);
            $(".loader").css('display', 'none');
            $("body").css('background-color', 'white');
            $("#msg_box").val("");
        },
        error: function(data){
          console.log(data);
        },     
      });
  });

  var userModule = (function(){

    function displayMessage(speaker, message) {
        var messageHtml = "<tr><td>" + speaker + "</td><td>" + message + "</td></tr>";
        $('#chatTable tr:last').after(messageHtml);
      }

    return{
        displayMessage: function(speaker, message){
            displayMessage(speaker, message);
        }
    }
  })();
  

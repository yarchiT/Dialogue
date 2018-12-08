$( "#sendMessage").click(function() {
    var message = $("#msg_box").val();
    if(message === "")
        return;

    userModule.displayMessage("me", message);
    $(".loader").css('display', 'block');
    $("body").css('background-color', 'aliceblue')

    $.ajax({
        type:"POST",
        url:"/Chat/HandleMessage",
        data: {
            message: message
        },
        success: function(data) {
            userModule.displayMessage("siri", data.result);
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
        var liNode = document.createElement('li');
        liNode.className = speaker === "me" ? 'ChatLog__entry ChatLog__entry_mine' : 'ChatLog__entry';

        var pNode = document.createElement('p');
        pNode.className = 'ChatLog__message';
        pNode.innerHTML = message;

        liNode.appendChild(pNode);
        document.getElementById('ChatLog').appendChild(liNode);

        $('#ChatLog').scrollTop($('#ChatLog li:last-child').position().top);
      }

    return{
        displayMessage: function(speaker, message){
            displayMessage(speaker, message);
        }
    }
  })();
  

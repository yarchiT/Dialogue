$( "#sendMessage" ).click(function() {
    var username = $("#userField").text();
    var message = $("#msg_box").val();
    var messageHtml = "<tr><td>" + username + "</td><td>" + message + "</td></tr>"
    $('#chatTable tr:last').after(messageHtml);

    $.ajax({
        type:"POST",
        url:"/Chat/HandleMessage",
        data: {
            message: message
        },
        success: function(data) {
            alert( "fantastich " + data.result );
        },
        dataType: 'jsonp',
      });
  });

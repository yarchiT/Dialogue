// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$( "#sendMessage" ).click(function() {
    var username = $("#userField").text();
    var message = $("#msg_box").val();
    var messageHtml = "<tr><td>" + username + "</td><td>" + message + "</td></tr>"
    $('#chatTable tr:last').after(messageHtml);

    $.ajax({
        type:"POST",
        url:"/Home/HandleMessage",
        data: {
            message: message
        },
        success: function(data) {
            alert( "fantastich " + data.result );
        },
        dataType: 'jsonp',
      });
  });

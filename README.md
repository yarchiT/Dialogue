# Dialogue

Here we implement web app with ablitity to login and register user, and communicate with simple chat bot.

# Technical details  

1. Our client application - is asp net core mvc app.  
2. Our service - is asp net core web api application, which performs all the bussiness logic. 
It uses sqlite database to store users and messages.  

In order to user our Dialogue app, you first need to run Dialogue.Web service from your project folder, more details are below.


# Dialogue.Web

cd into Dialogue.Web and run:
> dotnet build  
> dotnet run

To test functionality of the web api service you can then go to http://localhost:58707/swagger/index.html

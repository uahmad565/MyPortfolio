$(function () {

    var chatbotBox = $('#chatbot-box');
    $.ajax({
        url: '/Chatbot/GetChatbotPartialView',
        type: 'Get',
        //data: { message: userInput },
        success: function (response) {
            chatbotBox.append(response);
        },
        error: function () {
            alert('Error sending message.');
        }
    });
});
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


    //Skills collapse
    $('[data-toggle="collapse"]').on('click',function () {
        var target = $(this).attr('data-target');
        $(target).collapse('toggle');
        
        // Toggle SVG icons
        $(this).find('.icon-open').toggle();
        $(this).find('.icon-closed').toggle();
    });
});
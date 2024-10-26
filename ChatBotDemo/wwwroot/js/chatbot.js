$(function () {
    //To set scroll
    var chatContainer = $('#chatContainer');

    //Handle Form submission
    $('#chatForm').on('submit', function (event) {
        event.preventDefault();
        const formattedTime = new Date().toLocaleTimeString([], {
            hour: 'numeric',
            minute: '2-digit',
            hour12: true
        });
        var userInput = $('#userInput').val();

        var messageContainer = $('#messageContainer');
        messageContainer.append(`
            <div class="d-flex justify-content-end w-100">
                <div class="maxwidth-75">
                    <small class="text-muted d-flex justify-content-end mb-1">
                        ${formattedTime}
                    </small>
                    <p class="message user-message">
                        ${userInput}
                    </p>
                </div>
            </div>
        `);

        const messages = $('#messageContainer p.message').map(function () {
            return $(this).text().trim();
        }).get();
        debugger;

        $.ajax({
            url: '/Chatbot/SendMessage',
            type: 'Post',
            data: { messages: messages },
            success: function (response) {
                messageContainer.append(response);
                scrollChatToTop();
                $('#userInput').val('');
            },
            error: function () {
                alert('Error sending message.');
            }
        })
    });

    $('#minimizeChat').on('click',function () {
        $('#chatbot').toggle(); // You can also add logic to minimize to just a button
        $('#toggleChat').toggle();
    });

    $('#toggleChat').on('click', function () {
        $('#chatbot').toggle();
        $('#toggleChat').toggle();
        scrollChatToTop();
    })

    function scrollChatToTop() {
        chatContainer.scrollTop(chatContainer.prop('scrollHeight'));
    }
});



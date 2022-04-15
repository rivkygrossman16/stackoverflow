$(() => {
    setInterval(() => {
        updateLikes();
    }, 1000)
    $("#like-question").on('click', function () {
        const button = $(this);
        const questionId = button.data('question-id');

        $.post('/home/like', { questionId }, function (result) {
            updateLikes();
            $("#like-question").prop('disabled', true);
        })
    });

    function updateLikes() {
        const id = $("#question-id").val();
        $.get('/home/getlikes', { id }, function (Likes) {
            $("#likes-count").text(Likes);
        });
    }
})
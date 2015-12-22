(function (jQuery) {
    jQuery(document).ready(function() {
        jQuery('.topic-vote-form').submit(function (event) {
            event.preventDefault();

            var id = jQuery(this).attr('id');
            var email = jQuery('#' + id + 'email').val();
            jQuery.ajax({
                url: '/topics/' + id + '/vote',
                method: 'POST',
                data: {
                    topicId: id,
                    email: email
                }
            })
            .success(function () {
                location.reload();
            });
        });

        jQuery('#createTopicForm').submit(function (event) {
            event.preventDefault();

            var topicName = jQuery('#topicName').val();
            jQuery.ajax({
                url: '/topics',
                method: 'POST',
                data: {
                    name: topicName
                }
            })
            .success(function () {
                location.reload();        
            });
        });
    });
})(jQuery = window.jQuery || {});
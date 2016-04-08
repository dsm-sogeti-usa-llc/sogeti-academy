(function($) {
    $(document).ready(function() {
        $('#createTopicForm').submit(function(evt) {
            evt.preventDefault();
            showLoading();

            var json = getJson(this);
            $.ajax({
                url: '$apiUrl$/topics',
                contentType:'application/json; charset=UTF-8',
                method: 'POST',
                data: JSON.stringify(json),
                dataType: 'json'
            }).success(function() {
                location.reload();
            });
        });

        $('.vote-form').submit(function(evt) {
            evt.preventDefault();
            showLoading();

            var id = $(this).attr('id');
            var json = getJson(this);
            $.ajax({
                url: '$apiUrl$/topics/' + id + '/vote',
                contentType:'application/json; charset=UTF-8',
                method: 'POST',
                data: JSON.stringify(json),
                dataType: 'json'
            }).success(function() {
                location.reload();
            });
        });
    });

    function getJson(form) {
        var inputs = $(form).find(':input');
        var json = {};
        for (var i = 0; i < inputs.length; i++) {
            var input = $(inputs[i]);
            json[input.attr('name')] = input.val();
        }
        return json;
    }
})($ = window.$ || {});
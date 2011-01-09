$(function () {
    $('.cell').click(function () {
        var row = $(this).data('row');
        var column = $(this).data('column');

        var form = $('<form>', {
                action: '/' 
            }).
            append($('<input>', {
                type: 'text',
                value: row,
                name: 'row' 
            })).
            append($('<input>', {
                type: 'text',
                value: column,
                name: 'column' 
            }));

        $('body').append(form);
        form.submit();
    });
});
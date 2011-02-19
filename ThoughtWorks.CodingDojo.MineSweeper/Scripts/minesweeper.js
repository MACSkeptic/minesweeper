$(function () {
    function onCellClick() {
        var row = $(this).data('row');
        var column = $(this).data('column');

        $.post('/Game/Open', { row: row, column: column }, function (data) {
            $('.board').replaceWith(data);
            $('.cell').bind('click', onCellClick);
        }, 'html');
    }

    $('.cell').bind('click', onCellClick);
});
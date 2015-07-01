$(document).ready(function() {
    $('#btnIncrement').click(function () {
        var count = $('#txtCount').val();
        $('#txtCount').val(Number(count) + 1);
    });

    $('#btnDecrement').click(function () {
        var count = $('#txtCount').val();
        $('#txtCount').val(Number(count) - 1);
    });

    $('#btnMax').click(function () {
        var count = $('#txtCount').val();
        $('#txtCount').val('100');
    });
});
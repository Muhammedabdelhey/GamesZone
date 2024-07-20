$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        console.log(btn.data('id'));

        $.ajax({
            url: `/Games/Delete/${btn.data('id')}`,
            method: 'DELETE',
            success: function () {
                alert('success');
                btn.parents('tr').fadeOut(); 
            },
            error: function () {
                alert('error');
            }
        });
        
    });
});

$(document).ready(function () {
    //if access elemnt by id add # 
    $('#Cover').on('change', function () {
        //if access elemnt by Class add .
        $('.cover-preview').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none')
    })
});
$(document).ready(function() {
    // document.getElementById('msg_test').innerHTML = "Hello world";
    $('#btnId').click(function() {
        // // $(this).text()
        // var btnUserId = $(this).text();
        // // alert($(this).text());
        // // document.getElementById('view_id_btn1').innerHTML
        // document.getElementById('msg_test').innerHTML = btnUserId;
        // // p++;
        // //Create a logic for opening a modal window

        e.preventDefault();

        $(this).parent().find('.msg_test').show();
    });


});
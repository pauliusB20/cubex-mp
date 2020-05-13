// Live search bar code
$(document).ready(function () {
    if ($("#search").val().length == 0) {
        $.ajax({ //Sends an ajax request to a searchusers controller
            url: '/searchusers', //controller route name
            method: 'get', //route type
            datatype: "json",
            data: { 'search': '' }, //data for quering
            success: function (data) {
                $('tbody').val(data.users); //if query successfull, then display data
                console.log(data.users);
            }
        }, "json");
    }
    $('#search').on('keyup', function () {
        var value = $(this).val();
        $.ajax({
            url : '/searchusers',
            method: 'get',
            datatype: "json",
            data: { 'search': value },
            success: function (data) {
                $('tbody').val(data.users);
                console.log(data.users);
            }
        }, "json");
    })
});
$.ajaxSetup({ headers: { 'csrftoken': '{{ csrf_token() }}' } });



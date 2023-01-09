$('#exampleModalCenter').on('shown.bs.modal', function (event) {
    $('#Popup').html();
    var url = '';
    url = event.relatedTarget.getAttribute('data-url');
    $.get(url)
        .done(function (response) {
            $('#Popup').html(response);
        });
});


    //$(document).on("click", ".btn-addtoquantity", function (e) {
    //        //debugger;
    //        var link = e.currentTarget.getAttribute("data-href")+"&q="+$(e.currentTarget).siblings(".hdnquantity").val()
    //console.log(link);

    //    });

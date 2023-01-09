﻿$(document).ready(function () {
    $('#myTableCategory').DataTable(
        {
            "Processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": true, // for disable multiple column at once
            "bDestroy": true,
            "ajax": {
                "url": "/Category/GetAllCategory",
                "type": "POST",
                "datatype": "json"
            },
            "columnDefs": [{
                "order": [[0]],
                "targets": [0],
                "visible": false,
                "searchable": false,
                "language": {
                    "emptyTable": "No record found. Please click on Add User for create new record.",
                    "Processing": '<i class="fa fa- refresh fa- spin">Laoding...</i>'
                },
            }],
            columns: [
                { data: "id" },
                { data: "categoryName" },
                { data: "description" },
                
                {
                    data: "id", "render": function (data, type, row) {
                        return '<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter" href="" data-url="/Category/AddOrEditCategory?id=' + data + '" > <i class="fa fa-edit"></i></button>  <a href="#" class="btn btn-danger" onclick=DelCategory("' + data + '")><i class="fa fa-trash"></i></a>'
                    },
                    "Width": "autowidth"
                }
            ],
        });
});

$('#exampleModalCenter').on('shown.bs.modal', function (event) {
    $('#Popup').html();
    var url = '';
    url = event.relatedTarget.getAttribute('data-url');
    $.get(url)
        .done(function (response) {
            $('#Popup').html(response);
        });
});

if ($.validator) {
    $.validator.setDefaults({
        ignore: [] // DON'T IGNORE PLUGIN HIDDEN SELECTS, CHECKBOXES AND RADIOS!!!
        ,
        highlight: function (element, errorClass) {
            $(element).removeClass(errorClass);
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        unhighlight: function (element, errorClass) {
            $(element).closest('.form-group').addClass('has-success').removeClass('has-error');
        },
        errorPlacement: function (error, element) {
            if (element.data().chosen) {
                {
                    element.next().after(error);
                }
            } else {
                if (element.parent().hasClass("input-group")) {
                    element.parent().next().after(error);
                } else {
                    element.after(error);
                }
            }
        }
    });

}
function validateEmpForm() {
    $('#myForm').validate({
        rules: {
            name: { required: true },
            email: { required: true },
            roles_list: { required: true },


        },
        messages: {
            name: { required: 'Please provide User Name' },
            email: { required: 'Please provide User Email' },
            roles_list: { required: 'Roles assigned To User' },


        },
        submitHandler: function (form) {
            var form = $('#myForm')[0];
            var formData = new FormData(form);
            $.ajax({
                url: form.action,
                data: formData,
                type: 'POST',
                contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
                processData: false, // NEEDED, DON'T OMIT THIS
                success: function (response) {
                    if (response.status == 200) {
                        $("#exampleModalCenter").modal("hide");
                        $.notify({
                            // options
                            title: '<strong>Success</strong>',
                            message: "<br>Record has been saved successfully",
                            icon: 'glyphicon glyphicon-ok'
                        },
                            {
                                // settings
                                element: 'body',
                                //position: null,
                                type: "success",
                                //allow_dismiss: true,
                                //newest_on_top: false,
                                showProgressbar: false,
                                placement: {
                                    from: "top",
                                    align: "center"
                                },
                                offset: 20,
                                spacing: 10,
                                z_index: 1051,
                                delay: 3300,
                                timer: 1000,

                            });
                        $('#myTableCategory').DataTable().ajax.reload();
                      
                    }
                    else {
                        $.notify({
                            // options
                            title: '<strong>Error</strong>',
                            message: "<br>There was error an occured.",
                            icon: 'glyphicon glyphicon-remove-sign',

                        }, {
                            // settings
                            element: 'body',
                            //position: null,
                            type: "danger",
                            //allow_dismiss: true,
                            //newest_on_top: false,
                            showProgressbar: false,
                            placement: {
                                from: "top",
                                align: "center"
                            },
                            offset: 20,
                            spacing: 10,
                            z_index: 1051,
                            delay: 3300,
                            timer: 1000,

                        });
                    }
                }, error: function () {
                }
            });
            return false;
        }
    });
}

function DelCategory(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            {
                $.ajax({
                    type: "POST",
                    url: '@Url.Content("~/")' + "Category/DeleteCategory" / + id,
                    /* url: '@Url.Action("DeleteLocation", "Location")/' + id,*/
                    success: function (data) {
                        dataTable.ajax.reload();
                    }
                });
            }
            swalWithBootstrapButtons.fire(
                'Deleted!',
                'Your Data has been deleted.',
                'success'
            )
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your imaginary data is safe :)',
                'error'
            )
        }
    });
}

//function DelCategory(id) {
//    if (confirm("Are you sure you want to delete ...?")) {
//        Delete(id);
//    } else {
//        return false;
//    }
//}
//function Delete(id) {
//    var url = '@Url.Content("~/")' + "Category/DeleteCategory";

//    $.post(url, { ID: id }, function (data) {
//        if (data) {
//            oTable = $('#myTableCategory').DataTable();
//            oTable.draw();
//        } else {
//            alert("Something Went Wrong!");
//        }
//    });
//}
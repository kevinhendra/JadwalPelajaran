var table = null;

$(document).ready(function () {
    //debugger;
    table = $('#myTable').DataTable({
        "processing": true,
        "ajax": {
            url: "/Student/LoadStudent",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "id", visible: false
            },
            {
                "data": "name"
            },
            {
                "render": function (data, type, row, meta) {
                    return '<button class="btn btn-warning " data-placement="left" data-tooggle="tooltip" data-animation="false" title"Edit" onclick="return GetById(' + row.id + ') "><i class="fa fa-edit"></i>Edit</button>' + '&nbsp;' +
                        '<button class="btn btn-danger " data-placement="right" data-tooggle="tooltip" data-animation="false" title"Delete" onclick="return Delete(' + row.id + ') "> <i class="fa fa-trash"></i>Delete</button>'
                }
            }]
    });
});


function Save() {
    var StudentVM = new Object();
    StudentVM.Name = $('#Siswa').val();
    debugger;
    $.ajax({
        type: "POST",
        url: '/Student/AddStudent',
        data: StudentVM
    }).then((result) => {
        debugger;
        console.log(result);
        if (result != "GAGAL") {
            Swal.fire({
                position: 'center',
                type: 'success',
                icon: 'success',
                title: 'Added Successfully'
            });
            debugger;
            table.ajax.reload();
        }
        else {
            Swal.fire({
                position: 'center',
                type: 'error',
                icon: 'error',
                title: 'Failed to add!'
            });
        }
    }).catch((error) => {
        console.log(error);
    });
}

function Add() {
    $('#Siswa').val('');
    $('#updateBtn').hide();
    $('#saveBtn').show();
    ClearScreen();
}
function ClearScreen() {
    $('#Id').val('');
    $('#Siswa').val('');
}

function GetById(Id) {
    $('#Id').val("");
    $('#Siswa').val("");
    $('#updateBtn').show();
    $('#saveBtn').hide();
    debugger;
    $.ajax({
        url: "/Student/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            debugger;
            const obj = JSON.parse(result);
            $('#Id').val(obj.Id);
            $('#Siswa').val(obj.name);
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Update() {
    var StudentVM = new Object();
    StudentVM.Id = $('Id').val();
    StudentVM.Name = $('#Siswa').val();
    debugger;
    $.ajax({
        type: "PUT",
        url: '/student/updatestudent' + id,
        data: StudentVM
    }).then((result) => {
        debugger;
        console.log(result);
        if (result != "GAGAL") {
            Swal.fire({
                position: 'center',
                type: 'success',
                icon: 'success',
                title: 'Updated successfully!'
            });
            table.ajax.reload();
            ClearScreen();
        }
        else {
            Swal.fire({
                position: 'center',
                type: 'error',
                icon: 'error',
                title: 'Failed to update!'
            });
        }
    }).catch((error) => {
        console.log(error);
    });
}

function Delete(Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            debugger;
            $.ajax({
                url: "/Student/Delete/" + Id,
                data: { Id: Id },
                type: "DELETE",
            }).then((result) => {
                debugger;
                if (result != "GAGAL") {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                    table.ajax.reload();
                }
                else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}
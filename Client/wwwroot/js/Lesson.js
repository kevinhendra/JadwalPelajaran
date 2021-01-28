var table = null;

$(document).ready(function () {
   // debugger;
    var dropdown = document.getElementById('Student');
    $.ajax({
        "type": "GET",
        "url": "/Student/LoadStudent",
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "success": function (data) {
            console.log(data);
            /*qt = JSON.parse(data);*/
        //    debugger;
            for (var i = 0; i < data.length; i++) {
                dropdown.innerHTML = dropdown.innerHTML +
                    '<option value="' + data[i]['id'] + '">' + data[i]['name'] + '</option>';
            }
        }
    }); 
    var dropdown2 = document.getElementById('Subject');
    $.ajax({
        "type": "GET",
        "url": "/Subject/LoadSubject",
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "success": function (data) {
            console.log(data);
            /*qt = JSON.parse(data);*/
        //    debugger;
            for (var i = 0; i < data.length; i++) {
                dropdown2.innerHTML = dropdown2.innerHTML +
                    '<option value="' + data[i]['id'] + '">' + data[i]['name'] + '</option>';
            }
        }
    }); 
    table = $('#myTable').DataTable({
        "processing": true,
        "ajax": {
            url: "/Lesson/LoadLesson",
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
                "data": "mataPelajaranName"
            },
            {
                "data": "siswaName"
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
    var LessonVM = new Object();
    LessonVM.SiswaId = $('#Student').val();
    LessonVM.MataPelajaranId = $('#Subject').val();
    $('#updateBtn').hide();
    $('#saveBtn').show();
  //  debugger;
    $.ajax({
        type: "POST",
        url: '/Lesson/AddLesson',
        data: LessonVM
    }).then((result) => {
     //   debugger;
        console.log(result);
        if (result != "GAGAL") {
            Swal.fire({
                position: 'center',
                type: 'success',
                icon: 'success',
                title: 'Added Successfully'
            });
    //        debugger;
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

function GetById(Id) {
    $('#Id').val("");
    $('#mataPelajaranId').val("");
    $('#SiswaId').val("");
    $('#updateBtn').show();
    $('#saveBtn').hide();
  //  debugger;
    $.ajax({
        url: "/Lesson/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
     //       debugger;
            const obj = JSON.parse(result);
            $('#Id').val(obj.id);
            $('#Subject').val(obj.mataPelajaranId);
            $('#Student').val(obj.siswaId);
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Update() {
    var LessonVM = new Object();
    LessonVM.Id = $('#Id').val();
    LessonVM.MataPelajaranId = $('#Subject').val();
    LessonVM.SiswaId = $('#Student').val();
  //  debugger;
    $.ajax({
        type: "PUT",
        url: '/Lesson/UpdateLesson',
        data: LessonVM
    }).then((result) => {
     //   debugger;
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

function Add() {
    $('#updateBtn').hide();
    $('#saveBtn').show();
    ClearScreen();
}
function ClearScreen() {
    $('#Id').val('');
    $('#Subject').val('');
    $('#Student').val('');
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
       //     debugger;
            $.ajax({
                url: "/Lesson/Delete/" + Id,
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
var table = null;

$(document).ready(function () {
    debugger;
 /*   $.ajax({
        "type": "GET",
        "url": "/Subject/LoadSubject",
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "success": function (data) {
            qt = data;

            debugger;
            console.log(qt.data);
            for (var i = 0; i < qt.data.length; i++) {
                dropdown.innerHTML = dropdown.innerHTML +
                    '<option value="' + qt.data[i]['id'] + '">' + qt.data[i]['title'] + '</option>';
            }
        }
    }); */
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

function GetById(Id) {
    $('#Id').val("");
    $('#mataPelajaranName').val("");
    $('#SiswaName').val("");
    $('#updateBtn').show();
    $('#saveBtn').hide();
    debugger;
    $.ajax({
        url: "/Lesson/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            debugger;
            const obj = JSON.parse(result);
            $('#Id').val(obj.id);
            $('#mataPelajaranName').val(obj.mataPelajaranId);
            $('#SiswaName').val(obj.SiswaId);
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


var totalPage = 1;
var lst = null;
function selectClass(ctl) {
    let lop = $(ctl).val();
    if (lop != "0" && lop != undefined && lop != null) {
        getCourse(lop, 1);
        $("#tblResult").show(500);
    } else {
        $("#tblResult").hide(500);
    }
}

function getCourse(grp, p) {
    $.ajax({
        type: "POST",
        url: "/Home/get_course",
        data: { 'Group': grp, 'Page': p, 'Size': 5 },
        async: false,
        success: function (res) {
            if (res.success) {
                let data = res.data;
                if (data.data != null && data.data != undefined) {
                    let stt = (p - 1) * 5 + 1;
                    let data1 = [];
                    for (var i = 0; i < data.data.length; i++) {
                        let item = data.data[i];
                        item.STT = stt;
                        data1.push(item);
                        stt++;
                    }
                    lst = data1;
                    $("#tblResult tbody").html("");
                    $("#courseTemplate").tmpl(data1).appendTo("#tblResult tbody");
                }

                totalPage = data.totalPage;
                $("#curPage").text(p);

            } else {
                alert(res.message);
            }
        },
        failure: function (res) {

        },
        error: function (res) {

        }
    });
}

function goPrev() {
    var curPage = parseInt($("#curPage").text());
    if (curPage == 1) {
        alert("This is first page");
    } else {
        var p = curPage - 1;
        var grp = $("#selClass").val();

        getCourse(grp, p)
    }
}
function goNext() {
    var curPage = parseInt($("#curPage").text());
    if (curPage == totalPage) {
        alert("This is last page");
    } else {
        var p = curPage + 1;
        var grp = $("#selClass").val();
        getCourse(grp, p)
    }
}

function openModal(id) {
    $("#btnSave").show();
    $("btnInsert").hide();
    if (lst != null && id != null && id > 0) {
        var item = $.grep(lst, function (obj) {
            return obj.id == id;
        })[0];

        $("#txtId").val(item.id);
        $("#txtName").val(item.courseName);
        $("#txtGroup").val(item.group);
        $("#txtCredit").val(item.credit);
        $("#txtCode").val(item.subCode);
        $("#txtMajor").val(item.major);
        $("#txtNote").val(item.note);
    }
}

function save() {
    var item = {
        id: $("#txtId").val(),
        courseName: $("#txtName").val(),
        group: $("#txtGroup").val(),
        credit: $("#txtCredit").val(),
        subCode: $("#txtCode").val(),
        major: $("#txtMajor").val(),
        note: $("#txtNote").val(),
    };

    $.ajax({
        type: "POST",
        url: "/Home/update_course",
        data: { 'course': item },
        async: false,
        success: function (res) {
            if (res.success) {
                alert("Update Success !!!");
                let c = res.data;
                var i = 0
                for (i = 0; i < lst.length; i++) {
                    if (lst[i].id == c.id) {
                        c.STT = lst[i].STT;
                        break;
                    }
                }
                lst[i] = c;
                $("#tblResult tbody").html("");
                $("#courseTemplate").tmpl(lst).appendTo("#tblResult tbody");
            } else {
                alert(res.message);
            }
        },
        failure: function (res) {

        },
        error: function (res) {

        }
    });
}
function addNew() {
    $("#btnSave").hide();
    $("btnInsert").show();
    $("#txtId").val("");
    $("#txtName").val("");
    $("#txtGroup").val($("#selClass").val());
    $("#txtCredit").val("");
    $("#txtCode").val("");
    $("#txtMajor").val("");
    $("#txtNote").val("");
}

function insert() {
    var item = {
        id: 0,
        courseName: $("#txtName").val(),
        group: $("#txtGroup").val(),
        credit: $("#txtCredit").val(),
        subCode: $("#txtCode").val(),
        major: $("#txtMajor").val(),
        note: $("#txtNote").val(),
    };

    $.ajax({
        type: "POST",
        url: "/Home/insert_course",
        data: { 'course': item },
        async: false,
        success: function (res) {
            if (res.success) {
                alert("Insert Success !!!");
                let c = res.data;
                $("#txtId").val(c.id);
                var grp = $("#selClass").val();
                getCourse(grp, 1);
            } else {
                alert(res.message);
            }
        },
        failure: function (res) {

        },
        error: function (res) {

        }
    });
}

function deleteCourse(id) {
    if (confirm("Are you sure delete this course?") == false)
        return;
    if (id != null && id != undefined && id > 0) {
        $.ajax({
            type: "POST",
            url: "/Home/delete_course",
            data: { 'id': id },
            async: false,
            success: function (res) {
                if (res.success) {
                    alert("Delete Success !!!");
                    var grp = $("#selClass").val();
                    var page = parseInt($("#curPage").text());
                    getCourse(grp, page);
                } else {
                    alert(res.message);
                }
            },
            failure: function (res) {

            },
            error: function (res) {

            }
        });
    }
}
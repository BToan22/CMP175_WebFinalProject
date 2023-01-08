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

function getCourse(mj, p) {
    $.ajax({
        type: "POST",
        url: "/Home/get_course",
        data: { 'Major': mj, 'Page': p, 'Size': 5 },
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
        $("#txtSubId").val(item.subjectId);
        $("#txtName").val(item.subjectName);      
        $("#txtCredit").val(item.credits);
        $("#txtMajor").val(item.major);
       
    }
}

function save() {
    var item = {
        id: $("#txtId").val(),
        subjectId: $("#txtSubId").val(),
        subjectName: $("#txtName").val(),
        credits: $("#txtCredit").val(),   
        major: $("#txtMajor").val(),
        
    };
    console.log(item);
    $.ajax({
        type: "POST",
        url: "/Home/update_course",
        data: { 'Subject': item },
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
    $("#txtSubId").val("");
    $("#txtName").val("");
    $("#txtMajor").val($("#selClass").val());
    $("#txtCredit").val("");
   
}

function insert() {
    var item = {
        id: 0,
        subjectName: $("#txtName").val(), 
        credits: $("#txtCredit").val(),
        subjectId: $("#txtSubId").val(),
        major: $("#txtMajor").val(),
    };

    $.ajax({
        type: "POST",
        url: "/Home/insert_course",
        data: { 'Subject': item },
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
//STUDENT
function openStudentModal(id) {
    $("#btnSave").show();
    $("btnInsert").hide();
    if (lst != null && id != null && id > 0) {
        var item = $.grep(lst, function (obj) {
            return obj.id == id;
        })[0];
        $("#txtId").val(item.id);
        $("#txtStuId").val(item.studentId);
        $("#txtStuName").val(item.studentName);
        $("#txtPhone").val(item.studentPhone);
        $("#txtTown").val(item.studentTown);
        $("#txtGender").val(item.studentGender);
        $("#txtEmail").val(item.studentEmail);

    }
}
function show() {
    getStudent(1);
}
function getStudent(p) {
    console.log(p);
    $.ajax({
        type: "POST",
        url: "/Home/get_student",
        data: { 'Page': p, 'Size': 5 },
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
                    $("#tblStudent tbody").html("");
                    $("#studentTemplate").tmpl(data1).appendTo("#tblStudent tbody");
                }

                totalPage = data.totalPage;
                $("#curSPage").text(p);

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
function goPrevInStu() {
    var curPage = parseInt($("#curSPage").text());
    if (curPage == 1) {
        alert("This is first page");
    } else {
        var p = curPage - 1;


        getStudent(p)
    }
}
function goNextInStu() {
    var curPage = parseInt($("#curSPage").text());
    if (curPage == totalPage) {
        alert("This is last page");
    } else {
        var p = curPage + 1;

        getStudent(p)
    }
}
function deleteStudent(id) {
    if (confirm("Are you sure delete this student?") == false)
        return;
    if (id != null && id != undefined && id > 0) {
        $.ajax({
            type: "POST",
            url: "/Home/delete_student",
            data: { 'id': id },
            async: false,
            success: function (res) {
                if (res.success) {
                    alert("Delete Success !!!");
                    var page = parseInt($("#curSPage").text());
                    getStudent(page);
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
function addNewStudent() {
    $("#btnSave").hide();
    $("btnInsert").show();
    $("#txtId").val("");
    $("#txtStuId").val("");
    $("#txtStuName").val("");
    $("#txtPhone").val();
    $("#txtTown").val("");
    $("#txtGender").val("");
    $("#txtEmail").val("");
}

function saveStudent() {
    var item = {
        id: $("#txtId").val(),
        studentId: $("#txtStuId").val(),
        studentName: $("#txtStuName").val(),
        studentPhone: $("#txtPhone").val(),
        studentTown: $("#txtTown").val(),
        studentGender: $("#txtGender").val(),
        studentEmail: $("#txtEmail").val(),

    };
    console.log(item);
    $.ajax({
        type: "POST",
        url: "/Home/update_student",
        data: { 'Student': item },
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
                $("#tblStudent tbody").html("");
                $("#studentTemplate").tmpl(lst).appendTo("#tblStudent tbody");
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
function insertStudent() {
    var item = {
        id: 0,
        studentId: $("#txtStuId").val(),
        studentName: $("#txtStuName").val(),
        studentPhone: $("#txtPhone").val(),
        studentTown: $("#txtTown").val(),
        studentGender: $("#txtGender").val(),
        studentEmail: $("#txtEmail").val(),
    };

    $.ajax({
        type: "POST",
        url: "/Home/insert_student",
        data: { 'Student': item },
        async: false,
        success: function (res) {
            if (res.success) {
                alert("Insert Success !!!");
                let c = res.data;
                $("#txtId").val(c.id);

                getStudent(1);
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
//LECTURER
function openModalLec(id) {
    $("#btnSave").show();
    $("btnInsert").hide();
    if (lst != null && id != null && id > 0) {
        var item = $.grep(lst, function (obj) {
            return obj.id == id;
        })[0];
        $("#txtId").val(item.id);
        $("#txtLecId").val(item.lecturerId);
        $("#txtLecName").val(item.lecturerName);
        $("#txtLecPhone").val(item.lecturerPhone);
        $("#txtLecEmail").val(item.lecturerEmail);

    }
}
function showLec() {
    getLec(1);
}
function getLec(p) {
    console.log(p);
    $.ajax({
        type: "POST",
        url: "/Home/get_lecturer",
        data: { 'Page': p, 'Size': 5 },
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
                    $("#tblLecturer tbody").html("");
                    $("#LecturerTemplate").tmpl(data1).appendTo("#tblLecturer tbody");
                }

                totalPage = data.totalPage;
                $("#curLPage").text(p);

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
function goPrevInLec() {
    var curPage = parseInt($("#curLPage").text());
    if (curPage == 1) {
        alert("This is first page");
    } else {
        var p = curPage - 1;


        getLec(p)
    }
}
function goNextInLec() {
    var curPage = parseInt($("#curLPage").text());
    if (curPage == totalPage) {
        alert("This is last page");
    } else {
        var p = curPage + 1;

        getLec(p)
    }
}
function deleteLec(id) {
    if (confirm("Are you sure delete this lecturer?") == false)
        return;
    if (id != null && id != undefined && id > 0) {
        $.ajax({
            type: "POST",
            url: "/Home/delete_lecturer",
            data: { 'id': id },
            async: false,
            success: function (res) {
                if (res.success) {
                    alert("Delete Success !!!");
                    var page = parseInt($("#curLPage").text());
                    getLec(page);
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
function addNewLec() {
    $("#btnSave").hide();
    $("btnInsert").show();
    $("#txtId").val("");
    $("#txtLecId").val("");
    $("#txtLecName").val("");
    $("#txtLecPhone").val();
    $("#txtLecEmail").val("");
}

function saveLec() {
    var item = {
        id: $("#txtId").val(),
        lecturerId: $("#txtLecId").val(),
        lecturerName: $("#txtLecName").val(),
        lecturerPhone: $("#txtLecPhone").val(),
        lecturerEmail: $("#txtLecEmail").val(),
    };
    console.log(item);
    $.ajax({
        type: "POST",
        url: "/Home/update_lecturer",
        data: { 'Lecturer': item },
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
                $("#tblLecturer tbody").html("");
                $("#LecturerTemplate").tmpl(lst).appendTo("#tblLecturer tbody");
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
function insertLec() {
    var item = {
        id: 0,
        lecturerId: $("#txtLecId").val(),
        lecturerName: $("#txtLecName").val(),
        lecturerPhone: $("#txtLecPhone").val(),
        lecturerEmail: $("#txtLecEmail").val(),
    };

    $.ajax({
        type: "POST",
        url: "/Home/insert_lecturer",
        data: { 'Lecturer': item },
        async: false,
        success: function (res) {
            if (res.success) {
                alert("Insert Success !!!");
                let c = res.data;
                $("#txtId").val(c.id);

                getStudent(1);
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
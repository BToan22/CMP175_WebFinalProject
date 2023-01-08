function signOut() {
    if (confirm("Bạn có chắc kèo là thoát chưa ?????") == true) {
        $.ajax({
            type: "POST",
            url: "/Login/signOut",
            async: false,
            success: function (res) {
                if (res.success) {
                    document.location.href = "/";
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
function changePass() {
    username = $("#txtUsername").val();
    oldpass = $("#txtOldPass").val();
    pass1 = $("#txtPassword1").val();
    pass2 = $("#txtPassword2").val();
    if (oldpass == "" || oldpass == undefined || oldpass == null || oldpass.length < 3) {
        alert("Vui lòng nhập mật khẩu cũ !!");
        return;
    }
    if (pass1 == "" || pass1 == undefined || pass1 == null || pass1.length < 3) {
        alert("Vui lòng nhập mật khẩu mới !!");
        return;
    }
    if (pass2 == "" || pass2 == undefined || pass2 == null || pass2.length < 3) {
        alert("Vui lòng nhập lại mật khẩu mới !!");
        return;
    }
    if (pass1 != pass2) {
        alert(" Mật khẩu mới chưa trùng khớp !! ");
        return;
    }
    if (pass1 == oldpass) {
        alert(" Nhập mật khẩu del gì y chang cái cũ !!!!!");
        return;
    }
    if (pass2 == oldpass) {
        alert(" Nhập mật khẩu del gì y chang cái cũ !!!!!");
        return;
    }
    if (confirm("Bạn có chắc kèo là đổi pass không ?????") == true) {
        $.ajax({
            type: "POST",
            url: "/Login/change_Pass",
            data: { 'username': username, 'oldPass': oldpass, 'newPass': pass1 },
            async: false,
            success: function (res) {
                if (res.success) {
                    document.location.href = "/";
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
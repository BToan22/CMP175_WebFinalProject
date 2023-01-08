function signOut() {
    if (confirm("Are you sure you want to LOG OUT ?????") == true) {
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
        alert("Please enter OLD PASSWORD !!");
        return;
    }
    if (pass1 == "" || pass1 == undefined || pass1 == null || pass1.length < 3) {
        alert("Please enter NEW PASSWORD !!");
        return;
    }
    if (pass2 == "" || pass2 == undefined || pass2 == null || pass2.length < 3) {
        alert("Please re-enter NEW PASSWORD !!");
        return;
    }
    if (pass1 != pass2) {
        alert(" NEW PASSWORD does not MATCH !! ");
        return;
    }
    if (pass1 == oldpass) {
        alert(" The NEW PASSWORD and The OLD PASSWORD are the same !!!!!");
        return;
    }
    if (pass2 == oldpass) {
        alert("The NEW PASSWORD and The OLD PASSWORD are the same !!!!!");
        return;
    }
    if (confirm("Are you sure you want to CHANGE PASSWORD ?????") == true) {
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
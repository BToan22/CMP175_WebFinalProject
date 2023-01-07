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
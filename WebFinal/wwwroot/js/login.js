function login() {
    let uid = $('#txtUsername').val();
    let pwd = $('#txtPassword').val();

    console.log("User name: ", uid);
    console.log("Pass: ", pwd);
    let s = check(uid, pwd);
    if (s == "") {
        // login();
        let dataLogin = { "username": uid, "password": pwd };
        $.ajax({
            type: "POST",
            url: "/Login/doLogin",
            data: { "Login": dataLogin },
            async: false,
            success: function (res) {
                if (res.success) {
                    let usr = res.user;
                    alert("Xin chào: " + usr.fullname);
                    document.location.href = "/Home";
                } else {
                    alert(res.message);
                }
            },
            failure: function (res) {

            },
            error: function (res) {

            }
        });
    } else {
        alert(s);
    }

}
function check(email, pass) {
    var s = "";
    if (email == "" || email == undefined || email == null)
        s += "Chưa nhập email! ";
    if (pass == "" || pass == undefined || pass == null)
        s += " Chưa nhập pass! ";
    return s;
}
// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()

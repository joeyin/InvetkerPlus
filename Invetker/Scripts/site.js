$(window).ready(async () => {

    var el = document.getElementById('user-modal');
    var modal = new bootstrap.Modal(el);

    el.addEventListener('hidden.bs.modal', function () {
        $(this).find('form').trigger('reset');
    });

    $("#signinup,#start-now").click(function () {
        modal.show();
    });

    document.querySelector("form[name='login']").onsubmit = function (e) {
        this.classList.add('was-validated');

        if (!this.checkValidity()) {
            e.preventDefault();
        }
    }

    document.querySelector("form[name='register']").onsubmit = function (e) {
        this.classList.add('was-validated');

        var password = document.getElementById("Password");
        var confirmPassword = document.getElementById("ConfirmPassword");

        if (password.value.trim() !== confirmPassword.value.trim()) {
            confirmPassword.setCustomValidity('no match');
        } else {
            confirmPassword.setCustomValidity('');
        }

        if (!this.checkValidity()) {
            e.preventDefault();
        }
    }

})

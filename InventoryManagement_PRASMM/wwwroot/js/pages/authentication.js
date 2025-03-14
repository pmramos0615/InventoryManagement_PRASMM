$(document).ready(function () {
    function validateInput(inputField) {
        if (inputField[0].validity.patternMismatch) {
            inputField[0].setCustomValidity(inputField.data('custom-message') + "\nEx:!@#$%^&*()_");
        } else {
            inputField[0].setCustomValidity('');
        }
    }
    var usernameInput = $('input[name="UserName"]');
    var passwordInput = $('input[name="Password"]');

    usernameInput.on('input', function () {
        validateInput($(this));
    });

    passwordInput.on('input', function () {
        validateInput($(this));
    });

    $("#Form-login").on('submit', function (e) {
        validateInput(usernameInput);
        validateInput(passwordInput);
        if (!this.checkValidity()) {
            e.preventDefault();
        }
    });
});

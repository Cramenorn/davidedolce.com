$('#contactForm').on('submit', function (e) {
    if (grecaptcha.getResponse() == "") {
        e.preventDefault();
        $('#failed-captcha').append("<div class='alert alert-danger'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>You must validate the captcha</div>");
    }
});
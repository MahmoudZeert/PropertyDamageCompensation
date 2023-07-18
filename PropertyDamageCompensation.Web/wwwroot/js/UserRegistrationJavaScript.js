$(function () {
    alert("User Registration model");
    let url = '/Identity/Authentication/Register';
    $("#userRegistrationModal input[name='AcceptTerms']").click(onCAcceptAgreement);
    function onCAcceptAgreement() {
        if ($(this).is(':checked')) {
            $("#userRegistrationModal button[name='btnRegister']").prop('disabled', false);
        }
        else {
            $("#userRegistrationModal button[name='btnRegister']").prop('disabled', true);
        }
    };
    $("#userRegistrationModal input[name='UserName']").blur(function () {

        let username = $("#userRegistrationModal input[name='UserName']").val();
        let url = '/Identity/Authentication/UserAlreadyExists?username=' + username;
        $.ajax({
            url: url,
            type: "GET",
            success: function (data) {
                let alert = $("#alert_placeholder_Register");
                if (data == true) {
                    PresentClosableBootstrapAlert(alert, 'warning', 'Invalid User name', 'This user has already been registered !');
                } else {
                    CloseBootstrapAlert(alert);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                let errorText = 'Status rrr : ' + xhr.status + ' - ' + xhr.errorText;
                PresentClosableBootstrapAlert($("#alert_placeholder_Register"), 'danger', "Error", errorText);
                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }
        });
    });

    let userloginbutton = $("#userRegistrationModal button[name='btnRegister']").click(onClickEventHandler);
    $("#userRegistrationModal button[name='btnRegister']").prop('disabled', true);

    function onClickEventHandler() {
        let username = $("#userRegistrationModal input[name='UserName']").val();
        let password = $("#userRegistrationModal input[name='Password']").val();
        let confirmpassword = $("#userRegistrationModal input[name='ConfirmPassword']").val();

        let requestVerificationToken = $("#userRegistrationModal input[name='__RequestVerificationToken']").val();
        let userInput = {
            __RequestVerificationToken: requestVerificationToken,
            UserName:username,
            Password: password,
            ConfirmPassword: confirmpassword

        }
     //   alert('ajax');
        $.ajax({
            url: url,
            data: userInput,
            type:"POST",
            success: function (data) {
                let parser = $.parseHTML(data);
                let logininvalid = $(parser).find("input[name='RegistrationInvalid']").val();
                if (logininvalid=="true") {
                    $("#userRegistrationModal").html(data);
                    userRegisterbutton = $("#userRegistrationModal button[name='btnRegister']").click(onClickEventHandler);
                    let form = $("#UserRegistrationForm");
                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);

                } else {
                    location.href = "Home/Index";
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                let errorText = 'Status : ' + xhr.status + ' - ' + xhr.errorText;
                PresentClosableBootstrapAlert($("#alert_placeholder_Register"), 'danger', "Error", errorText);
                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n'+ xhr.responseText);
            }
        })
    }
});


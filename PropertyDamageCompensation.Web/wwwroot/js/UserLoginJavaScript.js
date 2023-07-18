$(function () {

    alert( $("#UserLoginForm").prop("tabindex"));
    let url = '/Identity/Authentication/Login';
    let userloginbutton = $("#userLoginModal button[name='btnLogin']").click(onClickEventHandler);
    function onClickEventHandler() {
        let email = $("#userLoginModal input[name='Email']").val();
        let password = $("#userLoginModal input[name='Password']").val();
        let rememberme = $("#userLoginModal input[name='RemeberMe']").prop("checked");
        let requestVerificationToken = $("#userLoginModal input[name='__RequestVerificationToken']").val();
        let userInput = {
            __RequestVerificationToken: requestVerificationToken,
            Email: email,
            Password: password,
            RemeberMe: rememberme
        }

        $.ajax({
            url: url,
            data: userInput,
            type:"POST",
            success: function (data) {
                let parser = $.parseHTML(data);
                let logininvalid = $(parser).find("input[name='LoginInInvalid']").val();
                if (logininvalid=="true") {
                    $("#userLoginModal").html(data);
                    userloginbutton = $("#userLoginModal button[name='btnLogin']").click(onClickEventHandler);
                    let form = $("#UserLoginForm");
                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);
                } else {
                    location.href = "Home/Index";
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                let errorText = 'Status : ' + xhr.status + ' - ' + xhr.errorText;
                PresentClosableBootstrapAlert($("#alert_placeholder_Login"), 'danger', "Error", errorText)
              //  alert(xhr.responseText);
                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n'+ xhr.responseText);
            }
        })


    }








});


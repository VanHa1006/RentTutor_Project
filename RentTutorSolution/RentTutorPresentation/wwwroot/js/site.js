$(() => {
    const connection = new signalR.HubConnectionBuilder().withUrl("/tutorHub").build();

    connection.start().then(function () {
        console.log("SignalR Connected");
    }).catch(function (err) {
        return console.error(err.toString());
    });

    $('#registerTutorButton').click(function () {
        RegisterTutor();
    });

    function RegisterTutor() {
        const userData = {
            username: $('#username').val(),
            email: $('#email').val(),
            password: $('#password').val(),
            qualifications: $('#qualifications').val(),
            experience: $('#experience').val(),
            specialization: $('#specialization').val(),
            role: 'Tutor',
            status: 'Pending'
        };

        $.ajax({
            url: 'RegisterPage',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(userData),
            success: (response) => {
                alert("Đăng ký thành công! Đang chờ duyệt.");

                // Thông báo admin về đăng ký mới
                connection.invoke("NotifyAdminAboutNewRegistration").catch(function (err) {
                    return console.error(err.toString());
                });
            },
            error: (error) => {
                console.error(error);
                alert("Có lỗi xảy ra khi đăng ký.");
            }
        });
    }
});
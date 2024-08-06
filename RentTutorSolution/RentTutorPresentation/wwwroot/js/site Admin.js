$(() => {
    const connection = new signalR.HubConnectionBuilder().withUrl("/tutorHub").build();

    connection.start().then(function () {
        console.log("SignalR Connected for Admin");
    }).catch(function (err) {
        return console.error(err.toString());
    });

    // Lắng nghe sự kiện từ SignalR
    connection.on("ReceiveNewTutorRegistration", function () {
        alert("Có đăng ký tutor mới! Vui lòng kiểm tra và duyệt.");
        // Hoặc bạn có thể cập nhật UI để hiển thị đăng ký mới
    });
});
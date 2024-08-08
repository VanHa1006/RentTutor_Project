< !DOCTYPE html >
    <html>
        <head>
            <title>SignalR User Notifications</title>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/7.0.0/signalr.min.js"></script>
            <script>
                document.addEventListener("DOMContentLoaded", function () {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/userHub")
                .build();

                connection.on("ReceiveUserUpdate", function (message) {
                // Xử lý tin nhắn nhận được từ Hub
                const msg = document.createElement("li");
                msg.textContent = message;
                document.getElementById("messagesList").appendChild(msg);
            });

                connection.start().catch(function (err) {
                return console.error(err.toString());
            });
        });
            </script>
        </head>
        <body>
            <ul id="messagesList"></ul>
        </body>
    </html>

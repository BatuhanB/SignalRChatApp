$(document).ready(() => {
    var conn = new signalR.HubConnectionBuilder().
        withUrl("https://localhost:7270/chat-hub")
        .withAutomaticReconnect([1000,1000,4000,8000]) // in normally tries in 0 - 2 - 10 - 30 seconds
        .build();

    async function start() {
        try {
            await conn.start();
        } catch (e) {
            setTimeout(() => start(), 2000);
        }
    }

    /*conn.start();*/
    start();

    const status = $("#conn-result");
    conn.onreconnecting(error => {
        status.css("background-color", "blue");
        status.css("color", "white");
        status.html("Connecting...");
        status.fadeIn(2000, () => {
            setTimeout(() => {
                status.fadeOut(2000);
            }, 2000);
        })
    });

    conn.onreconnected(connId => {
        status.css("background-color", "green");
        status.css("color", "white");
        status.html("Connected!");
        status.fadeIn(2000, () => {
            setTimeout(() => {
                status.fadeOut(2000);
            }, 2000);
        })
    });

    conn.onclose(connId => {
        status.css("background-color", "red");
        status.css("color", "white");
        status.html("Connection Error!");
        status.fadeIn(2000, () => {
            setTimeout(() => {
                status.fadeOut(2000);
            }, 2000);
        })
    });

    $("#btn-send").click(() => {
        let message = $("#txt-message").val();
        let user = $("#txt-user").val();
        conn.invoke("SendMessage", user, message).catch(err => console.log(`Something went wrong on: ${err}`));
    })

    conn.on("receiveMessage",function( user,message ){
        $("#result").append(user + " says " + message + "<br>");
    })

    
});

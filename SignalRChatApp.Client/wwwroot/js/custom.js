$(document).ready(() => {
    var conn = new signalR.HubConnectionBuilder().
        withUrl("https://localhost:7270/chat-hub").build();

    conn.start();

    $("#btn-send").click(() => {
        let message = $("#txt-message").val();
        let user = $("#txt-user").val();
        conn.invoke("SendMessage", user, message).catch(err => console.log(`Something went wrong on: ${err}`));
    })

    conn.on("receiveMessage",function( user,message ){
        $("#result").append(user + " says " + message + "<br>");
    })
});

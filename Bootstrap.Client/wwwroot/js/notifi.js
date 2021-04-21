$(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl($.formatUrl('NotiHub')).build();

    async function start() {
        try {
            await connection.start();
            $("#reconnect-modal").removeClass('show');
        } catch (err) {
            setTimeout(() => start(), 5000);
        }
    };

    connection.on('rev', function (result) {
        if (!result || !result.Message) return;
        var timeOut = toastr.options.timeOut;
        toastr.options.timeOut = "0";
        if (result.TimeOut) toastr.options.timeOut = result.TimeOut;
        toastr.warning(result.Message, '系統廣播');
        toastr.options.timeOut = timeOut;
    });

    connection.onclose(async () => {
        $("#reconnect-modal").addClass('show');
    });

    start();

    var userId = Math.floor((Math.random() * 100) + 1);

    var chat = new signalR.HubConnectionBuilder().withUrl($.formatUrl('ChatHub?userId=' + userId)).build();

    async function startchat() {
        try {
            await chat.start().then(function () {
                chat.invoke("GetConnectionId").then(function (connectionId) {
                    console.log("GetConnectionId: " + connectionId);
                });
            });
        } catch (err) {
            setTimeout(() => startchat(), 5000);
        }
    };

    chat.on('chat', function (result) {
        if (!result || !result.Message) return;
        var timeOut = toastr.options.timeOut;
        toastr.options.timeOut = "0";
        if (result.TimeOut) toastr.options.timeOut = result.TimeOut;
        toastr.success(result.Message, '訊息');
        toastr.options.timeOut = timeOut;
    });

    chat.onclose(async () => {
        await startchat();
    });

    startchat();

})
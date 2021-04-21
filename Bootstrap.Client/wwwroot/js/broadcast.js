$(function () {
    $('#btnSubmit').unbind("click");
    $("#Message").val("系統將於30秒後重新發佈，請暫停作業");

    $.bc({
        url: 'api/Broadcast/GetConnections',
        method: 'get',
        callback: function (result) {
            if (!result) return;
            var data = [{
                value: '',
                text: '全部',
                selected: false
            }];
            result.forEach(function (row) {
                data.push({
                    value: row,
                    text: row,
                    selected: false
                })
            })
            $("#User").lgbSelect('reset', data);
        }
    });

    $("#btnSend").click(function () {
        $("#UserName").val($("#User").val());
        $("#dialogNotification").modal('show');
    });

    $("#btnSubmit").click(function () {
        var username = $("#UserName").val();
        var msg = $("#Message").val();
        var timeOut = $("#TimeOut").val();
        if (username == "") {
            $.bc({
                url: 'api/Broadcast/Broadcast',
                method: 'post',
                data: {
                    TimeOut: timeOut,
                    Message: msg
                },
                crud: '廣播',
                btn: this,
                callback: function (result) {
                }
            });
        } else {
            $.bc({
                url: 'api/Broadcast/Send',
                method: 'post',
                data: {
                    UserName: username,
                    TimeOut: timeOut,
                    Message: msg
                },
                crud: '傳送訊息',
                btn: this,
                callback: function (result) {
                }
            });
        }
    });

});
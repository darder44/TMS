﻿(function ($) {
    var logPlugin = function (options) {
        this.options = $.extend({}, logPlugin.settings, options);

        var that = this;
        for (var name in this.options.click) {
            $(name).on('click', { handler: this.options.click[name] }, function (e) {
                e.data.handler.call(that);
            });
        }
    };

    logPlugin.settings = {
        url: 'api/Logs',
        click: {
            /*
            '#btnSubmit': function () {
                this.log({ crud: '保存' });
            },
            '#btnSubmitRole': function () {
                this.log({ crud: '分配角色' });
            },
            '#btnSubmitGroup': function () {
                this.log({ crud: '分配部門' });
            },
            '#btnSubmitUser': function () {
                this.log({ crud: '分配用戶' });
            },
            '#btnSubmitMenu': function () {
                this.log({ crud: '分配菜單' });
            },
            '#btnReset': function () {
                this.log({ crud: '重置密碼' });
            },
            '#btnSaveDisplayName': function () {
                this.log({ crud: '設置顯示名稱' });
            },
            '#btnSavePassword': function () {
                this.log({ crud: '修改密碼' });
            },
            '#btnSaveApp': function () {
                this.log({ crud: '設置默認應用' });
            },
            '#btnSaveCss': function () {
                this.log({ crud: '設置個人樣式' });
            },
            'a.btn.fileinput-upload-button': function () {
                this.log({ crud: '設置頭像' });
            },
            'button.kv-file-remove': function () {
                this.log({ crud: '刪除頭像' });
            },
            'button[data-method="title"]': function () {
                this.log({ crud: '保存網站標題' });
            },
            'button[data-method="footer"]': function () {
                this.log({ crud: '保存網站頁腳' });
            },
            'button[data-method="css"]': function () {
                this.log({ crud: '設置網站樣式' });
            },
            'button[data-method="UISettings"]': function () {
                this.log({ crud: '保存網站設置' });
            },
            'button[data-method="LoginSettings"]': function () {
                this.log({ crud: '保存登錄設置' });
            }
            */
        }
    };

    logPlugin.prototype = {
        constructor: logPlugin,
        log: function (data) {
            var bcData = $.logData.shift();
            if (bcData !== undefined) $.extend(data, { requestData: JSON.stringify(bcData) });
            $.extend(data, { requestUrl: window.location.pathname });
            $.post({
                url: $.formatUrl(this.options.url),
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json'
            });
        }
    };

    $.extend({
        logPlugin: function (options) {
            if (!window.logPlugin) window.logPlugin = new logPlugin(options);
            return window.logPlugin;
        }
    });
    $.logData = [];
    $.logData.log = function () {
        $.logPlugin().log({ crud: '刪除數據' });
    };
})(jQuery);

$(function () {
    $.logPlugin();
});
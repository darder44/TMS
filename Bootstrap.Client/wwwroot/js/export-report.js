function ExcelExport(options) {
    var getQuery = function () {
        var query = options.queryParams();
        for (var key in query){
            var attrValue = query[key];
            if ($.isFunction(attrValue)) {
                query[key] = attrValue();
            }
        }
        query.order = 'asc';
        if (options.Options) {
            if (options.Options.totalRows > 65535) {
                toastr.warning('匯出資料筆數過多，請耐心等候', '系統提醒');
            }
            //切換排序
            query.order = options.Options.sortOrder
            options.Sheets.forEach(function (sheet) {
                var found = sheet.Columns.some(function (col) { return col.field == options.Options.sortName });
                if (found === true) {
                    sheet.sortName = options.Options.sortName;
                }
            });
        }
        return query;
    };
    var getVisibleSheets = function () {
        var Sheets = options.Sheets;
        if (options.Options.bootstrapTable) {
            var hiddenColumns = $(options.Options.bootstrapTable).bootstrapTable("getHiddenColumns");
            if (hiddenColumns.length > 0) {
                Sheets.forEach(sheet => {
                    sheet.Columns = sheet.Columns.filter(col => !hiddenColumns.some(hidecol => hidecol.field == col.field))
                });
                if (Sheets.length > 1) {
                    var timeOut = toastr.options.timeOut;
                    toastr.options.timeOut = "10000";
                    toastr.warning("匯出資料有一個以上的工作表，隱藏的欄位也將不顯示", '系統提醒');
                    toastr.options.timeOut = timeOut;
                }
            }            
        }
        return Sheets;
    };
    $.bc({
        url: options.url,
        method: 'post',
        dataType: '',
        data: {
            FileName: options.FileName,
            Query: getQuery(),
            Sheets: getVisibleSheets()
        },
        crud: '匯出Excel',
        btn: options.btn || "#btn_export",
        callback: function (result) {
            if (!result) {
                swal({
                    title: "匯出失敗",
                    html: "請稍後再試",
                    type: "error",
                    confirmButtonColor: '#dc3545',
                    confirmButtonText: "確定"
                });
                return
            }
            setTimeout(function () {
                //window.open(result);
                var frm = $("<iframe style='display:none' />");
                frm.attr("src", $.formatUrl("TMS/Download?token=" + result));
                frm.appendTo("body");
                frm.on("load", function () {
                    //if the download link return a page
                    //load event will be triggered
                    alert("Error while downloading " + result);
                });
            }, 1000);            
        }
    });
}
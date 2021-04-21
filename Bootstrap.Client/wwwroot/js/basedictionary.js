$(function () {
    var queryParams = function (params) {
        return $.extend(params,
            {
                CodeName: $("#txt_CodeName").val(),   
                Code: $("#txt_Code").val(),   
                Description: $("#txt_Description").val(),                        
                Facility: $("#txt_Facility").val(),
            });
    };

    $('#BaseDictionary-table').lgbTable({
        url: 'api/BaseDictionary',
        //資料繫結
        dataBinder: {
            map: {
                Id: "#ID",
                CodeName: "#CodeName",
                Code: "#Code",
                Description: "#Description",
                Facility: "#Facility"
            },
            click: {
                "#btn_delete": function () {}
            },
            callback: function (data) {
                $("#CodeName").attr("disabled", (data && data.oper === 'edit'));
                $("#Code").attr("disabled", (data && data.oper === 'edit'));
            }
        },
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'CodeName',            
            //查詢參數
            queryParams: queryParams,
            //Body欄位顯示
            columns: [                
                { title: "字典名稱", field: "CodeName", sortable: true },
                { title: "字典代碼", field: "Code", sortable: true },
                { title: "描述", field: "Description", sortable: true },
                { title: "倉別", field: "Facility", sortable: true },
                { title: "新增者", field: "AddWho", sortable: true },
                { title: "新增時間", field: "AddDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} },
                { title: "編輯者", field: "EditWho", sortable: true },
                { title: "編輯時間", field: "EditDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} }        
            ],
            editButtons: {
                events: {
                    'click .del': function (e, value, row) {
                        DeleteDicts([row]);
                    }
                }
            }
        }
    });

    $("#btn_delete").click(function () {
        var selections = $('#BaseDictionary-table').bootstrapTable('getSelections'); //取得勾選資料
        if (selections.length === 0) {
            lgbSwal({ title: '請選擇要刪除的數據', type: "warning" });
            return;
        }
        DeleteDicts(selections);
    })

    function DeleteDicts(rows) {
        var swalDeleteOptions = { //彈出提醒畫面
            title: "刪除數據",
            html: "您確定要刪除選中的所有數據嗎",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "我要刪除",
            cancelButtonText: "取消"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (result.value) {
                $.bc({
                    url: "api/BaseDictionary",
                    data: rows,
                    method: "delete",
                    crud: '刪除字典表',
                    callback: function (result) {
                        if (result) {
                            $('#BaseDictionary-table').bootstrapTable('refresh'); //重整畫面
                            $("#dialogNew").modal("hide");
                        }
                        var title = (result) ? "成功" : "失敗";
                        var type = (result) ? "success" : "error";
                        lgbSwal({ title: "保存" + title, type: type });
                    }
                });
            }
        });
    }

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#BaseDictionary-table').bootstrapTable('getOptions');
        ExcelExport({
            url: 'api/BaseDictionary/ExportExcel',
            queryParams: queryParams,
            FileName: '字典表',
            Options: options,
            Sheets: [{
                SheetName: "BaseDictionary",
                sortName: 'CodeName',
                Columns: [
                    { title: "字典名稱", field: "CodeName", sortable: true },
                    { title: "字典代碼", field: "Code", sortable: true },
                    { title: "描述", field: "Description", sortable: true },
                    { title: "倉別", field: "Facility", sortable: true },
                    { title: "新增者", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' },
                    { title: "編輯者", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' }
                ]
            }]
        });
    })
});
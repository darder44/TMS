$(function () {
    var queryParams = function (params) {
        return $.extend(params,
            {
                Facility: $("#txt_Facility").val(),
                Type: $("#txt_Type").val(),
            });
    };

    $('#BaseFacility-table').lgbTable({
        url: 'api/BaseFacility',
        //資料繫結
        dataBinder: {
            map: {
                ID: "#ID",
                Facility: "#Facility",
                Code: "#Code",
                Description: "#Description",
                Phone: "#Phone",
                UOM: "#UOM",
                Address: "#Address",              
                Type: "#Type",
            }
        },
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'Facility',
            idField: 'ID',
            //查詢參數
            queryParams: queryParams,
            //Body欄位顯示
            columns: [                
                { title: "轉運站", field: "Facility", sortable: true },
                { title: "描述", field: "Description", sortable: true },
                { title: "電話", field: "Phone", sortable: true },
                { title: "地址", field: "Address", sortable: true },
                { title: "狀態", field: "Type", sortable: true, formatter: function (value) { return value == "" ? "" : $("#Type").getTextByValue(value) } },
                { title: "新增者", field: "AddWho", sortable: true },
                { title: "新增時間", field: "Adddate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} },
                { title: "編輯者", field: "EditWho", sortable: true },
                { title: "編輯時間", field: "EditDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} }
            ]
        }
    });    
    
    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#BaseFacility-table').bootstrapTable('getOptions');
        ExcelExport({
            url: 'api/BaseFacility/ExportExcel',
            queryParams: queryParams,
            FileName: '轉運站',
            Options: options,
            Sheets: [{
                SheetName: "BaseFacility",
                sortName: 'Facility',
                Columns: [
                    { title: "轉運站", field: "Facility", sortable: true },
                    { title: "描述", field: "Description", sortable: true },
                    { title: "電話", field: "Phone", sortable: true },
                    { title: "地址", field: "Address", sortable: true },
                    { title: "狀態", field: "Type", sortable: true },
                    { title: "新增者", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' },
                    { title: "編輯者", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' }
                ]
            }]
        });
    })
});
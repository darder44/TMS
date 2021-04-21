$(function () {
    var queryParams = function (params) {
        return $.extend(params,
            {
                Facility: $("#txt_Facility").val(),
                RouteNo: $("#txt_RouteNo").val(), 
                Stop: $("#txt_Stop").val(), 
                Dock: $("#txt_Dock").val(),  
                Driver: $("#txt_Driver").val(),   
                VehicleKey: $("#txt_VehicleKey").val(),  
                ExpectDate_Start: $("#txt_ExpectDate_Start").val(),
                ExpectDate_End: $("#txt_ExpectDate_End").val()
            });
    };
    $('#vllxdock-table').lgbTable({
        url: 'api/VLLxDock',
        //資料繫結
        dataBinder: {
            map: {
                RouteNo: "#RouteNo",
                Dock: "#Dock",
                Notes: "#Notes",
                Driver: "#Driver",
                VehicleKey: "#VehicleKey",
                CaseQty: "#CaseQty",
                Cube: "#Cube",
                Weight: "#Weight",
                PalletQty: "#PalletQty",
                ExpectDate: "#ExpectDate",
                ExpectTime: "#ExpectTime",
                Stop: "#Stop",
                C_Company: "#C_Company",
            },
            click: {
               
            },
            callback: function (data) {

            }
        },
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            checkbox: false,
            sortName: 'RouteNo',            
            //查詢參數
            queryParams: queryParams,
            //Body欄位顯示
            columns: [                
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "司機", field: "Driver", sortable: true }, 
                { title: "車號", field: "VehicleKey", sortable: true },
                { title: "揀貨單號", field: "Stop", sortable: true },
                { title: "客戶名稱", field: "C_Company", sortable: true },
                { title: "銷售客編", field: "SoldTo", sortable: true },
                { title: "銷售客戶名稱", field: "SoldToName", sortable: true },
                { title: "到貨客編", field: "ShipTo", sortable: true },
                { title: "到貨客戶名稱", field: "ShipToName", sortable: true },
                { title: "箱數", field: "CaseQty", sortable: true , formatter: function(value) { return value == "" ? "" : $.roundDecimal(value, 5)} }, 
                { title: "材積", field: "Cube", sortable: true , formatter: function(value) { return value == "" ? "" : $.roundDecimal(value, 5)} },
                { title: "重量", field: "Weight", sortable: true , formatter: function(value) { return value == "" ? "" : $.roundDecimal(value, 5)} },
                { title: "板數", field: "PalletQty", sortable: true , formatter: function(value) { return value == "" ? "" : $.roundDecimal(value, 5)} },
                { title: "碼頭", field: "Dock", sortable: true, formatter: function (value) { return value == "" ? "未派工" : value } },
                { title: "碼頭描述", field: "Notes", sortable: true },
                { title: "預計報到日期", field: "ExpectDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },  
                { title: "預計報到時間", field: "ExpectTime", sortable: true }   
            ],
            editButtons: {
                events: {

                }
            }
        }
    });
    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#vllxdock-table').bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋裝載碼頭總表", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: 'api/VLLxDock/ExportExcel',
            queryParams: queryParams,
            FileName: '裝載碼頭總表',
            Options: options,
            Sheets: [{
                SheetName: "VLLxDock",
                sortName: 'RouteNo',
                Columns: [
                    { title: "路線編號", field: "RouteNo", sortable: true },
                    { title: "司機", field: "Driver", sortable: true }, 
                    { title: "車號", field: "VehicleKey", sortable: true },
                    { title: "揀貨單號", field: "Stop", sortable: true },
                    { title: "客戶名稱", field: "C_Company", sortable: true },
                    { title: "銷售客編", field: "SoldTo", sortable: true },
                    { title: "銷售客戶名稱", field: "SoldToName", sortable: true },
                    { title: "到貨客編", field: "ShipTo", sortable: true },
                    { title: "到貨客戶名稱", field: "ShipToName", sortable: true },
                    { title: "箱數", field: "CaseQty", dataType: 2, dataFormat: '0.#####' },
                    { title: "材積", field: "Cube", dataType: 2, dataFormat: '0.#####' },
                    { title: "重量", field: "Weight", dataType: 2, dataFormat: '0.#####' },
                    { title: "板數", field: "PalletQty", dataType: 2, dataFormat: '0.#####' },
                    { title: "碼頭", field: "Dock", sortable: true },
                    { title: "碼頭描述", field: "Notes", sortable: true },
                    { title: "預計報到日期", field: "ExpectDate", sortable: true , dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "預計報到時間", field: "ExpectTime", sortable: true },  
                    { title: "實際報到時間", field: "CheckInTime", sortable: true },  
                    { title: "揀貨人員姓名", field: "PickUserName", sortable: true },
                    { title: "揀貨時間", field: "PickDateS", sortable: true },
                    { title: "揀貨完成時間", field: "PickDateE", sortable: true },
                    { title: "QC人員姓名", field: "QCUserName", sortable: true },
                    { title: "QC時間", field: "QCDateS", sortable: true },
                    { title: "QC完成時間", field: "QCDateE", sortable: true },
                    { title: "離場時間", field: "ExitTime", sortable: true }
                ]
            }]
        });      
    })
});